using System;
using System.Collections.Generic;
using UnstuckMEServer;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    public partial class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
    {
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
        /// Removes a user from their contacts.
        /// </summary>
        /// <param name="userID">The unique identifier of the callee.</param>
        /// <param name="fileID">The unique identifier of the user to removed from contacts.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int DeleteFriend(int userID, int friendID)
        {
            try
            {
                int retVal = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.DeleteFriend(userID, friendID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return -1; //If Failure to remove friend
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}