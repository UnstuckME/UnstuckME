using System.Windows.Controls;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerHistory.xaml
    /// </summary>
    public partial class StickerHistory : UserControl
    {
        UnstuckMESticker Sticker;
        public StickerHistory(UnstuckMESticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            ProblemDescription.Text = Sticker.ProblemDescription;
            if(UnstuckME.User.UserID == Sticker.StudentID)
            {
                RoleDescription.Content = "Student";
            }
            else if(UnstuckME.User.UserID == Sticker.TutorID)
            {
                RoleDescription.Content = "Tutor";
            }
            else
            {
                RoleDescription.Content = "N/A";
            }
            StarRatingValue.Value = .5d;
            LabelClassName.Content = Sticker.CourseCode + " " + Sticker.CourseNumber + " " + Sticker.CourseName;
        }
    }
}
