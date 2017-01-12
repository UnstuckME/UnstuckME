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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for NewAccountSetupPage.xaml
	/// </summary>
	public partial class NewAccountSetupPage : Page
	{
		public static IUnstuckMEService Server;
		public NewAccountSetupPage(IUnstuckMEService OpenServer)
		{
			InitializeComponent();
			Server = OpenServer;
		}

		private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (FNameTxtBx.Text.Length == 0)
					throw new Exception("First Name Required!");
				else if (LNameTxtBx.Text.Length == 0)
					throw new Exception("Last Name Required!");
				else if (EmailTxtBx.Text.Length == 0)
					throw new Exception("Email Address Required!");
				else if (passwordBox.Password.Length < 6)
					throw new Exception("Please enter a password with 6 or more characters!");
				else if (passwordBox.Password != passwordBoxConfirm.Password)
					throw new Exception("Passwords Not Match!");
				else
				{
					if (Server.CreateNewUser(FNameTxtBx.Text, LNameTxtBx.Text, EmailTxtBx.Text, passwordBox.Password))
					{
                        Server.UserLoginAttempt(EmailTxtBx.Text, passwordBox.Password);
						NavigationService.Navigate(new MainPage(Server.GetUserID(EmailTxtBx.Text), Server));
					}
					else
					{
						throw new Exception("Error occurred creating a new user, Please Contact Your Server Administrator");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "New User Creation Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			NavigationService.GoBack();
		}
	}
}
