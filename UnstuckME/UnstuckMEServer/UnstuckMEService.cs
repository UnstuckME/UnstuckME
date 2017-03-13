using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using UnstuckMEServer;
using UnstuckME_Classes;
using System.Security.Cryptography;
using System.Collections.Concurrent;
using System.Threading;
using System.Net;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using UnstuckMeLoggers;
using System.Net.Configuration;
using System.IO;

namespace UnstuckMEInterfaces
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    /// <summary>
    /// Implement any Operation Contracts from IUnstuckMEService.cs in this file.
    /// </summary>
    public class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
    {
        private ConcurrentDictionary<int, ConnectedClient> _connectedClients = new ConcurrentDictionary<int, ConnectedClient>();
        private ConcurrentDictionary<int, ConnectedServerAdmin> _connectedServerAdmins = new ConcurrentDictionary<int, ConnectedServerAdmin>();
        private static ConcurrentQueue<UnstuckMEBigSticker> _StickerList;
        private static ConcurrentQueue<UnstuckMEMessage> _MessageList;
		
		/// <summary>
		/// This function is for testing stored procedures. In program.cs replace:
		/// Thread userStatusCheck = new Thread(_server.CheckStatus); with Thread userStatusCheck = new Thread(_server.SPTest); 
		/// </summary>
		public void SPTest()
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                try
                {
                    db.CreateChat(2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

		#region Client-Server Functions
		/// <summary>
		/// Checks to see if there are any new messages for the client. If there are, sends the message to that client as long as they are logged into the server.
		/// </summary>
		public async void CheckForNewMessages()
        {
            _MessageList = new ConcurrentQueue<UnstuckMEMessage>();
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                while (true)
                {
                    if (_MessageList.Count != 0)
                    {
                        UnstuckMEMessage temp;
                        _MessageList.TryDequeue(out temp);
                        try
                        {
                            await Task.Factory.StartNew(() => AsyncMessageSendToUsers(temp));
                        }
                        catch (Exception)
                        { /*If Failure Message Will Be Lost, but server will not fail.*/ }
                    }
                }
            }
        }

		/// <summary>
		/// Asyncronously sends a message to a user and logs it in the database.
		/// </summary>
		/// <param name="inMessage">The message to be sent and stored in the database.</param>
        private void AsyncMessageSendToUsers(UnstuckMEMessage inMessage)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.InsertMessage(inMessage.ChatID, inMessage.Message, null, false, inMessage.SenderID);
                foreach (int client in inMessage.UsersInConvo)
                {
                    if (client != inMessage.SenderID)
                    {
                        if (_connectedClients.ContainsKey(client)) //Checks to see if client is online.
                        {
                            _connectedClients[client].connection.GetMessage(inMessage);
                        }
                    }
                }
            }
        }

		/// <summary>
		/// Starts a new task that sends stickers to the clients who meet the criteria specified with the sticker.
		/// </summary>
        public async void CheckForNewStickers()
        {
            _StickerList = new ConcurrentQueue<UnstuckMEBigSticker>();
            UnstuckMEBigSticker temp;
            while (true)
            {
                if (_StickerList.Count != 0)
                {
                    _StickerList.TryDequeue(out temp);
                    await Task.Factory.StartNew(() => SendStickerToClients(temp));
                }
            }

        }

		/// <summary>
		/// Checks for online users that match the criteria specified with the sticker and sends it to those clients.
		/// </summary>
		/// <param name="inSticker">The sticker to be sent to qualified online users.</param>
        public void SendStickerToClients(UnstuckMEBigSticker inSticker)
        {
            UnstuckMEAvailableSticker s = new UnstuckMEAvailableSticker();
            s.ClassID = inSticker.Class.ClassID;
            s.ProblemDescription = inSticker.ProblemDescription;
            s.StudentID = inSticker.StudentID;
            s.StickerID = inSticker.StickerID;
            s.Timeout = inSticker.Timeout;
            s.CourseCode = inSticker.Class.CourseCode;
            s.CourseName = inSticker.Class.CourseName;
            s.CourseNumber = inSticker.Class.CourseNumber;
            s.StudentRanking = inSticker.StudentRanking;
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var tutors = db.GetUsersThatCanTutorASticker(s.StickerID);

                    foreach (var tutor in tutors)
                    {
                        if(_connectedClients.ContainsKey(tutor.Value))
                        {
                            _connectedClients[tutor.Value].connection.RecieveNewSticker(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SendStickerToClients Function Error: " + ex.Message);
            }
        }

		/// <summary>
		/// Checks that clients are still connected to the server. This is called on a separate thread every five seconds by the server.
		/// </summary>
        public void CheckUserStatus()
        {
            List<int> offlineUsers = new List<int>();
            try
            {
                while (true)
                {
                    foreach (KeyValuePair<int, ConnectedClient> client in _connectedClients)
                    {
                        if (client.Value.ChannelInfo.Channel.State != CommunicationState.Opened)
                        {
                            offlineUsers.Add(client.Key);
                        }
                    }
                    foreach (int user in offlineUsers)
                    {
                        ConnectedClient removedClient = new ConnectedClient();
                        _connectedClients.TryRemove(user, out removedClient);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(removedClient.User.EmailAddress + "'s socket is in a faulted state. They are now considered offline");
                        Console.ResetColor();
                    }
                    offlineUsers.Clear();
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
		/// Creates a new user with the specified name, email address, and password.
		/// </summary>
		/// <param name="displayFName">The first name of the new user.</param>
		/// <param name="displayLName">The last name of the new use.r</param>
		/// <param name="emailAddress">The email address of the new user.</param>
		/// <param name="userPassword">The password of the new user. The password is hashed and saved on the database.</param>
		/// <returns>True if the user was successfully created and stored in the database, false if not.</returns>
        public bool CreateNewUser(string displayFName, string displayLName, string emailAddress, string userPassword)
        {
            bool success = false;
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                UnstuckMEPassword hashedUserPassword = new UnstuckMEPassword();
                hashedUserPassword = UnstuckMEHashing.GetHashedPassword(userPassword);
                int retVal = db.CreateNewUser(displayFName, displayLName, emailAddress, hashedUserPassword.Password, hashedUserPassword.Salt);
                if (retVal == 1)
                {
                    success = true;
                }
            }
            return success;
        }

		/// <summary>
		/// Gets the unique identifier of a particular user.
		/// </summary>
		/// <param name="emailAddress">The email address of the user we need the unique identifier for.</param>
		/// <returns>An integer representing the unique identifier associated with the given email address</returns>
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
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                try
                {
                    UserInfo users = GetUserInfo(null, emailAddress);

                    string stringOfPassword = UnstuckMEHashing.RecreateHashedPassword(passWord, users.Salt);

                    if (stringOfPassword == users.UserPassword)
                    {
                        //If Client is already logged on return false (This may be removed later).
                        foreach (var client in _connectedClients)
                        {
                            if (client.Key == users.UserID)
                            {
                                return null;
                            }
                        }

                        //Stores Client into Logged in Users List
                        var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();
                        newClient.ChannelInfo = OperationContext.Current;
                        newClient.connection = establishedUserConnection;
						newClient.User = users;
                        newClient.returnAddress = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                        _connectedClients.TryAdd(newClient.User.UserID, newClient);
                        //Login Success, Print to console window.
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Client Login: {0} at {1}", newClient.User.EmailAddress, System.DateTime.Now);
                        Console.ResetColor();
                        foreach (var admin in _connectedServerAdmins)
                        {
                            admin.Value.connection.GetUpdate(0, newClient.User);
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return newClient.User;
        }

		/// <summary>
		/// Gets the classes a user can tutor for.
		/// </summary>
		/// <param name="UserID">The unique identifier of the user we are getting the classes of.</param>
		/// <returns>A list of classes the specified user has signed up to tutor. This includes the subject, course name and number, and the unique identifier.</returns>
        public List<UserClass> GetUserClasses(int UserID)
        {
            try
            {
                List<UserClass> Rlist = new List<UserClass>();

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var classes = db.GetUserClasses(UserID);

                    foreach (var c in classes)
                    {
                        UserClass temp = new UserClass();
						temp.ClassID = c.ClassID;
                        temp.CourseCode = c.CourseCode;
                        temp.CourseName = c.CourseName;
                        temp.CourseNumber = c.CourseNumber;
                        Rlist.Add(temp);
                    }
                }

                return Rlist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Adds the specified class to a user's list of classes he/she can tutor.
		/// </summary>
		/// <param name="UserID">The unique identifier of the user.</param>
		/// <param name="ClassID">The unique identifier of the class.</param>
        public void InsertStudentIntoClass(int UserID, int ClassID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.InsertStudentIntoClass(UserID, ClassID);
            }
        }

		/// <summary>
		/// Gets all the information of a specific user given the unique identifier or the email address of the user.
		/// </summary>
		/// <param name="userID">The unique identifier of the user.</param>
		/// <param name="emailAddress">The email address of the user.</param>
		/// <returns>A UserInfo structure that contains the UserID, first and last name, email address, privileges, average student and
		/// tutor ranks, the total number of reviews submitted as a student and tutor, password, salt value used for hashing, and the bytes
		/// representing the data of their profile picture.</returns>
		public UserInfo GetUserInfo(Nullable<int> userID, string emailAddress)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var users = db.GetUserInfo(userID, emailAddress).First();

                UserInfo newClient = new UserInfo();
                newClient.UserID = users.UserID;
                newClient.FirstName = users.DisplayFName;
                newClient.LastName = users.DisplayLName;
                newClient.EmailAddress = users.EmailAddress;
                newClient.Privileges = users.Privileges;
                newClient.AverageStudentRank = (float)users.AverageStudentRank;
                newClient.AverageTutorRank = (float)users.AverageTutorRank;
                newClient.TotalStudentReviews = users.TotalStudentReviews;
                newClient.TotalTutorReviews = users.TotalTutorReviews;
                newClient.UserPassword = users.UserPassword;
                newClient.Salt = users.Salt;
                newClient.UserProfilePictureBytes = GetProfilePicture(newClient.UserID);
                return newClient;
            }
        }

		/// <summary>
		/// Checks to see if the email address exists on the database.
		/// </summary>
		/// <param name="emailAddress">The email address of the user.</param>
		/// <returns>True if the email address is specified with an account, false if not.</returns>
		public bool IsValidUser(string emailAddress)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                try
                {
                    var users = GetUserInfo(null, emailAddress);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

		/// <summary>
		/// Disconnects a user from the server.
		/// </summary>
        public void Logout()
        {
            ConnectedClient client = GetMyClient();
            if (client != null)
            {
                ConnectedClient removedClient;
                _connectedClients.TryRemove(client.User.UserID, out removedClient);
                foreach (var admin in _connectedServerAdmins)
                {
                    admin.Value.connection.GetUpdate(1, removedClient.User);
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Client Loggoff: {0} at {1}", removedClient.User.EmailAddress, System.DateTime.Now);
                Console.ResetColor();
            }
        }

		/// <summary>
		/// Checks the list of connected clients for the connection of the calling client.
		/// </summary>
		/// <returns>The client that is trying to disconnect.</returns>
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

		/// <summary>
		/// Checks the list of connected server administrators for the connection of the calling client.
		/// </summary>
		/// <returns>The server administrator that is trying to disconnect.</returns>
        public ConnectedServerAdmin GetMyAdmin()
        {
            IServer establishedAdminConnection = OperationContext.Current.GetCallbackChannel<IServer>();
            foreach (var admin in _connectedServerAdmins)
            {
                if (admin.Value.connection == establishedAdminConnection)
                {
                    return admin.Value;
                }
            }
            return null;
        }

		/// <summary>
		/// Changes the password of the specified user. Rehashes the password before storing on the database.
		/// </summary>
		/// <param name="User">All the necessary user information.</param>
		/// <param name="newPassword">The new password.</param>
        public void ChangePassword(UserInfo User, string newPassword)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                UnstuckMEPassword newHashedPassword = UnstuckMEHashing.GetHashedPassword(newPassword);
                var users = (from b in db.UserProfiles
                            where b.UserID == User.UserID
                            select b).First();
                
				users.UserPassword = newHashedPassword.Password;
                users.Salt = newHashedPassword.Salt;
                db.SaveChanges();
            }
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

		#region GetReviews Functions
        /// <summary>
        /// Returns the reviews submitted by a specific user as a student.
        /// </summary>
        /// <param name="userID">The unique identifier of the account holder.</param>
        /// <param name="minstarrank">The minimum star ranking to see reviews. This parameter is optional, with a default value of 0.</param>
        /// <returns>A list containing all the reviews submitted by the user specified as a student.</returns>
		public List<UnstuckMEReview> GetUserStudentReviews(int userID, float minstarrank = 0)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				var studentReviews = db.GetUserStudentReviews(userID, minstarrank);

				List<UnstuckMEReview> studentReviewList = new List<UnstuckMEReview>();
				foreach (var review in studentReviews)
				{
                    UnstuckMEReview usReview = new UnstuckMEReview()
                    {
                        ReviewID = review.ReviewID,
                        StickerID = review.StickerID,
                        ReviewerID = review.ReviewerID,
                        StarRanking = (float)review.StarRanking,
                        Description = review.Description
                    };

                    studentReviewList.Add(usReview);
				}

				return studentReviewList;
			}
		}

        /// <summary>
        /// Returns the reviews submitted by a specific user as a tutor.
        /// </summary>
        /// <param name="userID">The unique identifier of the account holder.</param>
        /// <param name="minstarrank">The minimum star ranking to see reviews. This parameter is optional, with a default value of 0.</param>
        /// <returns>A list containing all the reviews submitted by the user specified as a tutor.</returns>
		public List<UnstuckMEReview> GetUserTutorReviews(int userID, float minstarrank = 0)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				var tutorReviews = db.GetUserTutorReviews(userID, minstarrank);

				List<UnstuckMEReview> tutorReviewList = new List<UnstuckMEReview>();
				foreach (var review in tutorReviews)
				{
                    UnstuckMEReview usReview = new UnstuckMEReview()
                    {
                        ReviewID = review.ReviewID,
                        StickerID = review.StickerID,
                        ReviewerID = review.ReviewerID,
                        StarRanking = (review.StarRanking.HasValue) ? (float)review.StarRanking.Value : 0,
                        Description = review.Description
                    };

                    tutorReviewList.Add(usReview);
				}

				return tutorReviewList;
			}
		}
        #endregion

        #region GetSticker Functions
        /// <summary>
        /// Gets the stickers that have been accepted by a tutor and marked as resolved.
        /// </summary>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="userID">The unique identifer of the account that submitted the stickers. This parameter is optional, with a default value of null.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that have tutors and marked as resolved.</returns>
        public List<UnstuckMESticker> GetResolvedStickers(double minstarrank = 0, Nullable<int> organizationID = null, Nullable<int> userID = null, Nullable<int> classID = null)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				var stickers = db.GetResolvedStickers(minstarrank, organizationID, userID, classID);

				List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
				foreach (var sticker in stickers)
				{
					UnstuckMESticker usSticker = new UnstuckMESticker();
					usSticker.StickerID = sticker.StickerID;
					usSticker.ClassID = sticker.ClassID;
					usSticker.StudentID = sticker.StudentID;
					usSticker.ProblemDescription = sticker.ProblemDescription;
					usSticker.MinimumStarRanking = (float)sticker.MinimumStarRanking;
					usSticker.SubmitTime = sticker.SubmitTime;
					usSticker.Timeout = sticker.Timeout;// - DateTime.Now).TotalSeconds;
				}

				return stickerList;
			}
		}

        /// <summary>
        /// Gets the stickers that have not been accepted by a tutor and surpassed the timeout date.
        /// </summary>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="userID">The unique identifer of the account that submitted the stickers. This parameter is optional, with a default value of null.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that have not been accepted by a tutor and surpassed the timeout date.</returns>
        public List<UnstuckMESticker> GetTimedOutStickers(double minstarrank = 0, Nullable<int> organizationID = null, Nullable<int> userID = null, Nullable<int> classID = null)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				var stickers = db.GetTimedOutStickers(minstarrank, organizationID, userID, classID);

				List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
				foreach (var sticker in stickers)
				{
					UnstuckMESticker usSticker = new UnstuckMESticker();
					usSticker.StickerID = sticker.StickerID;
					usSticker.ClassID = sticker.ClassID;
					usSticker.StudentID = sticker.StudentID;
					usSticker.ProblemDescription = sticker.ProblemDescription;
					usSticker.MinimumStarRanking = (float)sticker.MinimumStarRanking;
					usSticker.SubmitTime = sticker.SubmitTime;
					usSticker.Timeout = sticker.Timeout;// - DateTime.Now).TotalSeconds;
				}

				return stickerList;
			}
		}

        /// <summary>
        /// Gets the stickers submitted by a user, regardless if they are resolved or active.
        /// </summary>
        /// <param name="userID">The unique identifer of the account.</param>
        /// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that have been submitted by a specific user that matches the filtering criteria.</returns>
		public List<UnstuckMESticker> GetUserSubmittedStickers(int userID, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> classID = null)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				var userStickers = db.GetUserSubmittedStickers(userID, organizationID, minstarrank, classID);

				List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
				foreach (var sticker in userStickers)
				{
					UnstuckMESticker usSticker = new UnstuckMESticker();
					usSticker.StickerID = sticker.StickerID;
					usSticker.ProblemDescription = sticker.ProblemDescription;
					usSticker.ClassID = sticker.ClassID;
					usSticker.StudentID = sticker.StudentID;
					usSticker.TutorID = (sticker.TutorID.HasValue) ? sticker.TutorID.Value : 1;
					usSticker.MinimumStarRanking = (sticker.MinimumStarRanking.HasValue) ? (float)sticker.MinimumStarRanking : 0;
					usSticker.SubmitTime = sticker.SubmitTime;
					usSticker.Timeout = sticker.Timeout;// - DateTime.Now).TotalSeconds;
					stickerList.Add(usSticker);
				}

				return stickerList;
			}
		}

        /// <summary>
        /// Gets the stickers a user has tutored, regardless if they are resolved or active.
        /// </summary>
        /// <param name="userID">The unique identifer of the account.</param>
        /// <param name="organizationID">The unique identifer of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This parameter is optional, with a default value of 0.</param>
        /// <param name="classID">The unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers that a specific user has tutored that matches the filtering criteria.</returns>
		public List<UnstuckMESticker> GetUserTutoredStickers(int userID, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> classID = null)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				var userStickers = db.GetUserTutoredStickers(userID, organizationID, minstarrank, classID);

				List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
				foreach (var sticker in userStickers)
				{
					UnstuckMESticker usSticker = new UnstuckMESticker();
					usSticker.StickerID = sticker.StickerID;
					usSticker.ProblemDescription = sticker.ProblemDescription;
					usSticker.ClassID = sticker.ClassID;
					usSticker.StudentID = sticker.StudentID;
					usSticker.TutorID = (sticker.TutorID.HasValue) ? sticker.TutorID.Value : 1;
					usSticker.MinimumStarRanking = (sticker.MinimumStarRanking.HasValue) ? (float)sticker.MinimumStarRanking : 0;
					usSticker.SubmitTime = sticker.SubmitTime;
					usSticker.Timeout = sticker.Timeout;// - DateTime.Now).TotalSeconds;
					stickerList.Add(usSticker);
				}

				return stickerList;
			}
		}

        /// <summary>
        /// Gets the stickers available to tutor. This is currently untested, though it should work.
        /// </summary>
        /// <param name="caller">The unqiue identifier of the caller of the function.</param>
        /// <param name="organizationID">The unique identifier of the of the organization to filter. This parameter is optional, with a default value of null.</param>
        /// <param name="minstarrank">The minimum star ranking required in order to see the sticker. This should be the callee's student star ranking. This paramter is optional, with a default value of 0.</param>
        /// <param name="userID">The unique identifier of the account to filter. This paramter is optional, with a default value of null.</param>
        /// <param name="classID">the unique identifier of the class to filter the results through. This parameter is optional, with a default value of null.</param>
        /// <returns>A list of stickers available to tutor that meets the filtering criteria.</returns>
		public List<UnstuckMESticker> GetActiveStickers(int caller, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> userID = null, Nullable<int> classID = null)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				//var userStickers = db.GetActiveStickers_ClassASC(caller, minstarrank, firstrow, lastrow, userID.HasValue ? userID : null, classID.HasValue ? classID : null);
				var userStickers = db.GetActiveStickers(caller, organizationID, minstarrank, userID, classID);

				List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
				foreach (var sticker in userStickers)
				{
					UnstuckMESticker usSticker = new UnstuckMESticker();
					usSticker.StickerID = (int)sticker.StickerID;
					usSticker.ProblemDescription = sticker.ProblemDescription;
					usSticker.ClassID = (int)sticker.ClassID;
					usSticker.CourseCode = sticker.CourseCode;
					usSticker.CourseName = sticker.CourseName;
					usSticker.CourseNumber = (short)sticker.CourseNumber;
					usSticker.StudentID = (int)sticker.StudentID;
					usSticker.TutorID = (sticker.TutorID.HasValue) ? sticker.TutorID.Value : new Nullable<int>();
					usSticker.StudentRanking = (sticker.MinimumStarRanking.HasValue) ? (double)sticker.MinimumStarRanking : 0;
					usSticker.SubmitTime = (DateTime)sticker.SubmitTime;
					usSticker.Timeout = (DateTime)sticker.Timeout;
					stickerList.Add(usSticker);
				}

				return stickerList;
			}
		}
		#endregion

		/// <summary>
		/// Associates a user with an official tutoring organization.
		/// </summary>
		/// <param name="userID">The unique identifier of the user.</param>
		/// <param name="organizationID">The unique identifier of the tutoring organization.</param>
		public void AddUserToTutoringOrganization(int userID, int organizationID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.InsertUserIntoMentorProgram(userID, organizationID);
            }
        }

		/// <summary>
		/// Submits a new sticker to the database and associates it with any specified tutoring organizations. Queues the sticker to be sent to qualified online users.
		/// </summary>
		/// <param name="newSticker">The new sticker.</param>
		/// <returns>The new sticker's unique identifier if it was submitted successfully, -1 if not.</returns>
        public int SubmitSticker(UnstuckMEBigSticker newSticker)
        {
            try
            {
                int retstickerID = 0;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var stickerID = db.CreateSticker(newSticker.ProblemDescription, newSticker.Class.ClassID, newSticker.StudentID, newSticker.MinimumStarRanking, newSticker.TimeoutInt).First();

                    if (stickerID.Value == 0)
                    {
                        throw new Exception("Create Sticker Failed, Returned sticker ID = 0");
                    }
                    else
                    {
                        retstickerID = stickerID.Value;
                    }
                    if (newSticker.AttachedOrganizations.Count != 0)
                    {
                        foreach (int orgID in newSticker.AttachedOrganizations)
                            db.AddOrgToSticker(retstickerID, orgID);
                    }
                    newSticker.StickerID = retstickerID;
                    _StickerList.Enqueue(newSticker);
                }
                return retstickerID;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Sticker Submit Error: " + ex.Message);
                return -1;
            }
        }

        /// <summary>
        /// Currently retrieves the data of the profile picture fro the database. Set up to retrieve the filepath of the picture from the database,
        /// open the file, and convert it to a byte array.
        /// </summary>
        /// <param name="userID">The unique identifier of the user.</param>
        /// <returns>A byte array containing the data of the image file. If streaming can be implmented, this will return a Stream instead.</returns>
        public byte[] GetProfilePicture(int userID)
        {
            string directory = string.Empty;
            directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\UnstuckME\" + userID.ToString() + @"\ProfilePicture.jpeg";

            //replace above with below once filepath is implemented
            //using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            //{
            //    directory = db.GetProfilePicture(userID).First();
            //}

            //FileStream file = new FileStream(directory, FileMode.Open, FileAccess.Read);
            //byte[] imgByte = new byte[file.Length];
            //file.Read(imgByte, 0, Convert.ToInt32(file.Length));

            
            
            /*******************************************************************
             * When using remote server comment this out and use code above ^^^
            *******************************************************************/
            byte[] imgByte = null;
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                imgByte = db.GetProfilePicture(userID).First();
            }

            return imgByte;
        }

		/// <summary>
		/// Overwrites the profile picture data of a specific user on the database.
		/// </summary>
		/// <param name="userID">The unique identifier of the user.</param>
		/// <param name="image">A byte array that contains the data of the new image.</param>
        public void SetProfilePicture(int userID, byte[] image)
        {
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create) + @"\UnstuckME\";
            Directory.CreateDirectory(directory += userID.ToString());
            directory += @"\ProfilePicture.jpeg";

            using (FileStream newfile = new FileStream(directory, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite, image.Length, FileOptions.Encrypted))
            {
                newfile.Write(image, 0, image.Length);
            }

            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.UpdateProfilePicture(userID, image); //replace image with directory
            }
        }

        /// <summary>
        /// Removes the specified class from a user's list of classes he/she can tutor.
        /// </summary>
        /// <param name="UserID">The unique identifier of the user.</param>
        /// <param name="ClassID">The unique identifier of the class.</param>
        public void RemoveUserFromClass(int UserID, int ClassID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.DeleteUserFromClass(UserID, ClassID);
            }
        }

		/// <summary>
		/// Gets all the course codes in the database.
		/// </summary>
		/// <returns>A list of strings containing the course codes in the database.</returns>
        public List<string> GetCourseCodes()
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var codes = db.GetCourseCodes();

                List<string> rlist = new List<string>();
                List<string> rlist2 = new List<string>();
                foreach (var code in codes)
                {
                    rlist.Add(code.ToString());
                }

                IEnumerable<string> list = rlist.Distinct();
                foreach (string classcode in list)
                {
                    rlist2.Add(classcode);
                }
				//codes.Dispose();		//need this to release memory   
				return rlist2;
            }
        }

		/// <summary>
		/// Gets the unique identifier of a specific class.
		/// </summary>
		/// <param name="code">The course code of the class.</param>
		/// <param name="number">The course number of the class.</param>
		/// <returns>An integer representing the unique identifier of a specific class.</returns>
        public int GetCourseIdNumberByCodeAndNumber(string code, string number)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                int num = Convert.ToInt32(number);
                var ID = db.GetCourseIDByCodeAndNumber(code, (short)num).First();

				return ID.Value;
            }
        }

		/// <summary>
		/// Gets the course name of a specific class.
		/// </summary>
		/// <param name="code">The course code of the class.</param>
		/// <param name="number">The course number of the class.</param>
		/// <returns>A string representing the name of a specific class.</returns>
        public string GetCourseNameByCodeAndNumber(string code, string number)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                int num = Convert.ToInt32(number);
                var name = db.GetCourseNameByCodeAndNumber(code, (short)num).First();
				
				return name;
            }
        }

		/// <summary>
		/// Gets all the course numbers by subject.
		/// </summary>
		/// <param name="CourseCode">The course code.</param>
		/// <returns>A list of course numbers associated with a subject.</returns>
        public List<string> GetCourseNumbersByCourseCode(string CourseCode)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var codes = db.GetCourseNumberByCourseCode(CourseCode);

                List<string> rlist = new List<string>();
                List<string> rlist2 = new List<string>();
                foreach (var code in codes)
                {
                    rlist.Add(code.Value.ToString());
                }

                IEnumerable<string> list = rlist.Distinct();
                foreach (string classcode in list)
                {
                    rlist2.Add(classcode);
                }
				//codes.Dispose();		//need this to release memory   
                return rlist2;
            }
        }

		/// <summary>
		/// Registers another user as a contact.
		/// </summary>
		/// <param name="userId">The unique identifier of the callee.</param>
		/// <param name="friendUserID">The unique identifier of the user to add as a contact.</param>
		/// <returns>The unique identifier of the user to add as a contact if successful, -1 if unsuccessful.</returns>
        public int AddFriend(int userId, int friendUserID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.AddFriend(userId, friendUserID);
                }

                return friendUserID; //On success return friendID
            }
            catch (Exception)
            {
                return -1; //If Failure to add friend
            }
        }

		/// <summary>
		/// Creates a chat associated with a user.
		/// </summary>
		/// <param name="userId">The unique identifer of the callee.</param>
		/// <returns>The unique identifier of the newly created chat if successful, -1 if unsuccessful.</returns>
        public int CreateChat(int userId)
        {
            try
            {
                int chatID = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    int result = db.CreateChat(userId).FirstOrDefault().Value;
                    chatID = result;
                }

                return chatID; //On success return chatID failure -1
            }
            catch (Exception)
            {
                return -1; //If Failure to create chat
            }
        }

		/// <summary>
		/// Submits a report to the database.
		/// </summary>
		/// <param name="reportDescription">The description of the report.</param>
		/// <param name="flaggerID">The unique identifier of the client who submitted the report.</param>
		/// <param name="reviewID">The unique idenitifer of the review that is being reported.</param>
		/// <returns>The unique identifier of the newly submitted report if successul, -1 if unsuccessful.</returns>
        public int CreateReport(string reportDescription, int flaggerID, int reviewID)
        {
            try
            {
                int retVal = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.CreateReport(reportDescription, flaggerID, reviewID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return -1; //If Failure to create report
            }
        }

		/// <summary>
		/// Submits a review to the database.
		/// </summary>
		/// <param name="stickerID">The unique identifier of the sticker associated with the review.</param>
		/// <param name="reviewerID">The unique identifier of the user submitting the review.</param>
		/// <param name="starRanking">The rating given to the user being reviewed.</param>
		/// <param name="description">The description of the review.</param>
		/// <returns>The unique identifier of the newly submitted review if successful, -1 if unsuccessful.</returns>
        public int CreateReview(int stickerID, int reviewerID, double starRanking, string description)
        {
            try
            {
                int retVal = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.CreateReview(stickerID, reviewerID, starRanking, description);
                }

                return retVal;
            }
            catch (Exception)
            {
                return -1; //If Failure to create review
            }
        }

		/// <summary>
		/// Removes a user from their contacts.
		/// </summary>
		/// <param name="userID">The unique identifier of the callee.</param>
		/// <param name="fileID">The unique identifier of the user to removed from contacts.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int DeleteFriend(int userID, int fileID)
        {
            try
            {
                int retVal = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.DeleteFriend(userID, fileID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return -1; //If Failure to remove friend
            }
        }

		/// <summary>
		/// Deletes a message. Should be broadcasted to the other people in the chat once it is deleted.
		/// </summary>
		/// <param name="messageID">The unique identifier of the message to be deleted.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int DeleteMessage(int messageID)
        {
            try
            {
                int retVal = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.DeleteMessageByMessageID(messageID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return -1; //If Failure to create chat.
            }
        }

		/// <summary>
		/// Delete a report submitted by a specific user. More than likely does not work, do not use.
		/// </summary>
		/// <param name="userID">The unique identifier of the user who submitted the report.</param>
		/// <param name="reportID">The unique identifier of the report to be removed.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful</returns>
        public int DeleteReportByUser(int userID, int reportID)
        {
            try
            {
                int retVal = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    //var report = from u in db.Reports
                    //where reportID == u.ReportID
                    //select new { ReportID = u.ReportID, ReporterID = u.FlaggerID };

                    //if (report.First().ReporterID == userID)
                    //{
                    //	db.DeleteReportByReportID(reportID);
                    //}
                    //else
                    //{
                    //	throw new Exception();
                    //}

                    //not sure if this works
                    var report = db.GetReportsSubmittedByUser(userID);

                    for (int i = 0; report.ElementAt(i).ReportID == reportID; i++)
                    {
                        if (report.ElementAt(i).ReportID == reportID)
                            retVal = db.DeleteReportByReportID(reportID);
                    }
                }

                return retVal;
            }
            catch (Exception)
            {
                return -1; //failure to find or delete report
            }
        }

		/// <summary>
		/// Deprecated.
		/// </summary>
		/// <param name="chatID">The unique identifier of the chat to insert the message into.</param>
		/// <param name="message">The message data.</param>
		/// <param name="userID">The unique identifier of the user who sent the message.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int InsertMessage(int chatID, string message, int userID)
        {
            try
            {
                int retVal = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.InsertMessage(chatID, message, null, null, userID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return -1; //If Failure to insert message
            }
        }

		/// <summary>
		/// Adds a user to a chat.
		/// </summary>
		/// <param name="userID">The unique identifier of the user to add to the chat.</param>
		/// <param name="chatID">The unique identifier of the chat to add the user to.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int InsertUserIntoChat(int userID, int chatID)
        {
            try
            {
                int retVal = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.InsertUserIntoChat(userID, chatID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return -1; //If Failure to cinsert user into chat
            }
        }

		/// <summary>
		/// Gets all tutoring organizations in the database.
		/// </summary>
		/// <returns>A list of organizations containing the unique identifier and the name of each organization.</returns>
        public List<Organization> GetAllOrganizations()
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var organizations = db.GetAllOrganizations();

                List<Organization> orgs = new List<Organization>();

                foreach (var org in organizations)
                {
                    Organization new_org = new Organization();
                    new_org.MentorID = org.MentorID;
                    new_org.OrganizationName = org.OrganizationName;
                    orgs.Add(new_org);
                }
				//organizations.Dispose();		//need this to release memory   
				return orgs;
            }
        }

		/// <summary>
		/// Test function for streaming. Do not use.
		/// </summary>
		/// <param name="stream">A stream of data.</param>
		/// <returns>A stream of data.</returns>
		public System.IO.Stream Test(System.IO.Stream stream)
        {
            Console.WriteLine("Hello World");
			return stream;
        }

		/// <summary>
		/// Gets unique identifiers of all the chats a user is associated with.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>A list of chats, each containing the unique identifier of that chat.</returns>
        private List<UnstuckMEChat> GetChatIDs(int userID)
        {
            try
            {
                List<UnstuckMEChat> chatIDList = new List<UnstuckMEChat>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var dbChats = db.GetAllChatsAUserIsPartOF(userID);

                    foreach (var chatItem in dbChats)
                    {
                        UnstuckMEChat temp = new UnstuckMEChat();
                        temp.ChatID = chatItem.Value;
                        chatIDList.Add(temp);
                    }

					//dbChats.Dispose();		//need this to release memory   
				}
				return chatIDList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Gets all the members of a specific chat.
		/// </summary>
		/// <param name="chatID">The unique identifier of a specific chat.</param>
		/// <returns>A list of users, each containing the unique identifier of each user and their first name.</returns>
        private List<UnstuckMEChatUser> GetChatMembers(int chatID)
        {
            try
            {
                List<UnstuckMEChatUser> UserList = new List<UnstuckMEChatUser>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var chatMembers = db.GetAllMembersOfAChat(chatID);
                    foreach (var member in chatMembers)
                    {
                        UnstuckMEChatUser temp = new UnstuckMEChatUser();
                        temp.UserID = member.UserID;
                        temp.UserName = member.DisplayFName;
                        temp.ProfilePicture = null;
                        UserList.Add(temp);
                    }

					//chatMembers.Dispose();		//need this to release memory   
				}
				return UserList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Gets a set amount of messages from a chat. By default grabs the first 75 messages of that chat, however if older messages
		/// need to be gathered, this can be done by providing a value for the firstrow and lastrow parameters.
		/// </summary>
		/// <param name="chatID"></param>
		/// <param name="firstrow">The first row in the database table of messages to get. Optional parameter, defaults to 0.</param>
		/// <param name="lastrow">The number of messages to get from the database. Optional parameter, defaults to 75.</param>
		/// <returns>A list of messages, each containing the unique identifier, message data, time the message was sent,
		/// the filepath (if it is a file, otherwise will be an empty string), the unique identifier of the chat the message belongs to,
		/// and the unique identifier and first name of the user who sent the message.</returns>
        private List<UnstuckMEMessage> GetChatMessages(int chatID, short firstrow = 0, short lastrow = 75)
        {
            try
            {
                List<UnstuckMEMessage> MessageList = new List<UnstuckMEMessage>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
					var messages = db.GetChatMessages(chatID, firstrow, lastrow);

                    foreach (var message in messages)
                    {
                        UnstuckMEMessage temp = new UnstuckMEMessage();
                        temp.Message = message.MessageData;
                        temp.MessageID = message.MessageID;
                        temp.Time = message.SentTime;
                        temp.FilePath = message.FilePath;
                        temp.ChatID = chatID;
                        temp.SenderID = message.SentBy;
                        temp.Username = message.DisplayFName;
                        MessageList.Add(temp);
                    }

					//messages.Dispose();		//need this to release memory   
				}
				return MessageList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Gets the chats and messages of each chat a user is associated with.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>A list of chats and the messages in each chat.</returns>
        public List<UnstuckMEChat> GetUserChats(int userID)
        {
            try
            {
                List<UnstuckMEChat> chatList = GetChatIDs(userID);
                if (chatList == null)
                    throw new Exception("ChatID List Retrieval Failure");

                foreach (UnstuckMEChat chat in chatList)
                {
                    chat.Users = GetChatMembers(chat.ChatID);
                    chat.Messages = GetChatMessages(chat.ChatID);
                }
                return chatList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Queues a message to be sent to the users in the chat.
		/// </summary>
		/// <param name="message">The message to be sent.</param>
        public void SendMessage(UnstuckMEMessage message)
        {
            try
            {
                _MessageList.Enqueue(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

		/// <summary>
		/// Not implemented.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="file"></param>
		public void UploadFile(UnstuckMEMessage message, UnstuckMEFile file)
		{
			try
			{
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					//db.InsertMessage(message.ChatID, message.Message, message.FilePath, message.IsFile, message.SenderID);
					//user uploads a file contained in a message
					//server receives message
					//server saves bytes locally and updates filepath
					//server updates database with new message
					//server sends message to users in the chat

				}

				foreach (int client in message.UsersInConvo)
				{
					if (client != message.SenderID && _connectedClients.ContainsKey(client))
						_connectedClients[client].connection.GetFile(message, file);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Gets the stickers when first logging in.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>A list of available stickers, containing the unqiue identifier of the class the sticker is associated with
		/// and all the information for that class, the description, the unique identifier of the sticker, the unique identifier
		/// of the user who submitted the sticker, the student ranking of that user, and the timeout date.</returns>
        public List<UnstuckMEAvailableSticker> InitialAvailableStickerPull(int userID)
        {
            try
            {
                List<UnstuckMEAvailableSticker> stickerList = new List<UnstuckMEAvailableSticker>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var dbStickers = db.InitialStickerPull(userID);

                    foreach (var sticker in dbStickers)
                    {
                        UnstuckMEAvailableSticker temp = new UnstuckMEAvailableSticker();
                        temp.ClassID = sticker.ClassID.Value; 
                        temp.CourseCode = sticker.CourseCode;
                        temp.CourseName = sticker.CourseName;
                        temp.CourseNumber = sticker.CourseNumber.Value;
                        temp.ProblemDescription = sticker.ProblemDescription;
                        temp.StickerID = sticker.StickerID.Value;
                        temp.StudentID = sticker.StudentID.Value;
                        temp.StudentRanking = sticker.StudentRanking.Value;
                        temp.Timeout = sticker.Timeout.Value;
                        stickerList.Add(temp);
                    }

                }
                return stickerList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Gets the messages and users in a specific chat.
		/// </summary>
		/// <param name="chatID">The unique identifier of a specific chat.</param>
		/// <returns>An UnstuckMEChat that contains the messages and members of the specified chat.</returns>
        public UnstuckMEChat GetSingleChat(int chatID)
        {
            try
            {
                UnstuckMEChat returnedChat = new UnstuckMEChat();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    returnedChat.ChatID = chatID;
                    returnedChat.Messages = GetChatMessages(chatID);
                    returnedChat.Users = GetChatMembers(chatID);
                }

                return returnedChat;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Gets the information of a single class.
		/// </summary>
		/// <param name="classID">The unique identifier of a specific class.</param>
		/// <returns>A UserClass containing the course code, name, and number.</returns>
        public UserClass GetSingleClass(int classID)
        {
            try
            {
                UserClass temp = new UserClass();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
					var classDB = db.ViewClasses(classID).First();

                    temp.ClassID = classDB.ClassID;
                    temp.CourseNumber = classDB.CourseNumber;
                    temp.CourseName = classDB.CourseName;
                    temp.CourseCode = classDB.CourseCode;
                }
                return temp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Associates a sticker with a chat once the sticker has been acccepted.
		/// </summary>
		/// <param name="chatID">The unique identifier of the chat.</param>
		/// <param name="stickerID">The unique identifier of the sticker.</param>
		public void AddChatToSticker(int chatID, int stickerID)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				db.UpdateStickerByChatID(chatID, stickerID);
			}
		}

		/// <summary>
		/// Gets the classes and adds them to the client. There is a much more efficient way to do this.
		/// </summary>
		/// <param name="inClass">The unique identifier of the class to be added.</param>
		/// <param name="userID">The unique identifier of the user adding the class.</param>
		public void AddClassesToClient(int inClass, int userID)
        {
            try
            {
                UserClass temp = new UserClass();

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
					var c = db.ViewClasses(inClass).First();

                    temp.CourseNumber = c.CourseNumber;
                    temp.CourseName = c.CourseName;
                    temp.CourseCode = c.CourseCode;
                    temp.ClassID = c.ClassID;
                }

                foreach (var client in _connectedClients)
                {
                    if (client.Key == userID)
                    {
                        client.Value.connection.AddClasses(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Invoked when a tutor accepts a sticker. Updates the TutorID associated with that sticker.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who has accepted the sticker.</param>
        /// <param name="stickerID">The unique identifier fo the sticker that has been accepted.</param>
        public void AcceptSticker(int tutorID, int stickerID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.UpdateTutorIDByTutorIDAndStickerID(tutorID, stickerID);
                }

                foreach (var client in _connectedClients)
                {
                    if(client.Key != tutorID)
                    {
                        client.Value.connection.RemoveGUISticker(stickerID);
                    }  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

		/// <summary>
		/// Gets all the contacts associated with a specific user.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>A list of users containing the first name and unique identifier of each contact.</returns>
        public List<UnstuckMEChatUser> GetFriends(int userID)
        {
            try
            {
                List<UnstuckMEChatUser> FriendsList = new List<UnstuckMEChatUser>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
					var dbFriends = db.GetUserFriends(userID);

                    foreach (var friend in dbFriends)
                    {
                        UnstuckMEChatUser temp = new UnstuckMEChatUser();
                        temp.ProfilePicture = null;
                        temp.UserName = friend.DisplayFName;
                        temp.UserID = friend.FriendUserID;
                        FriendsList.Add(temp);
                    }
                }
                return FriendsList;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		/// <summary>
		/// Gets the first name of the specified user.
		/// </summary>
		/// <param name="userID">The unique identifier of a specific user.</param>
		/// <returns>The first name of the specified user.</returns>
        public string GetUserDisplayName(int userID)
        {
            try
            {
                string username = string.Empty;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
					var dbusername = db.GetDisplayNameAndEmail(userID);

					username = dbusername.First().DisplayFName;
                }

                return username;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
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

        //public Dictionary<int, UserClass> InitialUserClassesPull()
        //{
        //    Dictionary<int, UserClass> temp = new Dictionary<int, UserClass>();
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }
        //}

        /// <summary>
        /// Sends an account verification email containing a code in order to activiate the account. Uses email settings configured
        /// by the server administrator to send the email.
        /// </summary>
        /// <param name="emailtype">An enum specifying the purpose of the email being sent.</param>
        /// <param name="userEmailAddress">The email address of the new user.</param>
        /// <param name="username">The first name of the new user.</param>
        /// <returns>An 8-character code that must be entered in on the client in order to activate the account.</returns>
        public string SendEmail(EmailType emailtype, string userEmailAddress, string username)
        {
            string verification_code = GenerateVerificationCode();  //generate a random 8-character verification code

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var mailSettings = (SmtpSection)config.GetSection("system.net/mailSettings/smtp");
            MailAddress address = new MailAddress(mailSettings.Network.UserName, "UnstuckME");
            MailMessage email = new MailMessage(address, new MailAddress(userEmailAddress));

            switch (emailtype)
            {
                case EmailType.CreateAccount:
                    {
                        email.Subject = "Activating your UnstuckME account";
                        //this body is temporary as recipient shouldn't reply to the email
                        email.Body = "Thanks for joining UnstuckME " + username + "! Please activate your account by entering the verification code below into the prompt in the application.\n\n"
                            + "By creating an account, you agree to UnstuckME Terms of Service and your University's Student Code of Conduct\n\nYour verification code:\t" + verification_code
                            + "\n\nIf something is not working, please reply to this email with your problem and we will attempt to solve your issue.";
                        email.Priority = MailPriority.Normal;
                        break;
                    }
                case EmailType.ResetPassword:
                    {
                        email.Subject = "UnstuckME account password reset";
                        //this body is temporary as recipient shouldn't reply to the email
                        email.Body = "Hello, " + username + "!\n\nThis is to notify you of an attempt to change your UnstuckME account password. You may use this password to log in to your account"
                            + ", but please remember to change it again from within the application once you have successfully logged in. To do this, go to your User Profile and click on 'Edit Profile'"
                            + " button directly underneath your profile picture. To change your password from within the application, enter your new password in the box and click 'Save'.\n\nYour "
                            + "temporary password is:\t" + verification_code + "\n\nIf you did not request to reset your UnstuckME account password or you are experiencing other problems, please "
                            + "reply to this email and we will attempt to solve your issue.";
                        email.Priority = MailPriority.Normal;
                        break;
                    }
            }

            SmtpClient client = new SmtpClient()
            {
                Credentials = new NetworkCredential(mailSettings.Network.UserName, mailSettings.Network.Password),
                DeliveryFormat = mailSettings.DeliveryFormat,
                DeliveryMethod = mailSettings.DeliveryMethod,
                EnableSsl = mailSettings.Network.EnableSsl,
                Timeout = 300000    //milliseconds = 300 seconds = 5 minutes
            };

            try
			{
				client.Send(email);		//send the email
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				throw ex;					  //throw the error back to the client
			}
			finally
			{
				email.Dispose();    //clean up memory
				client.Dispose();
			}

			return verification_code;
		}

		/// <summary>
		/// Generates a random 8-character verification code for a user to activate their account.
		/// </summary>
		/// <returns>A randomly generated 8-character code.</returns>
		private string GenerateVerificationCode()
		{
			string value = string.Empty;

			try
			{
				using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
				{
					byte[] tokenData = new byte[128];
					rng.GetBytes(tokenData);

					for (int i = 0, bytes_skipped = 0; i < tokenData.Length && value.Length < 8; i++)
					{
						byte temp = tokenData[i + bytes_skipped];
						while ((tokenData[i + bytes_skipped] <= 48 || tokenData[i + bytes_skipped] >= 57) &&
								(tokenData[i + bytes_skipped] <= 65 || tokenData[i + bytes_skipped] >= 90) &&
								(tokenData[i + bytes_skipped] <= 97 || tokenData[i + bytes_skipped] >= 122))
						{
							bytes_skipped++;
						}

						value += Convert.ToChar(tokenData[i + bytes_skipped]);
					}
				}
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(ex.Message);
				Console.ResetColor();
			}

			return value;
		}

        /// <summary>
        /// Creates a new mentor organzation on the database.
        /// </summary>
        /// <param name="name">The name of the new organization.</param>
        public void CreateMentorOrg(string name)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.CreateMentorOrganization(name);
            }
        }

        public bool AddClass(DBClass newClass)
        {
            bool addedClassSucessfully = true;
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                int callReturned = db.CreateNewClass(newClass.CourseName, newClass.CourseCode, newClass.CourseNumber);
                if(callReturned == 0)
                {
                    addedClassSucessfully = false;
                }
            }

            return addedClassSucessfully;
        }

        #endregion

        #region ServerGUI Functions
        /// <summary>
        /// Deprecated.
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
						onlineAdmin.Value.connection = establishedUserConnection;
						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.WriteLine("Server Admin Re-Login: {0} at {1}", onlineAdmin.Value.Admin.EmailAddress, System.DateTime.Now);
						Console.ResetColor();
					}
				}

				if (!oldConnection)
				{
					ConnectedServerAdmin newAdmin = new ConnectedServerAdmin();
					newAdmin.connection = establishedUserConnection;
					newAdmin.Admin = LoggingInAdmin;
					_connectedServerAdmins.TryAdd(newAdmin.Admin.ServerAdminID, newAdmin);
					Console.ForegroundColor = ConsoleColor.Cyan;
					Console.WriteLine("Server Admin Login: {0} at {1}", newAdmin.Admin.EmailAddress, System.DateTime.Now);
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
				Console.WriteLine("Server Admin Logoff: {0} at {1}", removedAdmin.Admin.EmailAddress, System.DateTime.Now);
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
			Console.WriteLine("Message: {0} Sent by: {1} at {2}", message, currentAdmin.Admin.EmailAddress, System.DateTime.Now);
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
			{
				userList.Add(user.Value.User);
			}
			return userList;
		}

        /// <summary>
        /// Sends a message to all users who are online.
        /// </summary>
        /// <param name="recipients">The recipients of the </param>
        /// <param name="message"></param>
        public void AdminSendMessageToUsers(List<string> recipients, string message)
        {
            if (recipients.Count == 0)
            {
                try
                {
                    foreach (var client in _connectedClients)
                        client.Value.connection.GetMessageFromServer(message);
                }
                catch (Exception)
                { }
            }
            else
            {
                try
                {
                    for (int i = 0; i < recipients.Count; i++)
                    {
                        var client = _connectedClients.First();

                        for (int j = 0; j < _connectedClients.Count; j++)
                        {
                            if (client.Value.User.EmailAddress == recipients[i])
                                client.Value.connection.GetMessageFromServer(message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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
				{
					client.Value.connection.ForceClose();
				}
			}
			catch (Exception)
			{ }
		}

		/// <summary>
		/// Registers a new tutoring organization. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="organizationName">The name of the new tutoring organization.</param>
		/// <returns>The unique identifer of the newly created organzation if successful, -1 if unsuccessful.</returns>
		public int AdminCreateMentoringOrganization(string organizationName)
		{
			try
			{
				int retVal = -1;
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					retVal = db.CreateMentorOrganization(organizationName);
				}

				return retVal;
			}
			catch (Exception)
			{
				return -1; //If Failure to create organization
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
			try
			{
				int retVal = -1;
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					retVal = db.CreateNewClass(courseName, courseCode, (short)courseNumber);
				}

				return retVal;
			}
			catch (Exception)
			{
				return -1; //If Failure to create class
			}
		}

		/// <summary>
		/// Remove a class from the database. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="classID">The unique identifier of the class to be removed.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
		public int AdminDeleteClass(int classID)
		{
			try
			{
				int retVal = -1;
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					retVal = db.DeleteClassByClassID(classID);
				}

				return retVal;
			}
			catch (Exception)
			{
				return -1; //If Failure to delete class
			}
		}

		/// <summary>
		/// Removes a tutoring organization from the database. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="organizationID">The unique identifier of the organization to be removed.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
		public int AdminDeleteMentoringOrganization(int organizationID)
		{
			try
			{
				int retVal = -1;
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					retVal = db.DeleteMentorOrganizationByMentorID(organizationID);
				}

				return retVal;
			}
			catch (Exception)
			{
				return -1; //If Failure to delete organization
			}
		}

		/// <summary>
		/// Removes a report from the database. Can only be invoked by an administrator.
		/// </summary>
		/// <param name="reportID">The unique identifier of the report to be removed.</param>
		/// <returns>Returns 0 if sucessful, -1 if unsuccessful.</returns>
		public int AdminDeleteReport(int reportID)
		{
			try
			{
				int retVal = -1;
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					retVal = db.DeleteReportByReportID(reportID);
				}

				return retVal;
			}
			catch (Exception)
			{
				return -1; //If Failure to delete report
			}
		}

		/// <summary>
		/// Not implemented. Do not use.
		/// </summary>
		/// <returns>Returns a file.</returns>
		public UnstuckMEFile UploadDocument()
		{
			UnstuckMEFile file = new UnstuckMEFile();
			file.Content = System.IO.File.ReadAllBytes(@"C:\Data\Introduction to WCF.ppt");
			file.Name = "Introduction to WCF.ppt";

			return file;
		}
        #endregion
    }
}