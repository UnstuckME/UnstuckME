using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UnstuckME_Classes;

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

            if (!test.Any())
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
                        using (MemoryStream ms = new MemoryStream())
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
            Background = Background == null ? Brushes.Gainsboro : Brushes.LightGray;
        }

        private void ConversationUserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = Background == Brushes.Gainsboro ? null : Brushes.LightGray;
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
                        user.ProfilePicture = UnstuckME.UserProfilePicture;
                    else
                    {
                        using (MemoryStream ms = new MemoryStream())
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
                converstion.Background = converstion.Chat.ChatID == UnstuckME.CurrentChatSession.ChatID ? Brushes.LightGray : null;

            NewMessageNotification temp = null;
            foreach (NewMessageNotification notification in UnstuckME.MainWindow.NotificationStack.Children.OfType<NewMessageNotification>())
            {
                if (notification.Message.ChatID == Chat.ChatID)
                    temp = notification;
            }
            if (temp != null)
                UnstuckME.MainWindow.NotificationStack.Children.Remove(temp);
        }
    }
}
