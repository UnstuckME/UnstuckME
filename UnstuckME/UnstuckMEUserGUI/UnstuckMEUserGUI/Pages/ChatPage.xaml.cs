using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using UnstuckMeLoggers;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for ChatPage.xaml
	/// </summary>
	public partial class ChatPage : Page
	{
	    internal UnstuckMEMessage newMessage;

		public ChatPage()
		{
			InitializeComponent();
		    newMessage = new UnstuckMEMessage();
			UnstuckME.CurrentChatSession = new UnstuckMEChat
			{
				ChatID = -1
			};
			ButtonAddUserToConvo.Visibility = Visibility.Hidden;
			ButtonAddUserToConvo.IsEnabled = false;
		}

		//This Box Determines what happens when a user clicks a new message notification.
		public void NotificationCall(UnstuckMEMessage message)
		{
			foreach (Conversation item in StackPanelConversations.Children.OfType<Conversation>())
			{
				if (item.Chat.ChatID == message.ChatID)
				    item.ShowConversation();
			}
		}

		private void MessageTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			MessageTextBox.CaretIndex = MessageTextBox.Text.Length;
		}

		public void AddConversation(UnstuckMEChat inChat)
		{
			StackPanelConversations.Children.Add(new Conversation(inChat));
		}

		private void SendButton_Click(object sender, RoutedEventArgs e)
		{
			if (MessageTextBox.Text != string.Empty)
			{
				SendMessage(MessageTextBox.Text);
				MessageTextBox.Text = string.Empty;
			}
		}

		public void AddMessage(UnstuckMEMessage message)
		{
			bool chatIDExists = false;

			try
			{
				foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
				{
					if (chat.ChatID == message.ChatID)
					{
						chatIDExists = true;
						chat.Messages.Add(message);

						if (UnstuckME.CurrentChatSession.ChatID == chat.ChatID)
						{
							UnstuckMEGUIChatMessage temp = new UnstuckMEGUIChatMessage(message, chat);

						    if (string.IsNullOrEmpty(message.FilePath))
						        StackPanelMessages.Children.Add(new ChatMessage(temp));
						    else
						        StackPanelMessages.Children.Add(new ChatMessageFile(temp));

							ScrollViewerMessagesBox.ScrollToBottom();
						}
					}
				}

				if (!chatIDExists)
				{
					UnstuckMEChat temp = UnstuckME.Server.GetSingleChat(message.ChatID);
					UnstuckME.ChatSessions.Add(temp);
					StackPanelConversations.Children.Add(new Conversation(temp));
				}
			}
			catch (Exception ex)
			{
			    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, ex.TargetSite.Name);
			}
        }

		public void RemoveMessage(UnstuckMEMessage message)
		{
			try
			{
				foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
				{
					if (chat.ChatID == message.ChatID)
					{
						for (int i = 0; i < StackPanelMessages.Children.Count; i++)
						{
							if (((ChatMessage)StackPanelMessages.Children[i]).Message.ChatMessage.MessageID == message.MessageID)
							{
								StackPanelMessages.Children.Remove(StackPanelMessages.Children[i]);
								break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, ex.TargetSite.Name);
			}
        }

		public void EditMessage(UnstuckMEMessage message)
		{
			try
			{
				foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
				{
					if (chat.ChatID == message.ChatID)
					{
						for (int i = 0; i < StackPanelMessages.Children.Count; i++)
						{
							if (((ChatMessage)StackPanelMessages.Children[i]).Message.ChatMessage.MessageID == message.MessageID)
							{
								((ChatMessage)StackPanelMessages.Children[i]).Message.ChatMessage.Message = message.Message;
								((ChatMessage)StackPanelMessages.Children[i]).TextBoxChatMessage.Text = message.Message;
							    ((ChatMessage)StackPanelMessages.Children[i]).TextBlockChatMessage.Text = message.Message;
                                break;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, ex.TargetSite.Name);
			}
		}

		private void ScrollViewerConversationBox_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ScrollViewerMessagesBox.ScrollToBottom();
		}

		private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			    SendButton_Click(null, null);
		}

		public async void SendMessage(string message)
		{
			try
			{
				if (UnstuckME.CurrentChatSession.ChatID < 0)
					throw new Exception();   //If no conversation is chosen

			    newMessage.ChatID = UnstuckME.CurrentChatSession.ChatID;
			    newMessage.Message = message;
			    newMessage.MessageID = 0;
			    newMessage.SenderID = UnstuckME.User.UserID;
			    newMessage.Time = DateTime.Now;
			    newMessage.Username = UnstuckME.User.FirstName;
			    newMessage.UsersInConvo = new List<int>();

				foreach (UnstuckMEChatUser user in UnstuckME.CurrentChatSession.Users)
				{
					if (user.UserID != UnstuckME.User.UserID)
					    newMessage.UsersInConvo.Add(user.UserID);
				}

			    if (!string.IsNullOrEmpty(newMessage.FilePath))
			    {
			        string[] filenames = newMessage.FilePath.Split('\\');
			        await Task.Factory.StartNew(() => AsyncUploadFile(newMessage.FilePath, filenames[filenames.Length - 1]));
			    }

                newMessage.MessageID = UnstuckME.Server.SendMessage(newMessage);

                UnstuckME.CurrentChatSession.Messages.Add(newMessage);
				UnstuckMEGUIChatMessage temp = new UnstuckMEGUIChatMessage(newMessage, UnstuckME.CurrentChatSession);

                if (string.IsNullOrEmpty(newMessage.FilePath))
			        StackPanelMessages.Children.Add(new ChatMessage(temp));
			    else
			        StackPanelMessages.Children.Add(new ChatMessageFile(temp));

                ScrollViewerMessagesBox.ScrollToBottom();
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, ex.TargetSite.Name);
                UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Chat Send Failed. Error: " + ex.Message, "Failed to Send Message", UnstuckMEBoxImage.Error);
                messagebox.ShowDialog();
			}
		}

	    private void AsyncUploadFile(string filepath, string filename)
	    {
	        using (FileStream fs = File.Open(filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
	        {
	            using (MemoryStream ms = new MemoryStream())
	            {
	                fs.CopyTo(ms);
	                ms.Position = 0L;
	                newMessage.FilePath = UnstuckME.FileStream.SendFile(new UnstuckMEStream(ms.ToArray(), false)
	                {
                        UserID = UnstuckME.User.UserID,
                        Filename = filename
	                });
	                newMessage.FileSize = ms.Length;
	            }
	        }
	    }

  //      private void ButtonCreateChat_Click(object sender, RoutedEventArgs e)
		//{
		//	ButtonCreateChat.Visibility = Visibility.Hidden;
		//	ButtonCreateChat.IsEnabled = false;
		//	GridConversations.Visibility = Visibility.Hidden;
		//	GridConversations.IsEnabled = false;
		//	GridNewConversation.Visibility = Visibility.Visible;
		//	GridNewConversation.IsEnabled = true;
		//}

		public void ButtonAddUserDone_Click(object sender, RoutedEventArgs e)
		{
			ButtonCreateChat.Visibility = Visibility.Visible;
			ButtonCreateChat.IsEnabled = true;
			GridConversations.Visibility = Visibility.Visible;
			GridConversations.IsEnabled = true;
            if (UnstuckME.CurrentChatSession.ChatID != -1)
            {
                LeaveCreateChat.Visibility = Visibility.Visible;
            }
            LeaveCreateChat.IsEnabled = true;
            GridNewConversation.Visibility = Visibility.Hidden;
			GridNewConversation.IsEnabled = false;
			TextBoxManualUserNameSearch.Text = string.Empty;
		}

		private void ButtonStartConversation_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				LabelInvalidUserNameSearch.Visibility = Visibility.Hidden;
			    if (UnstuckME.Server.IsValidUser(TextBoxManualUserNameSearch.Text))
				{
				    int searchedUserID = UnstuckME.Server.GetUserID(TextBoxManualUserNameSearch.Text);
				    StartNewConversation(searchedUserID);
				}
				else
			        LabelInvalidUserNameSearch.Visibility = Visibility.Visible;
			}
			catch (Exception)
			{
				LabelInvalidUserNameSearch.Visibility = Visibility.Visible;
			}
		}

		private void ButtonAddUserToConvo_MouseEnter(object sender, MouseEventArgs e)
		{
			ButtonAddUserToConvo.Background = Brushes.LightBlue;
		}

		private void ButtonAddUserToConvo_MouseLeave(object sender, MouseEventArgs e)
		{
			ButtonAddUserToConvo.Background = Brushes.SteelBlue;
		}

		private void ButtonAddUserToConvo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			AddUserToConversationWindow addUserWindow = new AddUserToConversationWindow(UnstuckME.CurrentChatSession);
			addUserWindow.Show();
		}

		public void StartNewConversation(int TargetUser)
		{
			try
			{
				if (SoloConversationAlreadyExists(TargetUser))
				    throw new Exception();

			    const int searchedUserID = -1;
			    int chatID = UnstuckME.Server.CreateChat(UnstuckME.User.UserID);
				UnstuckME.Server.InsertUserIntoChat(TargetUser, chatID);
			    UnstuckMEMessage temp = new UnstuckMEMessage
			    {
			        ChatID = chatID,
			        FilePath = string.Empty,
			        Message = "New Conversation with " + UnstuckME.User.FirstName + " " + UnstuckME.User.LastName + " started.",
			        MessageID = 0,
			        Username = UnstuckME.User.FirstName,
			        SenderID = UnstuckME.User.UserID,
			        UsersInConvo = new List<int> {UnstuckME.User.UserID, searchedUserID}
			    };
			    UnstuckME.Server.SendMessage(temp);
				AddMessage(temp);
				ButtonAddUserDone_Click(null, null);
				foreach (Conversation convo in StackPanelConversations.Children.OfType<Conversation>())
				{
					if (convo.Chat.ChatID == chatID)
					    convo.ConversationUserControl_MouseLeftButtonDown(null, null);
				}
			}
			catch (Exception)
			{
				ButtonAddUserDone_Click(null, null);
			}
		}

	    private bool SoloConversationAlreadyExists(int TargetUser)
		{
			bool convoExists = false;

			foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
			{
				if (chat.Users.Count == 2)
				{
					foreach (UnstuckMEChatUser user in chat.Users)
					{
						if (user.UserID == TargetUser)
						{
							convoExists = true;
							foreach (Conversation convo in UnstuckME.Pages.ChatPage.StackPanelConversations.Children.OfType<Conversation>())
							{
								if (convo.Chat.ChatID == chat.ChatID)
								    convo.ShowConversation();
							}
						}
					}
				}
			}

			return convoExists;
		}

        private void UploadButton_MouseEnter(object sender, MouseEventArgs e)
        {
            UploadButton.BorderBrush = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF7EB4EA"));
        }

        private void UploadButton_MouseLeave(object sender, MouseEventArgs e)
        {
            UploadButton.BorderBrush = null;
        }

        private void UploadButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenFileDialog uploadFileDialog = new OpenFileDialog
                {
                    AddExtension = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer),
                    Multiselect = false,
                    ValidateNames = true,
                    CheckPathExists = true,
                    CheckFileExists = true,
                    Filter = "All Files (*.*)|*.*",
                    Title = "Upload File"
                };

                bool? open = uploadFileDialog.ShowDialog();

                if (open.HasValue && open.Value)
                {
                    FileInfo file = new FileInfo(uploadFileDialog.FileName);

                    if (file.Length < 26214400)
                    {
                        UploadFileToChatWindow uploadFileWindow = new UploadFileToChatWindow(uploadFileDialog.FileName);
                        uploadFileWindow.ShowDialog();
                        //newMessage.FilePath = uploadFileDialog.FileName;
                    }
                    else
                    {
                        UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "The file you have selected exceeds the 25 MB file size limit. Please select a smaller file.", "File Size Too Large", UnstuckMEBoxImage.Error);
                        messagebox.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, ex.TargetSite.Name);
            }
        }

        private void ButtonCreateChat_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonCreateChat.Background = Brushes.LightSteelBlue;
        }

        private void ButtonCreateChat_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonCreateChat.Background = Brushes.SteelBlue;
        }

        private void ButtonCreateChat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ButtonCreateChat.Visibility = Visibility.Hidden;
            ButtonCreateChat.IsEnabled = false;
            LeaveCreateChat.Visibility = Visibility.Hidden;
            LeaveCreateChat.IsEnabled = false;
            GridConversations.Visibility = Visibility.Hidden;
            GridConversations.IsEnabled = false;
            GridNewConversation.Visibility = Visibility.Visible;
            GridNewConversation.IsEnabled = true;
        }

        private void LeaveCreateChat_MouseEnter(object sender, MouseEventArgs e)
        {
            LeaveCreateChat.Background = Brushes.IndianRed;
        }

        private void LeaveCreateChat_MouseLeave(object sender, MouseEventArgs e)
        {
            LeaveCreateChat.Background = UnstuckME.Red;
        }

        private void LeaveCreateChat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UnstuckMEMessageBox confirm = new UnstuckMEMessageBox(UnstuckMEBox.YesNo, "Are you sure you want to leave the current conversation?", "Leave Conversation", UnstuckMEBoxImage.Information);
            if (confirm.ShowDialog().Value)
            {
                try
                {
                    Conversation temp = null;
                    UnstuckMEChat tempChat = null;
                    //UnstuckME.Server.LeaveConversation(UnstuckME.User.UserID, UnstuckME.CurrentChatSession.ChatID, UnstuckME.CurrentChatSession.Users);
                    UnstuckME.Server.LeaveConversation(UnstuckME.User.UserID, UnstuckME.CurrentChatSession.ChatID);
                    UnstuckME.Pages.ChatPage.StackPanelMessages.Children.Clear();
                    foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
                    {
                        if (chat.ChatID == UnstuckME.CurrentChatSession.ChatID)
                            tempChat = chat;
                    }
                    foreach (Conversation convo in UnstuckME.Pages.ChatPage.StackPanelConversations.Children.OfType<Conversation>())
                    {
                        if(convo.Chat.ChatID == UnstuckME.CurrentChatSession.ChatID)
                            temp = convo;
                    }
                    UnstuckME.Pages.ChatPage.StackPanelConversations.Children.Remove(temp);
                    UnstuckME.ChatSessions.Remove(tempChat);
                    if (UnstuckME.Pages.ChatPage.StackPanelConversations.Children.Count > 0)
                        (UnstuckME.Pages.ChatPage.StackPanelConversations.Children[0] as Conversation).ConversationUserControl_MouseLeftButtonDown(null, null);
                    else
                    {
                        UnstuckME.CurrentChatSession.ChatID = -1;
                        UnstuckME.CurrentChatSession.Messages.Clear();
                        UnstuckME.CurrentChatSession.Users.Clear();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
