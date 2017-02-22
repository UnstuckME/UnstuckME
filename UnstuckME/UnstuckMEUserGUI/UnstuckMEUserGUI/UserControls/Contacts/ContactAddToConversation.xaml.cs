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
        public UnstuckMEChatUser Contact;
        
        public ContactAddToConversation(UnstuckMEChatUser inContact)
        {
            InitializeComponent();
            Contact = inContact;
            LabelUsername.Content = Contact.UserName;
            if(Contact.ProfilePicture == null)
            {
                Contact.ProfilePicture = UnstuckMEWindow._pages.ChatPage.ic.ConvertFrom(UnstuckMEWindow.Server.GetProfilePicture(Contact.UserID)) as ImageSource;
            }
            ImageUserProfilePic.Source = Contact.ProfilePicture;
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            UnstuckMEWindow.Server.InsertUserIntoChat(Contact.UserID, UnstuckMEWindow._pages.ChatPage.currentChat.ChatID);
            UnstuckMEMessage temp = new UnstuckMEMessage();
            temp.ChatID = UnstuckMEWindow._pages.ChatPage.currentChat.ChatID;
            temp.FilePath = string.Empty;
            temp.IsFile = false;
            temp.Message = Contact.UserName + " has joined the conversation!";
            temp.MessageID = 0;
            temp.SenderID = UnstuckMEWindow.User.UserID;
            temp.Username = UnstuckMEWindow.User.FirstName;
            temp.UsersInConvo = new List<int>();
            foreach (UnstuckMEChatUser user in UnstuckMEWindow._pages.ChatPage.currentChat.Users)
            {
                temp.UsersInConvo.Add(user.UserID);
            }
            UnstuckMEWindow._pages.ChatPage.currentChat.Users.Add(Contact);
            UnstuckMEWindow.Server.SendMessage(temp);
            UnstuckMEWindow._pages.ChatPage.AddMessage(temp);
            //Removes Sticker From Stack Panel
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
