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
        StackPanel ItemContainer = null;
        public ClassDisplay(StackPanel itemcontainer, int UserID, IUnstuckMEService OpenServer, string code, int number, string desc, int IDnum)
        {
            m_UserID = UserID;
            m_OpenServer = OpenServer;
            m_code = code;
            m_number = number;
            m_desc = desc;
            m_IDnum = IDnum;
            ItemContainer = itemcontainer;

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
            m_OpenServer.RemoveUserFromClass(m_UserID, m_IDnum);    //this call currently does nothing but if it did this function would work
            lastClick.Visibility = Visibility.Collapsed;    // this line may not even be needed
            ItemContainer.Children.Clear();
            List<UserClass> classes = m_OpenServer.GetUserClasses(m_UserID);
            foreach (UserClass C in classes)
            {
                ClassDisplay usersClass = new ClassDisplay(ItemContainer, m_UserID, m_OpenServer, C.CourseCode, C.CourseNumber, C.CourseName, 1);
                ItemContainer.Children.Add(usersClass);
            }

        }
    }
}
