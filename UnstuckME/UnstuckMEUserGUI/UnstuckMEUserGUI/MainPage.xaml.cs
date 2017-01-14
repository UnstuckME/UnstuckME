﻿using System;
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
            ImageSource img = null;
            byte[] imgByte;
            //Opens a connection to UnstuckME Server.
            Server = OpenServer;
			User = Server.GetUserInfo(UserID);
			InitializeComponent();
			FNameTxtBx.Text = User.FirstName; // get the name from the server and insert it
			LNameTxtBx.Text = User.LastName;
			EmailtextBlock.Text = User.EmailAddress; // get the email and show it
            imgByte = Server.GetProfilePicture(UserID);
            img = ic.ConvertFrom(imgByte) as ImageSource;
            UserPhoto.Source = img;

            for (int i = 0; i < 5; i++)
			{
				TextBlock test = new TextBlock();
				test.Text = "test text" + i;
				ClassesStack.Children.Add(test);
			}
            List<UserClasses> enrolledClasses = Server.GetUserClasses(UserID);
            foreach (UserClasses UserClass in enrolledClasses)
            {
                TextBlock test = new TextBlock();
                test.Text = UserClass.CourseCode + UserClass.CourseNumber + UserClass.CourseName;
                ClassesStack.Children.Add(test);
            }
        }

		private void AddRemoveClasses_Click(object sender, RoutedEventArgs e)
		{
			if (AddRemoveClassesView.Visibility == Visibility.Collapsed)
			{
				ClassesView.Visibility = Visibility.Collapsed;
				AddRemoveClassesView.Visibility = Visibility.Visible;
                CourseCodeDropdown.Visibility = Visibility.Visible;
                CourseNumberDropdown.Visibility = Visibility.Visible;

                CourseCodeDropdown.Items.Add("test");
			}
			else
			{
				ClassesView.Visibility = Visibility.Visible;
				AddRemoveClassesView.Visibility = Visibility.Collapsed;
                CourseCodeDropdown.Visibility = Visibility.Collapsed;
                CourseNumberDropdown.Visibility = Visibility.Collapsed;
			}

		}

		private void Commit_Click(object sender, RoutedEventArgs e)
		{
			ClassesView.Visibility = Visibility.Visible;
			AddRemoveClassesView.Visibility = Visibility.Collapsed;
		}

		private void UserPhotoBtn_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog file_browser = new OpenFileDialog();
			file_browser.AddExtension = true;
			file_browser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			file_browser.Multiselect = false;
			file_browser.ValidateNames = true;
			file_browser.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

			if (file_browser.ShowDialog() == DialogResult.OK)
			{
				Stream file = file_browser.OpenFile();
				byte[] image = null;

				using (MemoryStream ms = new MemoryStream())
				{
					file.CopyTo(ms);
					image = ms.ToArray();
				}

				Server.SetProfilePicture(User.UserID, image);
                ImageSourceConverter ic = new ImageSourceConverter();
                ImageSource img = null;
                byte[] imgByte;
                imgByte = Server.GetProfilePicture(User.UserID);
                img = ic.ConvertFrom(imgByte) as ImageSource;
                UserPhoto.Source = img;
            }
		}

		private void ChangeUserName_Click(object sender, RoutedEventArgs e)
		{
			//Server.ChangeUserName(User.EmailAddress, FNameTxtBx.Text, LNameTxtBx.Text);
		}
	}
}