using System;
using System.Windows;
using System.Windows.Controls;
using UnstuckMeLoggers;
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
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, e.Message, "This error occured while retriving a review for moderation");
            }
            
            DescriptionContent.Text = review.Description;
            try
            {
                IssueContent.Text = UnstuckME.Server.GetReportDescription(reportID);
            }
            catch (Exception e)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, e.Message, "This error occured while retriving a review problem description for moderation");
            }
            
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            UnstuckME.Server.MarkReportedReviewAsResolved(true, reportID);
            Visibility = Visibility.Collapsed;
        }

        private void NotOkBtn_Click(object sender, RoutedEventArgs e)
        {
            UnstuckME.Server.MarkReportedReviewAsResolved(false, reportID);
            Visibility = Visibility.Collapsed;
        }
    }
}
