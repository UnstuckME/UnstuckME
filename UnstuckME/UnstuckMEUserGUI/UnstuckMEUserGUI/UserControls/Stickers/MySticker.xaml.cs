using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
    /// Interaction logic for MySticker.xaml
    /// </summary>
    public partial class MySticker : UserControl
    {
        public UnstuckMESticker Sticker;

        public MySticker(UnstuckMESticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            UserClass Class = UnstuckME.Server.GetSingleClass(Sticker.ClassID);
            LabelClassName.Content = Class.CourseCode + "-" + Class.CourseNumber + ":  " + Class.CourseName;
            ProblemDescription.Text = "Problem Description:\n" + Sticker.ProblemDescription;

            if (Sticker.TutorID <= 1)
            {
                LabelTutorName.Content = "Currently Not Tutored";
                ButtonCompleted.Visibility = Visibility.Hidden;
            }
            else
            {
                try
                {
                    if (Sticker.TutorID.HasValue)
                    {
                        LabelTutorName.Content = "Tutor: " + UnstuckME.Server.GetUserDisplayName(Sticker.TutorID.Value);
                        ButtonDelete.Visibility = Visibility.Collapsed;
                        if (UnstuckME.Server.BeenReviewed(Sticker.StickerID, UnstuckME.User.UserID))
                            ButtonCompleted.Visibility = Visibility.Hidden;
                    }
                }
                catch (Exception ex)
                {
                    UnstuckMEMessageBox error = new UnstuckMEMessageBox(UnstuckMEBox.OK, ex.Message, "No Tutor Found", UnstuckMEBoxImage.Warning);
                    error.ShowDialog();
                }
            }
        }

        private void ButtonRemove_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (UnstuckME.Server.DeleteSticker(Sticker.StickerID) != Task.FromResult(-1))
                    ((StackPanel)Parent).Children.Remove(this);
            }
            catch (Exception ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }
        }

        private void ButtonCompleted_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window win = new AddTutorReviewWindow(Sticker.StickerID);
            win.ShowDialog();
        }

        private void ButtonCompleted_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonCompleted.Background = Brushes.ForestGreen;
        }

        private void ButtonCompleted_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonCompleted.Background = Brushes.DarkGreen;
        }

        private void ButtonDelete_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonDelete.Background = Brushes.IndianRed;
        }

        private void ButtonDelete_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonDelete.Background = Brushes.White;
        }
    }
}
