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

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddStudentReviewWindow.xaml
    /// </summary>
    public partial class AddStudentReviewWindow : Window
    {
        double starVal = 3.5;
        int _stickerID = 0;

        public AddStudentReviewWindow(int stickerID)
        {
            InitializeComponent();
            _stickerID = stickerID;
            sliderRating.Value = starVal;
        }

        private void sliderRating_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            starVal = sliderRating.Value;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            UnstuckME.Server.CreateReview(_stickerID, UnstuckME.User.UserID, starVal, ReviewDescriptionTxtBox.Text, true);
            this.Close();
            UnstuckME.Pages.StickerPage.RemoveOpenSticker(_stickerID);
        }
    }
}
