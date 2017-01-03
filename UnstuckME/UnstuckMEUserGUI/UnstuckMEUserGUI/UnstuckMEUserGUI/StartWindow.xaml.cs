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
			_mainFrame.Navigate(new LoginPage(Server));
        }

        private void UnstuckME_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
			try
			{
				Server.Logout();
			}
			catch (Exception)
			{ }
        }
    }
}
