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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                Microsoft.Win32.OpenFileDialog file_browser = new Microsoft.Win32.OpenFileDialog();
                file_browser.AddExtension = true;
                file_browser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                file_browser.Multiselect = false;
                file_browser.ValidateNames = true;
                //file_browser.Filter = "Image Files (*.jpeg;*.png;*.jpg)|*.jpeg;*.png;*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
                file_browser.Filter = "Text Files (*.txt;*.csv)"; // i have no idea how to use this thing
                file_browser.Title = "Classes File Selection";

                if (file_browser.ShowDialog().Value)
                {
                    Stream file = file_browser.OpenFile();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
