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
using System.Windows.Shapes;
using UnstuckME_Classes;
using UnstuckMEInterfaces;

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
                    {
                        throw new Exception("Failure to Kill Server!");
                    }
                    else
                    {
                        _channelFactory.Abort();
                        MainWindow window = new MainWindow(ref Admin);
                        window.Show();
                        this.Close();
                    }
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
            App.Current.MainWindow = deleteAdmin;
        }

        private void ChangeCredintials_Click(object sender, RoutedEventArgs e)
        {
            AdminCredChange adminChange = new AdminCredChange(ref Admin);
            adminChange.ShowDialog();
            //labelEmailAddress.Content = Admin.EmailAddress;
            App.Current.MainWindow = adminChange;
        }

        private void CreateAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminCreation adminCreate = new AdminCreation(ref Admin);
            adminCreate.Show();
            App.Current.MainWindow = adminCreate;
        }

        private void ChangeFirstLastName_Click(object sender, RoutedEventArgs e)
        {
            AdminNameChange nameChange = new AdminNameChange(ref Admin);
            App.Current.MainWindow = nameChange;
            nameChange.ShowDialog();
            //labelName.Content = Admin.FirstName + " " + Admin.LastName;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                Server.AdminLogout();
                _channelFactory.Abort();
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
            this.Hide();
            loginWindow.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            _channelFactory.Abort();
            this.Close();
        }

        private void ShutdownAndExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to shutdown the server?", "Sever Shutdown", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if(result == MessageBoxResult.Yes)
            {
                this.Hide();
                Server.AdminLogMessage("Exit and Shutdown Initiated.");
                Server.AdminLogout();
                KillServer();
                _channelFactory.Abort();
                this.Close();
            }
        }

        public void AddUser(string emailAddress, int privileges)
        {
			string priv = string.Empty;

			switch (privileges)
			{
				case '1':
					priv = "U"; //User
					break;
				case '2':
					priv = "M"; //Moderator
					break;
				case '3':
					priv = "A"; //Administrator
					break;
				default:
					//broadcast to disabled user they can't log in
					break;
			}

			TextBlock newUser = new TextBlock();
			newUser.Text = priv + ' ' + emailAddress;
			newUser.FontSize = 14;
            StackPanelOnlineUsers.Children.Add(newUser);
            labelOnlineUsers.Content = "Online Users: " + StackPanelOnlineUsers.Children.Count;
        }
        public void RemoveUser(string emailAddress)
        {
            foreach (TextBlock block in StackPanelOnlineUsers.Children)
            {
                if (block.Text == emailAddress)
                {
                    StackPanelOnlineUsers.Children.Remove(block);
                    labelOnlineUsers.Content = "Online Users: " + StackPanelOnlineUsers.Children.Count;
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
                AddUser(user.EmailAddress, user.Privileges);
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
    }
}
