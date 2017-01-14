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
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ClassDisplay.xaml
    /// </summary>
    public partial class ClassDisplay : UserControl
    {
        public static int m_UserID;
        public static IUnstuckMEService m_OpenServer;
        string m_code;
        int m_number;
        string m_desc;
        int m_IDnum;
        FrameworkElement lastClick = null;
        public ClassDisplay(int UserID, IUnstuckMEService OpenServer, string code, int number, string desc, int IDnum)
        {
            m_UserID = UserID;
            m_OpenServer = OpenServer;
            m_code = code;
            m_number = number;
            m_desc = desc;
            m_IDnum = IDnum;

            InitializeComponent();
            CourseCode.Text = m_code;
            CourseNumber.Text = m_number.ToString();
            Coursedesc.Text = m_desc;
            //Deletebtn.Name = "DeleteBtn" + m_IDnum.ToString();
            MainContainer.Name = "MainContainer" + m_IDnum.ToString();
        }

        private void MainContainer_Click(object sender, RoutedEventArgs e)
        {
            Deletebtn.Visibility = Visibility.Visible;
            lastClick = e.Source as FrameworkElement;
        }

        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement s = e.Source as FrameworkElement;
            
            string toDelete = lastClick.Name;
            toDelete = toDelete.Remove(0, 13);
            //remove the above course number
            int courseToRemove = Convert.ToInt32(toDelete);
            //int courseToRemove = Convert.ToInt32(lastClick.Name);
            m_OpenServer.RemoveUserFromClass(m_UserID, courseToRemove);
            lastClick.Visibility = Visibility.Collapsed;
        }
    }
}
