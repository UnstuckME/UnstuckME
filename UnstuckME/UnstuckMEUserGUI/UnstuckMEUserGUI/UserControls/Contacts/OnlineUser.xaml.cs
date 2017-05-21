using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for OnlineUser.xaml
    /// </summary>
    public partial class OnlineUser : UserControl
    {
        public UnstuckMEChatUser Friend;
        public OnlineUser(UnstuckMEChatUser inFriend)
        {
            InitializeComponent();
            Friend = inFriend;
            LabelUserName.Content = Friend.UserName;

            if (Friend.ProfilePicture == null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    UnstuckME.FileStream.GetProfilePicture(Friend.UserID).CopyTo(ms);
                    Friend.ProfilePicture = UnstuckME.ImageConverter.ConvertFrom(ms.ToArray()) as ImageSource;
                }
            }

            ImageUser.Source = Friend.ProfilePicture;
        }

        private void UserButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OnlineUserGrid.Background = Brushes.SteelBlue;
        }

        private void UserButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OnlineUserGrid.Background = null;
        }

        private void UserButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            UserMenu menu = new UserMenu(Friend);
            menu.Show();
        }
    }
}
