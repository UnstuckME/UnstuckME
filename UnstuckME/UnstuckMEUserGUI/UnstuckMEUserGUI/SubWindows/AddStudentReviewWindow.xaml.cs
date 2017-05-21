using System.Threading.Tasks;
using System.Windows;
using UnstuckMeLoggers;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddStudentReviewWindow.xaml
    /// </summary>
    public partial class AddStudentReviewWindow : Window
    {
        private UnstuckMESticker _sticker;

        public AddStudentReviewWindow(int stickerID)
        {
            Owner = UnstuckME.MainWindow;
            InitializeComponent();
            StarRatingValue.Value = .8;
            
            _sticker = UnstuckME.Server.GetSticker(stickerID);
            _sticker.StickerID = stickerID;

            StickerCourseName.Content = _sticker.CourseName;
            StickerDescription.Text = _sticker.ProblemDescription;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (StarRatingValue.Value.HasValue)
            {
                if (UnstuckME.Server.CreateReview(_sticker.StickerID, UnstuckME.User.UserID, StarRatingValue.Value.Value * 5, 
                                                                                           ReviewDescriptionTxtBox.Text, true) != Task.FromResult(-1))
                    UnstuckME.Pages.StickerPage.RemoveOpenSticker(_sticker.StickerID);
                else
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, "Failed to submit review", "AddStudentReviewWindow: Submit_Click");
            }

            Close();
        }
    }
}
