using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckMeLoggers;
using UnstuckMEUserGUI.SubWindows;
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
            Window win = new AddStudentReviewWindow(Sticker.StickerID);
            win.ShowDialog();
        }

        public OpenSticker Remove()
        {
            ((StackPanel)Parent).Children.Remove(this);
            return this;
        }

        private void ButtonUnTutor_MouseEnter(object sender, MouseEventArgs e)
        {
            //ButtonUnTutor.Background = Brushes.IndianRed;
        }

        private void ButtonUnTutor_MouseLeave(object sender, MouseEventArgs e)
        {
            //ButtonUnTutor.Background = Brushes.White;
        }

        private void ButtonUnTutor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.YesNo, "Are you sure you wish to untutor this sticker?", "Warning", UnstuckMEBoxImage.Warning);
            bool? open = messagebox.ShowDialog();

            if (open.HasValue && open.Value)
            {
                try
                {
                    UnstuckME.Server.RemoveTutorFromSticker(UnstuckME.User.UserID);
                    Remove();
                }
                catch (Exception ex)
                {
                    var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
                }
            }
        }
    }
}
