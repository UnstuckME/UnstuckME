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
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
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
            menu.Activate();
            menu.Focus();
            menu.ShowDialog();
        }
    }
}
