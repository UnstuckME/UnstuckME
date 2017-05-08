using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckMeLoggers;
using UnstuckME_Classes;
using System.Threading.Tasks;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ReportSubmitWindow.xaml
    /// </summary>
    public partial class ReportSubmitWindow : Window
    {
        private static UnstuckMEReview _review;

        public string Description
        {
            get { return _review.Description; }
        }

        public float StarRank
        {
            get { return _review.StarRanking; }
        }

        public ReportSubmitWindow(UnstuckMEReview review)
        {
            InitializeComponent();
            _review = review;
        }

        private void ReportBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            ReportBorder.Background = Brushes.IndianRed;
        }

        private void ReportBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            ReportBorder.Background = Brushes.DarkRed;
        }

        private void ReportLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UnstuckMEMessageBox confirm = new UnstuckMEMessageBox(UnstuckMEBox.YesNo, "Are you sure you wish to send this report? This cannot be undone.", "Send Report",
                                                                      UnstuckMEBoxImage.Warning);
                bool? result = confirm.ShowDialog();

                if (result.HasValue && result.Value)
                    SetDialogResult(UnstuckME.Server.CreateReport(Reportdescription.Text, UnstuckME.User.UserID, _review.ReviewID) != Task.FromResult(-1));
                else
                    SetDialogResult(false);
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, ex.Source);
                UnstuckMEMessageBox messagebox = new UnstuckMEMessageBox(UnstuckMEBox.OK,
                                            "An error occured when trying to send the report. Please contact an UnstuckME administrator if this problem persists. Thank you.",
                                            "Report could not be sent", UnstuckMEBoxImage.Error);
                messagebox.ShowDialog();
                SetDialogResult(false);
            }

            CancelLabel_MouseLeftButtonDown(sender, e);
        }

        private void CancelBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            CancelBorder.Background = Brushes.GreenYellow;
        }

        private void CancelBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            CancelBorder.Background = Brushes.Green;
        }

        private void CancelLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void SetDialogResult(bool result)
        {
            DialogResult = result;
        }

        private void ReportSubmitWindowGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetDialogResult(false);
            CancelLabel_MouseLeftButtonDown(sender, e);
        }
    }
}
