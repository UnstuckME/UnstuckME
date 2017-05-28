using System.ServiceModel;
using UnstuckME_Classes;

namespace UnstuckMEInterfaces
{
    [ServiceContract]
    public interface IClient
    {
        /// <summary>
        /// If a user has left a chat, remove them from the chat in the client.
        /// </summary>
        /// <param name="UserID">The unique identifier of the user to remove from the chat.</param>
        /// <param name="ChatID">The unique identifier of the chat to remove the user from.</param>
        [OperationContract(IsOneWay = true)]
        void ChatUserLeft(int UserID, int ChatID);

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

        /// <summary>
        /// When a tutor drops a sticker rather than submitting a review, this will find the sticker of the
        /// student who submitted it and reactivates the completed and delete buttons.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker.</param>
        /// <param name="status">The new status of the sticker.</param>
        [OperationContract(IsOneWay = true)]
        void UpdateStickerStatus(StickerStatus status, int stickerID);

        /// <summary>
        /// When a review is submitted of a user, adds it to the list of reviews that have been
        /// submitted on that user.
        /// </summary>
        /// <param name="review">The review to add to the user's profile page.</param>
        /// <param name="newtutorRating">The new average tutor rating of the user.</param>
        /// <param name="newstudentRating">The new average student rating of the user.</param>
        [OperationContract(IsOneWay = true)]
        void RecieveReviewAndUpdateRatings(UnstuckMEReview review, float newtutorRating, float newstudentRating);

        /// <summary>
        /// Finds all the chat messages that a user has sent and changes the name of the sender.
        /// </summary>
        /// <param name="userID">The unique identifier of the user who sent the message.</param>
        /// <param name="newName">The new name of the user who sent the message.</param>
        [OperationContract(IsOneWay = true)]
        void ChangeChatMessageUsernames(int userID, string newName);
    }
}