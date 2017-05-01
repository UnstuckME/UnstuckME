using System;
using System.Windows;
using System.Windows.Controls;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ClassDisplay.xaml
    /// </summary>
    public partial class ClassDisplay : UserControl
    {
        private UserClass Class;
        private FrameworkElement lastClick = null;

        public ClassDisplay(UserClass inClass)
        {
            Class = inClass;

            InitializeComponent();
            CourseCode.Text = Class.CourseCode;
            CourseNumber.Text = Class.CourseNumber.ToString();
            Coursedesc.Text = Class.CourseName;
            //Deletebtn.Name = "DeleteBtn" + m_IDnum.ToString();
            MainContainer.Name = "MainContainer" + Class.ClassID;
        }

        private void MainContainer_Click(object sender, RoutedEventArgs e)
        {
            Deletebtn.Visibility = Deletebtn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            lastClick = e.Source as FrameworkElement;
        }
        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            try
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
                ((StackPanel)Parent).Children.Remove(this);
            }
            catch (Exception ex)
            {
                UnstuckMeLoggers.UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(UnstuckMeLoggers.ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message);
            }
        }
    }
}
