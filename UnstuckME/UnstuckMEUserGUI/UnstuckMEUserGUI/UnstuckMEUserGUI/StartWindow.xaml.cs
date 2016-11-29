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

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = UserNameTxtBx.Text;
            string password = passwordBox.Password;
            bool isValid = false;

            //Opens a connection to UnstuckME Server.
            ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");
            IUnstuckMEService proxy = channelFactory.CreateChannel();

            //Calls UnstuckME Server Function that checks email credentials
            isValid = proxy.UserLoginAttempt(email, password);
            //if valid login
            if (isValid)
            {
                Window disp = new MainWindow();
                disp.Show();
                this.Close();
            }
            
            
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            Window disp = new NewAccountSetupWindow();
            disp.Show();
            this.Close();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Window disp = new UserLoginSettingsWindow();
            disp.Show();            
        }
    }
}
