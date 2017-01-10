﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UnstuckMEServer;
using UnstuckME_Classes;
using System.Security.Cryptography;
using System.Collections.Concurrent;
using System.Security;

namespace UnstuckMEInterfaces
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    /// <summary>
    /// Implement any Operation Contracts from IUnstuckMEService.cs in this file.
    /// </summary>
    public class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer
    {
        public ConcurrentDictionary<int, ConnectedClient> _connectedClients = new ConcurrentDictionary<int, ConnectedClient>();
        public ConcurrentDictionary<int, ConnectedServerAdmin> _connectedServerAdmins = new ConcurrentDictionary<int, ConnectedServerAdmin>();
        public void ChangeUserName(string emailaddress, string newFirstName, string newLastName)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var users = (from u in db.UserProfiles
                             where u.EmailAddress == emailaddress
                             select u).First();
                users.DisplayFName = newFirstName;
                users.DisplayLName = newLastName;
                db.SaveChanges();
            }
        }

        public bool CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword)
        {
            bool success = false;
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                UnstuckMEPassword hashedUserPassword = new UnstuckMEPassword();
                hashedUserPassword = UnstuckMEHashing.GetHashedPassword(userPassword);
                int retVal = db.CreateNewUser(displayFName, displayLName, emailAddress, hashedUserPassword.Password, "User", hashedUserPassword.Salt);
                if(retVal == 1)
                {
                    success = true;
                }
            }
            return success;
        }

        public int GetUserID(string emailAddress)
        {
            int userID = 0;
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var temp = db.GetUserID(emailAddress);
                userID = temp.First().Value;
            }
            return userID;
        }

        public bool UserLoginAttempt(string emailAddress, string passWord)
        {
            Console.WriteLine("a user with the email address of: " + emailAddress + " attempted a login");

            bool loginAttempt = false;

            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                try
                {
                    var users = (from u in db.UserProfiles
                                 where u.EmailAddress == emailAddress
                                 select u).First();

                    string stringOfPassword = UnstuckMEHashing.RecreateHashedPassword(passWord, users.Salt);

                    if (stringOfPassword == users.UserPassword)
                    {
                        int userID = GetUserID(emailAddress);
                        //If Client is already logged on return false (This may be removed later).
                        foreach (var client in _connectedClients)
                        {
                            if (client.Key == userID)
                            {
                                return false;
                            }
                        }
                        loginAttempt = true;

                        //Stores Client into Logged in Users List
                        var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();
                        ConnectedClient newClient = new ConnectedClient();
                        newClient.connection = establishedUserConnection;
                        newClient.User = GetUserInfo(userID);
                        _connectedClients.TryAdd(newClient.User.UserID, newClient);
                        //Login Success, Print to console window.
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Client Login: {0} at {1}", newClient.User.EmailAddress, System.DateTime.Now);
                        Console.ResetColor();
                        
                    }
                }
                catch (Exception)
                {
                    loginAttempt = false;
                }
            }
            return loginAttempt;
        }

        public List<UserClasses> GetUserClasses(int UserID)
        {
            try
            {
                List<UserClasses> Rlist = new List<UserClasses>();

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var classes = db.GetUserClasses(UserID);
                    UserClasses temp = new UserClasses();

                    //This might work, if not let me know and i'll figure out something else.
                    foreach (var c in classes)
                    {
                        temp.CourseCode = c.CourseCode;
                        temp.CourseName = c.CourseName;
                        temp.CourseNumber = c.CourseNumber;
                        Rlist.Add(temp);
                    }
                }
                return Rlist;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void InsertStudentIntoClass(int UserID, int ClassID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.InsertStudentIntoClass(UserID, ClassID);
            }
        }

        //public void SendMessageToAllUsers(string message, string emailAddress)
        //{
        //    foreach (var client in _connectedClients)
        //    {
        //        if(client.Key.ToLower() != emailAddress.ToLower())
        //        {
        //            client.Value.connection.GetMessage(message, emailAddress);
        //        }
        //    }
        //}

        public UserInfo GetUserInfo(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var users = (from u in db.UserProfiles
                             where u.UserID == userID
                             select u).First();
                UserInfo newClient = new UserInfo();
                newClient.UserID = users.UserID;
                newClient.FirstName = users.DisplayFName;
                newClient.LastName = users.DisplayLName;
                newClient.EmailAddress = users.EmailAddress;
                newClient.Privileges = users.Privileges;
                return newClient;
            }
        }

        public bool IsValidUser(string emailAddress)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                try
                {
                    var users = (from u in db.UserProfiles
                                 where u.EmailAddress == emailAddress
                                 select u).First();
                    Console.WriteLine(users.EmailAddress);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public void DoWork()
        {
            throw new NotImplementedException();
        }

        public void RegisterServerAdmin(AdminInfo admin)
        {
            try
            {
                //Stores Server Admin into Logged in _ConnectedServerAdmins List
                IServer establishedUserConnection = OperationContext.Current.GetCallbackChannel<IServer>();
                ConnectedServerAdmin newAdmin = new ConnectedServerAdmin();
                newAdmin.connection = establishedUserConnection;
                newAdmin.Admin = admin;
                _connectedServerAdmins.TryAdd(newAdmin.Admin.ServerAdminID, newAdmin);

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Server Admin Login: {0} at {1}", newAdmin.Admin.EmailAddress, System.DateTime.Now);
                Console.ResetColor();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR WHILE REGISTERING SERVER ADMIN!\nMESSAGE: " + ex.Message);
                Console.ResetColor();
            }
         }

        

        public void Logout()
        {
            ConnectedClient client = GetMyClient();
            if(client != null)
            {
                ConnectedClient removedClient;
                _connectedClients.TryRemove(client.User.UserID, out removedClient);

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Client Loggoff: {0} at {1}", removedClient.User.EmailAddress, System.DateTime.Now);
                Console.ResetColor();
            }
        }

        public ConnectedClient GetMyClient()
        {
            IClient establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();
            foreach (var client in _connectedClients)
            {
                if (client.Value.connection == establishedUserConnection)
                {
                    return client.Value;
                }
            }
            return null;
        }

        public void AdminLogout()
        {
            ConnectedServerAdmin connectedAdmin = GetMyAdmin();
            if(connectedAdmin != null)
            {
                ConnectedServerAdmin removedAdmin;
                _connectedServerAdmins.TryRemove(connectedAdmin.Admin.ServerAdminID, out removedAdmin);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Server Admin Logoff: {0} at {1}", removedAdmin.Admin.EmailAddress, System.DateTime.Now);
                Console.ResetColor();
            }
        }

        public ConnectedServerAdmin GetMyAdmin()
        {
            IServer establishedAdminConnection = OperationContext.Current.GetCallbackChannel<IServer>();
            foreach (var admin in _connectedServerAdmins)
            {
                if(admin.Value.connection == establishedAdminConnection)
                {
                    return admin.Value;
                }
            }
            return null;
        }

        public void AdminLogMessage(string message)
        {
            ConnectedServerAdmin currentAdmin = GetMyAdmin();
            //Future Will Log to Log File.
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Message: {0} Sent by: {1} at {2}", message, currentAdmin.Admin.EmailAddress, System.DateTime.Now);
            Console.ResetColor();
        }
    }
}
