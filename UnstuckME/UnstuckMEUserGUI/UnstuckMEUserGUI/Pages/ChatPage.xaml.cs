using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckMeLoggers;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for ChatPage.xaml
	/// </summary>
	public partial class ChatPage : Page
	{
		public ChatPage()
		{
			InitializeComponent();
			UnstuckME.CurrentChatSession = new UnstuckMEChat()
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
				if(item.Chat.ChatID == message.ChatID)
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
							StackPanelMessages.Children.Add(new ChatMessage(temp));
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
							if (((ChatMessage)StackPanelMessages.Children[i]).Message.MessageID == message.MessageID)
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
							if (((ChatMessage)StackPanelMessages.Children[i]).Message.MessageID == message.MessageID)
							{
								((ChatMessage)StackPanelMessages.Children[i]).Message.Message = message.Message;
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

		private void SendMessage(string message)
		{
			try
			{
				if (UnstuckME.CurrentChatSession.ChatID < 0)
					throw new Exception();   //If no conversation is chosen

				UnstuckMEMessage sendingMessage = new UnstuckMEMessage()
				{
					ChatID = UnstuckME.CurrentChatSession.ChatID,
					FilePath = string.Empty,
					Message = message,
					MessageID = 0,
					SenderID = UnstuckME.User.UserID,
					Time = DateTime.Now,
					Username = UnstuckME.User.FirstName,
					UsersInConvo = new List<int>()
				};

				foreach (UnstuckMEChatUser user in UnstuckME.CurrentChatSession.Users)
				{
					if (user.UserID != UnstuckME.User.UserID)
					    sendingMessage.UsersInConvo.Add(user.UserID);
				}

				sendingMessage.MessageID = UnstuckME.Server.SendMessage(sendingMessage);
				UnstuckME.CurrentChatSession.Messages.Add(sendingMessage);
				UnstuckMEGUIChatMessage temp = new UnstuckMEGUIChatMessage(sendingMessage, UnstuckME.CurrentChatSession);
				StackPanelMessages.Children.Add(new ChatMessage(temp));
				ScrollViewerMessagesBox.ScrollToBottom();
			}
			catch(Exception ex)
			{
				MessageBox.Show("Chat Send Failed. Error: " + ex.Message, "Failed Message Send", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, ex.TargetSite.Name);
			}
		}

		private void ButtonCreateChat_Click(object sender, RoutedEventArgs e)
		{
			ButtonCreateChat.Visibility = Visibility.Hidden;
			ButtonCreateChat.IsEnabled = false;
			GridConversations.Visibility = Visibility.Hidden;
			GridConversations.IsEnabled = false;
			GridNewConversation.Visibility = Visibility.Visible;
			GridNewConversation.IsEnabled = true;
		}

		public void ButtonAddUserDone_Click(object sender, RoutedEventArgs e)
		{
			ButtonCreateChat.Visibility = Visibility.Visible;
			ButtonCreateChat.IsEnabled = true;
			GridConversations.Visibility = Visibility.Visible;
			GridConversations.IsEnabled = true;
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

        }
    }
}
