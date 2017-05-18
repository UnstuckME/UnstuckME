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

namespace UnstuckMEUserGUI.UserControls.Reviews
{
    /// <summary>
    /// Interaction logic for ResolveReportsDisplay.xaml
    /// </summary>
    public partial class ResolveReportsDisplay : UserControl
    {
        int reportID;
        UnstuckMEReview review;
        public ResolveReportsDisplay(int ReportID)
        {
            InitializeComponent();
            reportID = ReportID;
            try
            {
                review = (UnstuckME.Server.GetReportedReview(reportID));
            }
            catch (Exception e)
            {
                UnstuckMeLoggers.UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(UnstuckMeLoggers.ERR_TYPES.USER_SERVER_CONNECTION_ERROR, e.Message, "This error occured while retriving a review for moderation");
            }
            
            DescriptionContent.Text = review.Description;
            try
            {
                IssueContent.Text = UnstuckME.Server.GetReportDescription(reportID);
            }
            catch (Exception e)
            {
                UnstuckMeLoggers.UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(UnstuckMeLoggers.ERR_TYPES.USER_SERVER_CONNECTION_ERROR, e.Message, "This error occured while retriving a review problem description for moderation");
            }
            
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            UnstuckME.Server.MarkReportedReviewAsResolved(true, reportID);
            this.Visibility = Visibility.Collapsed;
        }

        private void NotOkBtn_Click(object sender, RoutedEventArgs e)
        {
            UnstuckME.Server.MarkReportedReviewAsResolved(false, reportID);
            this.Visibility = Visibility.Collapsed;
        }
    }
}
