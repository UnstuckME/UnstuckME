using System.Windows;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddTutorReviewWindow.xaml
    /// </summary>
    public partial class AddTutorReviewWindow : Window
    {
        int _stickerID = 0;
        UnstuckMESticker _sticker;

        public AddTutorReviewWindow(int sticker)
        {
            InitializeComponent();
            StarRatingValue.Value = .8;
            _stickerID = sticker;
            _sticker = UnstuckME.Server.GetSticker(sticker);
            
            StickerCourseName.Content = _sticker.CourseName;
            StickerDescription.Text = _sticker.ProblemDescription;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (StarRatingValue.Value != null)
                UnstuckME.Server.CreateReview(_stickerID, UnstuckME.User.UserID, StarRatingValue.Value.Value * 5, ReviewDescriptionTxtBox.Text, false);
            Close();
            UnstuckME.Pages.StickerPage.RemoveOpenSticker(_stickerID);
        }
    }
}
