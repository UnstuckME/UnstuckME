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
using System.Windows.Shapes;
using UnstuckME_Classes;
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for AddUserToConversationWindow.xaml
    /// </summary>
    public partial class AddUserToConversationWindow : Window
    {
        public UnstuckMEChat CurrentChat;
        public List<UnstuckMEChatUser> ConversationContactList;
        public AddUserToConversationWindow(UnstuckMEChat inCurrentChat)
        {
            InitializeComponent();

            CurrentChat = inCurrentChat;
            ConversationContactList = UnstuckME.FriendsList.ToList();

            List<UnstuckMEChatUser> AlreadyInChatList = new List<UnstuckMEChatUser>();
            foreach (UnstuckMEChatUser friend in ConversationContactList)
            {
                foreach (UnstuckMEChatUser user in CurrentChat.Users)
                {
                    if(friend.UserID == user.UserID)
                    {
                        AlreadyInChatList.Add(friend);
                    }
                }
            }

            foreach (UnstuckMEChatUser alreadyInChatUser in AlreadyInChatList)
            {
                ConversationContactList.Remove(alreadyInChatUser);
            }

            foreach (UnstuckMEChatUser friend in ConversationContactList)
            {
                StackPanelFriendsList.Children.Add(new ContactAddToConversation(friend));
            }
        }

        private void MoveBottomRightEdgeOfWindowToMousePosition()
        {
            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var mouse = transform.Transform(GetMousePosition());
            Left = mouse.X - this.ActualWidth;
            Top = mouse.Y;
        }

        public System.Windows.Point GetMousePosition()
        {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new System.Windows.Point(point.X, point.Y);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveBottomRightEdgeOfWindowToMousePosition();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Close();
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
            if(UnstuckME.Server.IsValidUser(TextBoxManualSearch.Text) && TextBoxManualSearch.Text.Length > 6)
            {
                int userID = UnstuckME.Server.GetUserID(TextBoxManualSearch.Text);
                UnstuckME.Server.InsertUserIntoChat(userID, CurrentChat.ChatID);
                UnstuckMEMessage temp = new UnstuckMEMessage();
                temp.ChatID = CurrentChat.ChatID;
                temp.FilePath = string.Empty;
                temp.IsFile = false;
                temp.Message = "A new member has joined the conversation!";
                temp.MessageID = 0;
                temp.SenderID = UnstuckME.User.UserID;
                temp.Username = UnstuckME.User.FirstName;
                temp.UsersInConvo = new List<int>();
                foreach (UnstuckMEChatUser user in CurrentChat.Users)
                {
                    temp.UsersInConvo.Add(user.UserID);
                }
                UnstuckME.Server.SendMessage(temp);
                UnstuckME.Pages.ChatPage.AddMessage(temp);

            }
            else
            {
                LabelInvalidUsername.Visibility = Visibility.Visible;
            }
        }
    }
}
