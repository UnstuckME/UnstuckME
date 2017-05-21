using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UploadFileToChatWindow.xaml
    /// </summary>
    public partial class UploadFileToChatWindow : Window
    {
        private string _filepath;

        public UnstuckMEMessage NewMessage
        {
            get { return UnstuckME.Pages.ChatPage.newMessage; }
            set { UnstuckME.Pages.ChatPage.newMessage = value; }
        }

        public UploadFileToChatWindow(string filepath)
        {
            Owner = UnstuckME.MainWindow;
            InitializeComponent();
            _filepath = filepath;

            FileInfo file = new FileInfo(_filepath);
            FileHyperlink.Inlines.Add(file.Name);
            FileSizeLabel.Content = file.Length < 1048576 ?
                Convert.ToSingle(file.Length / 1024) + " KB" :
                Convert.ToSingle(file.Length / 1048576) + " MB";

            if (file.Extension == ".pdf")
            {
                filetypeImage.Source = Imaging.CreateBitmapSourceFromHBitmap(Properties.Resources.DocumentPDF.GetHbitmap(),
                                                                             IntPtr.Zero, Int32Rect.Empty,
                                                                             BitmapSizeOptions.FromEmptyOptions());
            }
        }

        private void SubmitReportWindow_ContentRendered(object sender, EventArgs e)
        {
            Height = UnstuckME.MainWindow.ActualHeight - 8;
            Width = UnstuckME.MainWindow.Overlay.ActualWidth;
            RenderTransform = UnstuckME.MainWindow.RenderTransform;
            Left = UnstuckME.MainWindow.Left + 8;
            Top = UnstuckME.MainWindow.Top;
        }

        private void BackgroundCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewMessage.FilePath = null;
            Close();
        }

        private void SubmitBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            SubmitBorder.Background = UnstuckME.Blue;
        }

        private void SubmitBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            SubmitBorder.Background = Brushes.SteelBlue;
        }

        private void SubmitLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewMessage.FilePath = _filepath;
            UnstuckME.Pages.ChatPage.SendMessage(FileComments.Text);
            Close();
        }

        private void CancelBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            CancelBorder.Background = Brushes.LawnGreen;
        }

        private void CancelBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            CancelBorder.Background = Brushes.Green;
        }
    }
}
