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
		private static bool _readonly = false;
		private static float _starrank = 0;

		public float StarRank
		{
			get { return _starrank; }
			set { _starrank = value; }
		}

		public bool ReadOnly
		{
			get { return _readonly; }
			set
			{
				_readonly = value;

				if (!_readonly) //false
					IsHitTestVisible = false;
				else    //true
					IsHitTestVisible = true;
			}
		}

		public StarRankTemplate_UC()
		{
			InitializeComponent();
		}

		private void OneStar_Checked(object sender, RoutedEventArgs e)
		{
			_starrank = 1;
			StarFill.Width = 20;
		}

		private void OneStar_Unchecked(object sender, RoutedEventArgs e)
		{
			_starrank = 0;
			StarFill.Width = 0;
		}

		private void TwoStar_Checked(object sender, RoutedEventArgs e)
		{
			_starrank = 2;
			StarFill.Width = 40;
		}

		private void ThreeStar_Checked(object sender, RoutedEventArgs e)
		{
			_starrank = 3;
			StarFill.Width = 60;
		}

		private void FourStar_Checked(object sender, RoutedEventArgs e)
		{
			_starrank = 4;
			StarFill.Width = 80;
		}

		private void FiveStar_Checked(object sender, RoutedEventArgs e)
		{
			_starrank = 5;
			StarFill.Width = 100;
		}

		private void OneStar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (_starrank == 1)
				OneStar_Unchecked(sender, e);
			else
				OneStar_Checked(sender, e);
		}

		private void TwoStar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (_starrank == 2)
				OneStar_Checked(sender, e);
			else
				TwoStar_Checked(sender, e);
		}

		private void ThreeStar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (_starrank == 3)
				TwoStar_Checked(sender, e);
			else
				ThreeStar_Checked(sender, e);
		}

		private void FourStar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (_starrank == 4)
				ThreeStar_Checked(sender, e);
			else
				FourStar_Checked(sender, e);
		}

		private void FiveStar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (_starrank == 5)
				FourStar_Checked(sender, e);
			else
				FiveStar_Checked(sender, e);
		}
	}
}
