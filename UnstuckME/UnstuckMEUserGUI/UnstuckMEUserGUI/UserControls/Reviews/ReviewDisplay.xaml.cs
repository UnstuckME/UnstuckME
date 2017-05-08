using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using UnstuckME_Classes;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for ReviewDisplay.xaml
	/// </summary>
	public partial class ReviewDisplay : UserControl
	{
        private static UnstuckMEReview _review;
        private static bool _reported;

        public string Description
        {
            get { return _review.Description; }
        }

        public float StarRank
        {
            get { return _review.StarRanking; }
        }

        internal bool Reported
        {
            get { return _reported; }
        }

		public ReviewDisplay(UnstuckMEReview review, bool reported)
		{
			InitializeComponent();
            _review = review;
            _reported = reported;

            if (_reported)
                UpdateReportStatus();
		}

        private void UpdateReportStatus()
        {
            ReportBorder.Background = Brushes.DimGray;
            ReportLabel.Content = "Reported";
        }

	    private void ReportLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	    {
	        try
	        {
	            ReportSubmitWindow reportWindow = new ReportSubmitWindow(_review)
	            {
	                Owner = Application.Current.MainWindow
	            };

	            bool? result = reportWindow.ShowDialog();

                if (result.HasValue && result.Value)
    	            UpdateReportStatus();
	        }
	        catch (Exception ex)
	        {
	            UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, ex.Source);
	        }
	    }

        private void DescriptionTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            ReportBorder.Visibility = Visibility.Visible;
        }

        private void DescriptionTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            ReportBorder.Visibility = Visibility.Hidden;
        }

        private void ReportBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            ReportBorder.Background = Brushes.IndianRed;
        }

        private void ReportBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            ReportBorder.Background = Brushes.DarkRed;
        }
    }
}
