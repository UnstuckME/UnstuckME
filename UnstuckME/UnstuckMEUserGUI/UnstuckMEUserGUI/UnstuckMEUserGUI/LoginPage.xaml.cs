using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for LoginPage.xaml
	/// </summary>
	public partial class LoginPage : Page
	{
		public static IUnstuckMEService Server;
		private static List<School> schools = new List<School>();
		private static ImageSourceConverter source = new ImageSourceConverter();

		public LoginPage(IUnstuckMEService OpenServer)
		{
			try
			{
				InitializeComponent();
				Server = OpenServer;

				// check the config file and see if this program is linked to a school
				var appSettings = ConfigurationManager.AppSettings;
				string associatedSchool = appSettings["AssociatedSchool"] ?? "Not Found";
				schools = SchoolDB_Connection.GetSchoolNamesAndImages();

				// if linked display school logo
				if (associatedSchool != "Not Found")
				{
					int index = 0;
					while (associatedSchool != schools[index].Name)
						index++;

					SetSchoolImages(index);
				}
				else    //if not linked display default order of schools
				{
					SetSchoolImages(1);
				}
			}
			catch (Exception e)
			{
				Selected_SchoolName.Content = "Error: " + e.Message;
			}
		}

		//orders the 
		private void SetSchoolImages(int index)
		{
			int length = schools.Count;
						
			if (index != 0)
				Left_School_Logo.Source = source.ConvertFrom(schools[index - 1].Logo) as ImageSource;
			else
				Left_School_Logo.Source = null;

			Selected_School_Logo.Source = source.ConvertFrom(schools[index].Logo) as ImageSource;

			if (index != length - 1)
				Right_School_Logo.Source = source.ConvertFrom(schools[index + 1].Logo) as ImageSource;
			else
				Right_School_Logo.Source = null;

			Selected_SchoolName.Content = schools[index].Name;
		}

		//Checks for proper login information and attempts to login; if successful navigates to main interface
		private void LoginBtn_Click(object sender, RoutedEventArgs e)
		{
			string email = UserNameTxtBx.Text;
			string password = passwordBox.Password;
			bool isValid = false;

			if (email == string.Empty)
			{
				EmailBox_ErrorCode.Text = "Please Enter a Valid Email";
				PasswordBox_ErrorCode.Text = string.Empty;
			}
			else if (password == string.Empty)
			{
				PasswordBox_ErrorCode.Text = "Please Enter a Password";
				EmailBox_ErrorCode.Text = string.Empty;
			}
			else
			{
				//add stuff here regarding manual connection string
				//if (ConnectionStrTxtBx.Visibility != Visibility.Hidden)	//if a connection string is specified

				//Calls UnstuckME Server Function that checks email credentials
				isValid = Server.UserLoginAttempt(email, password);
				
				if (isValid)	//if valid login
				{
					//this will crash without valid login info
					//save school info in app.config
					var appSettings = ConfigurationManager.AppSettings;
					appSettings.Set("AssociatedSchool", Selected_SchoolName.Content.ToString());

					NavigationService.Navigate(new MainPage(Server.GetUserID(email), Server));
				}
				else
				{
					passwordBox.Password = string.Empty;
					EmailBox_ErrorCode.Text = string.Empty;
					PasswordBox_ErrorCode.Text = "Login Info Incorrect";
				}
			}
		}

		//Navigates to the create account menu
		private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new NewAccountSetupPage(Server));
		}

		//Navigates to the Settings menu
		private void SettingsBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.Navigate(new UserLoginSettingsPage());
		}

		//Handles Enter Being Pressed While in the Password/Username Box
		private void OnKeyDownPasswordHandler(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
				LoginBtn_Click(sender, e);
		}

		//Close the Application
		private void ExitBtn_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		//Rotates left between schools
		private void Rotate_Left_SchoolBtn_Click(object sender, RoutedEventArgs e)
		{
			if (Selected_SchoolName.Content.ToString() != schools[0].Name)
			{
				int index = -1;
				while (Selected_SchoolName.Content.ToString() != schools[index + 1].Name)
					index++;

				SetSchoolImages(index);
			}
		}

		//Rotates right between schools
		private void Rotate_Right_SchoolBtn_Click(object sender, RoutedEventArgs e)
		{
			int length = schools.Count;

			if (Selected_SchoolName.Content.ToString() != schools[length - 1].Name)
			{
				int index = 1;
				while (Selected_SchoolName.Content.ToString() != schools[index - 1].Name)
					index++;

				SetSchoolImages(index);
			}
		}

		private void AdvancedSettingsBtn_Click(object sender, RoutedEventArgs e)
		{
			if (ConnectionStrTxtBx.Visibility == Visibility.Hidden)
			{
				LoginBtn.Margin = new Thickness(LoginBtn.Margin.Left, LoginBtn.Margin.Top, LoginBtn.Margin.Right, LoginBtn.Margin.Bottom - 100);
				CreateAccountBtn.Margin = new Thickness(CreateAccountBtn.Margin.Left, CreateAccountBtn.Margin.Top, CreateAccountBtn.Margin.Right, CreateAccountBtn.Margin.Bottom - 100);
				AdvancedSettingsBtn.Margin = new Thickness(AdvancedSettingsBtn.Margin.Left, AdvancedSettingsBtn.Margin.Top, AdvancedSettingsBtn.Margin.Right, AdvancedSettingsBtn.Margin.Bottom - 100);
				School_Logos.Margin = new Thickness(School_Logos.Margin.Left, School_Logos.Margin.Top, School_Logos.Margin.Right, School_Logos.Margin.Bottom - 100);
				ConnectionStrTxtBx.Visibility = Visibility.Visible;
				ConnectionStr_Label.Visibility = Visibility.Visible;
			}
			else
			{
				LoginBtn.Margin = new Thickness(LoginBtn.Margin.Left, LoginBtn.Margin.Top, LoginBtn.Margin.Right, LoginBtn.Margin.Bottom + 100);
				CreateAccountBtn.Margin = new Thickness(CreateAccountBtn.Margin.Left, CreateAccountBtn.Margin.Top, CreateAccountBtn.Margin.Right, CreateAccountBtn.Margin.Bottom + 100);
				AdvancedSettingsBtn.Margin = new Thickness(AdvancedSettingsBtn.Margin.Left, AdvancedSettingsBtn.Margin.Top, AdvancedSettingsBtn.Margin.Right, AdvancedSettingsBtn.Margin.Bottom + 100);
				School_Logos.Margin = new Thickness(School_Logos.Margin.Left, School_Logos.Margin.Top, School_Logos.Margin.Right, School_Logos.Margin.Bottom + 100);
				ConnectionStrTxtBx.Visibility = Visibility.Hidden;
				ConnectionStr_Label.Visibility = Visibility.Hidden;
			}
		}
	}
}