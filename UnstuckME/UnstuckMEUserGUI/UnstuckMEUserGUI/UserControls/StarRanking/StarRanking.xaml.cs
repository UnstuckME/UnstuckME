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
    /// Interaction logic for StarRanking.xaml
    /// </summary>
    public partial class StarRanking : UserControl
    {
        public enum BoxColor { Blue, Red, White, Black, Gray, DimGray }

        BitmapImage BlueStar = new BitmapImage(new Uri("/Resources/Ranking/RankingBlue.png", UriKind.Relative));
        BitmapImage RedStar = new BitmapImage(new Uri("/Resources/Ranking/RankingRed.png", UriKind.Relative));
        BitmapImage BlackStar = new BitmapImage(new Uri("/Resources/Ranking/RankingBlack.png", UriKind.Relative));
        BitmapImage WhiteStar = new BitmapImage(new Uri("/Resources/Ranking/RankingWhite.png", UriKind.Relative));
        BitmapImage GrayStar = new BitmapImage(new Uri("/Resources/Ranking/RankingGray.png", UriKind.Relative));
        BitmapImage DimGrayStar = new BitmapImage(new Uri("/Resources/Ranking/RankingDimGray.png", UriKind.Relative));
        public StarRanking(BoxColor inColor, string inText, float inRating)
        {
            InitializeComponent();
            ChangeBoxColor(inColor);
            RankingTextBox.Text = inText;
            StarRankingValue.Value = inRating;
        }

        private void ChangeBoxColor(BoxColor color)
        {
            switch(color)
            {
                case BoxColor.Blue:
                    {
                        Image1.Source = BlueStar;
                        Image2.Source = BlueStar;
                        Image3.Source = BlueStar;
                        Image4.Source = BlueStar;
                        Image5.Source = BlueStar;
                        Border1.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
                        Border2.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
                        Border3.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
                        Border4.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
                        Border5.BorderBrush = UnstuckMEWindow._UnstuckMEBlue;
                        BackGroundColor.Background = UnstuckMEWindow._UnstuckMEBlue;
                        break;
                    }
                case BoxColor.Red:
                    {
                        Image1.Source = RedStar;
                        Image2.Source = RedStar;
                        Image3.Source = RedStar;
                        Image4.Source = RedStar;
                        Image5.Source = RedStar;
                        Border1.BorderBrush = UnstuckMEWindow._UnstuckMERed;
                        Border2.BorderBrush = UnstuckMEWindow._UnstuckMERed;
                        Border3.BorderBrush = UnstuckMEWindow._UnstuckMERed;
                        Border4.BorderBrush = UnstuckMEWindow._UnstuckMERed;
                        Border5.BorderBrush = UnstuckMEWindow._UnstuckMERed;
                        BackGroundColor.Background = UnstuckMEWindow._UnstuckMERed;
                        break;
                    }
                case BoxColor.White:
                    {
                        Image1.Source = WhiteStar;
                        Image2.Source = WhiteStar;
                        Image3.Source = WhiteStar;
                        Image4.Source = WhiteStar;
                        Image5.Source = WhiteStar;
                        Border1.BorderBrush = Brushes.White;
                        Border2.BorderBrush = Brushes.White;
                        Border3.BorderBrush = Brushes.White;
                        Border4.BorderBrush = Brushes.White;
                        Border5.BorderBrush = Brushes.White;
                        BackGroundColor.Background = Brushes.White;
                        break;
                    }
                case BoxColor.Black:
                    {
                        Image1.Source = BlackStar;
                        Image2.Source = BlackStar;
                        Image3.Source = BlackStar;
                        Image4.Source = BlackStar;
                        Image5.Source = BlackStar;
                        Border1.BorderBrush = Brushes.Black;
                        Border2.BorderBrush = Brushes.Black;
                        Border3.BorderBrush = Brushes.Black;
                        Border4.BorderBrush = Brushes.Black;
                        Border5.BorderBrush = Brushes.Black;
                        BackGroundColor.Background = Brushes.Black;
                        break;
                    }
                case BoxColor.Gray:
                    {
                        Image1.Source = GrayStar;
                        Image2.Source = GrayStar;
                        Image3.Source = GrayStar;
                        Image4.Source = GrayStar;
                        Image5.Source = GrayStar;
                        Border1.BorderBrush = Brushes.Gray;
                        Border2.BorderBrush = Brushes.Gray;
                        Border3.BorderBrush = Brushes.Gray;
                        Border4.BorderBrush = Brushes.Gray;
                        Border5.BorderBrush = Brushes.Gray;
                        BackGroundColor.Background = Brushes.Gray;
                        break;
                    }
                case BoxColor.DimGray:
                    {
                        Image1.Source = DimGrayStar;
                        Image2.Source = DimGrayStar;
                        Image3.Source = DimGrayStar;
                        Image4.Source = DimGrayStar;
                        Image5.Source = DimGrayStar;
                        Border1.BorderBrush = Brushes.DimGray;
                        Border2.BorderBrush = Brushes.DimGray;
                        Border3.BorderBrush = Brushes.DimGray;
                        Border4.BorderBrush = Brushes.DimGray;
                        Border5.BorderBrush = Brushes.DimGray;
                        BackGroundColor.Background = Brushes.DimGray;
                        break;
                    }
            }
        }
        public void SetRatingValue(float inValue)
        {
            StarRankingValue.Value = inValue;
        }
    }
}
