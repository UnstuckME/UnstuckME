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
    /// Interaction logic for AddFriendWindow.xaml
    /// </summary>
    public partial class AddFriendWindow : Window
    {
        public AddFriendWindow()
        {
            InitializeComponent();
        }

        private void ButtonAddContact_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonAddContact.Background = Brushes.LimeGreen;
        }

        private void ButtonAddContact_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonAddContact.Background = Brushes.ForestGreen;
        }

        private void MoveBottomRightEdgeOfWindowToMousePosition()
        {
            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var mouse = transform.Transform(GetMousePosition());
            Left = mouse.X - this.ActualWidth;
            Top = mouse.Y - ActualHeight;
        }

        public System.Windows.Point GetMousePosition()
        {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new System.Windows.Point(point.X, point.Y);
        }

        private void ButtonAddContact_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelInvalidUser.Visibility = Visibility.Hidden;
            if(UnstuckME.Server.IsValidUser(TextBoxUsername.Text))
            {
                UnstuckMEChatUser temp = new UnstuckMEChatUser();
                temp.UserID = UnstuckME.Server.GetUserID(TextBoxUsername.Text);
                temp.UserName = UnstuckME.Server.GetUserDisplayName(temp.UserID);

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    UnstuckME.FileStream.GetProfilePicture(temp.UserID).CopyTo(ms);
                    temp.ProfilePicture = UnstuckME.ImageConverter.ConvertFrom(ms.ToArray()) as ImageSource;
                }

                UnstuckME.Server.AddFriend(UnstuckME.User.UserID, temp.UserID);
                UnstuckME.FriendsList.Add(temp);
                Application.Current.Windows.OfType<UnstuckMEWindow>().FirstOrDefault().AddUserToContactsStack(temp);
                TextBoxUsername.Text = string.Empty;
                Application.Current.Windows.OfType<UnstuckMEWindow>().FirstOrDefault().Focus();
            }
            else
            {
                LabelInvalidUser.Visibility = Visibility.Visible;
            }
        }

        private void AddContactWindow_Deactivated(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddContactWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MoveBottomRightEdgeOfWindowToMousePosition();
        }

        private void TextBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ButtonAddContact_MouseLeftButtonDown(null, null);
            }
        }
    }
}
