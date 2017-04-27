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
using System.Windows.Shapes;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddStudentReviewWindow.xaml
    /// </summary>
    public partial class AddStudentReviewWindow : Window
    {
        
        private UnstuckMESticker _sticker;
        int _stickerID = 0;

        public AddStudentReviewWindow(int stickerID)
        {
            InitializeComponent();
            StarRatingValue.Value = .8;
            _stickerID = stickerID;
            
            _sticker = UnstuckME.Server.GetSticker(_stickerID);
            
            StickerCourseName.Content = _sticker.CourseName;
            StickerDescription.Text = _sticker.ProblemDescription;
        }

        //private void sliderRating_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    starVal = sliderRating.Value;
        //}

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            UnstuckME.Server.CreateReview(_stickerID, UnstuckME.User.UserID, StarRatingValue.Value.Value * 5, ReviewDescriptionTxtBox.Text, true);
            this.Close();
            UnstuckME.Pages.StickerPage.RemoveOpenSticker(_stickerID);
        }
    }
}
