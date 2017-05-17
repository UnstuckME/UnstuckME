using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using UnstuckMEInterfaces;
using UnstuckME_Classes;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
	internal class ClientCallback : IClient
	{
		/// <summary>
		/// Adds a class to the user's GUI.
		/// </summary>
		/// <param name="inClass">The class to add to the user's GUI.</param>
		public void AddClasses(UserClass inClass)
		{
			try
			{
				Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RecieveAddedClass(inClass);
			}
			catch (InvalidOperationException ex)
			{
			    var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
			}
        }

		/// <summary>
		/// Forces the client to close with a messagebox popup.
		/// </summary>
		public void ForceClose()
		{
			UnstuckMEMessageBox messageBox = new UnstuckMEMessageBox(UnstuckMEBox.Shutdown, "UnstuckME Server has shutdown. Please Contact Your Server Administrator", "Server Shutdown", UnstuckMEBoxImage.Error);
			messageBox.ShowDialog();
		}

		/// <summary>
		/// Gets a message from the server to show to the client.
		/// </summary>
		/// <param name="message">The message to send to the client.</param>
		public void GetMessageFromServer(string message)
		{
			UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, message, "Message from server", UnstuckMEBoxImage.Information);
			messagebox.ShowDialog();
		}

		/// <summary>
		/// Updates a user's conversation if they are online and another user sends them a message.
		/// </summary>
		/// <param name="message">The message being sent to the user.</param>
		public void GetMessage(UnstuckMEMessage message)
		{
		    try
		    {
		        Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RecieveChatMessage(message);
		    }
		    catch (Exception ex)
		    {
		        var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
            }
        }

		/// <summary>
		/// Removes a sticker from any online, qualified user's GUI.
		/// </summary>
		/// <param name="stickerID">The sticker to be removed.</param>
		public void RemoveGUISticker(int stickerID)
		{
			try
			{
				Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RemoveStickerFromAvailableStickers(stickerID);
			}
			catch (InvalidOperationException ex)
			{
			    var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
			}
        }

		/// <summary>
		/// Updates a user's list of stickers if they are online and meet the qualifications to see the newly submitted sticker.
		/// </summary>
		/// <param name="inSticker">The sticker being sent to the users.</param>
		public void RecieveNewSticker(UnstuckMEAvailableSticker inSticker)
		{
			try
			{
				Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RecieveNewAvailableSticker(inSticker);
			}
			catch (InvalidOperationException ex)
			{
			    var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
			}
        }

        /// <summary>
        /// Updates a chat message if a user in the conversation has edited it.
        /// </summary>
        /// <param name="message">The message that has been edited.</param>
        public void UpdateChatMessage(UnstuckMEMessage message)
        {
            try
            {
                Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().UpdateChatMessage(message);
            }
            catch (InvalidOperationException ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
            }
        }

        /// <summary>
        /// Removes a chat message if a user in the conversation has deleted it.
        /// </summary>
        /// <param name="message">The message that has been removed.</param>
        public void DeleteChatMessage(UnstuckMEMessage message)
        {
            try
            {
                Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().DeleteChatMessage(message);
            }
            catch (InvalidOperationException ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
            }
        }

        /// <summary>
        /// Opens a CreateTutorReview window for the submission of a review on
        /// the sticker identified by <paramref name="stickerID"/>.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to submit a review on.</param>
        public void CreateReviewAsTutor(int stickerID)
        {
            try
            {
                Window window = new SubWindows.AddTutorReviewWindow(stickerID)
                {
                    Topmost = true
                };
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
                UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "You need to logout and log back in to submit a review on " + stickerID +
                                                                         ". If this does not work, please contact an UnstuckME administrator to resolve this issue.",
                                                                         "Could not create a new review", UnstuckMEBoxImage.Error);
                messagebox.ShowDialog();
            }
        }

        /// <summary>
        /// Opens a CreateStudentReview window for the submission of a review on
        /// the sticker identified by <paramref name="stickerID"/>.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker to submit a review on.</param>
        public void CreateReviewAsStudent(int stickerID)
        {
            try
            {
                Window window = new SubWindows.AddStudentReviewWindow(stickerID)
                {
                    Topmost = true
                };
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
                UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "You need to logout and log back in to submit a review on " + stickerID +
                                                                         ". If this does not work, please contact an UnstuckME administrator to resolve this issue.",
                                                                         "Could not create a new review", UnstuckMEBoxImage.Error);
                messagebox.ShowDialog();
            }
        }

        /// <summary>
        /// When a tutor drops a sticker rather than submitting a review, this will find the sticker of the
        /// student who submitted it and reactivates the completed and delete buttons.
        /// </summary>
        /// <param name="stickerID">The unique identifier of the sticker.</param>
        /// <param name="status">The status to change the sticker to.</param>
        public void UpdateStickerStatus(StickerStatus status, int stickerID)
        {
            try
            {
                switch (status)
                {
                    case StickerStatus.Available:   //sticker has been untutored
                        UnstuckME.Pages.StickerPage.MakeMyStickerActive(stickerID);
                        break;
                    case StickerStatus.Accepted:    //sticker has been accepted
                        UnstuckME.Pages.StickerPage.MakeMyStickerAccepted(stickerID);
                        break;
                    case StickerStatus.Resolved:    //sticker has both reviews, move it to sticker history
                        UnstuckME.Pages.StickerPage.MakeMyStickerResolved(stickerID);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, 
                                                                         "Could not update status of sticker " + stickerID + ", Source = " + trace.Name);
            }
        }

	    /// <summary>
	    /// When a review is submitted of a user, adds it to the list of reviews that have been
	    /// submitted on that user.
	    /// </summary>
	    /// <param name="review">The review to add to the user's profile page.</param>
	    /// <param name="newtutorRating">The new average tutor rating of the user.</param>
	    /// <param name="newstudentRating">The new average student rating of the user.</param>
	    public void RecieveReviewAndUpdateRatings(UnstuckMEReview review, float newtutorRating, float newstudentRating)
	    {
	        try
	        {
	            UnstuckME.Pages.UserProfilePage.AddReview(review);
	            UnstuckME.Pages.UserProfilePage.UpdateRatings(newstudentRating, newtutorRating);
	        }
	        catch (Exception ex)
	        {
	            var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Could not add review " + review.ReviewID + " to interface, Source = " + trace.Name);
	        }
	    }

        public void ChatUserLeft(int UserID, int ChatID)
        {
            UnstuckMEChatUser temp = null;
            if(UnstuckME.CurrentChatSession.ChatID == ChatID)
            {
                foreach (var User in UnstuckME.CurrentChatSession.Users)
                {
                    if(User.UserID == UserID)
                    {
                        temp = User;
                    }
                }
                UnstuckME.CurrentChatSession.Users.Remove(temp);
            }

            foreach (var Chat in UnstuckME.ChatSessions)
            {
                if(Chat.ChatID == ChatID)
                {
                    foreach (var User in Chat.Users)
                    {
                        if (User.UserID == UserID)
                        {
                            temp = User;
                        }
                    }
                    Chat.Users.Remove(temp);
                }
            }
        }
    }
}
