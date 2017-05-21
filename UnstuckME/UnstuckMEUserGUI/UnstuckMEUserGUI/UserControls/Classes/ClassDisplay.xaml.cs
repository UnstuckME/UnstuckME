using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckMeLoggers;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ClassDisplay.xaml
    /// </summary>
    public partial class ClassDisplay : UserControl
    {
        private readonly UserClass Class;

        public ClassDisplay(UserClass inClass)
        {
            Class = inClass;

            InitializeComponent();
            CourseCode.Text = Class.CourseCode;
            CourseNumber.Text = Class.CourseNumber.ToString();
            Coursedesc.Text = Class.CourseName;
            MainContainer.Name = "MainContainer" + Class.ClassID;
        }

        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OKCancel, string.Format("Are you sure you wish to remove {0} from your profile?", Class.CourseName), "Remove Class?", UnstuckMEBoxImage.Warning);
                bool? open = messagebox.ShowDialog();

                if (open.HasValue && open.Value)
                {
                    UnstuckME.Server.RemoveUserFromClass(UnstuckME.User.UserID, Class.ClassID);

                    UIElementCollection availablestickers = UnstuckME.Pages.StickerPage.StackPanelAvailableStickers.Children;
                    for (int index = availablestickers.Count - 1; index >= 0; index--)
                    {
                        AvailableSticker availSticker = availablestickers[index] as AvailableSticker;
                        if (availSticker != null && availSticker.Sticker.ClassID == Class.ClassID)
                        {
                            UnstuckME.Pages.StickerPage.AvailableStickers.Remove(availSticker.Sticker);
                            availSticker.RemoveFromStackPanel();
                        }
                    }

                    //Removes Sticker From Stack Panel
                    ((StackPanel) Parent).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, trace.Name);
            }
        }

        private void MainContainer_MouseEnter(object sender, MouseEventArgs e)
        {
            Deletebtn.Visibility = Visibility.Visible;
            ClassBorder.BorderBrush = Brushes.LightGray;
        }

        private void MainContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            Deletebtn.Visibility = Visibility.Hidden;
            ClassBorder.BorderBrush = Brushes.Black;
        }
    }
}
