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
	    private readonly bool _reported;

		public ReviewDisplay(UnstuckMEReview review, bool reported)
		{
			InitializeComponent();
            _review = review;
		    _reported = reported;
		    ReviewDescription.Text = _review.Description;
            StarRating.Value = _review.StarRanking / 5;

            if (_reported)
                UpdateReportStatus();
		}

        private void UpdateReportStatus()
        {
            ReportBorder.Background = Brushes.DimGray;
            ReportLabel.Content = "Reported";
            ReportLabel.IsEnabled = false;
        }

	    private void ReportLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	    {
	        try
	        {
	            ReportSubmitWindow reportWindow = new ReportSubmitWindow(_review);
                reportWindow.ShowDialog();

                if (reportWindow.Result)
    	            UpdateReportStatus();
	        }
	        catch (Exception ex)
	        {
	            var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, ex.Message, trace.Name);
	        }
	    }

        private void DescriptionTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            ReportBorder.Visibility = Visibility.Visible;
            DescriptionTextBox.BorderBrush = Brushes.LightGray;
        }

        private void DescriptionTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            ReportBorder.Visibility = Visibility.Hidden;
            DescriptionTextBox.BorderBrush = Brushes.Black;
        }

        private void ReportBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            DescriptionTextBox_MouseEnter(sender, e);
            ReportBorder.Background = !_reported ? Brushes.IndianRed : Brushes.LightGray;
        }

        private void ReportBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            ReportBorder.Background = !_reported ? Brushes.DarkRed : Brushes.DimGray;
        }
    }
}
