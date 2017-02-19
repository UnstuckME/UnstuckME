using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using UnstuckMEInterfaces;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UnstuckMEWindow.xaml
    /// </summary>
    public partial class UnstuckMEWindow : Window
    {
        public static IUnstuckMEService Server;
        public static UserInfo User;
        public enum Privileges { InvalidUser, Admin, Moderator, User }
        public static UnstuckMEPages _pages = new UnstuckMEPages();
        private static Privileges userPriviliges;
        public UnstuckMEWindow thisWindow;

        public static Brush _UnstuckMEBlue;
        public static Brush _UnstuckMERed;

        public UnstuckMEWindow(ref IUnstuckMEService inServer, ref UserInfo inUser)
        {
            InitializeComponent();
            _UnstuckMERed = StickerButtonBorder.Background;
            _UnstuckMEBlue = ChatButtonBorder.Background;
            thisWindow = this;
            Server = inServer;
            User = inUser;
            UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_LOGIN, User.EmailAddress);
        }

        async private void Window_ContentRendered(object sender, EventArgs e)
        {
            _pages.StickerPage = new StickerPage();
            _pages.SettingsPage = new SettingsPage();
            _pages.ChatPage = new ChatPage(ref User, ref Server);
            _pages.UserProfilePage = new UserProfilePage();
            _pages.ModeratorPage = new ModeratorPage();
            _pages.AdminPage = new AdminPage();

            //userPriviliges = (Privileges)User.Privileges; //This Works But for Design Purposes is commented out.
            userPriviliges = Privileges.Admin; //This will be replaced with above line

            CheckAdminPrivledges(userPriviliges); //Disables/Enables Admin/Moderator Tab Depending on Privilege
            SwitchToStickerTab();
            try
            {
                await Task.Factory.StartNew(() => LoadStickerPageAsync());
                await Task.Factory.StartNew(() => LoadChatPageAsync());
                await Task.Factory.StartNew(() => LoadUserProfilePageAsync());
                await Task.Factory.StartNew(() => LoadSettingsPageAsync());
                await Task.Factory.StartNew(() => LoadAdminPageAsync());
                await Task.Factory.StartNew(() => LoadSideBarsAsync());

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Source: " + ex.Source + "\nMessage: " + ex.Message);
            }
        }

        ///////////////////////////////////////////////ASYNCRONOUS LOADING SECTION/////////////////////////////////////////////////////////////////
        private void LoadStickerPageAsync()
        {
            this.Dispatcher.Invoke(() =>
            {
                _pages.StickerPage.AvailableStickers = Server.InitialAvailableStickerPull(User.UserID);
                foreach (UnstuckMEAvailableSticker sticker in _pages.StickerPage.AvailableStickers)
                {
                    _pages.StickerPage.StackPanelAvailableStickers.Children.Add(new AvailableSticker(sticker));
                }
            });
        }

        private void LoadChatPageAsync()
        {
            this.Dispatcher.Invoke(() =>
            {
                _pages.ChatPage.allChats = Server.GetUserChats(User.UserID);
                foreach (UnstuckMEChat chat in _pages.ChatPage.allChats)
                {
                    _pages.ChatPage.AddConversation(chat);
                }
            });
        }

        private void LoadUserProfilePageAsync()
        {
            this.Dispatcher.Invoke(() =>
            {
                ImageSourceConverter ic = new ImageSourceConverter();
                _pages.UserProfilePage.ProfilePicture.Source = ic.ConvertFrom(User.UserProfilePictureBytes) as ImageSource;  //convert image so it can be displayed
                _pages.UserProfilePage.FirstName.Text = User.FirstName;
                _pages.UserProfilePage.LastName.Text = User.LastName;
                _pages.UserProfilePage.EmailAddress.Text = User.EmailAddress;
                _pages.UserProfilePage.SetStudentRating(User.AverageStudentRank);
                _pages.UserProfilePage.SetTutorRating(User.AverageTutorRank);
                _pages.UserProfilePage.RepopulateClasses();
            });
        }

        private void LoadSettingsPageAsync()
        {
            this.Dispatcher.Invoke(() =>
            {
                // your asynchronous code here.
            });
        }

        private void LoadAdminPageAsync()
        {
            this.Dispatcher.Invoke(() =>
            {
                // your asynchronous code here.
            });
        }

        private void LoadSideBarsAsync()
        {
            this.Dispatcher.Invoke(() =>
            {
                for (int i = 0; i < 30; i++)
                {
                    OnlineUsersStack.Children.Add(new OnlineUser("Hello User " + i));
                }

                for (int i = 0; i < 30; i++)
                {
                    //AvailableStickersStack.Children.Add(new NewMessageNotification("User " + i, ref _pages, this));
                    AvailableStickersStack.Children.Add(new AvailableStickerNotification("CST 11" + i));
                }
            });
        }

        /////////////////////////////////////////////////////END ASYNCHRONOUS LOADING SECTION///////////////////////////////////////////////////////////////
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

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToAdminTab(userPriviliges);
        }

        public void CheckAdminPrivledges(Privileges inPrivleges)
        {
            switch (inPrivleges)
            {
                case Privileges.Admin: //Admin
                    {
                        AdminButtonBorder.Visibility = Visibility.Visible;
                        AdminButtonBorder.IsEnabled = true;
                        break;
                    }
                case Privileges.Moderator: //Moderator
                    {
                        AdminButtonBorder.Visibility = Visibility.Visible;
                        AdminButtonBorder.IsEnabled = true;
                        break;
                    }
                case Privileges.User: //User
                    {
                        AdminButtonBorder.Visibility = Visibility.Hidden;
                        AdminButtonBorder.IsEnabled = false;
                        break;
                    }
            }
        }

        public void SwitchToChatTab()
        {
            MainFrame.Navigate(_pages.ChatPage);
            ChatButtonBorder.Background = _UnstuckMERed;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            UserProfileButtonBorder.Background = _UnstuckMEBlue;
            AdminButtonBorder.Background = _UnstuckMEBlue;
            DisableStickerSubmit();
        }
        public void SwitchToStickerTab()
        {
            MainFrame.Navigate(_pages.StickerPage);
            ChatButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMERed;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            UserProfileButtonBorder.Background = _UnstuckMEBlue;
            AdminButtonBorder.Background = _UnstuckMEBlue;
            EnableStickerSubmit();
        }
        public void SwitchToUserProfileTab()
        {
            MainFrame.Navigate(_pages.UserProfilePage);
            ChatButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            UserProfileButtonBorder.Background = _UnstuckMERed;
            AdminButtonBorder.Background = _UnstuckMEBlue;
            DisableStickerSubmit();
        }
        public void SwitchToSettingsTab()
        {
            MainFrame.Navigate(_pages.SettingsPage);
            ChatButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMERed;
            UserProfileButtonBorder.Background = _UnstuckMEBlue;
            AdminButtonBorder.Background = _UnstuckMEBlue;
            DisableStickerSubmit();
        }
        public void SwitchToAdminTab(Privileges inPriviledges)
        {
            switch (inPriviledges)
            {
                case Privileges.Admin: //Admin
                    {
                        MainFrame.Navigate(_pages.AdminPage);
                        break;
                    }
                case Privileges.Moderator: //Moderator
                    {
                        MainFrame.Navigate(_pages.ModeratorPage);
                        break;
                    }
                default: //In case someone figures out a way to make admin button show
                    {
                        MessageBox.Show("You do not have access to this tab.", "Unauthorized Access", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
            }
            ChatButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            UserProfileButtonBorder.Background = _UnstuckMEBlue;
            AdminButtonBorder.Background = _UnstuckMERed;
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
            StickerCreationWindow window = new StickerCreationWindow(ref Server, ref User);
            window.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_LOGOUT, User.EmailAddress);
            try
            {
                Server.Logout();
            }
            catch(Exception)
            { /*This is empty because we don't want the program to break but we also don't want to catch this exception*/ }
        }

        public void RecieveChatMessage(UnstuckMEMessage message)
        {
            AvailableStickersStack.Children.Add(new NewMessageNotification("User " + message.SenderID, ref _pages, this));
            _pages.ChatPage.AddMessage(message);
        }
    }

    public class UnstuckMEPages
    {
        public StickerPage StickerPage { get; set; }
        public SettingsPage SettingsPage { get; set; }
        public UserProfilePage UserProfilePage { get; set; }
        public ChatPage ChatPage { get; set; }
        public AdminPage AdminPage { get; set; }
        public ModeratorPage ModeratorPage { get; set; }
    }
}
