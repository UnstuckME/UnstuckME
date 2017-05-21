using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using UnstuckMeLoggers;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI.UserControls.Admin
{
    /// <summary>
    /// Interaction logic for OrgsArea.xaml
    /// </summary>
    public partial class OrgsArea : UserControl
    {
        public OrgsArea()
        {
            InitializeComponent();
            List<Organization> orgList = UnstuckME.Server.GetAllOrganizations();
            foreach (Organization item in orgList)
                // this is going to end up needing to be a different control with the same look so we can remove these on click from the db
                StackPanelOrganization.Children.Add(new TutoringOrganizationDisplay(item.MentorID, item.OrganizationName));
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
