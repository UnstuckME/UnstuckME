using System;
using System.Linq;
using System.ServiceModel;
using UnstuckMEServer;
using UnstuckME_Classes;
using System.ServiceModel.Channels;

namespace UnstuckMEInterfaces
{
    public partial class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
    {
        /// <summary>
        /// Gets all the information of a specific user given the unique identifier or the email address of the user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <param name="emailAddress">The email address of the user.</param>
        /// <returns>A UserInfo structure that contains the UserID, first and last name, email address, privileges, average student and
        /// tutor ranks, the total number of reviews submitted as a student and tutor, password, salt value used for hashing, and the bytes
        /// representing the data of their profile picture.</returns>
        public UserInfo GetUserInfo(int? userID, string emailAddress)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var users = db.GetUserInfo(userID, emailAddress).First();

                UserInfo newClient = new UserInfo()
                {
                    UserID = users.UserID,
                    FirstName = users.DisplayFName,
                    LastName = users.DisplayLName,
                    EmailAddress = users.EmailAddress,
                    Privileges = users.Privileges,
                    AverageStudentRank = (float)users.AverageStudentRank,
                    AverageTutorRank = (float)users.AverageTutorRank,
                    TotalStudentReviews = users.TotalStudentReviews,
                    TotalTutorReviews = users.TotalTutorReviews,
                    UserPassword = users.UserPassword,
                    Salt = users.Salt
                };

                return newClient;
            }
        }

        /// <summary>
        /// Gets the unique identifier of a particular user.
        /// </summary>
        /// <param name="emailAddress">The email address of the user we need the unique identifier for.</param>
        /// <returns>An integer representing the unique identifier associated with the given email address</returns>
        public int GetUserID(string emailAddress)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var temp = db.GetUserID(emailAddress).First();
                return temp ?? -1;
            }
        }

        /// <summary>
        /// Changes the first and last name of the user with the specified email address.
        /// </summary>
        /// <param name="emailaddress">The email address of the user.</param>
        /// <param name="newFirstName">The new first name of the user.</param>
        /// <param name="newLastName">The new last name of the user.</param>
        public void ChangeUserName(string emailaddress, string newFirstName, string newLastName)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var users = (from b in db.UserProfiles
                             where b.EmailAddress == emailaddress
                             select b).First();

                users.DisplayFName = newFirstName;
                users.DisplayLName = newLastName;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Attempts to log the user in. Starts by recreating the hashed password, then checks to see if the user is already logged on so
        /// that they can't be logged in more than once. If successful, logs the callback channel, the incoming message properties, and
        /// the all the user's info from the database in the server's list of connected clients.
        /// </summary>
        /// <param name="emailAddress">The email address of the user attempting to log in.</param>
        /// <param name="passWord">The password of the user attempting to log in.</param>
        /// <returns>A UserInfo structure that contains the UserID, first and last name, email address, privileges, average student and
        /// tutor ranks, the total number of reviews submitted as a student and tutor, password, salt value used for hashing, and the bytes
        /// representing the data of their profile picture.</returns>
        public UserInfo UserLoginAttempt(string emailAddress, string passWord)
        {
            ConnectedClient newClient = new ConnectedClient();

            try
            {
                UserInfo user = GetUserInfo(null, emailAddress);

                string stringOfPassword = UnstuckMEHashing.RecreateHashedPassword(passWord, user.Salt);

                if (stringOfPassword == user.UserPassword)
                {
                    //If Client is already logged on return false (This may be removed later).
                    foreach (var client in _connectedClients)
                    {
                        if (client.Key == user.UserID)
                            return null;
                    }

                    //Stores Client into Logged in Users List
                    var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();
                    newClient.ChannelInfo = OperationContext.Current;
                    newClient.Connection = establishedUserConnection;
                    newClient.User = user;
                    newClient.ReturnAddress = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                    _connectedClients.TryAdd(newClient.User.UserID, newClient);

                    //Login Success, Print to console window.
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Client Login: {0} at {1}", newClient.User.EmailAddress, DateTime.Now);
                    Console.ResetColor();

                    foreach (var admin in _connectedServerAdmins)
                        admin.Value.Connection.GetUpdate(0, newClient.User);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return newClient.User;
        }

        /// <summary>
        /// Attempts to log the user in. Starts by recreating the hashed password, then checks to see if the user is already logged on so
        /// that they can't be logged in more than once. If successful, logs the callback channel, the incoming message properties, and
        /// the all the user's info from the database in the server's list of connected clients.
        /// </summary>
        /// <param name="displayFName">The first name of the new user.</param>
        /// <param name="displayLName">The last name of the new user.</param>
        /// <param name="emailAddress">The email address of the user attempting to log in.</param>
        /// <param name="userPassword">The password of the user attempting to log in.</param>
        /// <returns>A UserInfo structure that contains the UserID, first and last name, email address, privileges, average student and
        /// tutor ranks, the total number of reviews submitted as a student and tutor, password, salt value used for hashing, and the bytes
        /// representing the data of their profile picture.</returns>
        public bool CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword)
        {
            bool success = false;

            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                UnstuckMEPassword hashedUserPassword = UnstuckMEHashing.GetHashedPassword(userPassword);
                int retVal = db.CreateNewUser(displayFName, displayLName, emailAddress, hashedUserPassword.Password, hashedUserPassword.Salt);

                if (retVal == 1)
                    success = true;
            }

            return success;
        }

        /// <summary>
        /// Deletes an account and everything associated with that account.
        /// </summary>
        /// <param name="userID">The unique identifier of the account.</param>
        public void DeleteUserAccount(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.DeleteUserProfileByUserID(userID);
            }
        }

        /// <summary>
        /// Gets the first name of the specified user.
        /// </summary>
        /// <param name="userID">The unique identifier of a specific user.</param>
        /// <returns>The first name of the specified user.</returns>
        public string GetUserDisplayName(int userID)
        {
            string username = string.Empty;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    username = db.GetDisplayNameAndEmail(userID).First().DisplayFName;
                }

                return username;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return username;
            }
        }

        /// <summary>
        /// Changes the privileges of a specific user.
        /// </summary>
        /// <param name="userPrivs">The new user's privlieges.</param>
        /// <param name="userID">The unique identifier of a specific user.</param>
        public void SetUserPrivileges(Privileges userPrivs, int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.UpdatePrivilegesByUserID(userID, (int)userPrivs);
            }
        }
    }
}