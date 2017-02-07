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
            float studentRatingValue = 2.5f; //Eventually Get This From Database
            float tutorRatingValue = 1.275555555f; //Eventually Get This From Database

            FirstName.Text = "Arthur"; //Eventually Get This From DataBase
            LastName.Text = "Clark"; //Eventually Get This From Database
            EmailAddress.Text = "arthur.clark@oit.edu"; //Eventually Get This From DataBase

            studentRanking = new StarRanking(StarRanking.BoxColor.Gray, "Avg Student Rating: (" + Math.Round(studentRatingValue, 2) + ")", studentRatingValue);
            tutorRanking = new StarRanking(StarRanking.BoxColor.Gray, "Avg Tutor Rating: (" + Math.Round(tutorRatingValue, 2) + ")", tutorRatingValue);
            RatingsStack.Children.Add(studentRanking);
            RatingsStack.Children.Add(tutorRanking);
        }
    }
}
