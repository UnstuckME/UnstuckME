using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using Microsoft.WindowsAPICodePack.Dialogs;
using UnstuckMeLoggers;
using UnstuckME_Classes;

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
            TextBoxUserName.Content = inMessage.ChatMessage.Username;
            FileSizeLabel.Content = Message.ChatMessage.FileSize < 1048576 ? 
                                    Convert.ToSingle(Message.ChatMessage.FileSize / 1024) + " KB" : 
                                    Convert.ToSingle(Message.ChatMessage.FileSize / 1048576) + " MB";
            FileInfo file = new FileInfo(Message.ChatMessage.FilePath);
            FileHyperlink.Inlines.Add(file.Name);

            if (file.Extension == ".pdf")
            {
                filetypeImage.Source = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.DocumentPDF.GetHbitmap(), 
                                                                                                    IntPtr.Zero, Int32Rect.Empty,
                                                                                                    BitmapSizeOptions.FromEmptyOptions());
            }
            
            if (!string.IsNullOrEmpty(inMessage.ChatMessage.Message))
            {
                TextBoxChatMessage.Text = inMessage.ChatMessage.Message;
                TextBlockChatMessage.Text = inMessage.ChatMessage.Message;
                TextBlockChatMessage.Visibility = Visibility.Visible;
                EditMessageButton.Visibility = inMessage.ChatMessage.SenderID == UnstuckME.User.UserID ? Visibility.Visible : Visibility.Collapsed;
            }
            
            ImageProfilePicture.Source = inMessage.ProfilePic;
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

        private void FileHyperlink_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileInfo fileinfo = new FileInfo(Message.ChatMessage.FilePath);

                CommonSaveFileDialog saveDialog = new CommonSaveFileDialog
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    OverwritePrompt = true,
                    IsExpandedMode = true,
                    Title = "Save File",
                    EnsurePathExists = true,
                    DefaultFileName = FileHyperlink.Inlines.FirstInline.ContentStart.GetTextInRun(LogicalDirection.Forward)
                };
                saveDialog.Filters.Add(GetFileDialogFilter(fileinfo.Extension));

                if (saveDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    using (UnstuckMEStream _stream = UnstuckME.FileStream.GetFile(Message.ChatMessage.MessageID))
                    {
                        using (FileStream fs = File.Create(saveDialog.FileName, Convert.ToInt32(_stream.Length), FileOptions.WriteThrough))
                        {
                            _stream.CopyTo(fs);
                        }
                    }
                }
            }
            catch (CommunicationException ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
            catch (Exception ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
            }
        }

        private CommonFileDialogFilter GetFileDialogFilter(string extension)
        {
            CommonFileDialogFilter dialogfilter = null;

            switch (extension)
            {
                case ".doc":
                    dialogfilter = new CommonFileDialogFilter("Microsoft Word 97-2003 Document", extension);
                    break;
                case ".docx":
                    dialogfilter = new CommonFileDialogFilter("Microsoft Word Document", extension);
                    break;
                case ".xls":
                    dialogfilter = new CommonFileDialogFilter("Microsoft Excel 97-2003 Spreadsheet", extension);
                    break;
                case ".xlsx":
                    dialogfilter = new CommonFileDialogFilter("Microsoft Excel Spreadsheet", extension);
                    break;
                case ".ppt":
                    dialogfilter = new CommonFileDialogFilter("Microsoft PowerPoint 97-2003 Presentation", extension);
                    break;
                case ".pptx":
                    dialogfilter = new CommonFileDialogFilter("Microsoft PowerPoint Presentation", extension);
                    break;
                case ".c":
                    dialogfilter = new CommonFileDialogFilter("C Source File", extension);
                    break;
                case ".h":
                    dialogfilter = new CommonFileDialogFilter("C Header File", extension);
                    break;
                case ".cpp":
                    dialogfilter = new CommonFileDialogFilter("C++ Source File", extension);
                    break;
                case ".cs":
                    dialogfilter = new CommonFileDialogFilter("C# Source File", extension);
                    break;
                case ".java":
                    dialogfilter = new CommonFileDialogFilter("JAVA File", extension);
                    break;
                case ".class":
                    dialogfilter = new CommonFileDialogFilter("CLASS File", extension);
                    break;
                case ".py":
                    dialogfilter = new CommonFileDialogFilter("Python File", extension);
                    break;
                case ".pyc":
                    dialogfilter = new CommonFileDialogFilter("Compiled Python File", extension);
                    break;
                case ".xaml":
                    dialogfilter = new CommonFileDialogFilter("Windows Markup File", extension);
                    break;
                case ".xml":
                    dialogfilter = new CommonFileDialogFilter("XML Document", extension);
                    break;
                case ".config":
                    dialogfilter = new CommonFileDialogFilter("XML Configuration File", extension);
                    break;
                case ".exe":
                    dialogfilter = new CommonFileDialogFilter("Application", extension);
                    break;
                case ".dll":
                    dialogfilter = new CommonFileDialogFilter("Application Extension", extension);
                    break;
                case ".zip":
                    dialogfilter = new CommonFileDialogFilter("Compressed (zipped) Folder", extension);
                    break;
                case ".msi":
                    dialogfilter = new CommonFileDialogFilter("Windows Installer Package", extension);
                    break;
                case ".bmp":
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".ico":
                    dialogfilter = CommonFileDialogStandardFilters.PictureFiles;
                    break;
                case ".txt":
                    dialogfilter = CommonFileDialogStandardFilters.TextFiles;
                    break;
                default:
                    dialogfilter = new CommonFileDialogFilter(extension.ToUpper() + " File", extension);
                    break;
            }

            return dialogfilter;
        }
    }
}