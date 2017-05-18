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
using UnstuckMeLoggers;

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
            string OrgName = this.orgName.Text;

            if (OrgName != string.Empty)
            {
                // call server function to add an org
                try
                {
                    UnstuckME.Server.CreateMentorOrg(OrgName);
                }
                catch (Exception ex)
                {
                    var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error occured while creating mentor org, Source = " + trace.Name);
                }
            }
        }
    }
}
