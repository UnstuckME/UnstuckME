using System.Windows;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddStudentReviewWindow.xaml
    /// </summary>
    public partial class AddStudentReviewWindow : Window
    {
        private UnstuckMESticker _sticker;
        private int _stickerID = 0;

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
            if (StarRatingValue.Value != null)
                UnstuckME.Server.CreateReview(_stickerID, UnstuckME.User.UserID, StarRatingValue.Value.Value * 5, ReviewDescriptionTxtBox.Text, true);
            Close();
            UnstuckME.Pages.StickerPage.RemoveOpenSticker(_stickerID);
        }
    }
}
