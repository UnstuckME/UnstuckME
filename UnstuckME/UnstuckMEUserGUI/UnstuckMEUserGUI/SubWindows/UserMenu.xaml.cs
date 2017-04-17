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

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        UnstuckMEChatUser Friend;
        public UserMenu(UnstuckMEChatUser inFriend)
        {
            InitializeComponent();
            Activate();
            Topmost = true;
            Focus();
            Friend = inFriend;
            ProfilePicture.Source = Friend.ProfilePicture;
            LabelUsername.Content = Friend.UserName;
        }

        private void MoveBottomRightEdgeOfWindowToMousePosition()
        {
            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var mouse = transform.Transform(GetMousePosition());
            Left = mouse.X - Width;
            Top = mouse.Y - Width;
        }

        public System.Windows.Point GetMousePosition()
        {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new System.Windows.Point(point.X, point.Y);
        }

        private void MenuWindow_Deactivated(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveBottomRightEdgeOfWindowToMousePosition();
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonRemoveFriend_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            OnlineUser temp = null;
            ContactCreateConversation temp2 = null;
            UnstuckME.Server.DeleteFriend(UnstuckME.User.UserID, Friend.UserID);
            foreach (OnlineUser user in UnstuckME.MainWindow.OnlineUsersStack.Children.OfType<OnlineUser>())
            {
                if(user.Friend.UserID == Friend.UserID)
                {
                    temp = user;
                }
            }
            foreach (ContactCreateConversation contact in UnstuckME.Pages.ChatPage.StackPanelAddContacts.Children.OfType< ContactCreateConversation>())
            {
                if(contact.Contact.UserID == Friend.UserID)
                {
                    temp2 = contact;
                }
            }
            if(temp != null)
            {
                UnstuckME.MainWindow.OnlineUsersStack.Children.Remove(temp);
                UnstuckME.FriendsList.Remove(temp.Friend);
                if(temp2 != null)
                {
                    UnstuckME.Pages.ChatPage.StackPanelAddContacts.Children.Remove(temp2);
                }
            }
        }

        private void ButtonRemoveFriend_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonRemoveFriend.Background = Brushes.IndianRed;
        }

        private void ButtonRemoveFriend_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonRemoveFriend.Background = UnstuckME.Red;
        }

        private void ButtonSendMessage_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonSendMessage.Background = Brushes.LimeGreen;
        }

        private void ButtonSendMessage_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonSendMessage.Background = Brushes.ForestGreen;
        }

        private void ButtonSendMessage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
            UnstuckME.MainWindow.SwitchToChatTab();
            UnstuckME.Pages.ChatPage.StartNewConversation(Friend.UserID);
        }
    }
}
