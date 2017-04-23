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
using System.Windows.Shapes;
using UnstuckME_Classes;
using UnstuckMEInterfaces;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddClassWindow.xaml
    /// </summary>

    public partial class AddClassWindow : Window
    {
        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        List<string> courseNumberandNameList = new List<string>();
        List<string> courseCodeList = new List<string>();

        public AddClassWindow()
        {
            InitializeComponent();
            
            try
            {
                courseCodeList = UnstuckME.Server.GetCourseCodes();
                
            }
            catch (Exception exp)
            {
                UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
                logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
            }
            courseCodeList[0] = "(Class)";
            ComboBoxCourseNumberAndName.ItemsSource = courseCodeList;
            ComboBoxCourseCode.ItemsSource = courseCodeList;
            ComboBoxCourseCode.IsEnabled = true;
            
            ComboBoxCourseCode.SelectedIndex = 0;
        }
        private void ComboBoxCourseCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> courseNameList = new List<string>();
            int selected = ComboBoxCourseCode.SelectedIndex;
            if (selected != 0)
            {
                try
                {
                    courseNameList = UnstuckME.Server.GetCourseNumbersByCourseCode(ComboBoxCourseCode.SelectedValue as string);
                    courseNameList.Insert(0, "(Select Class)");
                }
                catch (Exception exp)
                {
                    UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
                    logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
                }
                ComboBoxCourseNumberAndName.IsEnabled = true;
                ComboBoxCourseNumberAndName.ItemsSource = courseNameList;
                ComboBoxCourseNumberAndName.SelectedIndex = 0;
            }
            else
            {
                ComboBoxCourseCode.SelectedIndex = 0;
                ComboBoxCourseNumberAndName.IsEnabled = false;
            }

        }

        private void AddClassesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int ClassID = UnstuckME.Server.GetCourseIdNumberByCodeAndNumber(ComboBoxCourseCode.SelectedValue as string, ComboBoxCourseNumberAndName.SelectedValue as string);
                UnstuckME.Server.InsertStudentIntoClass(UnstuckME.User.UserID, ClassID);
                UnstuckME.Server.AddClassesToClient(ClassID, UnstuckME.User.UserID);
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message);
            }

            ComboBoxCourseCode.SelectedIndex = 0;
            ComboBoxCourseNumberAndName.SelectedIndex = 0;
            ComboBoxCourseNumberAndName.IsEnabled = false;
        }
    }
}
