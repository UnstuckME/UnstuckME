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

namespace UnstuckMEClientWPF
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

        /// <summary>
        /// This function is purely for testing stored procedures
        /// </summary>
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Beginnings of a function to check login information.
        /// </summary>
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = textBoxEmailAddress.Text;
            string password = passwordBox.Password;
            bool isValid = false;

            //Opens a connection to UnstuckME Server.
            ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");
            IUnstuckMEService proxy = channelFactory.CreateChannel();

            //Calls UnstuckME Server Function that checks email credentials
            isValid = proxy.UserLoginAttempt(email, password);

            if (isValid)
            {

            }
        }
    }
}
