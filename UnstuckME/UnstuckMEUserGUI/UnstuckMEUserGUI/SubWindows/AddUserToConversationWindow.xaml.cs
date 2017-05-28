using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckMeLoggers;
using UnstuckME_Classes;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;
using System.IO;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for AddUserToConversationWindow.xaml
    /// </summary>
    public partial class AddUserToConversationWindow : Window
    {
        private UnstuckMEChat CurrentChat;

        public AddUserToConversationWindow(UnstuckMEChat inCurrentChat)
        {
            InitializeComponent();

            CurrentChat = inCurrentChat;
            List<UnstuckMEChatUser> conversationContactList = UnstuckME.FriendsList.ToList();
            List<UnstuckMEChatUser> alreadyInChatList = new List<UnstuckMEChatUser>();

            foreach (UnstuckMEChatUser friend in conversationContactList)
            {
                foreach (UnstuckMEChatUser user in CurrentChat.Users)
                {
                    if (friend.UserID == user.UserID)
                        alreadyInChatList.Add(friend);
                }
            }

            foreach (UnstuckMEChatUser alreadyInChatUser in alreadyInChatList)
                conversationContactList.Remove(alreadyInChatUser);

            foreach (UnstuckMEChatUser friend in conversationContactList)
                StackPanelFriendsList.Children.Add(new ContactAddToConversation(friend));
        }

        private void MoveBottomRightEdgeOfWindowToMousePosition()
        {
            PresentationSource presentationSource = PresentationSource.FromVisual(this);
            if (presentationSource?.CompositionTarget != null)
            {
                var transform = presentationSource.CompositionTarget.TransformFromDevice;
                var mouse = transform.Transform(GetMousePosition());
                Left = mouse.X - ActualWidth;
                Top = mouse.Y;
            }
        }

        public Point GetMousePosition()
        {
            System.Drawing.Point point = Control.MousePosition;
            return new Point(point.X, point.Y);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveBottomRightEdgeOfWindowToMousePosition();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Close();
        }

        private void ButtonAddUser_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonAddUser.Background = Brushes.LimeGreen;
        }

        private void ButtonAddUser_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonAddUser.Background = Brushes.ForestGreen;
        }

        private void ButtonAddUser_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelInvalidUsername.Visibility = Visibility.Hidden;
            try
            {
                if (string.Equals(TextBoxManualSearch.Text, UnstuckME.User.EmailAddress, StringComparison.CurrentCultureIgnoreCase))
                    throw new Exception("Cannot Add Yourself");

                if (UnstuckME.Server.IsValidUser(TextBoxManualSearch.Text) && TextBoxManualSearch.Text.Length > 6)
                {
                    int userID = UnstuckME.Server.GetUserID(TextBoxManualSearch.Text);

                    foreach (var user in UnstuckME.CurrentChatSession.Users)
                    {
                        if (user.UserID == userID)
                            throw new Exception("User Already In Conversation");
                    }

                    UnstuckME.Server.InsertUserIntoChat(userID, CurrentChat.ChatID);
                    UnstuckMEMessage temp = new UnstuckMEMessage
                    {
                        ChatID = CurrentChat.ChatID,
                        FilePath = string.Empty,
                        Message = "A new member has joined the conversation!",
                        MessageID = 0,
                        SenderID = UnstuckME.User.UserID,
                        Username = UnstuckME.User.FirstName,
                        UsersInConvo = new List<int>()
                    };
                    UnstuckMEChatUser contact = new UnstuckMEChatUser
                    {
                        UserID = userID,
                        UserName = UnstuckME.Server.GetUserDisplayName(userID),
                        EmailAddress = TextBoxManualSearch.Text
                    };
                    using (MemoryStream ms = new MemoryStream())
                    {
                        UnstuckME.FileStream.GetProfilePicture(contact.UserID).CopyTo(ms);
                        contact.ProfilePicture = UnstuckME.ImageConverter.ConvertFrom(ms.ToArray()) as ImageSource;
                    }

                    CurrentChat.Users.Add(contact);

                    foreach (UnstuckMEChatUser user in CurrentChat.Users)
                        temp.UsersInConvo.Add(user.UserID);
                    foreach (Conversation convo in UnstuckME.Pages.ChatPage.StackPanelConversations.Children.OfType<Conversation>())
                    {
                        if (convo.Chat.ChatID == CurrentChat.ChatID)
                        {
                            convo.Chat.Users = CurrentChat.Users;
                            convo.SetConversationLabel();
                            if (UnstuckME.CurrentChatSession.ChatID == CurrentChat.ChatID)
                                UnstuckME.Pages.ChatPage.LabelConversationName.Content = convo.ConvoLabelText.Text;
                        }
                    }

                    temp.MessageID = UnstuckME.Server.SendMessage(temp);
                    UnstuckME.Pages.ChatPage.AddMessage(temp);
                }
                else
                    LabelInvalidUsername.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                LabelInvalidUsername.Visibility = Visibility.Visible;
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
        }
    }
}
