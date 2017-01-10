using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
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
            _channelFactory = new DuplexChannelFactory<IUnstuckMEServer>(new ServerCallback(), "UnstuckMEServerEndPoint");
            Server = _channelFactory.CreateChannel();
            Admin = passedInAdmin;
            Server.RegisterServerAdmin(Admin);
        }

        private void buttonKill_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to Kill the server?", "Server Kill Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Server.AdminLogMessage("Server Kill Attempt.");
                    Server.AdminLogout();
                    bool retVal = KillServer();
                    if (!retVal)
                    {
                        throw new Exception("Failure to Kill Server!");
                    }
                    else
                    {
                        MainWindow window = new MainWindow(ref Admin);
                        App.Current.MainWindow = window;
                        this.Close();
                        window.Show();
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
            //Process[] pname = Process.GetProcessesByName("UnstuckMEServer");
            //if (pname.Length > 0)
            //{
            //    Server.AdminLogout();
            //}
        }
    }
}
