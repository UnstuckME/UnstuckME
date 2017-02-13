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
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UserProfilePage.xaml
    /// </summary>
    public partial class UserProfilePage : Page
    {
        private static StarRanking studentRanking;
        private static StarRanking tutorRanking;
        public UserProfilePage()
        {
            InitializeComponent();

            studentRanking = new StarRanking(StarRanking.BoxColor.Gray);
            tutorRanking = new StarRanking(StarRanking.BoxColor.Gray);
            RatingsStack.Children.Add(studentRanking);
            RatingsStack.Children.Add(tutorRanking);
            RepopulateClasses();
            
        }
        private void RepopulateClasses()
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
    }
}
