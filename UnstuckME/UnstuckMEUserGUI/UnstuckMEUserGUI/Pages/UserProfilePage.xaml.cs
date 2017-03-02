using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnstuckME_Classes;
using UnstuckMEInterfaces;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        private static StarRanking studentRanking;
        private static StarRanking tutorRanking;
        public static UnstuckMEWindow CurrentWindow;
        public static IUnstuckMEService Server;
        UserInfo UsersInfo;
        public UserProfilePage(UnstuckMEWindow inWindow, IUnstuckMEService inServer, ref UserInfo info)
        {
            InitializeComponent();
            Server = inServer;
            studentRanking = new StarRanking(StarRanking.BoxColor.Gray);
            tutorRanking = new StarRanking(StarRanking.BoxColor.Gray);
            RatingsStack.Children.Add(studentRanking);
            RatingsStack.Children.Add(tutorRanking);
            //RepopulateClasses(); Added this to asynchronous load in UnstuckMEWindow.
            UsersInfo = info;

            TextBoxNewFirstName.Text = UsersInfo.FirstName;
            TextBoxNewLastName.Text = UsersInfo.LastName;

        }
        public void RepopulateClasses()
        {
            BottomLeftStack.Children.Clear();
            List<UserClass> classes = UnstuckMEWindow.Server.GetUserClasses(UnstuckMEWindow.User.UserID);
            foreach (UserClass C in classes)
            {
                //change GetUserClasses in future to return ClassID as well
                int ID = UnstuckMEWindow.Server.GetCourseIdNumberByCodeAndNumber(C.CourseCode, C.CourseNumber.ToString());
                ClassDisplay usersClass = new ClassDisplay(BottomLeftStack, UnstuckMEWindow.User.UserID, UnstuckMEWindow.Server, C.CourseCode, C.CourseNumber, C.CourseName, ID);
                BottomLeftStack.Children.Add(usersClass);
            }
            //CourseCodeComboBox.SelectedIndex = 0;
            
        }

        public void SetStudentRating(float inRating)
        {
            studentRanking.SetRatingText("Avg Student Rating: (" + Math.Round(inRating, 2) + ")");
            studentRanking.SetRatingValue(inRating);
        }
        public void SetTutorRating(float inRating)
        {
            tutorRanking.SetRatingText("Avg Tutor Rating: (" + Math.Round(inRating, 2) + ")");
            tutorRanking.SetRatingValue(inRating);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window w = new SubWindows.AddClassWindow(ref UnstuckMEWindow.Server, ref UnstuckMEWindow.User);
            w.Show();
        }

        private void ButtonEditProfile_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonEditProfile.Background = Brushes.SteelBlue;
        }

        private void ButtonEditProfile_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonEditProfile.Background = UnstuckMEWindow._UnstuckMEBlue;
        }

        private void ButtonEditProfile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridDefault.IsEnabled = false;
            GridDefault.Visibility = Visibility.Hidden;
            GridEditProfile.Visibility = Visibility.Visible;
            GridEditProfile.IsEnabled = true;
        }

        private void ButtonBrowseProfilePicture_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonBrowseProfilePicture.Background = UnstuckMEWindow._UnstuckMEBlue;
        }

        private void ButtonBrowseProfilePicture_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonBrowseProfilePicture.Background = Brushes.SteelBlue;
        }

        private void ButtonBrowseProfilePicture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog file_browser = new Microsoft.Win32.OpenFileDialog();
                file_browser.AddExtension = true;
                file_browser.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                file_browser.Multiselect = false;
                file_browser.ValidateNames = true;
                file_browser.Filter = "Image Files (*.jpeg;*.png;*.jpg)|*.jpeg;*.png;*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
				file_browser.Title = "Open Image";

				if (file_browser.ShowDialog().Value)
				{
					Stream file = file_browser.OpenFile();
					byte[] byte_array = null;

					if (file.Length > 26214400)
						throw new Exception("The image you have selected exceeds the 25 MB limit. Please select a different file that is within the size limit.");

					System.Drawing.Image image = System.Drawing.Image.FromStream(file);
					System.Drawing.Image thumbnail = image.GetThumbnailImage(Convert.ToInt32(0.6 * image.Width), Convert.ToInt32(0.6 * image.Height), null, IntPtr.Zero);

					using (MemoryStream ms = new MemoryStream())
					{
						thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
						byte_array = ms.ToArray();

						BitmapImage ix = new BitmapImage();
						ix.BeginInit();
						ix.CacheOption = BitmapCacheOption.OnLoad;
						ix.StreamSource = ms;
						ix.EndInit();

						ImageEditProfilePicture.Source = ix;
					}

					ProfilePicture.Source = ImageEditProfilePicture.Source;
					Server.SetProfilePicture(UnstuckMEWindow.User.UserID, byte_array);
                    foreach (UnstuckMEChat chat in UnstuckMEWindow._pages.ChatPage.allChats)

                    {
                        foreach (UnstuckMEChatUser user in chat.Users)
                        {
                            if(user.UserID == UnstuckMEWindow.User.UserID)
                            {
                                user.ProfilePicture = ProfilePicture.Source;
                            }
                        }
                    }

					image.Dispose();		//avoids memory leaks
					thumbnail.Dispose();	//avoids memory leaks
					file.Dispose();			//avoids memory leaks
                }
			}
            catch (Exception ex)
            {
				UnstuckMEMessageBox message = new UnstuckMEMessageBox(1, ex.Message, "Image Size Error");
				message.ShowDialog();
                string unstuckME = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
                Process.Start(unstuckME);
                System.Windows.Application.Current.Shutdown();
            }
		}

        private void ButtonBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GridDefault.IsEnabled = true;
            GridDefault.Visibility = Visibility.Visible;
            GridEditProfile.Visibility = Visibility.Hidden;
            GridEditProfile.IsEnabled = false;
        }

        private void ButtonSave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
			if (PasswordBoxNewPassword.Password != PasswordBoxConfirm.Password)
			{
				UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(1, "Your passwords do not match, please reenter your password.", "Incorrect Password");
				messagebox.ShowDialog();
			}
			else
			{
                // this doesnt seem to work, im not sure who wrote it but i was assigned to make it work so im just going to do it
                // if you dont like it, then fix it. -K

                //string newfirstname = TextBoxNewFirstName.Text != string.Empty ? UnstuckMEWindow.User.FirstName : TextBoxNewFirstName.Text;
                //string newlastname = TextBoxNewLastName.Text != string.Empty ? UnstuckMEWindow.User.LastName : TextBoxNewLastName.Text;
                //string newpassword = PasswordBoxConfirm.Password != string.Empty ? UnstuckMEWindow.User.UserPassword : PasswordBoxConfirm.Password;
    //            try
				//{
				//	Server.ChangeUserName(UnstuckMEWindow.User.EmailAddress, newfirstname, newlastname);
				//	Server.ChangePassword(UnstuckMEWindow.User, newpassword);
				//	FirstName.Text = newfirstname;
				//	LastName.Text = newlastname;
				//}
				//catch (Exception ex)
				//{
				//	UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(1, ex.Message, "Username/Password Change Failed");
				//	messagebox.ShowDialog();
				//}

                if (TextBoxNewFirstName.Text != UsersInfo.FirstName && TextBoxNewFirstName.Text != "" && TextBoxNewFirstName.Text != null)
                {
                    try
                    {
                        Server.ChangeUserName(UsersInfo.EmailAddress, TextBoxNewFirstName.Text, UsersInfo.LastName);
                    }
                    catch (Exception ex)
                    {
                        UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error Occured While changing user FName");
                    }
                }
                if (TextBoxNewLastName.Text != UsersInfo.LastName && TextBoxNewLastName.Text != "" && TextBoxNewLastName.Text != null)
                {
                    try
                    {
                        Server.ChangeUserName(UsersInfo.EmailAddress, UsersInfo.FirstName, TextBoxNewLastName.Text);
                    }
                    catch (Exception ex)
                    {
                        UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error Occured While changing user LName");
                    }
                }
                if (PasswordBoxNewPassword.Password != null && PasswordBoxNewPassword.Password != "" && PasswordBoxConfirm.Password != null && PasswordBoxConfirm.Password != "")
                {
                    if (PasswordBoxNewPassword.Password == PasswordBoxConfirm.Password)
                    {
                        try
                        {
                            Server.ChangePassword(UnstuckMEWindow.User, PasswordBoxNewPassword.Password);
                        }
                        catch (Exception ex)
                        {
                            UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error Occured While changing user Password");
                        }
                    }
                    else
                    {
                        UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(1, "The passwords that you entered do not match.", "Username/Password Change Failed");
                        messagebox.ShowDialog();
                    }
                }


                ButtonBack_MouseLeftButtonDown(sender, e);
			}
		}

        private void TextBoxNewFirstName_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void TextBoxNewLastName_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void PasswordBoxNewPassword_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void PasswordBoxConfirm_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
    }
}