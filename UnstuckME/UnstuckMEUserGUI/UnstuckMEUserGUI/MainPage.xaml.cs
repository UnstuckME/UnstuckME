using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnstuckMEInterfaces;
using UnstuckME_Classes;
using System.Data.SqlClient;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for MainPage.xaml
	/// </summary>
	public partial class MainPage : Page
	{
		public static IUnstuckMEService Server;
		public static UserInfo User;

		public MainPage(int UserID, IUnstuckMEService OpenServer)
		{
            ImageSourceConverter ic = new ImageSourceConverter();

            //Opens a connection to UnstuckME Server.
            Server = OpenServer;
			User = Server.GetUserInfo(UserID);
			InitializeComponent();
			FNameTxtBx.Text = User.FirstName; // get the name from the server and show it
			LNameTxtBx.Text = User.LastName;
			EmailtextBlock.Text = User.EmailAddress; // get the email and show it
			byte[] imgByte = Server.GetProfilePicture(UserID);
			UserPhoto.Source = ic.ConvertFrom(imgByte) as ImageSource;	//convert image so it can be displayed

            for (int i = 0; i < 50; i++)	//just testing
			{
				TextBlock test = new TextBlock();
				test.Text = "test text" + i;
				ClassesStack.Children.Add(test);
			}
		}

		//Adds/removes classes from the list of classes user can mentor for
		private void AddRemoveClasses_Click(object sender, RoutedEventArgs e)
		{
			if (AddRemoveClassesView.Visibility == Visibility.Collapsed)
			{
				ClassesView.Visibility = Visibility.Collapsed;
				AddRemoveClassesView.Visibility = Visibility.Visible;

			}
			else
			{
				ClassesView.Visibility = Visibility.Visible;
				AddRemoveClassesView.Visibility = Visibility.Collapsed;
			}

		}

		//Commits changes to the user's classes to mentor
		private void Commit_Click(object sender, RoutedEventArgs e)
		{
			ClassesView.Visibility = Visibility.Visible;
			AddRemoveClassesView.Visibility = Visibility.Collapsed;
		}

		//Uploads a new photo from the user's computer and inserts it as the user's new profile picture
		private void UserPhotoBtn_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog file_browser = new OpenFileDialog();
			file_browser.AddExtension = true;
			file_browser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			file_browser.Multiselect = false;
			file_browser.ValidateNames = true;
			file_browser.Filter = "Image Files (*.jpeg;*.png;*.jpg)|*.jpeg;*.png;*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

			if (file_browser.ShowDialog() == DialogResult.OK)
			{
				FileStream file = new FileStream(file_browser.FileName, FileMode.Open, FileAccess.Read);
				//FileStream file = file_browser.OpenFile() as FileStream;
				int stream_length = (int)file.Length;
				byte[] byte_array = new byte[stream_length];
				file.Read(byte_array, 0, stream_length);
				file.Close();


				//System.Drawing.Image image = System.Drawing.Image.FromStream(file);

				//using (MemoryStream ms = new MemoryStream())
				//{
				//	if (file_browser.SafeFileName.Split('.').Last() == "jpeg" || file_browser.SafeFileName.Split('.').Last() == "jpg")
				//		image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
				//	else if (file_browser.SafeFileName.Split('.').Last() == "png")
				//		image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

				//	byte_array = ms.ToArray();
				//}


				//Server.SetProfilePicture(User.UserID, byte_array);

				SqlConnection connection = GetConnection();

				SqlCommand command = new SqlCommand();
				command.Connection = connection;
				command.CommandText =
					"update Picture " +
					"set Photo = @photo " +
					"where @UserID = UserID";
				command.Parameters.AddWithValue("@photo", byte_array);
				command.Parameters.AddWithValue("@UserID", User.UserID);

				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();

				ImageSourceConverter ic = new ImageSourceConverter();
				UserPhoto.Source = ic.ConvertFrom(byte_array) as ImageSource;
			}
		}
		public static SqlConnection GetConnection()
		{
			SqlConnection connection = new SqlConnection();
			connection.ConnectionString =
				//"Data Source=localhost\\SqlExpress;" + 
				//"Data Source=ORANGE1\\ORANGE1" +
				"Data Source=aura.students.cset.oit.edu" +
				";Initial Catalog=UnstuckME_DB" +
				";Integrated Security=False" +
				";User ID=" + "matthew_cole" + ";Password=" + "Ensalada1";
			return connection;
		}

		//Opens prompt for user to change their username
		private void ChangeUserName_Click(object sender, RoutedEventArgs e)
		{
			FNameTxtBx.Text = string.Empty;
			LNameTxtBx.Text = string.Empty;
			ChangeUsernameBtn.Visibility = Visibility.Visible;
			AllowChangePasswordBtn.Visibility = Visibility.Hidden;
		}

		//Submits the username changes
		private void ChangeUsernameBtn_Click(object sender, RoutedEventArgs e)
		{
			Server.ChangeUserName(User.EmailAddress, FNameTxtBx.Text, LNameTxtBx.Text);
			ChangeUsernameBtn.Visibility = Visibility.Hidden;
			AllowChangePasswordBtn.Visibility = Visibility.Visible;
		}

		//Submits the password changes
		private void ChangePasswordBtn_Click(object sender, RoutedEventArgs e)
		{
			Server.ChangePassword(User, ChangePasswordBx.Text);
			ChangePasswordBx.Visibility = Visibility.Hidden;
			ChangePasswordBx.Text = string.Empty;
			ChangePasswordBtn.Visibility = Visibility.Hidden;
			AllowChangePasswordBtn.Visibility = Visibility.Visible;
		}

		//Opens prompt for user to change their password
		private void AllowChangePasswordBtn_Click(object sender, RoutedEventArgs e)
		{
			ChangePasswordBx.Visibility = Visibility.Visible;
			ChangePasswordBtn.Visibility = Visibility.Visible;
			AllowChangePasswordBtn.Visibility = Visibility.Hidden;
		}
		
		//Deletes the account and logs out, goes to login page
		private void DeleteAccountBtn_Click(object sender, RoutedEventArgs e)
		{
			Server.Logout();
			Server.DeleteUserAccount(User.UserID);
			NavigationService.Navigate(new LoginPage(Server));
		}
	}
}