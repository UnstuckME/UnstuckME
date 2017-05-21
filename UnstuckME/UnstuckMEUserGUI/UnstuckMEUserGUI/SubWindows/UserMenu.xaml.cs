using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        private readonly UnstuckMEChatUser _friend;

        public UserMenu(UnstuckMEChatUser inFriend)
        {
            InitializeComponent();
            Activate();
            Topmost = true;
            Focus();
            _friend = inFriend;
            ProfilePicture.Source = _friend.ProfilePicture;
            LabelUsername.Content = _friend.UserName;
        }

        private void MoveBottomRightEdgeOfWindowToMousePosition()
        {
            PresentationSource presentationSource = PresentationSource.FromVisual(this);
            if (presentationSource?.CompositionTarget != null)
            {
                var transform = presentationSource.CompositionTarget.TransformFromDevice;
                var mouse = transform.Transform(GetMousePosition());
                Left = mouse.X - Width;
                Top = mouse.Y - Width;
            }
        }

        public Point GetMousePosition()
        {
            System.Drawing.Point point = Control.MousePosition;
            return new Point(point.X, point.Y);
        }

        private void MenuWindow_Deactivated(object sender, EventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveBottomRightEdgeOfWindowToMousePosition();
        }

        private void CloseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonRemoveFriend_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
            OnlineUser temp = null;
            ContactCreateConversation temp2 = null;
            UnstuckME.Server.DeleteFriend(UnstuckME.User.UserID, _friend.UserID);

            foreach (OnlineUser user in UnstuckME.MainWindow.OnlineUsersStack.Children.OfType<OnlineUser>())
            {
                if(user.Friend.UserID == _friend.UserID)
                    temp = user;
            }
            foreach (ContactCreateConversation contact in UnstuckME.Pages.ChatPage.StackPanelAddContacts.Children.OfType< ContactCreateConversation>())
            {
                if(contact.Contact.UserID == _friend.UserID)
                    temp2 = contact;
            }

            if (temp != null)
            {
                UnstuckME.MainWindow.OnlineUsersStack.Children.Remove(temp);
                UnstuckME.FriendsList.Remove(temp.Friend);
                if(temp2 != null)
                    UnstuckME.Pages.ChatPage.StackPanelAddContacts.Children.Remove(temp2);
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
            WindowState = WindowState.Minimized;
            UnstuckME.MainWindow.SwitchToChatTab();
            UnstuckME.Pages.ChatPage.StartNewConversation(_friend.UserID);
        }
    }
}
