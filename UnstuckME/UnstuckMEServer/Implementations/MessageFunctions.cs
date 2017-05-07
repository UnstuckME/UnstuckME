using System;
using System.Collections.Generic;
using System.Linq;
using UnstuckMEServer;
using UnstuckME_Classes;
using System.Threading.Tasks;

namespace UnstuckMEInterfaces
{
    public partial class UnstuckMEService : IUnstuckMEService, IUnstuckMEServer, IUnstuckMEFileStream
    {
        /// <summary>
		/// Gets the number of messages in a particular chat.
		/// </summary>
		/// <param name="chatID">The unique identifier of a specific chat.</param>
		/// <returns>A number indicating how many messages a chat has. Returns -1 if no messages are found.</returns>
        public int GetNumberOfMessages(int chatID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    int? first = db.GetNumMsgsInAChat(chatID).First();
                    return first ?? -1;
                }
            }
            catch (Exception)
            {
                return -1;
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
                    using (var messageID = db.InsertMessage(message.ChatID, message.Message, null, message.SenderID))
                    {
                        int? first = messageID.First();

                        if (first.HasValue)
                            message.MessageID = first.Value;
                    }

                    _messageList.Enqueue(message);
                    return message.MessageID;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Updates a chat message if a user in the conversation has edited it.
        /// </summary>
        /// <param name="message">The message that has been edited.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public async Task<int> EditMessage(UnstuckMEMessage message)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.UpdateMessageByMessageID(message.MessageID, message.Message);
                    await Task.Factory.StartNew(() => AsyncEditMessage(message));

                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Asyncronously gets all the online users who can see the message that has been edited and 
        /// changes it on their interface.
        /// </summary>
        /// <param name="message">The message that has been edited.</param>
        private void AsyncEditMessage(UnstuckMEMessage message)
        {
            foreach (var client in _connectedClients)
            {
                if (message.SenderID != client.Key && message.UsersInConvo.Contains(client.Key))
                    client.Value.Connection.UpdateChatMessage(message);
            }
        }

        /// <summary>
        /// Deletes a message. Is broadcasted to the other online users in the chat once it is deleted.
        /// </summary>
        /// <param name="message">The message to be deleted.</param>
        /// <returns>Returns 0 if successful, -1 if unsuccessful.</returns>
        public async Task<int> DeleteMessage(UnstuckMEMessage message)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    db.DeleteMessageByMessageID(message.MessageID);
                    await Task.Factory.StartNew(() => AsyncDeleteMessage(message));

                    return 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Asyncronously gets all the online users who can see the message that has been deleted and 
        /// removes it from their interface.
        /// </summary>
        /// <param name="message">The message to be deleted.</param>
        private void AsyncDeleteMessage(UnstuckMEMessage message)
        {
            foreach (var client in _connectedClients)
            {
                if (message.SenderID != client.Key && message.UsersInConvo.Contains(client.Key))
                    client.Value.Connection.DeleteChatMessage(message);
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
        protected List<UnstuckMEMessage> GetChatMessages(int chatID, short firstrow = 0, short lastrow = 75)
        {
            try
            {
                List<UnstuckMEMessage> messageList = new List<UnstuckMEMessage>();
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    using (var messages = db.GetChatMessages(chatID, firstrow, lastrow))
                    {
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
                            messageList.Add(temp);
                        }
                    }
                }

                return messageList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Currently for Ryan's use with chat caching.
        /// </summary>
        /// <param name="chatID">The unique identifier of the chat to get messages from.</param>
        /// <param name="messageID">The unique identifier of the message to begin the query at.</param>
        /// <param name="numMessages">The number of messages to return. Default is 20.</param>
        /// <returns>A list of UnstuckMEMessages.</returns>
        public List<UnstuckMEMessage> Ryans_GetChatMessage(int chatID, int messageID, int numMessages = 20)
        {
            using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
            {
                List<UnstuckMEMessage> messagelist = new List<UnstuckMEMessage>();
                using (var messages = db.Ryans_GetChatMessage(chatID, messageID, numMessages))
                {
                    foreach (var message in messages)
                    {
                        messagelist.Add(new UnstuckMEMessage()
                        {
                            MessageID = message.MessageID,
                            Message = message.MessageData,
                            SenderID = message.SentBy,
                            Time = message.SentTime
                        });
                    }
                }

                return messagelist;
            }
        }
    }
}