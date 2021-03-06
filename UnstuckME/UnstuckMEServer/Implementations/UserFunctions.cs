﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using UnstuckMeLoggers;
using UnstuckMEServer;
using UnstuckME_Classes;
using System.Threading.Tasks;

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

                UserInfo newClient = new UserInfo
                {
                    UserID = users.UserID,
                    FirstName = users.DisplayFName,
                    LastName = users.DisplayLName,
                    EmailAddress = users.EmailAddress,
                    Privileges = (Privileges)users.Privileges,
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
                try
                {
                    var temp = db.GetUserID(emailAddress).First();
                    return temp ?? -1;
                }
                catch(Exception ex)
                {
                    UnstuckMEServerEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES_SERVER.DATABASE_RETURN_ERROR, ex.Message);
                    return -1;
                }
            }
        }

        /// <summary>
        /// Changes the first and last name of the user with the specified email address.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who is requesting to change their name.</param>
        /// <param name="newFirstName">The new first name of the user.</param>
        /// <param name="newLastName">The new last name of the user.</param>
        public async void ChangeUserName(int userID, string newFirstName, string newLastName)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.UpdateUserName(userID, newFirstName, newLastName);
                await Task.Factory.StartNew(() => AsyncChangeMessageSenderName(userID, newFirstName));
            }
        }

        /// <summary>
        /// Asynchronously sends the new first name of the user who are in conversations with online clients.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who changed their name.</param>
        /// <param name="newFirstName">The new first name of the user.</param>
        private void AsyncChangeMessageSenderName(int userID, string newFirstName)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var chatmembers = db.GetAllChatsAUserIsPartOF(userID);

                foreach (var client in _connectedClients)
                {
                    if (chatmembers.Contains(client.Key))
                        client.Value.Connection.ChangeChatMessageUsernames(client.Key, newFirstName);
                }
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
                    newClient.ChannelInfo.Channel.Closing += UserFaultedLogout;
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
        /// Called when the server faults a connected client's connection. Removes them from the online user list.
        /// </summary>
        /// <param name="sender">The client's IUnstuckMEService Channel.</param>
        /// <param name="ea">Event args.</param>
        internal void UserFaultedLogout(object sender, EventArgs ea)
        {
            try
            {
                KeyValuePair<int, ConnectedClient> faultedConnection = new KeyValuePair<int, ConnectedClient>(-1, null);
                ConnectedClient faultedClient = null;

                foreach (KeyValuePair<int, ConnectedClient> client in _connectedClients)
                {
                    if (client.Value.ChannelInfo.SessionId == ((IClientChannel)sender).SessionId)
                        faultedConnection = client;
                }

                if (faultedConnection.Key != -1)
                    _connectedClients.TryRemove(faultedConnection.Key, out faultedClient);

                if (faultedClient != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(faultedClient.User.EmailAddress + "'s socket is in a faulted state. They are now considered offline");
                    Console.ResetColor();
                }
                else
                    throw new Exception();
            }
            catch(Exception)
            {
                Console.WriteLine("Faulted Client Removal Failed, Server Restart May be required if Faulted client list is not empty.\nFaulted Clients:");
                foreach(KeyValuePair<int, ConnectedClient> client in _connectedClients)
                {
                    if (client.Value.ChannelInfo.Channel.State != CommunicationState.Opened)
                        Console.WriteLine("Faulted Client: {0}  Connection State: {1}.", client.Value.User.EmailAddress, client.Value.ChannelInfo.Channel.State);
                }
                Console.WriteLine("***Faulted Client Display End***");
            }
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