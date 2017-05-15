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
