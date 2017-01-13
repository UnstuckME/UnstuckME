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

                UpdatePhoto(filepath);
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(textBoxPathToSchoolPhoto.Text))
                {
                    throw new Exception("Please Enter a valid file path for the image");
                }
                using (UnstuckME_SchoolsEntities1 schoolDB = new UnstuckME_SchoolsEntities1())
                { 
                    var schoolIDs = schoolDB.GetSchoolID(textBoxSchoolName.Text);
                    if (schoolIDs.Count() < 1)
                    {
                        throw new Exception("No school matching that name exists as registered school site of UnstuckME");
                    }
                    
                }
                MessageBox.Show("Successfully Updated School Info", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "School DB Unable to Update", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void textBoxPathToSchoolPhoto_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (System.IO.File.Exists(textBoxPathToSchoolPhoto.Text))
            {
                UpdatePhoto(textBoxPathToSchoolPhoto.Text);
            }
        }

        private void UpdatePhoto(string filePath)
        {
            Image schoolImage = new Image();
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(filePath, UriKind.Relative);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            schoolImage.Source = src;
            schoolImage.Stretch = Stretch.UniformToFill;

            imageSchoolLogo.Source = schoolImage.Source;

            buttonClickToChangePhoto.Background = null;
            buttonClickToChangePhoto.BorderThickness = new Thickness(0, 0, 0, 0);
        }
    }
}
