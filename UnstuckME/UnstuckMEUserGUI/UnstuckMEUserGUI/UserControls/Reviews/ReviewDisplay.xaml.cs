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
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for ReviewDisplay.xaml
	/// </summary>
	public partial class ReviewDisplay : UserControl
	{
		private static UnstuckMEReview _review;
		private static IUnstuckMEService Server;

		public ReviewDisplay(ref IUnstuckMEService OpenServer, UnstuckMEReview review)
		{
			InitializeComponent();
			Server = OpenServer;
			_review = review;

			//DescriptionLabel.Content = _review.Description;
			//ReviewStarRank.StarRank = _review.StarRanking;
		}

		private void ReportBtn_Click(object sender, RoutedEventArgs e)
		{
			
		}
	}
}
