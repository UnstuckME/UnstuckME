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
        public UserProfilePage(UnstuckMEWindow inWindow, IUnstuckMEService inServer)
        {
            InitializeComponent();
            Server = inServer;
            studentRanking = new StarRanking(StarRanking.BoxColor.Gray);
            tutorRanking = new StarRanking(StarRanking.BoxColor.Gray);
            RatingsStack.Children.Add(studentRanking);
            RatingsStack.Children.Add(tutorRanking);
            //RepopulateClasses(); Added this to asynchronous load in UnstuckMEWindow.
            
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

                if (file_browser.ShowDialog().Value)
                {
                    Stream file = file_browser.OpenFile();
                    byte[] byte_array = null;

                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        byte_array = ms.ToArray();
                    }

                    Server.SetProfilePicture(UnstuckMEWindow.User.UserID, byte_array);
                    ImageSourceConverter ic = new ImageSourceConverter();
                    ImageEditProfilePicture.Source = ic.ConvertFrom(byte_array) as ImageSource;
                    ProfilePicture.Source = ImageEditProfilePicture.Source;
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
                }
            }
            catch(Exception)
            {
                System.Windows.MessageBox.Show("Image too large! This Needs to be fixed, it causes a communication fault and currently the app has to be shut down.", "Image Size Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
            System.Windows.MessageBox.Show("This Does Nothing Yet!");
        }
    }
}
