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
using UnstuckMEInterfaces;
using System.Threading;


namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ClassDisplay.xaml
    /// </summary>
    public partial class ClassDisplay : UserControl
    {
        UserClass Class;
        FrameworkElement lastClick = null;

        public ClassDisplay(UserClass inClass)
        {
            Class = inClass;

            InitializeComponent();
            CourseCode.Text = Class.CourseCode;
            CourseNumber.Text = Class.CourseNumber.ToString();
            Coursedesc.Text = Class.CourseName;
            //Deletebtn.Name = "DeleteBtn" + m_IDnum.ToString();
            MainContainer.Name = "MainContainer" + Class.ClassID.ToString();
        }

        private void MainContainer_Click(object sender, RoutedEventArgs e)
        {
            if (Deletebtn.Visibility == Visibility.Visible)
            {
                Deletebtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                Deletebtn.Visibility = Visibility.Visible;
            }
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
                    AvailableSticker avail_sticker = availablestickers[index] as AvailableSticker;
                    if (avail_sticker.Sticker.ClassID == Class.ClassID)
                    {
                        UnstuckME.Pages.StickerPage.AvailableStickers.Remove(avail_sticker.Sticker);
                        avail_sticker.RemoveFromStackPanel();
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
