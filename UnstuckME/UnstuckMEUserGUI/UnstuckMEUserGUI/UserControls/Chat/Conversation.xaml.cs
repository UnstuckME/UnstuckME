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
        public Conversation(UnstuckMEChat inChat)
        {
            InitializeComponent();
            Chat = inChat;
            var test = from a in inChat.Users
                       where a.UserID != UnstuckME.User.UserID
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
                    if (user.UserID != UnstuckME.User.UserID)
                    {
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            UnstuckME.FileStream.GetProfilePicture(user.UserID).CopyTo(ms);
                            ConversationImage.Source = UnstuckME.ImageConverter.ConvertFrom(ms.ToArray()) as ImageSource;
                        }
                    }
                }
                ConversationLabel.Content = test.First().ConversationName;
            }
        }

        private void ConversationUserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if(Background == null)
            {
                Background = Brushes.Gainsboro;
            }
            else
            {
                Background = Brushes.LightGray;
            }
        }

        private void ConversationUserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if(Background == Brushes.Gainsboro)
            {
                Background = null;
            }
            else
            {
                Background = Brushes.LightGray;
            }
        }

        public void ShowConversation()
        {
            ConversationUserControl_MouseLeftButtonDown(null, null);
        }

        public void ConversationUserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UnstuckME.Pages.ChatPage.ButtonAddUserToConvo.Visibility = Visibility.Visible;
            UnstuckME.Pages.ChatPage.ButtonAddUserToConvo.IsEnabled = true;
            UnstuckME.Pages.ChatPage.LabelConversationName.Content = ConversationLabel.Content;
            foreach (UnstuckMEChatUser user in Chat.Users)
            {
                if (user.ProfilePicture == null)
                {
                    if (user.UserID == UnstuckME.User.UserID)
                    {
                        user.ProfilePicture = UnstuckME.UserProfilePicture;
                    }
                    else
                    {
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        {
                            UnstuckME.FileStream.GetProfilePicture(user.UserID).CopyTo(ms);
                            user.ProfilePicture = UnstuckME.ImageConverter.ConvertFrom(ms.ToArray()) as ImageSource;
                        }
                    }
                }
            }
            UnstuckME.CurrentChatSession = Chat;
            UnstuckME.Pages.ChatPage.StackPanelMessages.Children.Clear();
            foreach (UnstuckMEMessage message in Chat.Messages)
            {
                UnstuckMEGUIChatMessage guiMessage = new UnstuckMEGUIChatMessage(message, Chat);
                UnstuckME.Pages.ChatPage.StackPanelMessages.Children.Add(new ChatMessage(guiMessage));
            }
            UnstuckME.Pages.ChatPage.ScrollViewerMessagesBox.ScrollToBottom();

            foreach (var converstion in ((StackPanel)Parent).Children.OfType<Conversation>())
            {
                if(converstion.Chat.ChatID == UnstuckME.CurrentChatSession.ChatID)
                {
                    converstion.Background = Brushes.LightGray;
                }
                else
                {
                    converstion.Background = null;
                }
            }
            NewMessageNotification temp = null;
            foreach (NewMessageNotification notification in UnstuckME.MainWindow.NotificationStack.Children.OfType<NewMessageNotification>())
            {
                if(notification.Message.ChatID == Chat.ChatID)
                {
                    temp = notification;
                }
            }
            if(temp != null)
            {
                UnstuckME.MainWindow.NotificationStack.Children.Remove(temp);
            }
        }
    }
}
