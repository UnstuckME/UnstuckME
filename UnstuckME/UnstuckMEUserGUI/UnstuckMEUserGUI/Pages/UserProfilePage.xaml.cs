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
using UnstuckMEUserGUI.SubWindows;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        private static StarRanking studentRanking;
        private static StarRanking tutorRanking;

        //public static IUnstuckMEService Server;
        public UserProfilePage()
        {
            InitializeComponent();

            studentRanking = new StarRanking(StarRanking.BoxColor.Gray);
            tutorRanking = new StarRanking(StarRanking.BoxColor.Gray);
            RatingsStack.Children.Add(studentRanking);
            RatingsStack.Children.Add(tutorRanking);
            //RepopulateClasses(); Added this to asynchronous load in UnstuckMEWindow.

            TextBoxNewFirstName.Text = UnstuckME.User.FirstName;
            TextBoxNewLastName.Text = UnstuckME.User.LastName;

        }
        public void RepopulateClasses()
        {
            BottomLeftStack.Children.Clear();
            List<UserClass> classes = UnstuckME.Server.GetUserClasses(UnstuckME.User.UserID);
            foreach (UserClass C in classes)
            {
                //change GetUserClasses in future to return ClassID as well
                int ID = UnstuckME.Server.GetCourseIdNumberByCodeAndNumber(C.CourseCode, C.CourseNumber.ToString());
                ClassDisplay usersClass = new ClassDisplay(C);
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
            AddClassWindow w = new AddClassWindow();
            w.Show();
        }

        private void ButtonEditProfile_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonEditProfile.Background = Brushes.SteelBlue;
        }

        private void ButtonEditProfile_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonEditProfile.Background = UnstuckME.Blue;
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
            ButtonBrowseProfilePicture.Background = UnstuckME.Blue;
        }

        private void ButtonBrowseProfilePicture_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ButtonBrowseProfilePicture.Background = Brushes.SteelBlue;
        }

        private void ButtonBrowseProfilePicture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog file_browser = new Microsoft.Win32.OpenFileDialog()
                {
                    AddExtension = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                    Multiselect = false,
                    ValidateNames = true,
                    CheckPathExists = true,
                    CheckFileExists = true,
                    Filter = "Image Files (*.jpeg;*.png;*.jpg)|*.jpeg;*.png;*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg",
                    Title = "Open Image"
                };
                if (file_browser.ShowDialog().Value)
				{
					Stream file = file_browser.OpenFile();
					byte[] byte_array = null;

					if (file.Length > 26214400)
						throw new Exception("The image you have selected exceeds the 25 MB limit. Please select a different file that is within the size limit.");

					System.Drawing.Image thumbnail = System.Drawing.Image.FromStream(file);
                    Thumbnail.Convert(ref thumbnail);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte_array = ms.ToArray();
                        ms.Seek(0, SeekOrigin.Begin);

                        BitmapImage ix = new BitmapImage();
                        ix.BeginInit();
                        ix.CacheOption = BitmapCacheOption.OnLoad;
                        ix.StreamSource = ms;
                        ix.EndInit();

                        //ms.Position = 0L;
                        using (UnstuckMEStream stream = new UnstuckMEStream(byte_array, true))
                        {
                            stream.User = UnstuckME.User;
                            UnstuckME.FileStream.SetProfilePicture(stream); //change picture on database/server
                        }

                        ImageEditProfilePicture.Source = ix;
                        ProfilePicture.Source = ImageEditProfilePicture.Source; //change picture on application
                    }

                    foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
                    {
                        foreach (UnstuckMEChatUser user in chat.Users)
                        {
                            if(user.UserID == UnstuckME.User.UserID)
                                user.ProfilePicture = ProfilePicture.Source;
                        }
                    }

					thumbnail.Dispose();	//avoids memory leaks
					file.Dispose();			//avoids memory leaks
                }
			}
            catch (Exception ex)
            {
				UnstuckMEMessageBox message = new UnstuckMEMessageBox(UnstuckMEBox.OK, ex.Message, "Image Size Error", UnstuckMEBoxImage.Warning);
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
				UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "Your passwords do not match, please reenter your password.", "Incorrect Password", UnstuckMEBoxImage.Warning);
				messagebox.ShowDialog();
			}
			else
			{
                if (TextBoxNewFirstName.Text != UnstuckME.User.FirstName && TextBoxNewFirstName.Text != "" && TextBoxNewFirstName.Text != null)
                {
                    try
                    {
                       UnstuckME.Server.ChangeUserName(UnstuckME.User.EmailAddress, TextBoxNewFirstName.Text, UnstuckME.User.LastName);
                    }
                    catch (Exception ex)
                    {
                        UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error Occured While changing user FName");
                    }
                }
                if (TextBoxNewLastName.Text != UnstuckME.User.LastName && TextBoxNewLastName.Text != "" && TextBoxNewLastName.Text != null)
                {
                    try
                    {
                        UnstuckME.Server.ChangeUserName(UnstuckME.User.EmailAddress, UnstuckME.User.FirstName, TextBoxNewLastName.Text);
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
                            UnstuckME.Server.ChangePassword(UnstuckME.User, PasswordBoxNewPassword.Password);
                        }
                        catch (Exception ex)
                        {
                            UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error Occured While changing user Password");
                        }
                    }
                    else
                    {
                        UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "The passwords that you entered do not match.", "Username/Password Change Failed", UnstuckMEBoxImage.Warning);
                        messagebox.ShowDialog();
                    }
                }

                ButtonBack_MouseLeftButtonDown(sender, e);
			}
		}

        public void UpdateRatings()
        {
            UserInfo temp = UnstuckME.Server.GetUserInfo(UnstuckME.User.UserID, UnstuckME.User.EmailAddress);
            UnstuckME.User.AverageStudentRank = temp.AverageStudentRank;
            UnstuckME.User.AverageTutorRank = temp.AverageTutorRank;
            studentRanking.SetRatingValue(temp.AverageStudentRank);
            tutorRanking.SetRatingValue(temp.AverageTutorRank);
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