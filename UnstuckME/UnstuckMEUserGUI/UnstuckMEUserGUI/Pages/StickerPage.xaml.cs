﻿using System;
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
    /// Interaction logic for StickerPage.xaml
    /// </summary>
    public partial class StickerPage : Page
    {
        public List<UnstuckMEAvailableSticker> AvailableStickers;
        public List<UnstuckMESticker> OpenStickers;
        public List<UnstuckMEAvailableSticker> RecentStickers;
        public List<UnstuckMESticker> MyStickersList;

        public StickerPage()
        {
            InitializeComponent();
        }

        public void RemoveSticker(int stickerID)
        {
            foreach (OpenSticker opensticker in StackPanelOpenStickers.Children)
            {
                if (opensticker.Sticker.StickerID == stickerID)
                {
                    StackPanelStickerHistory.Children.Add(opensticker.Remove());

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

            foreach (MySticker mysticker in StackPanelMyStickers.Children)
            {
                if (mysticker.Sticker.StickerID == stickerID)
                    mysticker.Resolve();
            }
        }

        private void ButtonAvailable_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelTitle.Content = "Available Stickers";
            LabelDescription.Content = "Stickers you qualify to tutor for.";
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
        }

        private void ButtonSubmitted_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelTitle.Content = "Submitted Stickers";
            LabelDescription.Content = "Stickers you have submitted that need to be tutored.";
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
        }

        private void ButtonTutoring_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelTitle.Content = "Tutor Stickers";
            LabelDescription.Content = "Stickers you are currently tutoring.";
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
        }

        private void ButtonHistory_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LabelTitle.Content = "Sticker History";
            LabelDescription.Content = "Closed Stickers you have submitted or tutored for.";
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
        }
    }
}