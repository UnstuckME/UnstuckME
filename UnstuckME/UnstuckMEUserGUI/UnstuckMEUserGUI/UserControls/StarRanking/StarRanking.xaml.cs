using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StarRanking.xaml
    /// </summary>
    public partial class StarRanking : UserControl
    {
        public enum BoxColor { Blue, Red, White, Black, Gray, DimGray }

        private readonly BitmapImage BlueStar = new BitmapImage(new Uri("/Resources/Ranking/RankingBlue.png", UriKind.Relative));
        private readonly BitmapImage RedStar = new BitmapImage(new Uri("/Resources/Ranking/RankingRed.png", UriKind.Relative));
        private readonly BitmapImage BlackStar = new BitmapImage(new Uri("/Resources/Ranking/RankingBlack.png", UriKind.Relative));
        private readonly BitmapImage WhiteStar = new BitmapImage(new Uri("/Resources/Ranking/RankingWhite.png", UriKind.Relative));
        private readonly BitmapImage GrayStar = new BitmapImage(new Uri("/Resources/Ranking/RankingGray.png", UriKind.Relative));
        private readonly BitmapImage DimGrayStar = new BitmapImage(new Uri("/Resources/Ranking/RankingDimGray.png", UriKind.Relative));

        public StarRanking(BoxColor inColor)
        {
            InitializeComponent();
            ChangeBoxColor(inColor);
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
                        Border1.BorderBrush = UnstuckME.Blue;
                        Border2.BorderBrush = UnstuckME.Blue;
                        Border3.BorderBrush = UnstuckME.Blue;
                        Border4.BorderBrush = UnstuckME.Blue;
                        Border5.BorderBrush = UnstuckME.Blue;
                        BackGroundColor.Background = UnstuckME.Blue;
                        break;
                    }
                case BoxColor.Red:
                    {
                        Image1.Source = RedStar;
                        Image2.Source = RedStar;
                        Image3.Source = RedStar;
                        Image4.Source = RedStar;
                        Image5.Source = RedStar;
                        Border1.BorderBrush = UnstuckME.Red;
                        Border2.BorderBrush = UnstuckME.Red;
                        Border3.BorderBrush = UnstuckME.Red;
                        Border4.BorderBrush = UnstuckME.Red;
                        Border5.BorderBrush = UnstuckME.Red;
                        BackGroundColor.Background = UnstuckME.Red;
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
                default:
                    break;
            }
        }

        public void SetRatingValue(float inValue)
        {
            StarRankingValue.Value = inValue;
        }

        public void SetRatingText(string inString)
        {
            RankingTextBox.Text = inString;
        }
    }
}
