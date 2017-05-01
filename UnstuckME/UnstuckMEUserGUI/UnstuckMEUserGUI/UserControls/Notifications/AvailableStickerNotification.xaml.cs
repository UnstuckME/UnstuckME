using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for AvailableStickerNotification.xaml
    /// </summary>
    public partial class AvailableStickerNotification : UserControl
    {
        public UnstuckMEAvailableSticker Sticker;
        public AvailableStickerNotification(UnstuckMEAvailableSticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            ClassNameInfoButton.Content = Sticker.CourseNumber + " " + Sticker.CourseCode + ": " + Sticker.CourseName;
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ClassNameInfoButton.Foreground = Brushes.Black;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ClassNameInfoButton.Foreground = Brushes.White;
        }

        private void ClassNameInfoButton_Click(object sender, RoutedEventArgs e)
        {
            ClassNameInfoButton.Content = "Clicked";
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            //Removes from stack panel.
            ((StackPanel)Parent).Children.Remove(this);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            //Removes from stack panel.
            ((StackPanel)Parent).Children.Remove(this);
        }
    }
}
