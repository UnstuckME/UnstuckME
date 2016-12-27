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
        }

        private void buttonKill_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Kill Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Server Kill Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
    }
}
