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
    /// Interaction logic for Conversation.xaml
    /// </summary>
    public partial class Conversation : UserControl
    {
        public UnstuckMEChat Chat;
        public ChatPage _ChatPage;
        public static IUnstuckMEService Server;
        public Conversation(UnstuckMEChat inChat, ChatPage inChatPage, ref IUnstuckMEService inServer)
        {
            InitializeComponent();
            Chat = inChat;
            _ChatPage = inChatPage;
            Server = inServer;
            var test = from a in inChat.Users
                       where a.UserID != _ChatPage.User.UserID
                       select new { ConversationName = a.UserName };
            if (test.Count() == 0)
            {
                ConversationLabel.Content = "Solo";
                var uri = new Uri("pack://application:,,,/Resources/AddUser/AddUserRed.png");
                var bitmap = new BitmapImage(uri);
                ConversationImage.Source = bitmap;
            }
            else if (test.Count() > 1)
            {
                ConversationLabel.Content = "Group";
                var uri = new Uri("pack://application:,,,/Resources/Group/GroupRed.png");
                var bitmap = new BitmapImage(uri);
                ConversationImage.Source = bitmap;
            }
            else
            {
                foreach (UnstuckMEChatUser user in Chat.Users)
                {
                    if(user.UserID != _ChatPage.User.UserID)
                    {
                       ConversationImage.Source =  _ChatPage.ic.ConvertFrom(Server.GetProfilePicture(user.UserID)) as ImageSource;
                    }
                }
                ConversationLabel.Content = test.First().ConversationName;
            }
        }

        private void ConversationUserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = Brushes.LightGray;
        }

        private void ConversationUserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = null;
        }

        public void ShowConversation()
        {
            ConversationUserControl_MouseLeftButtonDown(null, null);
        }

        public void ConversationUserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _ChatPage.LabelConversationName.Content = ConversationLabel.Content;
            foreach (UnstuckMEChatUser user in Chat.Users)
            {
                if(user.ProfilePicture == null)
                {
                    if(user.UserID == _ChatPage.User.UserID)
                    {
                        user.ProfilePicture = _ChatPage.UserImage;
                    }
                    else
                    {
                        user.ProfilePicture = _ChatPage.ic.ConvertFrom(Server.GetProfilePicture(user.UserID)) as ImageSource;
                    }
                }
            }
            _ChatPage.currentChat = Chat;
            _ChatPage.StackPanelMessages.Children.Clear();
            foreach (UnstuckMEMessage message in Chat.Messages)
            {
                UnstuckMEGUIChatMessage guiMessage = new UnstuckMEGUIChatMessage(message, Chat);
                _ChatPage.StackPanelMessages.Children.Add(new ChatMessage(guiMessage));
            }
            _ChatPage.ScrollViewerMessagesBox.ScrollToBottom();
        }
    }
}
