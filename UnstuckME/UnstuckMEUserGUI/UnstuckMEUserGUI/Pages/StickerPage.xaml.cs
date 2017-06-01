using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerPage.xaml
    /// </summary>
    public partial class StickerPage : Page
    {
        public List<UnstuckMEAvailableSticker> AvailableStickers;
        public List<UnstuckMESticker> OpenStickers;
        public List<UnstuckMESticker> RecentStickers;
        public List<UnstuckMESticker> MyStickersList;

        public StickerPage()
        {
            InitializeComponent();
        }

        public void RemoveOpenSticker(int stickerID)
        {
            UIElementCollection stickers = StackPanelOpenStickers.Children;
            for (int index = stickers.Count - 1; index >= 0; index--)
            {
                OpenSticker opensticker = stickers[index] as OpenSticker;

                if (opensticker != null && opensticker.Sticker.StickerID == stickerID)
                {
                    opensticker.Remove();
                    StackPanelStickerHistory.Children.Add(new StickerHistory(opensticker.Sticker));

                    for (int i = 0; i < OpenStickers.Count; i++)
                    {
                        if (OpenStickers[i].StickerID == stickerID)
                        {
                            OpenStickers.Remove(OpenStickers[i]);
                            return;
                        }
                    }
                }
            }
        }

        public void MakeMyStickerActive(int stickerID)
        {
            UIElementCollection mystickers = StackPanelMyStickers.Children;
            for (int index = mystickers.Count - 1; index >= 0; index--)
            {
                MySticker sticker = mystickers[index] as MySticker;

                if (sticker != null && sticker.Sticker.StickerID == stickerID)
                {
                    sticker.Sticker.TutorID = null;
                    sticker.LabelTutorName.Content = "Currently Not Tutored";
                    sticker.ButtonCompleted.Visibility = Visibility.Hidden;
                    //sticker.ButtonDelete.Visibility = Visibility.Visible;
                }
            }
        }

        public void MakeMyStickerAccepted(int stickerID)
        {
            UIElementCollection mystickers = StackPanelMyStickers.Children;
            for (int index = mystickers.Count - 1; index >= 0; index--)
            {
                MySticker sticker = mystickers[index] as MySticker;

                if (sticker != null && sticker.Sticker.StickerID == stickerID)
                {
                    sticker.Sticker.TutorID = UnstuckME.Server.GetSticker(stickerID).TutorID;
                    sticker.LabelTutorName.Content = UnstuckME.Server.GetUserDisplayName(sticker.Sticker.TutorID.Value);
                    sticker.ButtonCompleted.Visibility = Visibility.Visible;
                    //sticker.ButtonDelete.Visibility = Visibility.Collapsed;
                }
            }
        }

        public void MakeMyStickerResolved(int stickerID)
        {
            
        }

        private void ButtonAvailable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelTitle.Content = "Available Stickers";
            LabelDescription.Content = "Stickers you qualify to tutor for:";
            ButtonAvailable.Background = Brushes.SteelBlue;
            ButtonHistory.Background = UnstuckME.Blue;
            ButtonSubmitted.Background = UnstuckME.Blue;
            ButtonTutoring.Background = UnstuckME.Blue;
            ButtonAvailable.BorderBrush = Brushes.White;
            ButtonHistory.BorderBrush = Brushes.Black;
            ButtonSubmitted.BorderBrush = Brushes.Black;
            ButtonTutoring.BorderBrush = Brushes.Black;
            GridAvailable.Visibility = Visibility.Visible;
            GridHistory.Visibility = Visibility.Hidden;
            GridOpenStickers.Visibility = Visibility.Hidden;
            GridSubmitted.Visibility = Visibility.Hidden;
            RectangleAvailable.Visibility = Visibility.Visible;
            RectangleHistory.Visibility = Visibility.Hidden;
            RectangleSubmitted.Visibility = Visibility.Hidden;
            RectangleTutoring.Visibility = Visibility.Hidden;
        }

        private void ButtonSubmitted_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelTitle.Content = "Submitted Stickers";
            LabelDescription.Content = "Stickers you have submitted that need to be tutored:";
            ButtonAvailable.Background = UnstuckME.Blue;
            ButtonHistory.Background = UnstuckME.Blue;
            ButtonSubmitted.Background = Brushes.SteelBlue;
            ButtonTutoring.Background = UnstuckME.Blue;
            ButtonAvailable.BorderBrush = Brushes.Black;
            ButtonHistory.BorderBrush = Brushes.Black;
            ButtonSubmitted.BorderBrush = Brushes.White;
            ButtonTutoring.BorderBrush = Brushes.Black;
            GridAvailable.Visibility = Visibility.Hidden;
            GridHistory.Visibility = Visibility.Hidden;
            GridOpenStickers.Visibility = Visibility.Hidden;
            GridSubmitted.Visibility = Visibility.Visible;
            RectangleAvailable.Visibility = Visibility.Hidden;
            RectangleHistory.Visibility = Visibility.Hidden;
            RectangleSubmitted.Visibility = Visibility.Visible;
            RectangleTutoring.Visibility = Visibility.Hidden;
        }

        private void ButtonTutoring_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelTitle.Content = "Tutor Stickers";
            LabelDescription.Content = "Stickers you are currently tutoring:";
            ButtonAvailable.Background = UnstuckME.Blue;
            ButtonHistory.Background = UnstuckME.Blue;
            ButtonSubmitted.Background = UnstuckME.Blue;
            ButtonTutoring.Background = Brushes.SteelBlue;
            ButtonAvailable.BorderBrush = Brushes.Black;
            ButtonHistory.BorderBrush = Brushes.Black;
            ButtonSubmitted.BorderBrush = Brushes.Black;
            ButtonTutoring.BorderBrush = Brushes.White;
            GridAvailable.Visibility = Visibility.Hidden;
            GridHistory.Visibility = Visibility.Hidden;
            GridOpenStickers.Visibility = Visibility.Visible;
            GridSubmitted.Visibility = Visibility.Hidden;
            RectangleAvailable.Visibility = Visibility.Hidden;
            RectangleHistory.Visibility = Visibility.Hidden;
            RectangleSubmitted.Visibility = Visibility.Hidden;
            RectangleTutoring.Visibility = Visibility.Visible;
        }

        private void ButtonHistory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelTitle.Content = "Sticker History";
            LabelDescription.Content = "Closed Stickers you have submitted or tutored for:";
            ButtonAvailable.Background = UnstuckME.Blue;
            ButtonHistory.Background = Brushes.SteelBlue;
            ButtonSubmitted.Background = UnstuckME.Blue;
            ButtonTutoring.Background = UnstuckME.Blue;
            ButtonAvailable.BorderBrush = Brushes.Black;
            ButtonHistory.BorderBrush = Brushes.White;
            ButtonSubmitted.BorderBrush = Brushes.Black;
            ButtonTutoring.BorderBrush = Brushes.Black;
            GridAvailable.Visibility = Visibility.Hidden;
            GridHistory.Visibility = Visibility.Visible;
            GridOpenStickers.Visibility = Visibility.Hidden;
            GridSubmitted.Visibility = Visibility.Hidden;
            RectangleAvailable.Visibility = Visibility.Hidden;
            RectangleHistory.Visibility = Visibility.Visible;
            RectangleSubmitted.Visibility = Visibility.Hidden;
            RectangleTutoring.Visibility = Visibility.Hidden;
        }
    }
}