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
		/// Gets the number of messages in a particular chat.
		/// </summary>
		/// <param name="chatID">The unique identifier of a specific chat.</param>
		/// <returns>A number indicating how many messages a chat has.</returns>
        public int GetNumberOFMessages(int chatID)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    return (db.GetNumMsgsInAChat(chatID)).First().Value;
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
    }
}