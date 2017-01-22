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

		public MainPage(ref IUnstuckMEService OpenServer, ref UserInfo inboundUser, ref byte [] inboundImg)
		{
            ImageSourceConverter ic = new ImageSourceConverter();

            //Opens a connection to UnstuckME Server.
            Server = OpenServer;
            User = inboundUser;
			InitializeComponent();
			FNameTxtBx.Text = User.FirstName; // get the name from the server and show it
			LNameTxtBx.Text = User.LastName;
			EmailtextBlock.Text = User.EmailAddress; // get the email and show it
            byte[] imgByte = inboundImg;
			UserPhoto.Source = ic.ConvertFrom(imgByte) as ImageSource;	//convert image so it can be displayed

            RepopulateClasses();
			PopulateStudentReviews();
			PopulateMentorReviews();
			//RefreshBtn_Click(this, null);
			//GetNewStickers_Click(this, null);

			List<Organization> orgs = Server.GetAllOrganizations();
			ComboBoxItem item = new ComboBoxItem();

			foreach (var org in orgs)
			{
				item.Content = org;
				TutoringOrgComboBox.Items.Add(item);
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

			List<string> codes = Server.GetCourseCodes();
            CourseCodeComboBox.ItemsSource = codes;
		}

		//Commits changes to the user's classes to mentor
		private void Commit_Click(object sender, RoutedEventArgs e)
		{
            int classid = Server.GetCourseIdNumberByCodeAndNumber(CourseCodeComboBox.SelectedValue as string, CourseNumandNameComboBox.SelectedValue as string);
            Server.InsertStudentIntoClass(User.UserID, classid);
			ClassesView.Visibility = Visibility.Visible;
			AddRemoveClassesView.Visibility = Visibility.Collapsed;
            CourseNumandNameComboBox.Visibility = Visibility.Collapsed;
            RepopulateClasses();
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
			//NavigationService.Navigate(new LoginPage(ref Server));
		}

        private void RepopulateClasses()
        {
            ClassesStack.Children.Clear();
            List<UserClass> classes = Server.GetUserClasses(User.UserID);
            foreach (UserClass C in classes)
            {
				//change GetUserClasses in future to return ClassID as well
				int ID = Server.GetCourseIdNumberByCodeAndNumber(C.CourseCode, C.CourseNumber.ToString());
                ClassDisplay usersClass = new ClassDisplay(ClassesStack, User.UserID, Server, C.CourseCode, C.CourseNumber, C.CourseName, ID);
                ClassesStack.Children.Add(usersClass);
            }
        }

		private void PopulateStudentReviews()
		{
			StudentReviewsStack.Children.Clear();
			List<UnstuckMEReview> reviews = Server.GetUserStudentReviews(User.UserID);

			foreach (UnstuckMEReview review in reviews)
			{
				ReviewDisplay new_review = new ReviewDisplay(ref Server, review);
				StudentReviewsStack.Children.Add(new_review);
			}
		}

		private void PopulateMentorReviews()
		{
			MentorReviewsStack.Children.Clear();
			List<UnstuckMEReview> reviews = Server.GetUserTutorReviews(User.UserID);

			foreach (UnstuckMEReview review in reviews)
			{
				ReviewDisplay new_review = new ReviewDisplay(ref Server, review);
				MentorReviewsStack.Children.Add(new_review);
			}
		}

        private void CourseCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CourseNumandNameComboBox.Visibility = Visibility.Visible;
            string selected = CourseCodeComboBox.SelectedValue as string;

			if (selected != null)
            {
                List<string> coursenums = Server.GetCourseNumbersByCourseCode(selected);
                CourseNumandNameComboBox.ItemsSource = coursenums;
            }
        }

		private void LogoutBtn_Click(object sender, RoutedEventArgs e)
		{
			Server.Logout();
			NavigationService.Navigate(new LoginPage(ref Server));
		}

		private void SubmitStickerBtn_Click(object sender, RoutedEventArgs e)
		{
			var new_sticker = new CreateStickerTemplate(User.UserID, ref Server);
			new_sticker.InitializeComponent();
			Window window = new Window();
			window.Content = new_sticker;
			App.Current.MainWindow = window;
		}

		private void RefreshBtn_Click(object sender, RoutedEventArgs e)
		{
			MySubmittedStickersView.Children.Clear();

			List<UnstuckMESticker> sub_stickers = Server.GetUserSubmittedStickers(User.UserID);
			List<UnstuckMESticker> tut_stickers = Server.GetUserTutoredStickers(User.UserID);

			foreach (UnstuckMESticker sticker in sub_stickers)
			{
				StickerDisplay new_sticker = new StickerDisplay(User.UserID, ref Server, sticker);
				MySubmittedStickersView.Children.Add(new_sticker);
			}

			foreach (UnstuckMESticker sticker in tut_stickers)
			{
				StickerDisplay new_sticker = new StickerDisplay(User.UserID, ref Server, sticker);
				MyTutoredStickersView.Children.Add(new_sticker);
			}
		}

		private void GetNewStickers_Click(object sender, RoutedEventArgs e)
		{
			StickersView.Children.Clear();

			List<UnstuckMESticker> stickers = Server.GetAllStickers();

			foreach (UnstuckMESticker sticker in stickers)
			{
				StickerDisplay new_sticker = new StickerDisplay(User.UserID, ref Server, sticker);
				StickersView.Children.Add(new_sticker);
			}
		}

		private void AddToTutoringOrg_Click(object sender, RoutedEventArgs e)
		{
			List<Organization> orgs = Server.GetAllOrganizations();
			int org_ID = 0;

			while (org_ID < orgs.Count || orgs[org_ID].OrganizationName == TutoringOrgComboBox.SelectedValue as string)
				org_ID++;

			Server.AddUserToTutoringOrganization(User.UserID, orgs[org_ID].MentorID);
			TutoringOrgComboBox.SelectedIndex = 0;
			AddToTutoringOrg.Visibility = Visibility.Collapsed;
		}

		private void TutoringOrgComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (AddToTutoringOrg.Visibility == Visibility.Collapsed)
				AddToTutoringOrg.Visibility = Visibility.Visible;
		}
	}
}