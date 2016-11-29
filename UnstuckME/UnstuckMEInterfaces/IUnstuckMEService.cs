using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace UnstuckMEInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUnstuckMEService" in both code and config file together.
    [ServiceContract]
    public interface IUnstuckMEService
    {
        /// <summary>
        /// These Operation Contracts are the functions that the user can call from UnstuckMEClient using proxy.FunctionName(Parameters);
        /// Implement these functions in UnstuckMEService.cs
        /// </summary>

        [OperationContract]
        void ChangeUserName(string emailaddress, string newFirstName, string newLastName);

        [OperationContract]
        bool UserLoginAttempt(string emailAddress, string passWord);

        [OperationContract]
        bool CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword, string privileges, string salt);
        
    }
}
