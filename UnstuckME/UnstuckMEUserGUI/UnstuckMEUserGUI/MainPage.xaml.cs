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

            List<UserClass> classes = Server.GetUserClasses(UserID);
            foreach (UserClass C in classes)
            {
                ClassDisplay usersClass = new ClassDisplay(UserID, Server, C.CourseCode, C.CourseNumber, C.CourseName, 1);
                ClassesStack.Children.Add(usersClass);
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
				Stream file = file_browser.OpenFile();
				byte[] byte_array = null;

				using (MemoryStream ms = new MemoryStream())
				{
					file.CopyTo(ms);
					byte_array = ms.ToArray();
				}

				Server.SetProfilePicture(User.UserID, byte_array);
				ImageSourceConverter ic = new ImageSourceConverter();
				UserPhoto.Source = ic.ConvertFrom(byte_array) as ImageSource;
			}
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