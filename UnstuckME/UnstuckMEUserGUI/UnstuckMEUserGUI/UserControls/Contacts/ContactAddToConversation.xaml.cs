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
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ContactAddToConversation.xaml
    /// </summary>
    public partial class ContactAddToConversation : UserControl
    {
        public UserInfo Contact;
        public ContactAddToConversation(UserInfo inContact)
        {
            InitializeComponent();
            Contact = inContact;
            LabelUsername.Content = Contact.FirstName + " " + Contact.LastName;
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
