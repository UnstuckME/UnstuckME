using System;
using System.Collections.Generic;
using System.Configuration;
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

		public LoginPage(IUnstuckMEService OpenServer)
		{
			InitializeComponent();
			Server = OpenServer;

			//add code here to get school logos and names and instantiate the UI objects that need them

			// check the config file and see if this program is linked to a school
			/*
			var appSettings = ConfigurationManager.AppSettings;
            string associatedSchool = appSettings["AssociatedSchool"] ?? "Not Found";

            // if linked display school logo
            if (associatedSchool != "Not Found")
            {
                
            }
            // if not linked display the settings window
            else
            {
				NavigationService.Navigate(new UserLoginSettingsPage());
			}
			*/
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
				//Calls UnstuckME Server Function that checks email credentials
				isValid = Server.UserLoginAttempt(email, password);
				
				if (isValid)	//if valid login
				{
					//this will crash without valid login info
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

		}

		//Rotates right between schools
		private void Rotate_Right_SchoolBtn_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
