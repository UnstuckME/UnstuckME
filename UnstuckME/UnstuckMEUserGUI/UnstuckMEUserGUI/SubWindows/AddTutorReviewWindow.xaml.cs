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
    /// Interaction logic for AddTutorReviewWindow.xaml
    /// </summary>
    public partial class AddTutorReviewWindow : Window
    {
        double starVal = 3.5;
        int _stickerID = 0;
        UnstuckMESticker _sticker;

        public AddTutorReviewWindow(int sticker)
        {
            InitializeComponent();
            _stickerID = sticker;
            _sticker = UnstuckME.Server.GetSticker(sticker);
            sliderRating.Value = starVal;
            StickerCourseName.Content = _sticker.CourseName;
            StickerDescription.Text = _sticker.ProblemDescription;
        }

        private void sliderRating_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            starVal = sliderRating.Value;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            UnstuckME.Server.CreateReview(_stickerID, UnstuckME.User.UserID, starVal, ReviewDescriptionTxtBox.Text, false);
            this.Close();
            UnstuckME.Pages.StickerPage.RemoveOpenSticker(_stickerID);
        }
    }
}
