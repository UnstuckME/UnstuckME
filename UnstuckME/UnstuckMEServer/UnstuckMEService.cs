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
using System.Data.Objects;
using System.Drawing;
using System.Windows.Media;
using System.Threading;
using System.Net;
using System.ServiceModel.Channels;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

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

        //This function is for testing stored procedures. In program.cs replace:
        //Thread userStatusCheck = new Thread(_server.CheckStatus); with Thread userStatusCheck = new Thread(_server.SPTest); 
        public void SPTest()
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                try
                {
                    //db.ClearReviewDescriptionByReviewID(7);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void CheckUserStatus()
        {
            List<int> offlineUsers = new List<int>();
            List<Task<PingReply>> pingTasks = new List<Task<PingReply>>();
            int count = 1;
            try
            {
                while (true)
                {
                    Console.WriteLine("Loop: {0}", count);
                    count++;
                    foreach (var address in _connectedClients)
                    {
                        pingTasks.Add(PingAsync(address.Value.returnAddress.Address));
                    }
                    //Wait for all the tasks to complete
                    Task.WaitAll(pingTasks.ToArray());

                    //Now you can iterate over your list of pingTasks
                    foreach (var pingTask in pingTasks)
                    {
                        Console.WriteLine("PingTask Foreach");
                        if (pingTask.Result.Status != IPStatus.Success)
                        {
                            foreach (KeyValuePair<int, ConnectedClient> client in _connectedClients)
                            {
                                if (client.Value.returnAddress.Address == pingTask.Result.Address.ToString())
                                {
                                    offlineUsers.Add(client.Key);
                                }
                            }
                        }
                    }
                    foreach (int user in offlineUsers)
                    {
                        ConnectedClient removedClient = new ConnectedClient();
                        _connectedClients.TryRemove(user, out removedClient);
                        Console.WriteLine(removedClient.User.EmailAddress + " did not respond to a ping from the server. They are now considered offline");
                    }
                    offlineUsers.Clear();
                    pingTasks.Clear();
                    Thread.Sleep(10000);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static Task<PingReply> PingAsync(string address)
        {
            var tcs = new TaskCompletionSource<PingReply>();
            Ping ping = new Ping();
            ping.PingCompleted += (obj, sender) =>
            {
                tcs.SetResult(sender.Reply);
            };
            ping.SendAsync(address, 5000, new object());
            return tcs.Task;
        }

        public void CheckStatus()
        {
            bool isUserOnline = false;
            while (true)
            { 
                RestartUserPing: 
                foreach (var client in _connectedClients)
                {
                    try
                    {
                        isUserOnline = client.Value.connection.isOnline();
                    }
                    catch(Exception)
                    {
                        try
                        {
                            ConnectedClient removedClient;
                            _connectedClients.TryRemove(client.Key, out removedClient);
                            Console.WriteLine("User: {0} did not respond and is now logged off.", removedClient.User.EmailAddress);
                            foreach (var admin in _connectedServerAdmins)
                            {
                                admin.Value.connection.GetUpdate(1, removedClient.User.EmailAddress);
                            }
                            removedClient.connection.ForceClose("You have been logged out by the server. Please Re-Login to continue using UnstuckME"); //Incase of faulty ping and user is still connected.
                        }
                        catch(Exception)
                        { }
                        goto RestartUserPing; //Prevents foreach loop from breaking server.
                    }
                }
                Thread.Sleep(5000);

                RestartAdminPing:
                foreach (var admin in _connectedServerAdmins)
                {
                    try
                    {
                        isUserOnline = admin.Value.connection.isOnline();
                    }
                    catch(Exception)
                    {
                        ConnectedServerAdmin removedAdmin;
                        _connectedServerAdmins.TryRemove(admin.Key, out removedAdmin);
                        Console.WriteLine("Admin: {0} did not respond and is now logged off.", removedAdmin.Admin.EmailAddress);
                        goto RestartAdminPing; //Prevents foreach loop from breaking server.
                    }
                }
                Thread.Sleep(5000);
            }
        }

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
                        newClient.returnAddress = OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
                        _connectedClients.TryAdd(newClient.User.UserID, newClient);
                        //Login Success, Print to console window.
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Client Login: {0} at {1}", newClient.User.EmailAddress, System.DateTime.Now);
                        Console.ResetColor();
                        foreach (var admin in _connectedServerAdmins)
                        {
                            admin.Value.connection.GetUpdate(0, newClient.User.EmailAddress);
                        }
                    }
                }
                catch (Exception)
                {
                    loginAttempt = false;
                }
            }
            return loginAttempt;
        }

        public List<UserClass> GetUserClasses(int UserID)
        {
            try
            {
                List<UserClass> Rlist = new List<UserClass>();

                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var classes = db.GetUserClasses(UserID);
                    //This might work, if not let me know and i'll figure out something else.
                    foreach (var c in classes)
                    {
                        UserClass temp = new UserClass();
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
                    Console.WriteLine(emailAddress + ": is not a valid Username.");
                    return false;
                }
            }
        }

        public void RegisterServerAdmin(AdminInfo LoggingInAdmin)
        {
            try
            {
                //Stores Server Admin into Logged in _ConnectedServerAdmins List
                IServer establishedUserConnection = OperationContext.Current.GetCallbackChannel<IServer>();
                bool oldConnection = false;
                foreach(var onlineAdmin in _connectedServerAdmins)
                {
                    if(onlineAdmin.Key == LoggingInAdmin.ServerAdminID)
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
                foreach (var admin in _connectedServerAdmins)
                {
                    admin.Value.connection.GetUpdate(1, removedClient.User.EmailAddress);
                }
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

        public void ChangePassword(UserInfo User, string newPassword)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                UnstuckMEPassword newHashedPassword = UnstuckMEHashing.GetHashedPassword(newPassword);
                var users = (from u in db.UserProfiles
                             where u.EmailAddress == User.EmailAddress
                             select u).First();
                users.UserPassword = newHashedPassword.Password;
                users.Salt = newHashedPassword.Salt;
                db.SaveChanges();
            }
        }

        public void DeleteUserAccount(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.DeleteUserProfileByUserID(userID);
            }
        }

        public List<UnstuckMEReview> GetUserStudentReviews(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var studentReviews = from u in db.Reviews
                                     join j in db.Stickers on u.StickerID equals j.StickerID
                                     where u.ReviewerID == userID && j.StudentID == userID
                                     select new { Review = u };

                List<UnstuckMEReview> studentReviewList = new List<UnstuckMEReview>();
                UnstuckMEReview usReview = new UnstuckMEReview();
                foreach (var review in studentReviews)
                {
                    usReview.ReviewID = review.Review.ReviewID;
                    usReview.StickerID = review.Review.StickerID;
                    usReview.ReviewerID = review.Review.ReviewerID;
                    usReview.StarRanking = (float)review.Review.StarRanking;
                    usReview.Description = review.Review.Description;
                    studentReviewList.Add(usReview);
                }

                return studentReviewList;
            }
        }

        public List<UnstuckMEReview> GetUserTutorReviews(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var tutorReviews = from u in db.Reviews
                                     join j in db.Stickers on u.StickerID equals j.StickerID
                                     where u.ReviewerID == userID && j.TutorID == userID
                                     select new { Review = u };

                List<UnstuckMEReview> tutorReviewList = new List<UnstuckMEReview>();
                UnstuckMEReview usReview = new UnstuckMEReview();
                foreach (var review in tutorReviews)
                {
                    usReview.ReviewID = review.Review.ReviewID;
                    usReview.StickerID = review.Review.StickerID;
                    usReview.ReviewerID = review.Review.ReviewerID;
                    usReview.StarRanking = (review.Review.StarRanking.HasValue) ? (float)review.Review.StarRanking.Value : 0;
                    usReview.Description = review.Review.Description;
                    tutorReviewList.Add(usReview);
                }
                return tutorReviewList;
            }
        }

        public List<UnstuckMESticker> GetUserSubmittedStickers(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var userStickers = from u in db.Stickers
                                  where u.StudentID == userID
                                  select new { Sticker = u };
                
                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
                UnstuckMESticker usSticker = new UnstuckMESticker();

                foreach (var sticker in userStickers)
                {
                    usSticker.StickerID = sticker.Sticker.StickerID;
                    usSticker.ProblemDescription = sticker.Sticker.ProblemDescription;
                    usSticker.ClassID = sticker.Sticker.ClassID;
                    usSticker.StudentID = sticker.Sticker.StudentID;
                    usSticker.TutorID =  (sticker.Sticker.TutorID.HasValue) ? sticker.Sticker.TutorID.Value : 1;
                    usSticker.MinimumStarRanking = (sticker.Sticker.MinimumStarRanking.HasValue) ? (float)sticker.Sticker.MinimumStarRanking : 0;
                    usSticker.SubmitTime = sticker.Sticker.SubmitTime;
                    usSticker.Timeout = sticker.Sticker.Timeout;
                    stickerList.Add(usSticker);
                }
                return stickerList;
            }
        }

        public List<UnstuckMESticker> GetUserTutoredStickers(int userID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var userStickers = from u in db.Stickers
                                   where u.TutorID == userID
                                   select new { Sticker = u };

                List<UnstuckMESticker> stickerList = new List<UnstuckMESticker>();
                UnstuckMESticker usSticker = new UnstuckMESticker();

                foreach (var sticker in userStickers)
                {
                    usSticker.StickerID = sticker.Sticker.StickerID;
                    usSticker.ProblemDescription = sticker.Sticker.ProblemDescription;
                    usSticker.ClassID = sticker.Sticker.ClassID;
                    usSticker.StudentID = sticker.Sticker.StudentID;
                    usSticker.TutorID = (sticker.Sticker.TutorID.HasValue) ? sticker.Sticker.TutorID.Value : 1;
                    usSticker.MinimumStarRanking = (sticker.Sticker.MinimumStarRanking.HasValue) ? (float)sticker.Sticker.MinimumStarRanking : 0;
                    usSticker.SubmitTime = sticker.Sticker.SubmitTime;
                    usSticker.Timeout = sticker.Sticker.Timeout;
                    stickerList.Add(usSticker);
                }
                return stickerList;
            }
        }

        public void AddUserToTutoringOrganization(int userID, int organizationID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.InsertUserIntoMentorProgram(userID, organizationID);
            }
        }

        public void SubmitSticker(UnstuckMESticker newSticker)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.CreateSticker(newSticker.ProblemDescription, newSticker.ClassID, newSticker.StudentID, newSticker.MinimumStarRanking, (int)((newSticker.SubmitTime - newSticker.Timeout).TotalSeconds));
            }
        }

        public byte [] GetProfilePicture(int userID)
        {
            byte[] imgByte = null;

			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				imgByte = db.GetProfilePicture(userID).First();
			}

			return imgByte;
        }

        public void SetProfilePicture(int userID, byte[] image)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
				db.ChangeProfilePicture(userID, image);
            }
        }

		public void InsertProfilePicture(int userID, byte[] image)
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				db.InsertProfilePicture(userID, image);
			}
		}

        public List<string> GetAllOnlineUsers()
        {
            List<string> userList = new List<string>();
            foreach (var user in _connectedClients)
            {
                userList.Add(user.Value.User.EmailAddress);
            }
            return userList;
        }

        public void RemoveUserFromClass(int UserID, int ClassID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.DeleteUserFromClass(UserID, ClassID);
            }
        }

        public void ServerShuttingDown()
        {
            foreach  (var client in _connectedClients)
            {
                try
                {
                    client.Value.connection.ForceClose("Server has shutdown, Please contact your Server Administrator for more information.");
                }
                catch (Exception)
                { }
            }
        }

        public List<string> GetCourseCodes()
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var codes = from u in db.Classes
                            select new { CourseCode = u };

                List<String> rlist = new List<String>();
                List<String> rlist2 = new List<String>();
                foreach (var code in codes)
                {
                    rlist.Add(code.CourseCode.CourseCode.ToString());
                }

                IEnumerable<String> list = rlist.Distinct();
                foreach (String classcode in list)
                {
                    rlist2.Add(classcode);
                }
                return rlist2;
            }
        }

        public int GetCourseIdNumberByCodeAndNumber(string code, string number)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                int num = Convert.ToInt32(number);
                var ID = (from u in db.Classes
                         where u.CourseCode == code && u.CourseNumber == num
                         select new { ClassID = u }).First();
                return ID.ClassID.ClassID;
            }
        }

        public List<string> GetCourseNumbersByCourseCode(string CourseCode)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                var codes = from u in db.Classes
                            where u.CourseCode == CourseCode
                            select new { CourseNum = u };

                List<String> rlist = new List<String>();
                List<String> rlist2 = new List<String>();
                foreach (var code in codes)
                {
                    rlist.Add(code.CourseNum.CourseNumber.ToString());
                }

                IEnumerable<String> list = rlist.Distinct();
                foreach (String classcode in list)
                {
                    rlist2.Add(classcode);
                }
                return rlist2;
            }
        }

		public List<string> GetAllOrganizations()
		{
			using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
			{
				return db.GetAllOrganizations().ToList();
			}
		}
    }
}
