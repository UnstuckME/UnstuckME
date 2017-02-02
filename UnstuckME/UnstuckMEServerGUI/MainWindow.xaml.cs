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
using System.Diagnostics;
using System.IO;
using UnstuckMEServer;
using UnstuckMEInterfaces;
using System.ServiceModel;
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
			catch(Exception ex)
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
				else
				{                 
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
					App.Current.MainWindow = window;
					this.Close();
					window.Show();
				}
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
			App.Current.MainWindow = adminChange;

		}

		private void CreateAdmin_Click(object sender, RoutedEventArgs e)
		{
			AdminCreation adminCreate = new AdminCreation(ref Admin);
			adminCreate.Show();
			App.Current.MainWindow = adminCreate;
		}

		private void DeleteAdmin_Click(object sender, RoutedEventArgs e)
		{
			DeleteAdmin deleteAdmin = new DeleteAdmin(Admin);
			deleteAdmin.Show();
			App.Current.MainWindow = deleteAdmin;
		}

		private void ChangeFirstLastName_Click(object sender, RoutedEventArgs e)
		{
			AdminNameChange nameChange = new AdminNameChange(ref Admin);
			App.Current.MainWindow = nameChange;
			nameChange.ShowDialog();
			labelName.Content = Admin.FirstName + " " + Admin.LastName;
		}
	}
}
