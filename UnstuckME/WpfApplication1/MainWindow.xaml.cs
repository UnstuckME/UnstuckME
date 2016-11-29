using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnstuckMEInterfaces;

namespace UnstuckMEServerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = emailAddressTextBox.Text;
            string password = passwordBox.Password;
            bool isValid = false;
            byte[] hashedPassword = Encoding.ASCII.GetBytes(password); //Turns passed in password into bytes.

            //Opens a connection to UnstuckME Server.
            ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");
            IUnstuckMEService proxy = channelFactory.CreateChannel();

            //Calls UnstuckME Server Function that checks email credentials
            isValid = proxy.UserLoginAttempt(email, hashedPassword);

            if(isValid)
            {

            }
        }

        
    }
}
