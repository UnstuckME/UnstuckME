using System;
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
    public class UnstuckMEService : IUnstuckMEService
    {
        public ConcurrentDictionary<int, ConnectedClient> _connectedClients = new ConcurrentDictionary<int, ConnectedClient>();

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

        
    }
}
