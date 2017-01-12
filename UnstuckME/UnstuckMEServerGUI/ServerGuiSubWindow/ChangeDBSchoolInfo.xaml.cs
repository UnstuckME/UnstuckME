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
using System.Windows.Shapes;

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ChangeDBSchoolInfo : Window
    {
        public ChangeDBSchoolInfo()
        {
            InitializeComponent();
        }




        private void ButtonSchoolLogoImg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {

            //Create the actual brwse window
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //Set what file they are trying to upload
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            //Make sure they selected something
            Nullable<bool> result = dlg.ShowDialog();

            //If they did select a logo update the file path
            if (result == true)
            {
                string filepath = dlg.FileName;
                textBoxPathToSchoolPhoto.Text = filepath;
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
