using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;
using Application = System.Windows.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

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
            PresentationSource presentationSource = PresentationSource.FromVisual(this);
            if (presentationSource?.CompositionTarget != null)
            {
                var transform = presentationSource.CompositionTarget.TransformFromDevice;
                var mouse = transform.Transform(GetMousePosition());
                Left = mouse.X - ActualWidth;
                Top = mouse.Y - ActualHeight;
            }
        }

        public Point GetMousePosition()
        {
            System.Drawing.Point point = Control.MousePosition;
            return new Point(point.X, point.Y);
        }

        private void ButtonAddContact_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelInvalidUser.Visibility = Visibility.Hidden;
            try
            {
                if(TextBoxUsername.Text == UnstuckME.User.EmailAddress)
                    throw new Exception();
                if (UnstuckME.Server.IsValidUser(TextBoxUsername.Text))
                {
                    UnstuckMEChatUser temp = new UnstuckMEChatUser
                    {
                        UserID = UnstuckME.Server.GetUserID(TextBoxUsername.Text)
                    };

                    foreach (var friend in UnstuckME.FriendsList)
                    {
                        if (friend.UserID == temp.UserID)
                            throw new Exception();
                    }
                    temp.UserName = UnstuckME.Server.GetUserDisplayName(temp.UserID);

                    using (MemoryStream ms = new MemoryStream())
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
                    throw new Exception();
            }
            catch(Exception)
            {
                LabelInvalidUser.Visibility = Visibility.Visible;
            }
        }

        private void AddContactWindow_Deactivated(object sender, EventArgs e)
        {
            Close();
        }

        private void AddContactWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MoveBottomRightEdgeOfWindowToMousePosition();
        }

        private void TextBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                ButtonAddContact_MouseLeftButtonDown(null, null);
        }
    }
}
