using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using UnstuckMEServer;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    public partial class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
    {
        /// <summary>
        /// Returns a boolean value identifying that the server is running.
        /// </summary>
        /// <returns>True.</returns>
        public bool TestNewConfig()
        {
            return true;
        }

        /// <summary>
        /// Attempts to log in a server administrator.
        /// </summary>
        /// <param name="LoggingInAdmin">The information of the server administrator.</param>
        public void RegisterServerAdmin(AdminInfo LoggingInAdmin)
        {
            try
            {
                //Stores Server Admin into Logged in _ConnectedServerAdmins List
                IServer establishedUserConnection = OperationContext.Current.GetCallbackChannel<IServer>();
                bool oldConnection = false;
                foreach (var onlineAdmin in _connectedServerAdmins)
                {
                    if (onlineAdmin.Key == LoggingInAdmin.ServerAdminID)
                    {
                        oldConnection = true;
                        onlineAdmin.Value.Connection = establishedUserConnection;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Server Admin Re-Login: {0} at {1}", onlineAdmin.Value.Admin.EmailAddress, DateTime.Now);
                        Console.ResetColor();
                    }
                }

                if (!oldConnection)
                {
                    ConnectedServerAdmin newAdmin = new ConnectedServerAdmin()
                    {
                        Connection = establishedUserConnection,
                        Admin = LoggingInAdmin
                    };
                    _connectedServerAdmins.TryAdd(newAdmin.Admin.ServerAdminID, newAdmin);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Server Admin Login: {0} at {1}", newAdmin.Admin.EmailAddress, DateTime.Now);
                    Console.ResetColor();
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR WHILE REGISTERING SERVER ADMIN!\nMESSAGE: " + ex.Message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Disconnects a server administrator.
        /// </summary>
        public void AdminLogout()
        {
            ConnectedServerAdmin connectedAdmin = GetMyAdmin();
            if (connectedAdmin != null)
            {
                ConnectedServerAdmin removedAdmin;
                _connectedServerAdmins.TryRemove(connectedAdmin.Admin.ServerAdminID, out removedAdmin);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Server Admin Logoff: {0} at {1}", removedAdmin.Admin.EmailAddress, DateTime.Now);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Logs information for actions invoked by a server administrator. Currently only writes a message to the console.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        public void AdminLogMessage(string message)
        {
            ConnectedServerAdmin currentAdmin = GetMyAdmin();
            //Future Will Log to Log File.
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Message: {0} Sent by: {1} at {2}", message, currentAdmin.Admin.EmailAddress, DateTime.Now);
            Console.ResetColor();
        }

        /// <summary>
        /// Gets all the clients that are currently logged in.
        /// </summary>
        /// <returns>A list of UserInfo structures containing all the information of each online user.</returns>
        public List<UserInfo> AdminGetAllOnlineUsers()
        {
            List<UserInfo> userList = new List<UserInfo>();

            foreach (var user in _connectedClients)
                userList.Add(user.Value.User);
            
            return userList;
        }

        /// <summary>
        /// Sends a message to all users who are online.
        /// </summary>
        /// <param name="recipients">The recipients of the </param>
        /// <param name="message"></param>
        public void AdminSendMessageToUsers(List<string> recipients, string message)
        {
            try
            {
                if (recipients.Count == 0)
                {
                    foreach (var client in _connectedClients)
                        client.Value.Connection.GetMessageFromServer(message);
                }
                else
                {
                    foreach (string recipient in recipients)
                    {
                        foreach (var client in _connectedClients)
                        {
                            if (client.Value.User.EmailAddress == recipient)
                                client.Value.Connection.GetMessageFromServer(message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Sends a message to all connected clients that the server is shutting down.
        /// </summary>
        public void AdminServerShuttingDown()
        {
            try
            {
                foreach (var client in _connectedClients)
                    client.Value.Connection.ForceClose();
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Registers a new tutoring organization. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="organizationName">The name of the new tutoring organization.</param>
        /// <returns>The unique identifier of the newly created organization if successful, -1 if unsuccessful.</returns>
        public int AdminCreateMentoringOrganization(string organizationName)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.CreateMentorOrganization(organizationName);
                }

                return retVal;
            }
            catch (Exception)
            {
                return retVal; //If Failure to create organization
            }
        }

        /// <summary>
        /// Registers a new class. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="courseName">The name of the new class.</param>
        /// <param name="courseCode">The subject of the new class.</param>
        /// <param name="courseNumber">The course number of the new class.</param>
        /// <returns>The unique identifier of the newly created class if successful, -1 if unsuccessful.</returns>
        public int AdminCreateClass(string courseName, string courseCode, int courseNumber)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.CreateNewClass(courseName, courseCode, (short)courseNumber);
                }

                return retVal;
            }
            catch (Exception)
            {
                return retVal; //If Failure to create class
            }
        }

        /// <summary>
        /// Remove a class from the database. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="classID">The unique identifier of the class to be removed.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int AdminDeleteClass(int classID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.DeleteClassByClassID(classID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return retVal; //If Failure to delete class
            }
        }

        /// <summary>
        /// Removes a tutoring organization from the database. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="organizationID">The unique identifier of the organization to be removed.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int AdminDeleteMentoringOrganization(int organizationID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.DeleteMentorOrganizationByMentorID(organizationID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return retVal; //If Failure to delete organization
            }
        }

        /// <summary>
        /// Removes a report from the database. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="reportID">The unique identifier of the report to be removed.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int AdminDeleteReport(int reportID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.DeleteReportByReportID(reportID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return retVal; //If Failure to delete report
            }
        }
    }
}