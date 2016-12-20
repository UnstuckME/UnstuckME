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
using System.Diagnostics;
using System.IO;
using UnstuckMEServer;
using UnstuckMEInterfaces;
using System.ServiceModel;
using UnstuckME_Classes;

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public static IUnstuckMEServer Server;
        private static DuplexChannelFactory<IUnstuckMEServer> _channelFactory;
        public static AdminInfo Admin;
        public MainWindow(AdminInfo currentAdmin)
        {
            try
            {
                InitializeComponent();
                _channelFactory = new DuplexChannelFactory<IUnstuckMEServer>(new ServerCallback(), "UnstuckMEServerEndPoint");
                Server = _channelFactory.CreateChannel();
                Admin = currentAdmin;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            labelEmailAddress.Content = "Email Address: " + Admin.EmailAddress;
            labelName.Content = "Name: " + Admin.FirstName + " " + Admin.LastName;
        }

        /// <summary>
        /// This function is ran when the user clicks the Start Server Button.
        /// </summary>
        private void button_RunServer_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                Process[] pname = Process.GetProcessesByName("UnstuckMEServer");
                if (pname.Length > 0)
                    throw new InvalidOperationException("Server Is Already Running!");
                else
                {                 
                    DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
                    currentDir = currentDir.Parent.Parent.Parent;
                    string serverPath = currentDir.FullName + "/UnstuckMEServer/bin/Release/UnstuckMEServer.exe";
                    Process startServer = new Process();
                    startServer.StartInfo.RedirectStandardOutput = true;
                    startServer.StartInfo.UseShellExecute = false;
                    startServer.StartInfo.CreateNoWindow = true;
                    startServer.StartInfo.Verb = "runas";
                    startServer.StartInfo.FileName = serverPath;
                    startServer.Start();
                    MessageBox.Show("Server Is Now Running.", "Server Startup Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                }
            }
            catch(InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Server Start Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// This function is ran when the user clicks the Kill Server Button.
        /// </summary>
        private void buttonKillServer_Click(object sender, RoutedEventArgs e)
        {
            KillServer();
        }

        private void MenuItemKillAndExit_Click(object sender, RoutedEventArgs e)
        {
            bool killSuccess = KillServer();
            if(killSuccess)
            {
                ExitApplication();
            }
            else
            {
                MessageBoxResult boxResult = MessageBox.Show("Server Shutdown Failure! Would You Like to Exit Anyway?", "Server Shutdown Failure", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(MessageBoxResult.Yes == boxResult)
                {
                    ExitApplication();
                }
            }      
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            ExitApplication();
        }

        private void ExitApplication()
        {
            try
            {
                Application.Current.Shutdown();
            }
            catch(Exception)
            {
                MessageBox.Show("Application Exit Failure", "Exit Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool KillServer()
        {
            bool success = false;
            try
            {
                Process[] pname = Process.GetProcessesByName("UnstuckMEServer");
                if (pname.Length == 0)
                    throw new InvalidOperationException("Server Is Not Running.");
                else
                {
                    MessageBoxResult boxResult = MessageBox.Show("Are you sure you want to shutdown the server?", "Server Shutdown", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (boxResult == MessageBoxResult.Yes)
                    {
                        Process[] server = Process.GetProcessesByName("UnstuckMEServer");
                        server[0].Kill();
                        MessageBox.Show("Server Successfully Shutdown!", "Server Shutdown Success", MessageBoxButton.OK, MessageBoxImage.Warning);
                        success = true;
                    }
                    else
                    {
                        MessageBox.Show("Server Is Still Running.", "Server Shutdown Canceled", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                return success;
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Server Shutdown Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return success;
            }
        }

        private void ChangeCredintials_Click(object sender, RoutedEventArgs e)
        {
            
            AdminCredChange adminChange = new AdminCredChange(Admin);
            adminChange.Show();
            App.Current.MainWindow = adminChange;

        }

        private void CreateAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminCreation adminCreate = new AdminCreation(Admin);
            adminCreate.Show();
            App.Current.MainWindow = adminCreate;
        }

        private void RestartServer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            DeleteAdmin deleteAdmin = new DeleteAdmin(Admin);
            deleteAdmin.Show();
            App.Current.MainWindow = deleteAdmin;
        }
    }
}
