using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows;
using UnstuckMEServerGUI.ServerGuiSubWindow;
using UnstuckME_Classes;

namespace UnstuckMEServerGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	public partial class MainWindow : Window
	{
		public static AdminInfo Admin;
		public MainWindow(ref AdminInfo currentAdmin)
		{
			try
			{
				InitializeComponent();
				Admin = currentAdmin;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			labelEmailAddress.Content = "Email Address: " + Admin.EmailAddress;
			labelName.Content = "Name: " + Admin.FirstName + " " + Admin.LastName;
		}

		/// <summary>
		/// This function is ran when the user clicks the Start Server Button.
		/// </summary>
		private void button_RunServer_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Process[] pname = Process.GetProcessesByName("UnstuckMEServer");
				if (pname.Length > 0)
					throw new InvalidOperationException("Server Is Already Running!");
			    DirectoryInfo currentDir = new DirectoryInfo(Directory.GetCurrentDirectory());
			    currentDir = currentDir.Parent.Parent.Parent;                    
			    string serverPath = currentDir.FullName + "/UnstuckMEServer/bin/Release/UnstuckMEServer.exe";
			    Process startServer = new Process();
			    //startServer.StartInfo.RedirectStandardOutput = true;
			    //startServer.StartInfo.UseShellExecute = false;
			    //startServer.StartInfo.CreateNoWindow = true;
			    startServer.StartInfo.Verb = "runas";
			    startServer.StartInfo.FileName = serverPath;
			    startServer.Start();

			    ServerRunning window = new ServerRunning(ref Admin);
			    Application.Current.MainWindow = window;
			    Close();
			    window.Show();
			}
			catch(InvalidOperationException ex)
			{
				MessageBox.Show(ex.Message, "Server Start Error", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}


		private void MenuItemExit_Click(object sender, RoutedEventArgs e)
		{
			ExitApplication();
		}

		private void ExitApplication()
		{
			try
			{
				Application.Current.Shutdown();
			}
			catch(Exception)
			{
				MessageBox.Show("Application Exit Failure", "Exit Failure", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void ChangeCredintials_Click(object sender, RoutedEventArgs e)
		{
			
			AdminCredChange adminChange = new AdminCredChange(ref Admin);
			adminChange.ShowDialog();
			labelEmailAddress.Content = Admin.EmailAddress;
            Application.Current.MainWindow = adminChange;

		}

		private void CreateAdmin_Click(object sender, RoutedEventArgs e)
		{
			AdminCreation adminCreate = new AdminCreation(ref Admin);
			adminCreate.Show();
            Application.Current.MainWindow = adminCreate;
		}

		private void DeleteAdmin_Click(object sender, RoutedEventArgs e)
		{
			DeleteAdmin deleteAdmin = new DeleteAdmin(Admin);
			deleteAdmin.Show();
            Application.Current.MainWindow = deleteAdmin;
		}

        private void ChangeFirstLastName_Click(object sender, RoutedEventArgs e)
        {
            AdminNameChange nameChange = new AdminNameChange(ref Admin);
            Application.Current.MainWindow = nameChange;
            nameChange.ShowDialog();
            labelName.Content = "Name: " + Admin.FirstName + " " + Admin.LastName;
        }

        private void MenuItemLogout_Click(object sender, RoutedEventArgs e)
        {
            string unstuckME = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;
            Process.Start(unstuckME);
            Application.Current.Shutdown();
        }

		private void UpdateEmailSettings_Click(object sender, RoutedEventArgs e)
		{
			EmailSettings window = new EmailSettings(ref Admin);
			Application.Current.MainWindow = window;
			Application.Current.MainWindow.Show();
		}

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings["EmailSettingsSet"].Value == "false")
            {
                EmailSettings window = new EmailSettings(ref Admin);
                Application.Current.MainWindow = window;
                window.Show();
                window.Focus();
            }
        }
    }
}