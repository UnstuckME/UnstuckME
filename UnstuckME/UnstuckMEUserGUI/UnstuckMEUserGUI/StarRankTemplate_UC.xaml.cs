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

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for StarRankTemplate.xaml
	/// </summary>
	public partial class StarRankTemplate_UC : UserControl
	{
		public float StarRank { get; set; }

		public StarRankTemplate_UC()
		{
			InitializeComponent();
		}

		private void OneStarBtn_Click(object sender, RoutedEventArgs e)
		{
			//need to figure out how to shade images if possible

			if (StarRank % 10 == 0)
				StarRank = 0.5f;
			else
				StarRank = 1.0f;
		}

		private void TwoStarBtn_Click(object sender, RoutedEventArgs e)
		{
			//need to figure out how to shade images if possible

			if (StarRank % 10 == 0)
				StarRank = 1.5f;
			else
				StarRank = 2.0f;
		}

		private void ThreeStarBtn_Click(object sender, RoutedEventArgs e)
		{
			//need to figure out how to shade images if possible

			if (StarRank % 10 == 0)
				StarRank = 2.5f;
			else
				StarRank = 3.0f;
		}

		private void FourStarBtn_Click(object sender, RoutedEventArgs e)
		{
			//need to figure out how to shade images if possible

			if (StarRank % 10 == 0)
				StarRank = 3.5f;
			else
				StarRank = 4.0f;
		}

		private void FiveStarBtn_Click(object sender, RoutedEventArgs e)
		{
			//need to figure out how to shade images if possible

			if (StarRank % 10 == 0)
				StarRank = 4.5f;
			else
				StarRank = 5.0f;
		}
	}
}
