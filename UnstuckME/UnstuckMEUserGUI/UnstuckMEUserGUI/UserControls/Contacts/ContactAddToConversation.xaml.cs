using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UnstuckMeLoggers;
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
                using (MemoryStream ms = new MemoryStream())
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
                UnstuckMEMessage temp = new UnstuckMEMessage
                {
                    ChatID = UnstuckME.CurrentChatSession.ChatID,
                    FilePath = string.Empty,
                    Message = Contact.UserName + " has joined the conversation!",
                    MessageID = 0,
                    SenderID = UnstuckME.User.UserID,
                    Username = UnstuckME.User.FirstName,
                    UsersInConvo = new List<int>()
                };
                UnstuckME.CurrentChatSession.Users.Add(Contact);

                foreach (UnstuckMEChatUser user in UnstuckME.CurrentChatSession.Users)
                    temp.UsersInConvo.Add(user.UserID);
                foreach (Conversation convo in UnstuckME.Pages.ChatPage.StackPanelConversations.Children.OfType<Conversation>())
                {
                    if (convo.Chat.ChatID == UnstuckME.CurrentChatSession.ChatID)
                    {
                        convo.Chat.Users = UnstuckME.CurrentChatSession.Users;
                        convo.SetConversationLabel();
                        UnstuckME.Pages.ChatPage.LabelConversationName.Content = convo.ConvoLabelText.Text;
                    }
                }

                temp.MessageID = UnstuckME.Server.SendMessage(temp);
                UnstuckME.Pages.ChatPage.AddMessage(temp);
                //Removes Sticker From Stack Panel
                ((StackPanel) Parent).Children.Remove(this);
            }
            catch (Exception ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
        }
    }
}