using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnstuckME_Classes;
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        public UserInfo User;
        public ImageSource UserImage;
        public static IUnstuckMEService Server;
        public UnstuckMEChat currentChat;
        public List<UnstuckMEChat> allChats;
        public ImageSourceConverter ic;
        public ChatPage(ref UserInfo inUser, ref IUnstuckMEService inServer)
        {
            InitializeComponent();
            Server = inServer;
            User = inUser;
            ic = new ImageSourceConverter();
            UserImage = ic.ConvertFrom(User.UserProfilePictureBytes) as ImageSource;
            currentChat = new UnstuckMEChat();
            currentChat.ChatID = -1;
            for (int i = 0; i < 10; i++)
            {
                StackPanelAddContacts.Children.Add(new ContactAddToConversation());
            }
        }

        //This Box Determines what happens when a user clicks a new message notification.
        public void NotificationCall(UnstuckMEMessage message)
        {
            foreach (Conversation item in StackPanelConversations.Children.OfType<Conversation>())
            {
                if(item.Chat.ChatID == message.ChatID)
                {
                    item.ShowConversation();
                }
            }
        }

        private void MessageTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MessageTextBox.CaretIndex = MessageTextBox.Text.Length;
        }

        public void AddConversation(UnstuckMEChat inChat)
        {
            StackPanelConversations.Children.Add(new Conversation(inChat, this, ref Server));
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
                foreach (UnstuckMEChat chat in allChats)
                {
                    if(chat.ChatID == message.ChatID)
                    {
                        chatIDExists = true;
                        chat.Messages.Add(message);
                        if(currentChat.ChatID == chat.ChatID)
                        {
                            UnstuckMEGUIChatMessage temp = new UnstuckMEGUIChatMessage(message, chat);
                            StackPanelMessages.Children.Add(new ChatMessage(temp));
                            ScrollViewerMessagesBox.ScrollToBottom();
                        }
                    }
                }
                if(!chatIDExists)
                {
                    UnstuckMEChat temp = Server.GetSingleChat(message.ChatID);
                    allChats.Add(temp);
                    StackPanelConversations.Children.Add(new Conversation(temp, this, ref Server));
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Add Message Failed" + ": " + ex.Message);
            }
        }

		//should make a new GUIChatMessagethat can contain a file
		/// <summary>
		/// Currently doesn't implement the file, use first overload instead
		/// </summary>
		/// <param name="message"></param>
		/// <param name="file"></param>
		public void AddMessage(UnstuckMEMessage message, UnstuckMEFile file)
		{
			bool chatIDexists = false;

			try
			{
				foreach (UnstuckMEChat chat in allChats)
				{
					if (chat.ChatID == message.ChatID)
					{
						chatIDexists = true;
						chat.Messages.Add(message);
						if (currentChat.ChatID == chat.ChatID)
						{
							UnstuckMEGUIChatMessage temp = new UnstuckMEGUIChatMessage(message, chat);
							StackPanelMessages.Children.Add(new ChatMessage(temp));
							ScrollViewerMessagesBox.ScrollToBottom();
						}
					}
				}

				if (!chatIDexists)
				{
					UnstuckMEChat temp = Server.GetSingleChat(message.ChatID);
					allChats.Add(temp);
					StackPanelConversations.Children.Add(new Conversation(temp, this, ref Server));
				}
			}
			catch (Exception ex)
			{
				UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(1, ex.Message, "Add Message Failed");
			}
		}

        private void ScrollViewerConversationBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollViewerMessagesBox.ScrollToBottom();
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendButton_Click(null, null);
            }
        }

        private void SendMessage(string message)
        {
            try
            {
                if(currentChat.ChatID < 0) { throw new Exception(); } //If no conversation is chosen
                UnstuckMEMessage sendingMessage = new UnstuckMEMessage();
                sendingMessage.ChatID = currentChat.ChatID;
                sendingMessage.FilePath = string.Empty;
                sendingMessage.IsFile = false;
                sendingMessage.Message = message;
                sendingMessage.MessageID = 0;
                sendingMessage.SenderID = User.UserID;
                sendingMessage.Time = DateTime.Now;
                sendingMessage.Username = User.FirstName;
                sendingMessage.UsersInConvo = new List<int>();
                foreach (UnstuckMEChatUser user in currentChat.Users)
                {
                    if(user.UserID != User.UserID)
                    {
                        sendingMessage.UsersInConvo.Add(user.UserID);
                    }
                }
                Server.SendMessage(sendingMessage);
                currentChat.Messages.Add(sendingMessage);
                UnstuckMEGUIChatMessage temp = new UnstuckMEGUIChatMessage(sendingMessage, currentChat);
                StackPanelMessages.Children.Add(new ChatMessage(temp));
                ScrollViewerMessagesBox.ScrollToBottom();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Chat Send Failed. Error: " + ex.Message, "Failed Message Send", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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

        private void ButtonAddUserDone_Click(object sender, RoutedEventArgs e)
        {
            ButtonCreateChat.Visibility = Visibility.Visible;
            ButtonCreateChat.IsEnabled = true;
            GridConversations.Visibility = Visibility.Visible;
            GridConversations.IsEnabled = true;
            GridNewConversation.Visibility = Visibility.Hidden;
            GridNewConversation.IsEnabled = false;
            TextBoxManualUserNameSearch.Text = string.Empty;
        }

        private void ImageAddUserToConvo_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderAddUserToConvo.Background = Brushes.LightBlue;
        }

        private void ImageAddUserToConvo_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderAddUserToConvo.Background = Brushes.SteelBlue;
        }

        private void ImageAddUserToConvo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Button Clicked");
        }

        private void ButtonStartConversation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LabelInvalidUserNameSearch.Visibility = Visibility.Hidden;
                int searchedUserID = -1;
                int chatID;
                if(Server.IsValidUser(TextBoxManualUserNameSearch.Text))
                {
                    chatID = Server.CreateChat(User.UserID);
                    searchedUserID = Server.GetUserID(TextBoxManualUserNameSearch.Text);
                    Server.InsertUserIntoChat(searchedUserID, chatID);
                    UnstuckMEMessage temp = new UnstuckMEMessage();
                    temp.ChatID = chatID;
                    temp.FilePath = string.Empty;
                    temp.IsFile = false;
                    temp.Message = "New Conversation with " + User.FirstName + " " + User.LastName + " started.";
                    temp.MessageID = 0;
                    temp.SenderID = User.UserID;
                    temp.UsersInConvo = new List<int>();
                    temp.UsersInConvo.Add(User.UserID);
                    temp.UsersInConvo.Add(searchedUserID);
                    Server.SendMessage(temp);
                    AddMessage(temp);
                    ButtonAddUserDone_Click(null, null);
                    foreach (Conversation convo  in StackPanelConversations.Children.OfType<Conversation>())
                    {
                        if(convo.Chat.ChatID == chatID)
                        {
                            convo.ConversationUserControl_MouseLeftButtonDown(null, null);
                        }
                    }
                }
            }
            catch(Exception)
            {
                LabelInvalidUserNameSearch.Visibility = Visibility.Visible;
            }
        }
    }
}
