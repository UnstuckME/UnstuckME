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
        /// Creates a chat associated with a user.
        /// </summary>
        /// <param name="userID">The unique identifer of the callee.</param>
        /// <returns>The unique identifier of the newly created chat if successful, -1 if unsuccessful.</returns>
        public int CreateChat(int userID)
        {
            try
            {
                int chatID = -1;
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    int result = db.CreateChat(userID).FirstOrDefault().Value;
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
        protected List<UnstuckMEChatUser> GetChatMembers(int chatID)
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
    }
}