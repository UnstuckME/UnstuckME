using System;
using System.Collections.Generic;
using System.Linq;
using UnstuckMEServer;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    public partial class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
    {
        //public void LeaveConversation(int UserID, int ChatID, List<UnstuckMEChatUser> Users)
        public void LeaveConversation(int UserID, int ChatID)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                db.DeleteUserFromChat(UserID, ChatID);
            }

            //foreach (UnstuckMEChatUser user in Users)
            //{
            //    if (user.UserID != UserID && _connectedClients.ContainsKey(user.UserID))
            //    {
                    
            //    }
            //}
        }

        /// <summary>
        /// Creates a chat associated with a user.
        /// </summary>
        /// <param name="userID">The unique identifer of the callee.</param>
        /// <returns>The unique identifier of the newly created chat if successful, -1 if unsuccessful.</returns>
        public int CreateChat(int userID)
        {
            int chatID = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    int? firstOrDefault = db.CreateChat(userID).FirstOrDefault();

                    if (!firstOrDefault.HasValue)
                        return chatID;  //If Failure to create chat

                    int result = firstOrDefault.Value;
                    chatID = result;
                }

                return chatID; //On success return chatID
            }
            catch (Exception)
            {
                return chatID; //If Failure to create chat
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
            int retVal = -1;

            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    retVal = db.InsertUserIntoChat(userID, chatID);
                }

                return retVal;
            }
            catch (Exception)
            {
                return retVal; //If Failure to cinsert user into chat
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
        /// Gets the messages and users in a specific chat.
        /// </summary>
        /// <param name="chatID">The unique identifier of a specific chat.</param>
        /// <returns>An UnstuckMEChat that contains the messages and members of the specified chat.</returns>
        public UnstuckMEChat GetSingleChat(int chatID)
        {
            try
            {
                UnstuckMEChat returnedChat = new UnstuckMEChat
                {
                    ChatID = chatID,
                    Messages = GetChatMessages(chatID),
                    Users = GetChatMembers(chatID)
                };

                return returnedChat;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
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
                    using (var dbChats = db.GetAllChatsAUserIsPartOF(userID))
                    {
                        foreach (var chatItem in dbChats)
                        {
                            if (chatItem.HasValue)
                            {
                                UnstuckMEChat temp = new UnstuckMEChat
                                {
                                    ChatID = chatItem.Value
                                };

                                chatIDList.Add(temp);
                            }
                        }
                    }
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
        /// Gets the members of a chat.
        /// </summary>
        /// <param name="chatID">The unique identifier of the chat to get the members of.</param>
        /// <returns>A list of UnstuckMEChatUsers with all the information needed for chat to work.</returns>
        private List<UnstuckMEChatUser> GetChatMembers(int chatID)
        {
            try
            {
                List<UnstuckMEChatUser> userList = new List<UnstuckMEChatUser>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    using (var chatMembers = db.GetAllMembersOfAChat(chatID))
                    {
                        foreach (var member in chatMembers)
                        {
                            UnstuckMEChatUser temp = new UnstuckMEChatUser
                            {
                                UserID = member.UserID,
                                UserName = member.DisplayFName,
                                ProfilePicture = null
                            };
                            userList.Add(temp);
                        }
                    }
                }
                return userList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary> 
        /// Gets the number of messages a single chat holds. Deprecated.
        /// </summary> 
        /// <param name="chatID">The unique identifier of a specific chat.</param> 
        /// <returns>A integer continuing the number of messages for the provided chat.</returns> 
        public int GetChatMsgCount(int chatID)
        {
            return -1;
        }

        /// <summary>
        /// Gets all the members of a specific chat.
        /// </summary>
        /// <param name="chatID">The unique identifier of a specific chat.</param>
        /// <returns>A list of users, each containing the unique identifier of each user and their first name.</returns>
        public List<int?> GetMemberIDsFromChat(int chatID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    return db.GetChatMemeberIds(chatID).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}