using System.Collections.Generic;
using System.ServiceModel;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    [ServiceContract(CallbackContract = typeof(IServer))]
    public interface IUnstuckMEServer
    {
        /// <summary>
        /// Returns a boolean value identifying that the server is running.
        /// </summary>
        /// <returns>True.</returns>
        [OperationContract]
        bool TestNewConfig();

        /// <summary>
        /// Attempts to log in a server administrator.
        /// </summary>
        /// <param name="admin">The information of the server administrator.</param>
        [OperationContract(IsOneWay = true)]
        void RegisterServerAdmin(AdminInfo admin);

        /// <summary>
        /// Disconnects a server administrator.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void AdminLogout();

        /// <summary>
        /// Logs information for actions invoked by a server administrator. Currently only writes a message to the console.
        /// </summary>
        /// <param name="message">The message to be logged.</param>
        [OperationContract(IsOneWay = true)]
        void AdminLogMessage(string message);

        /// <summary>
        /// Gets all the clients that are currently logged in.
        /// </summary>
        /// <returns>A list of UserInfo structures containing all the information of each online user.</returns>
        [OperationContract]
        List<UserInfo> AdminGetAllOnlineUsers();

        /// <summary>
        /// Sends a message to all users who are online.
        /// </summary>
        /// <param name="recipients">The recipients of the </param>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void AdminSendMessageToUsers(List<string> recipients, string message);

        /// <summary>
        /// Sends a message to all connected clients that the server is shutting down.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void AdminServerShuttingDown();

        /// <summary>
        /// Registers a new tutoring organization. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="organizationName">The name of the new tutoring organization.</param>
        /// <returns>The unique identifier of the newly created organization if successful, -1 if unsuccessful.</returns>
        [OperationContract]
        int AdminCreateMentoringOrganization(string organizationName);

        /// <summary>
        /// Registers a new class. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="courseName">The name of the new class.</param>
        /// <param name="courseCode">The subject of the new class.</param>
        /// <param name="courseNumber">The course number of the new class.</param>
        /// <returns>The unique identifier of the newly created class if successful, -1 if unsuccessful.</returns>
        [OperationContract]
        int AdminCreateClass(string courseName, string courseCode, int courseNumber);

        /// <summary>
        /// Remove a class from the database. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="classID">The unique identifier of the class to be removed.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        [OperationContract]
        int AdminDeleteClass(int classID);

        /// <summary>
        /// Removes a tutoring organization from the database. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="organizationID">The unique identifier of the organization to be removed.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        [OperationContract]
        int AdminDeleteMentoringOrganization(int organizationID);

        /// <summary>
        /// Removes a report from the database. Can only be invoked by an administrator.
        /// </summary>
        /// <param name="reportID">The unique identifier of the report to be removed.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        [OperationContract]
        int AdminDeleteReport(int reportID);
    }
}
