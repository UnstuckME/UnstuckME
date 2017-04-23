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
        private static ConcurrentDictionary<int, DateTime> _ActiveStickers;
		private static ConcurrentQueue<UnstuckMEBigSticker> _StickerList;
		private static ConcurrentQueue<UnstuckMEMessage> _MessageList;
		private static ConcurrentQueue<int> _ReviewList = new ConcurrentQueue<int>();
		
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
        /// Gets all active stickers and puts them in a list upon server startup, and infinitely checks if
        /// any of the stickers have timed out without being accepted. If a sticker has timed out, then it
        /// checks for all online users who are eligible to view that sticker and removes it from their
        /// client interface.
        /// </summary>
        public void CheckForTimedOutStickers()
        {
            _ActiveStickers = new ConcurrentDictionary<int, DateTime>();

            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var activestickers = from s in db.Stickers
                                     where s.Timeout > DateTime.Now && s.TutorID == null
                                     select new { s.StickerID, s.Timeout };

                foreach (var sticker in activestickers)
                    _ActiveStickers.TryAdd(sticker.StickerID, sticker.Timeout);

                while (true)
                {
                    foreach (KeyValuePair<int, DateTime> sticker in _ActiveStickers)
                    {
                        if (sticker.Value <= DateTime.Now)
                        {
                            var tutors = db.GetUsersThatCanTutorASticker(sticker.Key);

                            foreach (var tutor in tutors)
                            {
                                if (_connectedClients.ContainsKey(tutor.Value))
                                {
                                    DateTime time = sticker.Value;
                                    _ActiveStickers.TryRemove(sticker.Key, out time);
                                    _connectedClients[tutor.Value].connection.RemoveGUISticker(sticker.Key);
                                }
                            }
                        }
                    }
                }
            }
        }

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
			while (true)
			{
				if (_StickerList.Count != 0)
				{
					UnstuckMEBigSticker temp;
					_StickerList.TryDequeue(out temp);
					await Task.Factory.StartNew(() => SendStickerToClients(temp));
                    DateTime timeout = temp.Timeout;
                    _ActiveStickers.TryRemove(temp.StickerID, out timeout);
				}
			}

		}

		/// <summary>
		/// Checks for online users that match the criteria specified with the sticker and sends it to those clients.
		/// </summary>
		/// <param name="inSticker">The sticker to be sent to qualified online users.</param>
		public void SendStickerToClients(UnstuckMEBigSticker inSticker)
		{
			UnstuckMEAvailableSticker s = new UnstuckMEAvailableSticker()
			{
				ClassID = inSticker.Class.ClassID,
				ProblemDescription = inSticker.ProblemDescription,
				StudentID = inSticker.StudentID,
				StickerID = inSticker.StickerID,
				Timeout = inSticker.Timeout,
				CourseCode = inSticker.Class.CourseCode,
				CourseName = inSticker.Class.CourseName,
				CourseNumber = inSticker.Class.CourseNumber,
				StudentRanking = inSticker.StudentRanking
			};

			try
			{
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					var tutors = db.GetUsersThatCanTutorASticker(s.StickerID);

					foreach (var tutor in tutors)
					{
						if (_connectedClients.ContainsKey(tutor.Value))
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
		/// When logging on, checks for any stickers that need reviews. This will only occur if the other member of the
		/// sticker has submitted a review and this user was not online when this occured.
		/// </summary>
		/// <param name="userID">The unique identifier of the user who is checking for reviews.</param>
		/// <returns>Contains the StickerID and true if they are the student, false if they are the tutor. If there is no sticker, returns 0 for the sticker ID.</returns>
		public KeyValuePair<int, bool> CheckForReviews(int userID)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				if (_ReviewList.Count != 0)
				{
					foreach (int Review_StickerID in _ReviewList)
					{
						var reviews = (from s in db.Stickers
									   join r in db.Reviews on s.StickerID equals r.StickerID
									   where s.StickerID == Review_StickerID
									   select new { s.StickerID, s.TutorID, s.StudentID, r.ReviewerID });

						foreach (var rev in reviews)
						{
							if (userID == rev.TutorID && userID != rev.ReviewerID)
							{
								int temp = rev.StickerID;
								_ReviewList.TryDequeue(out temp);
								return new KeyValuePair<int, bool>(rev.StickerID, false);
							}
							else if (userID == rev.StudentID && userID != rev.ReviewerID)
							{
								int temp = rev.StickerID;
								_ReviewList.TryDequeue(out temp);
								return new KeyValuePair<int, bool>(rev.StickerID, true);
							}
						}
					}
				}
			}

			return new KeyValuePair<int, bool>(0, false);
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
						newClient.connection = establishedUserConnection;
						newClient.User = user;
						newClient.returnAddress = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
						_connectedClients.TryAdd(newClient.User.UserID, newClient);

						//Login Success, Print to console window.
						Console.ForegroundColor = ConsoleColor.Green;
						Console.WriteLine("Client Login: {0} at {1}", newClient.User.EmailAddress, System.DateTime.Now);
						Console.ResetColor();

						foreach (var admin in _connectedServerAdmins)
							admin.Value.connection.GetUpdate(0, newClient.User);
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
						UserClass temp = new UserClass()
						{
							ClassID = c.ClassID,
							CourseCode = c.CourseCode,
							CourseName = c.CourseName,
							CourseNumber = c.CourseNumber
						};
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
					var user = GetUserInfo(null, emailAddress);
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
				Console.WriteLine("Client Loggoff: {0} at {1}", removedClient.User.EmailAddress, DateTime.Now);
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
					UnstuckMESticker usSticker = new UnstuckMESticker()
					{
						StickerID = sticker.StickerID,
						ClassID = sticker.ClassID,
						StudentID = sticker.StudentID,
						ProblemDescription = sticker.ProblemDescription,
						MinimumStarRanking = (float)sticker.MinimumStarRanking,
						SubmitTime = sticker.SubmitTime,
						Timeout = sticker.Timeout// - DateTime.Now).TotalSeconds;
					};
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
					UnstuckMESticker usSticker = new UnstuckMESticker()
					{
						StickerID = sticker.StickerID,
						ClassID = sticker.ClassID,
						StudentID = sticker.StudentID,
						ProblemDescription = sticker.ProblemDescription,
						MinimumStarRanking = (float)sticker.MinimumStarRanking,
						SubmitTime = sticker.SubmitTime,
						Timeout = sticker.Timeout// - DateTime.Now).TotalSeconds;
					};
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
					UnstuckMESticker usSticker = new UnstuckMESticker()
					{
						StickerID = sticker.StickerID,
						ProblemDescription = sticker.ProblemDescription,
						ClassID = sticker.ClassID,
						StudentID = sticker.StudentID,
						TutorID = sticker.TutorID ?? 1,
						MinimumStarRanking = (sticker.MinimumStarRanking.HasValue) ? (float)sticker.MinimumStarRanking : 0,
						SubmitTime = sticker.SubmitTime,
						Timeout = sticker.Timeout// - DateTime.Now).TotalSeconds;
					};
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
					UnstuckMESticker usSticker = new UnstuckMESticker()
					{
						StickerID = sticker.StickerID,
						ProblemDescription = sticker.ProblemDescription,
						ClassID = sticker.ClassID,
						StudentID = sticker.StudentID,
						TutorID = sticker.TutorID ?? 1,
						MinimumStarRanking = (sticker.MinimumStarRanking.HasValue) ? (float)sticker.MinimumStarRanking : 0,
						SubmitTime = sticker.SubmitTime,
						Timeout = sticker.Timeout// - DateTime.Now).TotalSeconds;
					};
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
		public List<UnstuckMEAvailableSticker> GetActiveStickers(int caller, Nullable<int> organizationID = null, float minstarrank = 0, Nullable<int> userID = null, Nullable<int> classID = null)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				var userStickers = db.GetActiveStickers(caller, organizationID, minstarrank, userID, classID);

				List<UnstuckMEAvailableSticker> stickerList = new List<UnstuckMEAvailableSticker>();

				foreach (var sticker in userStickers)
				{
					UnstuckMEAvailableSticker usSticker = new UnstuckMEAvailableSticker()
					{
						StickerID = (int)sticker.StickerID,
						ProblemDescription = sticker.ProblemDescription,
						ClassID = (int)sticker.ClassID,
						CourseCode = sticker.CourseCode,
						CourseName = sticker.CourseName,
						CourseNumber = (short)sticker.CourseNumber,
						StudentID = (int)sticker.StudentID,
						//TutorID = sticker.TutorID ?? new int?(),
						StudentRanking = (sticker.MinimumStarRanking.HasValue) ? (double)sticker.MinimumStarRanking : 0,
						//SubmitTime = (DateTime)sticker.SubmitTime,
						Timeout = (DateTime)sticker.Timeout
					};
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
						throw new Exception("Create Sticker Failed, Returned sticker ID = 0");

                    retstickerID = stickerID.Value;
					
					if (newSticker.AttachedOrganizations.Count != 0)
					{
						foreach (int orgID in newSticker.AttachedOrganizations)
							db.AddOrgToSticker(retstickerID, orgID);
					}
					newSticker.StickerID = retstickerID;
					_StickerList.Enqueue(newSticker);
                    _ActiveStickers.TryAdd(newSticker.StickerID, newSticker.Timeout);
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
		/// Currently retrieves the data of the profile picture from the database. Set up to retrieve the filepath of the picture from the database,
		/// opens the file, and converts it into a stream and returns it.
		/// </summary>
		/// <param name="userID">The unique identifier of the user.</param>
		/// <returns>A Stream object containing the data of the image file.</returns>
		public Stream GetProfilePicture(int userID)
		{
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                //string directory = db.GetProfilePicture(userID).First();

                //using (FileStream file = new FileStream(directory, FileMode.Open, FileAccess.Read))
                {
                    //MemoryStream ms = new MemoryStream();
                    //file.CopyTo(ms);
                    //ms.Position = 0L;

                    //return ms;
                    /*****************************************************************************************************
                     * Remove the bottom lines and uncomment the above lines once filepath is implemented on the database.
                    *****************************************************************************************************/
                    byte[] imgByte;
                    imgByte = db.GetProfilePicture(userID).First();

                    return new MemoryStream(imgByte);
                }
            }
		}

		/// <summary>
		/// Overwrites the profile picture data of a specific user on the database.
		/// </summary>
		/// <param name="image">A custom stream that contains the data of the image file and the information of the requesting user.</param>
		public void SetProfilePicture(UnstuckMEStream image)
		{
			string directory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData, Environment.SpecialFolderOption.Create) + @"\UnstuckME\";
			Directory.CreateDirectory(directory += image.User.UserID.ToString());
			Directory.CreateDirectory(directory);
			directory += @"\ProfilePicture.jpeg";

			using (FileStream newfile = new FileStream(directory, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite, 26214400, FileOptions.Encrypted))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					image.CopyTo(ms);   //write to a MemoryStream to easily convert to a byte array
					ms.Position = 0L;   //reset the position of the memorystream to write it to a different stream
					ms.CopyTo(newfile); //write to local filesystem
					ms.Position = 0L;

					/****************************************************************************************************************
					* When using remote server comment MemoryStream section out and swap out the UpdateProfilePicture function calls
					****************************************************************************************************************/
					using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
					{
						//db.UpdateProfilePicture(image.User.UserID, directory);
						db.UpdateProfilePicture(image.User.UserID, ms.ToArray()); //replace with line above once filepath is implemented on database
					}
				}
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
		/// Submits a review to the database. Finds the other user associated with the sticker and makes them submit
		/// a review if they are online, otherwise adds it to the _ReviewList queue so they can submit it when they
		/// next log on.
		/// </summary>
		/// <param name="stickerID">The unique identifier of the sticker associated with the review.</param>
		/// <param name="reviewerID">The unique identifier of the user submitting the review.</param>
		/// <param name="starRanking">The rating given to the user being reviewed.</param>
		/// <param name="description">The description of the review.</param>
		/// <param name="isAStudent">True if the user being reviewed is a student, false otherwise.</param>
		/// <returns>Returns 0 if the review was created successfully, 1 if unsuccessful.</returns>
		public int CreateReview(int stickerID, int reviewerID, double starRanking, string description, bool isAStudent)
		{
			try
			{
				Nullable<int> retVal = 0;
				Nullable<int> reviewedID = 0;

				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					retVal = db.CreateReview(stickerID, reviewerID, starRanking, description).First();

					if (retVal.HasValue && retVal.Value != -1)
					{
						if (isAStudent)// the person being reviewed is a student
						{
							reviewedID = (from I in db.Stickers
										  where I.TutorID == reviewerID
										  select I.StudentID).First();

							db.AddStudentStarRankToUser(reviewedID.Value, starRanking);
						}
						else // the person being reviewed is a tutor
						{
							reviewedID = (from I in db.Stickers
										  where I.StudentID == reviewerID
										  select I.TutorID).First();

							db.AddTutorStarRankToUser(reviewedID.Value, starRanking);
						}

						var Reviews = from r in db.Reviews
									  where r.StickerID == stickerID
									  select r.StickerID;

						if (Reviews.Count() <= 1)
						{
							bool found = false;
							foreach (var client in _connectedClients)
							{
								if (client.Key == reviewedID.Value)
								{
									if (isAStudent)
										client.Value.connection.CreateReviewAsTutor(stickerID);
									else
										client.Value.connection.CreateReviewAsStudent(stickerID);

									found = true;   //other sticker member is online
									break;
								}
							}

							if (!found)     //other sticker member is not online
								_ReviewList.Enqueue(stickerID);     //add sticker to queue so they can submit a review when they log on next
						}
						else
							db.MarkStickerAsResolved(stickerID);    //all reviews have been submitted, mark as resolved
					}
				}

				return retVal.Value;
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
					Organization new_org = new Organization()
					{
						MentorID = org.MentorID,
						OrganizationName = org.OrganizationName
					};
					orgs.Add(new_org);
				}
				//organizations.Dispose();		//need this to release memory   
				return orgs;
			}
		}

        /// <summary> 
        /// Gets the number of messages a single chat holds 
        /// </summary> 
        /// <param name="chatID">The unique identifier of a specific chat.</param> 
        /// <returns>A integer continuing the number of messages for the provided chat.</returns> 
        public int GetChatMsgCount(int chatID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {

            }

            return -1;
        }

        /// <summary>
        /// Gets unique identifiers of all the chats a user is associated with.
        /// </summary>
        /// <param name="userID">The unique identifier of a specific user.</param>
        /// <returns>A list of chats, each containing the unique identifier of that chat.</returns>
        public List<UnstuckMEChat> GetChatIDs(int userID)
		{
			try
			{
				List<UnstuckMEChat> chatIDList = new List<UnstuckMEChat>();
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					var dbChats = db.GetAllChatsAUserIsPartOF(userID);

					foreach (var chatItem in dbChats)
					{
						UnstuckMEChat temp = new UnstuckMEChat()
						{
							ChatID = chatItem.Value
						};

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
						UnstuckMEChatUser temp = new UnstuckMEChatUser()
						{
							UserID = member.UserID,
							UserName = member.DisplayFName,
							ProfilePicture = null
						};
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
						UnstuckMEMessage temp = new UnstuckMEMessage()
						{
							Message = message.MessageData,
							MessageID = message.MessageID,
							Time = message.SentTime,
							FilePath = message.FilePath,
							ChatID = chatID,
							SenderID = message.SentBy,
							Username = message.DisplayFName
						};
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
		/// <returns>The message ID of the message that was just inserted into the database.</returns>
		public int SendMessage(UnstuckMEMessage message)
		{
			try
			{
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					var messageID = db.InsertMessage(message.ChatID, message.Message, null, message.SenderID);
					message.MessageID = messageID.First().Value;
					_MessageList.Enqueue(message);

					return message.MessageID;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}
		}

		//public void UploadFile(UnstuckMEMessage message, UnstuckMEFile file)
		//{
		//	try
		//	{
		//		using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
		//		{
		//			//db.InsertMessage(message.ChatID, message.Message, message.FilePath, message.IsFile, message.SenderID);
		//			//user uploads a file contained in a message
		//			//server receives message
		//			//server saves bytes locally and updates filepath
		//			//server updates database with new message
		//			//server sends message to users in the chat

		//		}

		//		foreach (int client in message.UsersInConvo)
		//		{
		//			if (client != message.SenderID && _connectedClients.ContainsKey(client))
		//				_connectedClients[client].connection.GetFile(message, file);
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		Console.WriteLine(ex.Message);
		//	}
		//}

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
						UnstuckMEAvailableSticker temp = new UnstuckMEAvailableSticker()
						{
							ClassID = sticker.ClassID.Value,
							CourseCode = sticker.CourseCode,
							CourseName = sticker.CourseName,
							CourseNumber = sticker.CourseNumber.Value,
							ProblemDescription = sticker.ProblemDescription,
							StickerID = sticker.StickerID.Value,
							StudentID = sticker.StudentID.Value,
							StudentRanking = sticker.StudentRanking.Value,
							Timeout = sticker.Timeout.Value
						};
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
                List<UnstuckMEAvailableSticker> newstickers = GetActiveStickers(userID, classID: inClass);

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
                        foreach (UnstuckMEAvailableSticker sticker in newstickers)
                            client.Value.connection.RecieveNewSticker(sticker);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		/// <summary>
		/// Invoked when a tutor accepts a sticker. Updates the TutorID associated with that sticker and
		/// removes that sticker from the list of available stickers for clients who can see it.
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
                    DateTime time;
                    _ActiveStickers.TryRemove(stickerID, out time);
					var tutors = db.GetUsersThatCanTutorASticker(stickerID);

					foreach (var client in _connectedClients)
					{
						if (client.Key != tutorID && tutors.Contains(client.Key))
						{
							client.Value.connection.RemoveGUISticker(stickerID);
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
						UnstuckMEChatUser temp = new UnstuckMEChatUser()
						{
							ProfilePicture = null,
							UserName = friend.DisplayFName,
							UserID = friend.FriendUserID
						};
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
		/// Creates a new mentor organzation on the database.4
		/// </summary>
		/// <param name="name">The name of the new organization.</param>
		public void CreateMentorOrg(string name)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				db.CreateMentorOrganization(name);
			}
		}

		/// <summary>
		/// Adds a class to the database if it does not already exist.
		/// </summary>
		/// <param name="newClass">The class to add to the database. Should contain the CourseCode, CourseName, and CourseNumber.</param>
		/// <returns>Returns true if successful, false if not.</returns>
		public bool AddClass(UserClass newClass)
		{
			bool addedClassSucessfully = true;
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				int callReturned = db.CreateNewClass(newClass.CourseName, newClass.CourseCode, newClass.CourseNumber);

				if(callReturned == 0)
					addedClassSucessfully = false;
			}

			return addedClassSucessfully;
		}

		/// <summary>
		/// Updates a chat message if a user in the conversation has edited it.
		/// </summary>
		/// <param name="message">The message that has been edited.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
		public int EditMessage(UnstuckMEMessage message)
		{
			try
			{
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					foreach (var client in _connectedClients)
					{
						if (message.SenderID != client.Key && message.UsersInConvo.Contains(client.Key))
							client.Value.connection.UpdateChatMessage(message);
					}

					db.UpdateMessageByMessageID(message.MessageID, message.Message);
					return 0;
				}
			}
			catch (Exception)
			{
				return -1;
			}
		}

		/// <summary>
		/// Deletes a message. Is broadcasted to the other online users in the chat once it is deleted.
		/// </summary>
		/// <param name="message">The message to be deleted.</param>
		/// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
		public int DeleteMessage(UnstuckMEMessage message)
		{
			try
			{
				using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
				{
					foreach (var client in _connectedClients)
					{
						if (message.SenderID != client.Key && message.UsersInConvo.Contains(client.Key))
							client.Value.connection.DeleteChatMessage(message);
					}

					db.DeleteMessageByMessageID(message.MessageID);
					return 0;
				}
			}
			catch (Exception)
			{
				return -1;
			}
		}

        /// <summary>
        /// Deletes a sticker from the database and updates the client interfaces of tutors who are eligible to
        /// see that sticker. This should only be done if the sticker does not already have a tutor.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to delete.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int DeleteSticker(int stickerID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.DeleteStickerByStickerID(stickerID);
                    var tutors = db.GetUsersThatCanTutorASticker(stickerID);

                    foreach (var tutor in tutors)
                    {
                        if (_connectedClients.ContainsKey(tutor.Value))
                            _connectedClients[tutor.Value].connection.RemoveGUISticker(stickerID);
                    }

                    retVal = 0;
                }
            }
            catch (Exception)
            { }

            return retVal;
        }

        /// <summary>
        /// Removes the tutor associated with the sticker given by <paramref name="stickerID"/> and sends it out
        /// to online tutors who are eligible to see it.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to be relabeled as active.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int RemoveTutorFromSticker(int stickerID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.UpdateTutorIDByTutorIDAndStickerID(null, stickerID); //removes the tutor from the sticker
                    var sticker = (from s in db.Stickers
                                   where s.StickerID == stickerID
                                   select new { s.Class, s.StudentID, s.ProblemDescription, s.MinimumStarRanking, s.ChatID, s.TutorID, s.SubmitTime, s.Timeout }).First();

                    //not sure if this will work
                    UnstuckMEBigSticker bigsticker = new UnstuckMEBigSticker()
                    {
                        StickerID = stickerID,
                        ProblemDescription = sticker.ProblemDescription,
                        StudentID = sticker.StudentID,
                        ChatID = sticker.ChatID.Value,
                        TutorID = sticker.TutorID.Value,
                        MinimumStarRanking = sticker.MinimumStarRanking.Value,
                        Class = new UserClass()
                        {
                            ClassID = sticker.Class.ClassID,
                            CourseCode = sticker.Class.CourseCode,
                            CourseName = sticker.Class.CourseName,
                            CourseNumber = sticker.Class.CourseNumber
                        },
                        SubmitTime = sticker.SubmitTime,
                        Timeout = sticker.Timeout
                    };

                    _StickerList.Enqueue(bigsticker);
                    _ActiveStickers.TryAdd(bigsticker.StickerID, bigsticker.Timeout);

                    retVal = 0;
                }
            }
            catch (Exception)
            { }

            return retVal;
        }

		#endregion

		#region ServerGUI Functions
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
						onlineAdmin.Value.connection = establishedUserConnection;
						Console.ForegroundColor = ConsoleColor.Cyan;
						Console.WriteLine("Server Admin Re-Login: {0} at {1}", onlineAdmin.Value.Admin.EmailAddress, DateTime.Now);
						Console.ResetColor();
					}
				}

				if (!oldConnection)
				{
					ConnectedServerAdmin newAdmin = new ConnectedServerAdmin()
					{
						connection = establishedUserConnection,
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
			UnstuckMEFile file = new UnstuckMEFile()
			{
				Content = System.IO.File.ReadAllBytes(@"C:\Data\Introduction to WCF.ppt"),
				Name = "Introduction to WCF.ppt"
			};
			return file;
		}
		#endregion
	}
}