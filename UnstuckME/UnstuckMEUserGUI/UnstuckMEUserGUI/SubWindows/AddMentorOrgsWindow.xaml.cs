using System;
using System.Diagnostics;
using System.Windows;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddMentorOrgsWindow.xaml
    /// </summary>
    public partial class AddMentorOrgsWindow : Window
    {
        public AddMentorOrgsWindow()
        {
            InitializeComponent();
        }

        private void AddOrgBtn_Click(object sender, RoutedEventArgs e)
        {
            string OrgName = orgName.Text;
            if (OrgName != string.Empty)
            {
                // call server function to add an org
                try
                {
                    UnstuckME.Server.CreateMentorOrg(OrgName);
                }
                catch (Exception ex)
                {
                    var trace = new StackTrace(ex, true).GetFrame(0).GetMethod();
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error occured while creating mentor org, Source = " + trace.Name);
                }
            }
        }
    }
}
