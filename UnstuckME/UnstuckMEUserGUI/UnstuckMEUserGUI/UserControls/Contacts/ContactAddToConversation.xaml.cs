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
            if (Contact.ProfilePicture == null)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    UnstuckME.FileStream.GetProfilePicture(Contact.UserID).CopyTo(ms);
                    Contact.ProfilePicture = UnstuckME.ImageConverter.ConvertFrom(ms.ToArray()) as ImageSource;
                }
            }

            ImageUserProfilePic.Source = Contact.ProfilePicture;
        }

        private void ButtonAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Contact.UserID == UnstuckME.User.UserID)
                    throw new Exception("Cannot Add Yourself!");

                foreach (var user in UnstuckME.CurrentChatSession.Users)
                {
                    if (user.UserID == Contact.UserID)
                        throw new Exception("User Already In Conversation!");
                }

                UnstuckME.Server.InsertUserIntoChat(Contact.UserID, UnstuckME.CurrentChatSession.ChatID);
                UnstuckMEMessage temp = new UnstuckMEMessage();
                temp.ChatID = UnstuckME.CurrentChatSession.ChatID;
                temp.FilePath = string.Empty;
                temp.Message = Contact.UserName + " has joined the conversation!";
                temp.MessageID = 0;
                temp.SenderID = UnstuckME.User.UserID;
                temp.Username = UnstuckME.User.FirstName;
                temp.UsersInConvo = new List<int>();
                UnstuckME.CurrentChatSession.Users.Add(Contact);

                foreach (UnstuckMEChatUser user in UnstuckME.CurrentChatSession.Users)
                {
                    temp.UsersInConvo.Add(user.UserID);
                }

                temp.MessageID = UnstuckME.Server.SendMessage(temp);
                UnstuckME.Pages.ChatPage.AddMessage(temp);
                //Removes Sticker From Stack Panel
                ((StackPanel)this.Parent).Children.Remove(this);
            }
            catch (Exception)
            {

            }
        }
    }
}