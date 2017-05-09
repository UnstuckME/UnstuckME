using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using UnstuckME_Classes;
using UnstuckMeLoggers;
using UnstuckMEUserGUI.SubWindows;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        public static StarRanking _studentRanking;
        private static StarRanking _tutorRanking;

        public string FirstName
        {
            get { return UnstuckME.User.FirstName; }
            set { UnstuckME.User.FirstName = value; }
        }

        public string LastName
        {
            get { return UnstuckME.User.LastName; }
            set { UnstuckME.User.LastName = value; }
        }

        public string EmailAddress
        {
            get { return UnstuckME.User.EmailAddress; }
            set { UnstuckME.User.LastName = value; }
        }

        public UserProfilePage()
        {
            InitializeComponent();

            _studentRanking = new StarRanking(StarRanking.BoxColor.Gray);
            _tutorRanking = new StarRanking(StarRanking.BoxColor.Gray);
            RatingsStack.Children.Add(_studentRanking);
            RatingsStack.Children.Add(_tutorRanking);

            try
            {
                List<Organization> orgList = UnstuckME.Server.GetAllOrganizations();
                List<Organization> userOrgs = UnstuckME.Server.GetUserOrganizations(UnstuckME.User.UserID);

                ComboboxItem temp1 = new ComboboxItem
                {
                    Text = "(OfficialMentors)",
                    Value = 0
                };

                ComboBoxOrgName.Items.Add(temp1);

                foreach (Organization org in orgList)
                {
                    ComboboxItem temp2 = new ComboboxItem
                    {
                        Text = org.OrganizationName,
                        Value = org.MentorID
                    };

                    TutoringOrganizationDisplay tutororg = new TutoringOrganizationDisplay(org.MentorID, org.OrganizationName)
                    {
                        buttonRemoveOrg = { Visibility = Visibility.Hidden }
                    };

                    ComboBoxOrgName.Items.Add(temp2);
                    StackPanelOrganization.Children.Add(tutororg);

                    if (userOrgs.Contains(org))
                        StackPanelEditOrganization.Children.Add(new TutoringOrganizationDisplay(org.MentorID, org.OrganizationName));
                }
            }
            catch (CommunicationException ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, ex.Source);
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, ex.Source);
            }
        }

        internal void RepopulateClasses()
        {
            BottomLeftStack.Children.Clear();
            List<UserClass> classes = UnstuckME.Server.GetUserClasses(UnstuckME.User.UserID);
            foreach (UserClass c in classes)
            {
                ClassDisplay usersClass = new ClassDisplay(c);
                BottomLeftStack.Children.Add(usersClass);
            }
        }

        internal void SetStudentRating(float inRating)
        {
            _studentRanking.SetRatingText("Avg Student Rating: (" + Math.Round(inRating, 2) + ")");
            _studentRanking.SetRatingValue(inRating);
        }

        internal void SetTutorRating(float inRating)
        {
            _tutorRanking.SetRatingText("Avg Tutor Rating: (" + Math.Round(inRating, 2) + ")");
            _tutorRanking.SetRatingValue(inRating);
        }

        internal void PopulateReviews()
        {
            BottomRightStack.Children.Clear();
            List<UnstuckMEReview> reviews = UnstuckME.Server.GetReviewsOfUser(UnstuckME.User.UserID);
            List<int> reportedReviews = UnstuckME.Server.GetReportedReviewIDs(UnstuckME.User.UserID);

            foreach (UnstuckMEReview review in reviews)
            {
                ReviewDisplay userReview = reportedReviews.Contains(review.ReviewID) ? new ReviewDisplay(review, true) : new ReviewDisplay(review, false);
                BottomRightStack.Children.Add(userReview);
            }
        }

        internal void AddReview(UnstuckMEReview review)
        {
            ReviewDisplay newReview = new ReviewDisplay(review, false);
            BottomRightStack.Children.Insert(0, newReview);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddClassWindow w = new AddClassWindow();
            w.Show();
        }

        private void ButtonEditProfile_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonEditProfile.Background = Brushes.SteelBlue;
        }

        private void ButtonEditProfile_MouseLeave(object sender, MouseEventArgs e)
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

        private void ButtonBrowseProfilePicture_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonBrowseProfilePicture.Background = UnstuckME.Blue;
        }

        private void ButtonBrowseProfilePicture_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonBrowseProfilePicture.Background = Brushes.SteelBlue;
        }

        private void ButtonBrowseProfilePicture_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OpenFileDialog fileBrowser = new OpenFileDialog()
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

                bool? open = fileBrowser.ShowDialog();

                if (open.HasValue && open.Value)
				{
					Stream file = fileBrowser.OpenFile();

					if (file.Length > 26214400)
						throw new Exception("The image you have selected exceeds the 25 MB limit. Please select a different file that is within the size limit.");

					System.Drawing.Image thumbnail = System.Drawing.Image.FromStream(file);
                    Thumbnail.Convert(ref thumbnail);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        thumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ms.Seek(0, SeekOrigin.Begin);

                        BitmapImage ix = new BitmapImage();
                        ix.BeginInit();
                        ix.CacheOption = BitmapCacheOption.OnLoad;
                        ix.StreamSource = ms;
                        ix.EndInit();

                        ImageEditProfilePicture.Source = ix;
                    }

					thumbnail.Dispose();	//avoids memory leaks
					file.Dispose();			//avoids memory leaks
                }
			}
            catch (Exception ex)
            {
				UnstuckMEMessageBox message = new UnstuckMEMessageBox(UnstuckMEBox.OK, ex.Message, "Image Size Error", UnstuckMEBoxImage.Warning);
				message.ShowDialog();
                //string unstuckME = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;
                //Process.Start(unstuckME);
                //Application.Current.Shutdown();
            }
		}

        private void ButtonBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ProfilePicture.Source != ImageEditProfilePicture.Source)
                ImageEditProfilePicture.Source = ProfilePicture.Source;

            GridDefault.IsEnabled = true;
            GridDefault.Visibility = Visibility.Visible;
            GridEditProfile.Visibility = Visibility.Hidden;
            GridEditProfile.IsEnabled = false;
        }

        private void ButtonSave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (TextBoxNewFirstName.Text != UnstuckME.User.FirstName && !string.IsNullOrEmpty(TextBoxNewFirstName.Text))
            {
                try
                {
                    UnstuckME.Server.ChangeUserName(UnstuckME.User.EmailAddress, TextBoxNewFirstName.Text, UnstuckME.User.LastName);
                    UnstuckME.User.FirstName = TextBoxNewFirstName.Text;
                    TextBoxFirstName.Text = TextBoxNewFirstName.Text;
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error Occured While changing user FName, Source = " + ex.Source);
                }
            }
            if (TextBoxNewLastName.Text != UnstuckME.User.LastName && !string.IsNullOrEmpty(TextBoxNewLastName.Text))
            {
                try
                {
                    UnstuckME.Server.ChangeUserName(UnstuckME.User.EmailAddress, UnstuckME.User.FirstName, TextBoxNewLastName.Text);
                    UnstuckME.User.LastName = TextBoxNewFirstName.Text;
                    TextBoxLastName.Text = TextBoxNewFirstName.Text;
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error Occured While changing user LName, Source = " + ex.Source);
                }
            }
            if (!string.IsNullOrEmpty(PasswordBoxNewPassword.Password) && !string.IsNullOrEmpty(PasswordBoxConfirm.Password))
            {
                if (PasswordBoxNewPassword.Password == PasswordBoxConfirm.Password)
                {
                    try
                    {
                        UnstuckME.Server.ChangePassword(UnstuckME.User, PasswordBoxNewPassword.Password);
                        UnstuckME.User.UserPassword = PasswordBoxNewPassword.Password;
                        UnstuckME.UPW = PasswordBoxNewPassword.Password;
                    }
                    catch (Exception ex)
                    {
                        UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error Occured While changing user Password, Source = " + ex.Source);
                    }
                }
                else
                {
                    UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK, "The passwords that you entered do not match.", "Username/Password Change Failed", UnstuckMEBoxImage.Warning);
                    messagebox.ShowDialog();
                }
            }
            if (ImageEditProfilePicture.Source != ProfilePicture.Source)
            {
                try
                {
                    var bitmapsource = ImageEditProfilePicture.Source as BitmapImage;
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();

                    if (bitmapsource != null)
                    {
                        encoder.Frames.Add(BitmapFrame.Create(bitmapsource));
                        byte[] byteArray = null;

                        using (MemoryStream ms = new MemoryStream())
                        {
                            encoder.Save(ms);
                            byteArray = ms.ToArray();
                        }

                        using (UnstuckMEStream stream = new UnstuckMEStream(byteArray, true))
                        {
                            stream.UserID = UnstuckME.User.UserID;
                            UnstuckME.FileStream.SetProfilePicture(stream); //change picture on database/server
                        }

                        ProfilePicture.Source = ImageEditProfilePicture.Source; //change picture on application

                        foreach (UnstuckMEChat chat in UnstuckME.ChatSessions)
                        {
                            foreach (UnstuckMEChatUser user in chat.Users)
                            {
                                if (user.UserID == UnstuckME.User.UserID)
                                    user.ProfilePicture = ProfilePicture.Source;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error occured while attempting to change your profile picture, Source = " + ex.Source);
                }
            }

            try
            {
                for (int index = 0; index < StackPanelEditOrganization.Children.Count; index++)
                {
                    TutoringOrganizationDisplay org = StackPanelEditOrganization.Children[index] as TutoringOrganizationDisplay;

                    if (!StackPanelOrganization.Children.OfType<TutoringOrganizationDisplay>().Contains(org))
                    {
                        //UnstuckME.Server.AddUserToTutoringOrganization(UnstuckME.User.UserID, org.OrganizationID);
                        StackPanelOrganization.Children.Add(org);
                    }
                    else
                    {
                        //UnstuckME.Server.RemoveUserFromTutoringOrganization(UnstuckME.User.UserID, org.OrganizationID);
                        StackPanelOrganization.Children.Remove(org);
                    }
                }
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, ex.Source);
            }

            GridDefault.IsEnabled = true;
            GridDefault.Visibility = Visibility.Visible;
            GridEditProfile.Visibility = Visibility.Hidden;
            GridEditProfile.IsEnabled = false;
        }

        internal void UpdateRatings()
        {
            UserInfo temp = UnstuckME.Server.GetUserInfo(UnstuckME.User.UserID, UnstuckME.User.EmailAddress);
            UnstuckME.User.AverageStudentRank = temp.AverageStudentRank;
            UnstuckME.User.AverageTutorRank = temp.AverageTutorRank;
            _studentRanking.SetRatingValue(temp.AverageStudentRank);
            _tutorRanking.SetRatingValue(temp.AverageTutorRank);
        }

        private void TextBoxNewFirstName_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void TextBoxNewLastName_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void PasswordBoxNewPassword_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void PasswordBoxConfirm_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void ButtonAddOrganization_OnClick(object sender, RoutedEventArgs e)
        {
            ComboboxItem temp = ComboBoxOrgName.SelectedItem as ComboboxItem;
            bool exists = false;

            if (ComboBoxOrgName.SelectedIndex == 0)
                return;

            foreach (TutoringOrganizationDisplay a in StackPanelOrganization.Children.OfType<TutoringOrganizationDisplay>())
            {
                if (temp != null && temp.Value == a.OrganizationID)
                    exists = true;
            }

            if (!exists && temp != null)
                StackPanelEditOrganization.Children.Add(new TutoringOrganizationDisplay(temp.Value, temp.Text));
        }

        private void ButtonDeleteProfile_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.YesNo, "Are you sure you want to delete your account? This cannot be undone.", "Delete Account?", UnstuckMEBoxImage.Warning);
            bool? open = messagebox.ShowDialog();

            if (open.HasValue && open.Value)
            {
                try
                {
                    UnstuckME.Server.Logout();
                    UnstuckME.Server.DeleteUserAccount(UnstuckME.User.UserID);
                    Application.Current.MainWindow.Close();
                    Application.Current.MainWindow = new LoginWindow();
                    Application.Current.MainWindow.Show();
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, 
                        string.Format("Server failed to log out user {0}, Source = {1}", UnstuckME.User.EmailAddress, ex.Source));
                    Application.Current.Shutdown();
                }
            }
        }

        private void ButtonDeleteProfile_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonDeleteProfile.Background = Brushes.IndianRed;
        }

        private void ButtonDeleteProfile_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonDeleteProfile.Background = Brushes.DarkRed;
        }
    }
}