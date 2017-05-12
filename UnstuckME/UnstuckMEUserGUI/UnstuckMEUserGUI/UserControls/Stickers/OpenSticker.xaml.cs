using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for OpenSticker.xaml
    /// </summary>
    public partial class OpenSticker : UserControl
    {
        public UnstuckMESticker Sticker;

        public OpenSticker(UnstuckMESticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            UserInfo student = UnstuckME.Server.GetUserInfo(Sticker.StudentID, null);
            UserClass Class = UnstuckME.Server.GetSingleClass(Sticker.ClassID);
            LabelStudentName.Content = student.FirstName + " " + student.LastName;
            LabelClassName.Content = Class.CourseCode + "-" + Class.CourseNumber + ": " + Class.CourseName;
            ProblemDescription.Text = "Problem Description:\n" + Sticker.ProblemDescription;
            if (Sticker.Timeout < DateTime.Now)
                Visibility = Visibility.Collapsed;
        }

        private void ButtonCompleted_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonCompleted.Background = Brushes.ForestGreen;
        }

        private void ButtonCompleted_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonCompleted.Background = Brushes.DarkGreen;
        }

        private void ButtonCompleted_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window win = new SubWindows.AddStudentReviewWindow(Sticker.StickerID);
            win.Show();
        }

        public OpenSticker Remove()
        {
            ((StackPanel)Parent).Children.Remove(this);
            return this;
        }
    }
}
