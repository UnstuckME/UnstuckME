using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddClassWindow.xaml
    /// </summary>

    public partial class AddClassWindow : Window
    {
        List<string> courseNumberandNameList = new List<string>();
        List<string> courseCodeList = new List<string>();

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

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
                logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message, exp.Source);
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
                    logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message, exp.Source);
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
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, ex.Source);
            }

            ComboBoxCourseCode.SelectedIndex = 0;
            ComboBoxCourseNumberAndName.SelectedIndex = 0;
            ComboBoxCourseNumberAndName.IsEnabled = false;
        }
    }
}
