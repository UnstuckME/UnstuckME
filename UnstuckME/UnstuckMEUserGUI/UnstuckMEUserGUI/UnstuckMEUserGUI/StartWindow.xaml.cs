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

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string email = UserNameTxtBx.Text;
            string password = passwordBox.Password;
            bool isValid = false;

            //Opens a connection to UnstuckME Server.
            //ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");
            //IUnstuckMEService proxy = channelFactory.CreateChannel();

            
            if (email == "")
            {
                MessageBox.Show("Please Enter a Valid Email");
            }
            else if (password == "")
            {
                MessageBox.Show("Please Enter a Password");

            }
            else
            {   
                //Calls UnstuckME Server Function that checks email credentials
                isValid = Server.UserLoginAttempt(email, password);
                //if valid login
                if (isValid)
                { 

                    Window disp = new MainWindow(Server.GetUserID(email), Server); // this will crash without valid login info
                    disp.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login Info Incorrect");
                
                }
            }
            
            
            
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            Window disp = new NewAccountSetupWindow(Server);
            disp.Show();
            this.Close();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Window disp = new UserLoginSettingsWindow();
            disp.Show();            
        }

        //Handles Enter Being Pressed While in the Password/Username Box
        private void OnKeyDownPasswordHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                LoginBtn_Click(sender, e);
            }
        }

    }
}
