using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                var trace = new StackTrace(exp, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message, trace.Name);
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
                    var trace = new StackTrace(exp, true).GetFrame(0).GetMethod();
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message, trace.Name);
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
                var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
            }

            ComboBoxCourseCode.SelectedIndex = 0;
            ComboBoxCourseNumberAndName.SelectedIndex = 0;
            ComboBoxCourseNumberAndName.IsEnabled = false;
        }
    }
}
