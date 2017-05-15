using System;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using UnstuckME_Classes;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ChatMessageFile.xaml
    /// </summary>
    public partial class ChatMessageFile : UserControl
    {
        public UnstuckMEGUIChatMessage Message;

        public ChatMessageFile(UnstuckMEGUIChatMessage inMessage)
        {
            InitializeComponent();
            Message = inMessage;
            TextBoxUserName.Content = inMessage.Username;
            string[] subfilepaths = Message.Message.Split('/');
            FileHyperlink.Inlines.Add(subfilepaths[subfilepaths.Length - 1]);
            //long filesize = UnstuckME.Server.GetFileSize(Message.FilePath);
            //FileSizeLabel.Content = filesize < 1048576 ? filesize / 1048576 + "KB" : filesize / (1048576 * 1024) + "MB";
            FileInfo file = new FileInfo(FileHyperlink.Inlines.FirstInline.ContentStart.GetTextInRun(LogicalDirection.Forward));
            
            //subfilepaths[subfilepaths.Length - 1].Split('.')[1]   //if file.Extension doesn't work, try this
            if (file.Extension == ".pdf")
            {
                filetypeImage.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.DocumentPDF.GetHbitmap(), 
                                                                                                    IntPtr.Zero, Int32Rect.Empty,
                                                                                                    BitmapSizeOptions.FromEmptyOptions());
            }

            if (!string.IsNullOrEmpty(Message.Message))
            {
                TextBoxChatMessage.Text = inMessage.Message;
                TextBlockChatMessage.Text = inMessage.Message;
                TextBlockChatMessage.Visibility = Visibility.Visible;
                EditMessageButton.Visibility = inMessage.SenderID == UnstuckME.User.UserID ? Visibility.Visible : Visibility.Collapsed;
            }
            
            ImageProfilePicture.Source = inMessage.ProfilePic;
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
                    ChatID = Message.ChatID,
                    FilePath = Message.FilePath,
                    Message = TextBoxChatMessage.Text,
                    MessageID = Message.MessageID,
                    SenderID = Message.SenderID,
                    Time = Message.Time,
                    Username = Message.Username,
                    UsersInConvo = Message.UsersInConvo
                };

                if (UnstuckME.Server.EditMessage(editedMessage) == Task.FromResult(-1))
                    throw new Exception(string.Format("Failed to edit message {0}", editedMessage.Message));

                Message.Message = TextBoxChatMessage.Text;
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
            TextBoxChatMessage.Text = Message.Message;
            TextBlockChatMessage.Text = Message.Message;
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
                    ChatID = Message.ChatID,
                    FilePath = Message.FilePath,
                    Message = Message.Message,
                    MessageID = Message.MessageID,
                    SenderID = Message.SenderID,
                    Time = Message.Time,
                    Username = Message.Username,
                    UsersInConvo = Message.UsersInConvo
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

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                CommonSaveFileDialog saveDialog = new CommonSaveFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    OverwritePrompt = true,
                    IsExpandedMode = true,
                    Title = "Save File",
                    DefaultFileName = FileHyperlink.Inlines.FirstInline.ContentStart.GetTextInRun(LogicalDirection.Forward)
                };

                if (saveDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    //UnstuckME.FileStream.GetFile(Message.FilePath);
                }
            }
            catch (CommunicationException ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
            catch (Exception ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
            }
        }
    }
}