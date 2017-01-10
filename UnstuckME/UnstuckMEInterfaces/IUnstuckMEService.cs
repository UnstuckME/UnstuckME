using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;
using System.Text;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{

    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IUnstuckMEService
    {
        [OperationContract]
        UserInfo GetUserInfo(int userID);

        [OperationContract]
        int GetUserID(string emailAddress);

        //Still Testing
        //[OperationContract]
        //void SendMessageToAllUsers(string message, string emailAddress);

        [OperationContract]
        void ChangeUserName(string emailaddress, string newFirstName, string newLastName);

        [OperationContract]
        bool UserLoginAttempt(string emailAddress, string passWord);

        [OperationContract]
        bool CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword);

        [OperationContract]
        List<UserClasses> GetUserClasses(int UserID);

        [OperationContract]
        void InsertStudentIntoClass(int UserID, int ClassID);

        //Checks to see if specified email address exists in the database.
        [OperationContract]
        bool IsValidUser(string emailAddress);

        [OperationContract]
        void Logout();
    }

    [ServiceContract(CallbackContract = typeof(IServer))]
    public interface IUnstuckMEServer
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void RegisterServerAdmin(AdminInfo admin);

        [OperationContract]
        void AdminLogout();

        [OperationContract]
        void AdminLogMessage(string message);
    }
}
