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
        public IUnstuckMEService Server;
        public UnstuckMEChat CurrentChat;
        public List<UserInfo> FriendsList;
        public AddUserToConversationWindow(IUnstuckMEService inServer, UnstuckMEChat inCurrentChat, List<UserInfo> inFriendsList)
        {
            InitializeComponent();
            Server = inServer;
            CurrentChat = inCurrentChat;
            FriendsList = inFriendsList;
            foreach (UserInfo friend in FriendsList)
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
            MessageBox.Show("Button Clicked");
        }
    }
}
