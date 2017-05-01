using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ContactCreateConversatoin.xaml
    /// </summary>
    public partial class ContactCreateConversation : UserControl
    {
        public UnstuckMEChatUser Contact;
        public ContactCreateConversation(UnstuckMEChatUser inContact)
        {
            InitializeComponent();
            Contact = inContact;
            LabelUsername.Content = Contact.UserName;
        }

        private void ButtonConversationStart_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonConversationStart.Background = Brushes.LightSteelBlue;
        }

        private void ButtonConversationStart_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonConversationStart.Background = null;
        }

        private void ButtonConversationStart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UnstuckME.Pages.ChatPage.StartNewConversation(Contact.UserID);
        }
    }
}
