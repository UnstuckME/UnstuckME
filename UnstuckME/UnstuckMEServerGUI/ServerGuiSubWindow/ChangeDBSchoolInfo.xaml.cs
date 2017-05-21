using System;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using UnstuckMEServerGUI.ServerGuiSubWindow;

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
			var entityConnectionString = new EntityConnectionStringBuilder(ConfigurationManager.ConnectionStrings["UnstuckMEServer_DBEntities"].ConnectionString);
			var basicConnectionString = new SqlConnectionStringBuilder(entityConnectionString.ProviderConnectionString);
			textBoxDatabaseIP.Text = basicConnectionString.DataSource;

		    textBoxSchoolName.Text = ConfigurationManager.AppSettings["SchoolName"];
            textBoxDatabaseIP.Text = ConfigurationManager.AppSettings["DatabaseName"];
		    textBoxUnstuckMEServerIP.Text = ConfigurationManager.AppSettings["UnstuckMEServerIP"];
		}
		

		private void buttonBrowse_Click(object sender, RoutedEventArgs e)
		{
			//Create the actual browse window
		    OpenFileDialog dlg = new OpenFileDialog
		    {
		        DefaultExt = ".png",            //Set what file they are trying to upload
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
		        Multiselect = false,
		        //gifs do not work in WPF image controls
                Filter = "Image Files (*.jpeg; *.png; *.jpg)| *.jpeg; *.png; *.jpg | JPEG Files(*.jpeg) | *.jpeg | PNG Files(*.png) | *.png | JPG Files(*.jpg) | *.jpg|GIF Files (*.gif)|*.gif"
		    };

		    //Make sure they selected something
			bool? result = dlg.ShowDialog();

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
			MessageBox.Show("This SAVE button is not fully implemented yet!");

			try
			{
				if (!File.Exists(textBoxPathToSchoolPhoto.Text))
				    throw new Exception("Please Enter a valid file path for the image");
			    using (UnstuckME_SchoolsEntities schoolDB = new UnstuckME_SchoolsEntities())
				{
					var schoolIDs = schoolDB.GetSchoolID(textBoxSchoolName.Text);
					if (!schoolIDs.Any())
					    throw new Exception("No school matching that name exists as registered school site of UnstuckME");
				}
				MessageBox.Show("Successfully Updated School Info", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "School DB Unable to Update", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void buttonCancel_Click(object sender, RoutedEventArgs e)
		{
            Close();
		}

		private void textBoxPathToSchoolPhoto_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (File.Exists(textBoxPathToSchoolPhoto.Text))
			    UpdatePhoto(textBoxPathToSchoolPhoto.Text);
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

		private void buttonConfigure_Click(object sender, RoutedEventArgs e)
		{
			ChangeDabaseConnectionSettings settingsWindow = new ChangeDabaseConnectionSettings();

            Application.Current.MainWindow = settingsWindow;
			settingsWindow.ShowDialog();
		}

		private void buttonConfigureUnstuckMEServer_Click(object sender, RoutedEventArgs e)
		{
			//MessageBox.Show("Not yet implemented");

			Configuration wConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			ServiceModelSectionGroup wServiceSection = ServiceModelSectionGroup.GetSectionGroup(wConfig);

		    if (wServiceSection != null)
		    {
		        ClientSection wClientSection = wServiceSection.Client;
		        wClientSection.Endpoints[0].Address = new Uri(textBoxUnstuckMEServerIP.Text);
		    }
		    wConfig.Save();
		}
    }
}