using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
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
        public static DuplexChannelFactory<IUnstuckMEService> _channelFactory;
        public static UserInfo User;
        public static UnstuckMEPages _pages = new UnstuckMEPages();
        private static Privileges userPrivileges;
        public UnstuckMEWindow thisWindow;
        public static List<UnstuckMEChatUser> FriendsList;


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
            _pages.UserProfilePage = new UserProfilePage(this, Server, ref User);
            _pages.ModeratorPage = new ModeratorPage();
            _pages.AdminPage = new AdminPage(ref Server);

            userPrivileges = (Privileges)User.Privileges; //This Works But for Design Purposes is commented out.
            //userPrivileges = Privileges.Admin; //This will be replaced with above line

            CheckAdminPrivledges(userPrivileges); //Disables/Enables Admin/Moderator Tab Depending on Privilege
            SwitchToStickerTab();
            try
            {
                await Task.Factory.StartNew(() => LoadStickerPageAsync());
                await Task.Factory.StartNew(() => LoadSideBarsAsync());
                await Task.Factory.StartNew(() => LoadChatPageAsync());
                await Task.Factory.StartNew(() => LoadUserProfilePageAsync());
                await Task.Factory.StartNew(() => LoadSettingsPageAsync());
                await Task.Factory.StartNew(() => LoadAdminPageAsync());

            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Source: " + ex.Source + "\nMessage: " + ex.Message);
            }
        }

        #region Asynchronous Loading Section
        private void LoadStickerPageAsync()
        {
            this.Dispatcher.Invoke(() =>
            {
                _pages.StickerPage.AvailableStickers = Server.InitialAvailableStickerPull(User.UserID);

                foreach (UnstuckMEAvailableSticker sticker in _pages.StickerPage.AvailableStickers)
                {
                    _pages.StickerPage.StackPanelAvailableStickers.Children.Add(new AvailableSticker(sticker));
                }
                _pages.StickerPage.MyStickersList = Server.GetUserSubmittedStickersASC(User.UserID, 0, 100, 0, null);
                foreach (UnstuckMESticker sticker in _pages.StickerPage.MyStickersList)
                {
                    _pages.StickerPage.StackPanelMyStickers.Children.Add(new MySticker(sticker));
                }
                _pages.StickerPage.OpenStickers = Server.GetUserTutoredStickersASC(User.UserID, 0, 100, 0, null);
                foreach (UnstuckMESticker sticker in _pages.StickerPage.OpenStickers)
                {
                    _pages.StickerPage.StackPanelOpenStickers.Children.Add(new OpenSticker(sticker));
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
                foreach (UnstuckMEChatUser user in FriendsList)
                {
                    _pages.ChatPage.StackPanelAddContacts.Children.Add(new ContactCreateConversatoin(user));
                }
            });
        }

        private void LoadUserProfilePageAsync()
        {
            this.Dispatcher.Invoke(() =>
            {
                ImageSourceConverter ic = new ImageSourceConverter();
                _pages.UserProfilePage.ProfilePicture.Source = ic.ConvertFrom(User.UserProfilePictureBytes) as ImageSource;  //convert image so it can be displayed
                _pages.UserProfilePage.ImageEditProfilePicture.Source = _pages.UserProfilePage.ProfilePicture.Source;
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
                FriendsList = Server.GetFriends(User.UserID);
                foreach (UnstuckMEChatUser friend in FriendsList)
                {
                    OnlineUsersStack.Children.Add(new OnlineUser(friend));
                }
            });
        }
        #endregion

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

        public void AddUserToContactsStack(UnstuckMEChatUser inChatUser)
        {
            OnlineUsersStack.Children.Add(new OnlineUser(inChatUser));
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            SwitchToAdminTab(userPrivileges);
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
            MainFrame.NavigationService.RemoveBackEntry();
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
            MainFrame.NavigationService.RemoveBackEntry();
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
            MainFrame.NavigationService.RemoveBackEntry();
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
            MainFrame.NavigationService.RemoveBackEntry();
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
            StickerCreationWindow window = new StickerCreationWindow(ref Server, ref User, this);
            window.ShowDialog();
        }

        public void AddStickerToMyStickers(UnstuckMESticker inSticker)
        {
            _pages.StickerPage.StackPanelMyStickers.Children.Add(new MySticker(inSticker));
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_LOGOUT, User.EmailAddress);
            try
            {
                Server.Logout();
            }
            catch (Exception)
            { /*This is empty because we don't want the program to break but we also don't want to catch this exception*/ }
        }

        public void RecieveChatMessage(UnstuckMEMessage message)
        {
            _pages.ChatPage.AddMessage(message);
            if ((_pages.ChatPage.currentChat.ChatID != message.ChatID) || (MainFrame.Content != _pages.ChatPage))
            {
                NotificationStack.Children.Insert(0, new NewMessageNotification(message, ref _pages, this));
            }
        }

        /// <summary>
        /// Currently does nothing with the file
        /// </summary>
        /// <param name="message"></param>
        /// <param name="file"></param>
        public void RecieveChatFile(UnstuckMEMessage message, UnstuckMEFile file)
        {
            if ((_pages.ChatPage.currentChat.ChatID != message.ChatID) || (MainFrame.Content != _pages.ChatPage))
            {
                _pages.ChatPage.AddMessage(message, file);
                NotificationStack.Children.Insert(0, new NewMessageNotification(message, ref _pages, this));
            }
        }

        public void RecieveAddedClass(UserClass inClass)
        {
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    ClassDisplay temp = new ClassDisplay(_pages.UserProfilePage.BottomLeftStack, User.UserID, Server, inClass.CourseCode, inClass.CourseNumber, inClass.CourseCode + " " + inClass.CourseNumber + " " + inClass.CourseName, inClass.ClassID);
                    _pages.UserProfilePage.BottomLeftStack.Children.Add(temp);

                }
                catch (Exception)
                {

                }
            });
        }

        public void RecieveNewAvailableSticker(UnstuckMEAvailableSticker sticker)
        {
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    _pages.StickerPage.AvailableStickers.Add(sticker);
                    _pages.StickerPage.StackPanelAvailableStickers.Children.Add(new AvailableSticker(sticker));
                    //NotificationStack.Children.Add(new AvailableStickerNotification(sticker));  This looks gross currently so I'm not going to use it.
                }
                catch (Exception)
                {
                    MessageBox.Show("Sticker Update Failed");
                }
            });
        }

        public void RemoveStickerFromAvailableStickers(int stickerID)
        {
            this.Dispatcher.Invoke(() =>
            {
                try
                {
                    AvailableSticker temp = null;
                    foreach (var control in _pages.StickerPage.StackPanelAvailableStickers.Children.OfType<AvailableSticker>())
                    {
                        if (control.Sticker.StickerID == stickerID)
                        {
                            temp = control;
                        }
                    }
                    if (temp != null)
                    {
                        _pages.StickerPage.StackPanelAvailableStickers.Children.Remove(temp);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Remove Failed");
                }
            });
        }

        public void StickerAcceptedStartConversation(int studentID, int tutorID)
        {
            this.Dispatcher.Invoke(() =>
            {
                int PreExistingChatID = -1;
                //Search For Pre-Existing Conversation
                foreach (UnstuckMEChat chat in _pages.ChatPage.allChats)
                {
                    bool studentFound = false;
                    bool tutorFound = false;
                    if (chat.Users.Count == 2)
                    {
                        foreach (UnstuckMEChatUser user in chat.Users)
                        {
                            if (user.UserID == studentID)
                            { studentFound = true; }
                            if (user.UserID == tutorID)
                            { tutorFound = true; }
                        }
                        if (studentFound && tutorFound)
                        {
                            PreExistingChatID = chat.ChatID;
                        }
                    }
                }
                if (PreExistingChatID == -1)
                {
                    //Chat Not Found. Creating a new one.
                    PreExistingChatID = Server.CreateChat(User.UserID);
                    Server.InsertUserIntoChat(studentID, PreExistingChatID);
                }
                UnstuckMEMessage temp = new UnstuckMEMessage();
                temp.ChatID = PreExistingChatID;
                temp.FilePath = string.Empty;
                temp.IsFile = false;
                temp.Message = User.FirstName + " " + User.LastName + " has accepted a sticker you submitted!";
                temp.MessageID = 0;
                temp.SenderID = User.UserID;
                temp.Username = User.FirstName;
                temp.UsersInConvo = new List<int>();
                temp.UsersInConvo.Add(User.UserID);
                temp.UsersInConvo.Add(studentID);
                Server.SendMessage(temp);
                temp.Message = "Your have accepted a Sticker!";
                _pages.ChatPage.AddMessage(temp);

                SwitchToChatTab();
                _pages.ChatPage.ButtonAddUserDone_Click(null, null);
                foreach (Conversation convo in _pages.ChatPage.StackPanelConversations.Children.OfType<Conversation>())
                {
                    if (convo.Chat.ChatID == PreExistingChatID)
                    {
                        convo.ConversationUserControl_MouseLeftButtonDown(null, null);
                    }
                }
            });
        }

        private void ButtonLogout_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonLogout.Background = Brushes.IndianRed;
        }

        private void ButtonLogout_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonLogout.Background = _UnstuckMERed;
        }

        private void ButtonLogout_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Server.Logout();
            string unstuckME = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
            Process.Start(unstuckME);
            System.Windows.Application.Current.Shutdown();
        }

        private void ButtonAddContact_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonAddContact.Background = Brushes.LightSteelBlue;
        }

        private void ButtonAddContact_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonAddContact.Background = null;
        }

        private void ButtonAddContact_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AddFriendWindow friendWindow = new AddFriendWindow();
            friendWindow.Show();
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
