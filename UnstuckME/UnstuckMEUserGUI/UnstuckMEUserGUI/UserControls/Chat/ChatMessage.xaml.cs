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
using UnstuckMEInterfaces;
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
            TextBoxUserName.Text = inMessage.Username;
            TextBoxChatMessage.Text = inMessage.Message;

            ImageProfilePicture.Source = inMessage.ProfilePic;
            Message = inMessage;

            if (inMessage.SenderID == UnstuckME.User.UserID)
                EditMessageButton.Visibility = Visibility.Visible;
            else
                EditMessageButton.Visibility = Visibility.Collapsed;
        }

        private void EditMessageButton_Click(object sender, RoutedEventArgs e)
        {
            TextBoxChatMessage.Background = Brushes.White;
            TextBoxChatMessage.Foreground = Brushes.Black;
            TextBoxChatMessage.SelectionBrush = null;
            TextBoxChatMessage.IsReadOnly = false;
            TextBoxChatMessage.Focus();
            TextBoxChatMessage.CaretIndex = TextBoxChatMessage.Text.Length;
            buttonSaveChanges.Visibility = Visibility.Visible;
            buttonCancelChanges.Visibility = Visibility.Visible;
        }

        private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnstuckMEMessage edited_message = new UnstuckMEMessage()
                {
                    ChatID = Message.ChatID,
                    FilePath = Message.FilePath,
                    Message = TextBoxChatMessage.Text,
                    MessageID = Message.MessageID,
                    SenderID = Message.SenderID,
                    Time = Message.Time,
                    Username = Message.Username,
                    UsersInConvo = Message.UsersInConvo
                };

                if (UnstuckME.Server.EditMessage(edited_message) == -1)
                    throw new Exception(string.Format("Failed to edit message {0}", edited_message.Message));

                TextBoxChatMessage.Foreground = Brushes.White;
                TextBoxChatMessage.Background = null;
                TextBoxChatMessage.IsReadOnly = true;
                Message.Message = TextBoxChatMessage.Text;
                buttonSaveChanges.Visibility = Visibility.Collapsed;
                buttonCancelChanges.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message);
            }
        }

        private void buttonCancelChanges_Click(object sender, RoutedEventArgs e)
        {
            TextBoxChatMessage.Foreground = Brushes.White;
            TextBoxChatMessage.Background = null;
            TextBoxChatMessage.IsReadOnly = true;
            TextBoxChatMessage.Text = Message.Message;
            buttonSaveChanges.Visibility = Visibility.Collapsed;
            buttonCancelChanges.Visibility = Visibility.Collapsed;
        }

        private void DeleteMessageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnstuckMEMessage deleted = new UnstuckMEMessage()
                {
                    ChatID = Message.ChatID,
                    FilePath = Message.FilePath,
                    Message = Message.Message,
                    MessageID = Message.MessageID,
                    SenderID = Message.SenderID,
                    Time = Message.Time,
                    Username = Message.Username,
                    UsersInConvo = Message.UsersInConvo
                };

                if (UnstuckME.Server.DeleteMessage(deleted) == -1)
                    throw new Exception(string.Format("Failed to delete message {0}", deleted.MessageID));

                UnstuckME.Pages.ChatPage.StackPanelMessages.Children.Remove(this);
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message);
            }
        }
    }
}