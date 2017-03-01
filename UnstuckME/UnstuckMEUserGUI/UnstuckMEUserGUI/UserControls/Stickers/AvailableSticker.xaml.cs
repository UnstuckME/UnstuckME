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
    /// Interaction logic for AvailableSticker.xaml
    ///
    //public int StickerID { get; set; }
    //public string ProblemDescription { get; set; }
    //public int ClassID { get; set; }
    //public int ChatID { get; set; }
    //public int StudentID { get; set; }
    //public int TutorID { get; set; }
    //public float MinimumStarRanking { get; set; }
    //public List<int> AttachedOrganizations { get; set; }
    //public DateTime SubmitTime { get; set; }
    //public int Timeout { get; set; }
    /// </summary>
    public partial class AvailableSticker : UserControl
    {
        public UnstuckMEAvailableSticker Sticker;
        public UserClass Class;
        BitmapImage BlueStar = new BitmapImage(new Uri("/Resources/Ranking/RankingBlue.png", UriKind.Relative));
        BitmapImage SteelBlueStar = new BitmapImage(new Uri("/Resources/Ranking/RankingSteelBlue.png", UriKind.Relative));
        public AvailableSticker(UnstuckMEAvailableSticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            LabelClassName.Content = Sticker.CourseCode + " " + Sticker.CourseNumber + " " + Sticker.CourseName;
            StarRankingValue.Value = inSticker.StudentRanking;
        }

        public void RemoveFromStackPanel()
        {
            //Removes Sticker From Stack Panel
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void GridClassName_MouseEnter(object sender, MouseEventArgs e)
        {
            FullBackGround.Background = Brushes.SteelBlue;
            ChangeToSteelBlue();
        }

        public void ChangeToSteelBlue()
        {
            Border1.BorderBrush = Brushes.SteelBlue;
            Border2.BorderBrush = Brushes.SteelBlue;
            Border3.BorderBrush = Brushes.SteelBlue;
            Border4.BorderBrush = Brushes.SteelBlue;
            Border5.BorderBrush = Brushes.SteelBlue;
            Image1.Source = SteelBlueStar;
            Image2.Source = SteelBlueStar;
            Image3.Source = SteelBlueStar;
            Image4.Source = SteelBlueStar;
            Image5.Source = SteelBlueStar;
        }

        public void ChangeToUnstuckMEBlue()
        {
            Border1.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
            Border2.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
            Border3.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
            Border4.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
            Border5.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
            Image1.Source = BlueStar;
            Image2.Source = BlueStar;
            Image3.Source = BlueStar;
            Image4.Source = BlueStar;
            Image5.Source = BlueStar;
        }

        private void GridClassName_MouseLeave(object sender, MouseEventArgs e)
        {
            FullBackGround.Background = UnstuckMEWindow._UnstuckMEBlue;
            ChangeToUnstuckMEBlue();
        }

        private void GridClassName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StickerInfoWindow showInfo = new StickerInfoWindow(ref Sticker);
            showInfo.Show();
        }

        private void BorderX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Removes Sticker From Stack Panel
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void ButtonAccept_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonAccept.Background = Brushes.LimeGreen;
        }

        private void ButtonAccept_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonAccept.Background = Brushes.ForestGreen;
        }

        private void ButtonAccept_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UnstuckMEWindow.Server.AcceptSticker(UnstuckMEWindow.User.UserID, Sticker.StickerID);
                Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().StickerAcceptedStartConversation(Sticker, UnstuckMEWindow.User.UserID);
                ((StackPanel)this.Parent).Children.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Accept Failed In Available Sticker User Control. " + ex.Message);
            }
        }
    }
}
