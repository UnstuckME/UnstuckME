﻿using System;
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

            //Opens a connection to UnstuckME Server.
            ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");
            IUnstuckMEService proxy = channelFactory.CreateChannel();

            //Calls UnstuckME Server Function that checks email credentials
            isValid = proxy.UserLoginAttempt(email, password);

            if(isValid)
            {

            }
        }

        private void button_CreateNewUser_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// THIS FUNCTION IS SOLEY FOR TESTING STORED PROCEDURE RESULTS FROM SERVER
        /// </summary>
        private void button_StoredProc_Click(object sender, RoutedEventArgs e)
        {
            //Opens a connection to UnstuckME Server.
            ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");
            IUnstuckMEService proxy = channelFactory.CreateChannel();
            int test = 4;
            test = proxy.GetUserID("ajclark@oit.edu");
            System.Windows.MessageBox.Show("UserID = " + test.ToString());

        }
    }
}
