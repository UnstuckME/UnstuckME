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
    /// Interaction logic for MySticker.xaml
    /// </summary>
    public partial class MySticker : UserControl
    {
        public UnstuckMESticker Sticker;
        UserClass Class;

        public MySticker(UnstuckMESticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            Class = UnstuckME.Server.GetSingleClass(Sticker.ClassID);
            LabelClassName.Content = Class.CourseCode + "-" + Class.CourseNumber + ":  " + Class.CourseName;
            LabelDescription.Content = Sticker.ProblemDescription;
			LabelTimeout.Content = "Timeout: " + Sticker.Timeout.ToLongDateString();
        }

        private void ButtonRemove_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonRemove.Background = Brushes.MistyRose;
        }

        private void ButtonRemove_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonRemove.Background = UnstuckME.Red;
        }

        private void ButtonRemove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (UnstuckME.Server.DeleteSticker(Sticker.StickerID) == 0)
                ((StackPanel)Parent).Children.Remove(this);
        }

        private void ButtonCompleted_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonCompleted.Background = Brushes.LimeGreen;
        }

        private void ButtonCompleted_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonCompleted.Background = Brushes.ForestGreen;
        }

        private void ButtonCompleted_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window win = new SubWindows.AddTutorReviewWindow(Sticker.StickerID);
            win.Show();
        }
    }
}
