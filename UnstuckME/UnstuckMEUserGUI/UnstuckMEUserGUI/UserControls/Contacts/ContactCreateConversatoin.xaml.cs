﻿using System;
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
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ContactCreateConversatoin.xaml
    /// </summary>
    public partial class ContactCreateConversatoin : UserControl
    {
        public UnstuckMEChatUser Contact;
        public ContactCreateConversatoin(UnstuckMEChatUser inContact)
        {
            InitializeComponent();
            Contact = inContact;
            LabelUsername.Content = Contact.UserName;
        }

        private void ButtonConversationStart_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void ButtonConversationStart_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void ButtonConversationStart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}