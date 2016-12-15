using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{

    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IUnstuckMEService
    {
        /// <summary>
        /// These Operation Contracts are the functions that the user can call from UnstuckMEClient using proxy.FunctionName(Parameters);
        /// Implement these functions in UnstuckMEService.cs
        /// </summary>
        /// 

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
        int CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword, string privileges, string salt);

        [OperationContract]
        List<UserClasses> GetUserClasses(int UserID);

        [OperationContract]
        void InsertStudentIntoClass(int UserID, int ClassID);

        //Checks to see if specified email address exists in the database.
        [OperationContract]
        bool IsValidUser(string emailAddress);
    }
}
