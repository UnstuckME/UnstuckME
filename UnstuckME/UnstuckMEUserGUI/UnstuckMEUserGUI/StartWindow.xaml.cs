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
using System.ServiceModel;
using UnstuckMEInterfaces;
using System.Configuration;
using System.Threading;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public static IUnstuckMEService Server;
        private static DuplexChannelFactory<IUnstuckMEService> _channelFactory;

        public StartWindow()
        {
            InitializeComponent();
            _channelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
            Server = _channelFactory.CreateChannel();
			_mainFrame.Navigate(new LoginPage(ref Server));
        }

		public void MessageBoxToUserAndShutdown(int messageStyle, string message)
        {
            try
            {
                switch (messageStyle)
                {
                    case 0: //Blue Information (i) Message
                        {
                            MessageBox.Show(message, "Message From Server", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            break;
                        }
                    case 1: //Yellow Warning Message
                        {
                            MessageBox.Show(message, "Message From Server", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            break;
                        }
                    case 2: //Red Error message.
                        {
                            MessageBox.Show(message, "Message From Server", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                    default:
                        {
                            MessageBox.Show(message, "Case Statment Not Working", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                        }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

			_mainFrame.Navigate(new LoginPage(ref Server));
        }

        private void UnstuckME_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
			try
			{
                this.Hide();
				Server.Logout();
			}
			catch (Exception)
			{
                this.Show();
            }
        }
    }
}
