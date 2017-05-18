using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UnstuckME_Classes;
using UnstuckMeLoggers;

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
            TextBoxUserName.Content = inMessage.Message.Username;
            TextBoxChatMessage.Text = inMessage.Message.Message;
            TextBlockChatMessage.Text = inMessage.Message.Message;

            ImageProfilePicture.Source = inMessage.ProfilePic;
            Message = inMessage;

            EditMessageButton.Visibility = inMessage.Message.SenderID == UnstuckME.User.UserID ? Visibility.Visible : Visibility.Collapsed;
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
                UnstuckMEMessage editedMessage = new UnstuckMEMessage()
                {
                    ChatID = Message.Message.ChatID,
                    FilePath = Message.Message.FilePath,
                    Message = TextBoxChatMessage.Text,
                    MessageID = Message.Message.MessageID,
                    SenderID = Message.Message.SenderID,
                    Time = Message.Message.Time,
                    Username = Message.Message.Username,
                    UsersInConvo = Message.Message.UsersInConvo
                };

                if (UnstuckME.Server.EditMessage(editedMessage) == Task.FromResult(-1))
                    throw new Exception(string.Format("Failed to edit message {0}", editedMessage.Message));

                Message.Message.Message = TextBoxChatMessage.Text;
                TextBlockChatMessage.Text = TextBoxChatMessage.Text;
                TextBoxChatMessage.Visibility = Visibility.Collapsed;
                TextBlockChatMessage.Visibility = Visibility.Visible;
                buttonSaveChanges.Visibility = Visibility.Collapsed;
                buttonCancelChanges.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
        }

        private void buttonCancelChanges_Click(object sender, RoutedEventArgs e)
        {
            TextBoxChatMessage.Text = Message.Message.Message;
            TextBlockChatMessage.Text = Message.Message.Message;
            TextBoxChatMessage.Visibility = Visibility.Collapsed;
            TextBlockChatMessage.Visibility = Visibility.Visible;
            buttonSaveChanges.Visibility = Visibility.Collapsed;
            buttonCancelChanges.Visibility = Visibility.Collapsed;
        }

        private void DeleteMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnstuckMEMessage deleted = new UnstuckMEMessage()
                {
                    ChatID = Message.Message.ChatID,
                    FilePath = Message.Message.FilePath,
                    Message = Message.Message.Message,
                    MessageID = Message.Message.MessageID,
                    SenderID = Message.Message.SenderID,
                    Time = Message.Message.Time,
                    Username = Message.Message.Username,
                    UsersInConvo = Message.Message.UsersInConvo
                };

                if (UnstuckME.Server.DeleteMessage(deleted) == Task.FromResult(-1))
                    throw new Exception(string.Format("Failed to delete message {0}", deleted.MessageID));

                UnstuckME.Pages.ChatPage.StackPanelMessages.Children.Remove(this);
            }
            catch (Exception ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
        }
    }
}