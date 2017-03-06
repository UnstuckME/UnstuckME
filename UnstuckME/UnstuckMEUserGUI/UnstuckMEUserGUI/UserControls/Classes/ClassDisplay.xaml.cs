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
        //public static int m_UserID;
        //public static IUnstuckMEService m_OpenServer;
        UserClass Class;
        FrameworkElement lastClick = null;

        public ClassDisplay(UserClass inClass)
        {
            //m_UserID = UserID;
            //m_OpenServer = OpenServer;
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
            
            UnstuckME.Server.RemoveUserFromClass(UnstuckME.User.UserID, Class.ClassID);
            lastClick.Visibility = Visibility.Collapsed;    // this line may not even be needed
            //Removes Sticker From Stack Panel
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
