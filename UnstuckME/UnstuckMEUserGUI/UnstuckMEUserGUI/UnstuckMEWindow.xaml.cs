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

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UnstuckMEWindow.xaml
    /// </summary>
    public partial class UnstuckMEWindow : Window
    {
        public static UnstuckMEPages _pages = new UnstuckMEPages();

        private static Brush _UnstuckMEBlue;
        private static Brush _UnstuckMERed;
        public UnstuckMEWindow()
        {
            InitializeComponent();
            _UnstuckMERed = StickerButtonBorder.Background;
            _UnstuckMEBlue = ChatButtonBorder.Background;
 
            _pages.StickerPage = new StickerPage();
            _pages.SettingsPage = new SettingsPage();
            _pages.ChatPage = new ChatPage();
            _pages.UserProfilePage = new UserProfilePage();

            for (int i = 0; i < 30; i++)
            {
                OnlineUsersStack.Children.Add(new OnlineUser("Hello User " + i));
            }

            for (int i = 0; i < 30; i++)
            {
                AvailableStickersStack.Children.Add(new NewMessageNotification("User " +i, ref _pages, this));
                AvailableStickersStack.Children.Add(new AvailableSticker("CST 11" + i));
            }

            SwitchToStickerTab();
        }

        private void StickerButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToStickerTab();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToSettingsTab();
        }

        private void ChatButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToChatTab();
        }

        private void UserProfileButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToUserProfileTab();
        }

        public void SwitchToChatTab()
        {
            MainFrame.Navigate(_pages.ChatPage);
            ChatButtonBorder.Background = _UnstuckMERed;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            UserProfileButtonBorder.Background = _UnstuckMEBlue;
            DisableStickerSubmit();
        }
        public void SwitchToStickerTab()
        {
            MainFrame.Navigate(_pages.StickerPage);
            ChatButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMERed;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            UserProfileButtonBorder.Background = _UnstuckMEBlue;
            EnableStickerSubmit();
        }
        public void SwitchToUserProfileTab()
        {
            MainFrame.Navigate(_pages.UserProfilePage);
            ChatButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            UserProfileButtonBorder.Background = _UnstuckMERed;
            DisableStickerSubmit();
        }
        public void SwitchToSettingsTab()
        {
            MainFrame.Navigate(_pages.SettingsPage);
            ChatButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMERed;
            UserProfileButtonBorder.Background = _UnstuckMEBlue;
            DisableStickerSubmit();
        }

        private void DisableStickerSubmit()
        {
            CreateStickerButtonBorder.Visibility = Visibility.Hidden;
            CreateStickerButtonBorder.IsEnabled = false;
        }
        private void EnableStickerSubmit()
        {
            CreateStickerButtonBorder.Visibility = Visibility.Visible;
            CreateStickerButtonBorder.IsEnabled = true;
        }

        private void CreateStickerButton_Click(object sender, RoutedEventArgs e)
        {
            StickerCreationWindow window = new StickerCreationWindow();
            window.ShowDialog();
        }

        private void DisableAdmin()
        {
            //I'll Implement this later
        }
        private void EnableAdmin()
        {
            //I'll Implement this later
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            //Case Statement for Admin/Moderator Status.
        }
    }

    public class UnstuckMEPages
    {
        public  StickerPage StickerPage { get; set; }
        public  SettingsPage SettingsPage { get; set; }
        public UserProfilePage UserProfilePage { get; set; }
        public ChatPage ChatPage { get; set; }
    }
}
