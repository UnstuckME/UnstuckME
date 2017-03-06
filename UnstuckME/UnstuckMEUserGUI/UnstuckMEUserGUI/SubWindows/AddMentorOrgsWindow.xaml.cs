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
            if (OrgName != "")
            {
                // call server function to add an org
                try
                {
                    UnstuckME.Server.CreateMentorOrg(OrgName);
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error occured while creating mentor org");
                    
                }
                
            }
        }
        
    }
}
