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
    /// Interaction logic for OpenSticker.xaml
    /// </summary>
    public partial class OpenSticker : UserControl
    {
        public UnstuckMESticker Sticker;
        UserInfo Student;
        UserClass Class;

        public OpenSticker(UnstuckMESticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            Student = UnstuckME.Server.GetUserInfo(Sticker.StudentID, null);
            Class = UnstuckME.Server.GetSingleClass(Sticker.ClassID);
            LabelStudentName.Content = Student.FirstName + " " + Student.LastName;
            LabelClassName.Content = Class.CourseCode + "-" + Class.CourseNumber + ": " + Class.CourseName;
            LabelDescription.Content = Sticker.ProblemDescription;
			LabelTimeout.Content = "Timeout: " + Sticker.Timeout.ToLongDateString();//DateTime.Now.AddSeconds(Sticker.Timeout).ToLongDateString() + " " + DateTime.Now.AddSeconds(Sticker.Timeout).ToShortTimeString();
            if (Sticker.Timeout < DateTime.Now)
            {
                this.Visibility = Visibility.Collapsed;
            }
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
