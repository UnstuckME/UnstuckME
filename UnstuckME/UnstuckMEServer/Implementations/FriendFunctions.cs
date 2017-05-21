using System;
using System.Collections.Generic;
using System.Linq;
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
        /// Gets the info of a user who is friends with the specified user.
        /// </summary>
        /// <param name="userID">The unique identifier of the user to get the info for.</param>
        /// <returns>An UnstuckMEChatUser with information on a specific user.</returns>
        public UnstuckMEChatUser GetFriendInfo(int userID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var friend = db.GetInfoForFriend(userID).Select(u => new UnstuckMEChatUser
                    {
                        UserID = userID,
                        UserName = u.DisplayFName,
                        EmailAddress = u.EmailAddress
                    }).First();

                    return friend;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Removes a user from their contacts.
        /// </summary>
        /// <param name="userID">The unique identifier of the callee.</param>
        /// <param name="friendID">The unique identifier of the user to removed from contacts.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public int DeleteFriend(int userID, int friendID)
        {
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.DeleteFriend(userID, friendID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return retVal; //If Failure to remove friend
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
                List<UnstuckMEChatUser> friendsList = new List<UnstuckMEChatUser>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    var dbFriends = db.GetUserFriends(userID);

                    foreach (var friend in dbFriends)
                    {
                        UnstuckMEChatUser temp = new UnstuckMEChatUser
                        {
                            ProfilePicture = null,
                            UserName = friend.DisplayFName,
                            UserID = friend.FriendUserID
                        };

                        friendsList.Add(temp);
                    }
                }
                return friendsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}