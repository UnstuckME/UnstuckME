using System;
using System.IO;
using System.Windows;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddClassToServerWindow.xaml
    /// </summary>
    public partial class AddClassToServerWindow : Window
    {
        public AddClassToServerWindow()
        {
            InitializeComponent();
        }

        private void GetClassesBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog fileBrowser = new Microsoft.Win32.OpenFileDialog
                {
                    AddExtension = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Multiselect = false,
                    ValidateNames = true,
                    Filter = "Text Files (*.txt;*.csv)",
                    Title = "Classes File Selection"
                    //file_browser.Filter = "Image Files (*.jpeg;*.png;*.jpg)|*.jpeg;*.png;*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
                };

                bool? open = fileBrowser.ShowDialog();

                if (open.HasValue && open.Value)
                {
                    Stream file = fileBrowser.OpenFile();
                }

            }
            catch (Exception)
            {

            }
        }
    }
}
