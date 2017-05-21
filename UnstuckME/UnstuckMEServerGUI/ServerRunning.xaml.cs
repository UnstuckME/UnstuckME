using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckMEInterfaces;
using UnstuckME_Classes;

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for ServerRunning.xaml
    /// </summary>
    public partial class ServerRunning : Window
    {
        public static AdminInfo Admin;
        public static IUnstuckMEServer Server;
        private static DuplexChannelFactory<IUnstuckMEServer> _channelFactory;
        public ServerRunning(ref AdminInfo passedInAdmin)
        {
            InitializeComponent();
            Admin = passedInAdmin;
        }

        private void buttonKill_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Kill the server?", "Server Kill Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Server.AdminServerShuttingDown();
                    Server.AdminLogMessage("Server Kill Attempt.");
                    Server.AdminLogout();
                    bool retVal = KillServer();
                    if (!retVal)
                        throw new Exception("Failure to Kill Server!");
                    _channelFactory.Abort();
                    MainWindow window = new MainWindow(ref Admin);
                    window.Show();
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Kill Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Server.RegisterServerAdmin(Admin);
                    Server.AdminLogMessage("Server Kill Failure, Relogged Admin");
                }
            }
        }

        private bool KillServer()
        {
            try
            {
                Process[] pname = Process.GetProcessesByName("UnstuckMEServer");
                pname[0].Kill();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Server Kill Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void DeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            DeleteAdmin deleteAdmin = new DeleteAdmin(Admin);
            deleteAdmin.Show();
            Application.Current.MainWindow = deleteAdmin;
        }

        private void ChangeCredintials_Click(object sender, RoutedEventArgs e)
        {
            AdminCredChange adminChange = new AdminCredChange(ref Admin);
            adminChange.ShowDialog();
            //labelEmailAddress.Content = Admin.EmailAddress;
            Application.Current.MainWindow = adminChange;
        }

        private void CreateAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminCreation adminCreate = new AdminCreation(ref Admin);
            adminCreate.Show();
            Application.Current.MainWindow = adminCreate;
        }

        private void ChangeFirstLastName_Click(object sender, RoutedEventArgs e)
        {
            AdminNameChange nameChange = new AdminNameChange(ref Admin);
            Application.Current.MainWindow = nameChange;
            nameChange.ShowDialog();
            //labelName.Content = Admin.FirstName + " " + Admin.LastName;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Server.AdminLogout();
                _channelFactory.Close();
            }
            catch(Exception)
            {
                //This occurs when the administrator kills the server and the window closes. Server Unreachable.
            }

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Server.AdminLogout();
            _channelFactory.Abort();
            ServerLogin loginWindow = new ServerLogin();
            Hide();
            loginWindow.Show();
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _channelFactory.Abort();
            Close();
        }

        private void ShutdownAndExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to shutdown the server?", "Sever Shutdown", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if(result == MessageBoxResult.Yes)
            {
                Hide();
                Server.AdminLogMessage("Exit and Shutdown Initiated.");
                Server.AdminLogout();
                KillServer();
                _channelFactory.Abort();
                Close();
            }
        }

        public void AddUser(string emailAddress, Privileges privileges)
        {
			string text = string.Empty;
            TextBlock newUser = new TextBlock();

            switch (privileges)
			{
				case Privileges.User:
                    text = "U";
                    newUser.Foreground = Brushes.LightGreen;
                    break;
				case Privileges.Moderator:
                    text = "M";
                    newUser.Foreground = Brushes.Blue;
                    break;
				case Privileges.Admin:
                    text = "A";
                    newUser.Foreground = Brushes.Red;
                    break;
				default:    //broadcast to disabled user they can't log in
                    List<string> recipients = new List<string>();
                    recipients.Add(emailAddress);
                    Server.AdminSendMessageToUsers(recipients, "You account has been disabled. If you wish to reactivate your account, please contact an administrator.");
                    throw new Exception();
			}

            newUser.Text = text + " " + emailAddress;
			newUser.FontSize = 14;
            StackPanelOnlineUsers.Children.Add(newUser);
            labelOnlineUsers.Content = "Online Users: " + StackPanelOnlineUsers.Children.Count;


            if (selectusersGrid.Visibility == Visibility.Visible)
            {
                ListBoxItem item = new ListBoxItem
                {
                    Background = Brushes.DimGray,
                    Foreground = Brushes.White,
                    Content = emailAddress
                };

                item.Selected += selectAnyUser_Selected;
                comboboxOnlineUsers.Items.Add(item);
            }
        }

        public void RemoveUser(string emailAddress)
        {
            foreach (TextBlock block in StackPanelOnlineUsers.Children)
            {
                if (block.Text.Contains(emailAddress))
                {
                    StackPanelOnlineUsers.Children.Remove(block);
                    labelOnlineUsers.Content = "Online Users: " + StackPanelOnlineUsers.Children.Count;

                    if (selectusersGrid.Visibility == Visibility.Visible)
                    {
                        foreach (ListBoxItem item in comboboxOnlineUsers.Items)
                        {
                            if (item.Content.ToString() == emailAddress)
                            {
                                comboboxOnlineUsers.Items.Remove(item);
                                break;
                            }
                        }
                    }

                    return; //This Return Is Needed for some reason, otherwise ServerGuiBreaks.
                }
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            _channelFactory = new DuplexChannelFactory<IUnstuckMEServer>(new ServerCallback(), "UnstuckMEServerEndPoint");
            Server = _channelFactory.CreateChannel();
            Server.RegisterServerAdmin(Admin);

            List<UserInfo> userList = Server.AdminGetAllOnlineUsers();

            foreach (var user in userList)
            {
                try
                {
                    AddUser(user.EmailAddress, user.Privileges);
                }
                catch (Exception)
                { }
            }
        }

        private void AddMentorOrganization_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteMentorOrganization_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CreateClass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteClass_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MessageToAllUsers_Click(object sender, RoutedEventArgs e)
        {
            selectusersGrid.Visibility = Visibility.Visible;

            foreach (TextBlock user in StackPanelOnlineUsers.Children)
            {
                ListBoxItem item = new ListBoxItem
                {
                    Background = Brushes.DimGray,
                    Foreground = Brushes.White,
                    Height = 25,
                    Content = user.Text.Split(' ')[1]
                };

                item.Selected += selectAnyUser_Selected;
                comboboxOnlineUsers.Items.Add(item);
            }
        }

        private void backgroundcanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //ListBoxItem item = selectAllUsers;
            comboboxOnlineUsers.Items.Clear();
            comboboxOnlineUsers.Items.Add(selectAllUsers);

            textboxMessage.Text = string.Empty;
            selectusersGrid.Visibility = Visibility.Collapsed;
        }

        private void buttonSendMessage_Click(object sender, RoutedEventArgs e)
        {
            List<string> recipients = new List<string>();
            if (comboboxOnlineUsers.SelectedItems.Contains(selectAllUsers))
            {
                foreach (TextBlock client in StackPanelOnlineUsers.Children)
                    recipients.Add(client.Text.Split(' ')[1]);
            }
            else
            {
                foreach (ListBoxItem user in comboboxOnlineUsers.SelectedItems)
                    recipients.Add(user.Content.ToString());
            }

            Server.AdminSendMessageToUsers(recipients, textboxMessage.Text);
            backgroundcanvas_MouseDown(sender, e as MouseButtonEventArgs);
        }

        private void buttonCancelMessage_Click(object sender, RoutedEventArgs e)
        {
            backgroundcanvas_MouseDown(sender, e as MouseButtonEventArgs);
        }

        private void selectAllUsers_Selected(object sender, RoutedEventArgs e)
        {
            comboboxOnlineUsers.SelectedItems.Clear();
            comboboxOnlineUsers.SelectedItem = selectAllUsers;
        }

        private void selectAnyUser_Selected(object sender, RoutedEventArgs e)
        {
            if (comboboxOnlineUsers.SelectedItems.Contains(selectAllUsers))
                comboboxOnlineUsers.SelectedItems.Remove(selectAllUsers);
        }
    }
}                                             