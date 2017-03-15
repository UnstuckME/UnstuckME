using System;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using UnstuckMEInterfaces;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
	class ClientCallback : IClient
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
			catch(InvalidOperationException ex)
			{
				MessageBox.Show(ex.Message);
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
			Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RecieveChatMessage(message);
		}

		/// <summary>
		/// Updates a user's conversation if they are online and another user sends them a file.
		/// </summary>
		/// <param name="message">The message being sent to the user.</param>
		/// <param name="file">The file being sent to the user.</param>
		public void GetFile(UnstuckMEMessage message, UnstuckMEFile file)
		{
			Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().RecieveChatFile(message, file);
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
				MessageBox.Show(ex.Message);
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
				MessageBox.Show(ex.Message);
			}
		}
	}
}
