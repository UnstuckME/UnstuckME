using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    public interface IClient
    {
        /// <summary>
        /// Forces the client to close with a messagebox popup.
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void ForceClose();

        /// <summary>
        /// Gets a message from the server to show to the client.
        /// </summary>
        /// <param name="message">The message to send to the client.</param>
        [OperationContract(IsOneWay = true)]
        void GetMessageFromServer(string message);

        /// <summary>
        /// Updates a user's conversation if they are online and another user sends them a message.
        /// </summary>
        /// <param name="message">The message being sent to the user.</param>
        [OperationContract(IsOneWay = true)]
        void GetMessage(UnstuckMEMessage message);

        /// <summary>
        /// Updates a user's conversation if they are online and another user sends them a file.
        /// </summary>
        /// <param name="message">The message being sent to the user.</param>
        /// <param name="file">The file being sent to the user.</param>
        //      [OperationContract]
        //void GetFile(UnstuckMEMessage message, UnstuckMEFile file);

        /// <summary>
        /// Adds a class to the user's GUI.
        /// </summary>
        /// <param name="inClass">The class to add to the user's GUI.</param>
        [OperationContract(IsOneWay = true)]
        void AddClasses(UserClass inClass);

        /// <summary>
        /// Removes a sticker from any online, qualified user's GUI.
        /// </summary>
        /// <param name="stickerID">The sticker to be removed.</param>
        [OperationContract(IsOneWay = true)]
        void RemoveGUISticker(int stickerID);

        /// <summary>
        /// Updates a user's list of stickers if they are online and meet the qualifications to see the newly submitted sticker.
        /// </summary>
        /// <param name="inSticker">The sticker being sent to the users.</param>
        [OperationContract(IsOneWay = true)]
        void RecieveNewSticker(UnstuckMEAvailableSticker inSticker);

        /// <summary>
        /// Updates a chat message if a user in the conversation has edited it.
        /// </summary>
        /// <param name="message">The message that has been edited.</param>
        [OperationContract(IsOneWay = true)]
        void UpdateChatMessage(UnstuckMEMessage message);

        /// <summary>
        /// Removes a chat message if a user in the conversation has deleted it.
        /// </summary>
        /// <param name="message">The message that has been removed.</param>
        [OperationContract(IsOneWay = true)]
        void DeleteChatMessage(UnstuckMEMessage message);

        /// <summary>
        /// Opens a CreateTutorReview window for the submission of a review on
        /// the sticker identified by <paramref name="stickerID"/>.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to submit a review on.</param>
        [OperationContract(IsOneWay = true)]
        void CreateReviewAsTutor(int stickerID);

        /// <summary>
        /// Opens a CreateStudentReview window for the submission of a review on
        /// the sticker identified by <paramref name="stickerID"/>.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to submit a review on.</param>
        [OperationContract(IsOneWay = true)]
        void CreateReviewAsStudent(int stickerID);

        [OperationContract(IsOneWay = true)]
        void UpdateStickerStatus(int stickerID);
    }
}