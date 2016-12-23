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
/*
            // check the config file and see if this program is linked to a school
            var appSettings = ConfigurationManager.AppSettings;
            string associatedSchool = appSettings["AssociatedSchool"] ?? "Not Found";

            // if linked display school logo
            if (associatedSchool != "Not Found")
            {
                
            }
            // if not linked display the settings window
            else
            {
                Window disp = new UserLoginSettingsWindow();
                disp.Show();
            } 
            */
        }
    }
}
