using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UnstuckMeLoggers;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ChatMessage.xaml
    /// </summary>
    public partial class ChatMessage : UserControl
    {
        public UnstuckMEGUIChatMessage Message;

        public ChatMessage(UnstuckMEGUIChatMessage inMessage)
        {
            InitializeComponent();
            TextBoxUserName.Content = inMessage.ChatMessage.Username;
            Message = inMessage;
            TextBoxChatMessage.Text = inMessage.ChatMessage.Message;
            TextBlockChatMessage.Text = inMessage.ChatMessage.Message;

            ImageProfilePicture.Source = inMessage.ProfilePic;
            EditMessageButton.Visibility = inMessage.ChatMessage.SenderID == UnstuckME.User.UserID ? Visibility.Visible : Visibility.Collapsed;
        }

        internal string Username
        {
            get { return Message.ChatMessage.Username; }
            set
            {
                Message.ChatMessage.Username = value;
                TextBoxUserName.Content = value;
            }
        }

        private void EditMessageButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxChatMessage.Visibility = Visibility.Visible;
            TextBlockChatMessage.Visibility = Visibility.Collapsed;
            TextBoxChatMessage.Focus();
            TextBoxChatMessage.SelectAll();
            buttonSaveChanges.Visibility = Visibility.Visible;
            buttonCancelChanges.Visibility = Visibility.Visible;
        }

        private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnstuckMEMessage editedMessage = new UnstuckMEMessage
                {
                    FilePath = Message.ChatMessage.FilePath,
                    ChatID = Message.ChatMessage.ChatID,
                    Message = TextBoxChatMessage.Text,
                    MessageID = Message.ChatMessage.MessageID,
                    SenderID = Message.ChatMessage.SenderID,
                    Time = Message.ChatMessage.Time,
                    Username = Message.ChatMessage.Username,
                    UsersInConvo = Message.ChatMessage.UsersInConvo
                };

                if (UnstuckME.Server.EditMessage(editedMessage) == Task.FromResult(-1))
                    throw new Exception(string.Format("Failed to edit message {0}", editedMessage.Message));

                Message.ChatMessage.Message = TextBoxChatMessage.Text;
                TextBlockChatMessage.Text = TextBoxChatMessage.Text;
                TextBoxChatMessage.Visibility = Visibility.Collapsed;
                TextBlockChatMessage.Visibility = Visibility.Visible;
                buttonSaveChanges.Visibility = Visibility.Collapsed;
                buttonCancelChanges.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
        }

        private void buttonCancelChanges_Click(object sender, RoutedEventArgs e)
        {
            TextBoxChatMessage.Text = Message.ChatMessage.Message;
            TextBlockChatMessage.Text = Message.ChatMessage.Message;
            TextBoxChatMessage.Visibility = Visibility.Collapsed;
            TextBlockChatMessage.Visibility = Visibility.Visible;
            buttonSaveChanges.Visibility = Visibility.Collapsed;
            buttonCancelChanges.Visibility = Visibility.Collapsed;
        }

        private void DeleteMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnstuckMEMessage deleted = new UnstuckMEMessage
                {
                    FilePath = Message.ChatMessage.FilePath,
                    ChatID = Message.ChatMessage.ChatID,
                    Message = Message.ChatMessage.Message,
                    MessageID = Message.ChatMessage.MessageID,
                    SenderID = Message.ChatMessage.SenderID,
                    Time = Message.ChatMessage.Time,
                    Username = Message.ChatMessage.Username,
                    UsersInConvo = Message.ChatMessage.UsersInConvo
                };

                if (UnstuckME.Server.DeleteMessage(UnstuckME.User.UserID, deleted) == Task.FromResult(-1))
                    throw new Exception(string.Format("Failed to delete message {0}", deleted.MessageID));

                UnstuckME.Pages.ChatPage.StackPanelMessages.Children.Remove(this);
            }
            catch (Exception ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
        }
    }
}