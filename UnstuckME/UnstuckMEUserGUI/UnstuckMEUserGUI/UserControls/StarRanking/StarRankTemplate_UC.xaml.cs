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
			set { SetStarRank(value); }
		}

		public bool IsReadOnly
		{
			get { return _readonly; }
			set { ReadOnly(value); }
		}

		public StarRankTemplate_UC()
		{
			InitializeComponent();
		}

		private void ReadOnly(bool value)
		{
			_readonly = value;

			if (!_readonly) //false
			{
				FiveStarBtn.IsEnabled = false;
				FourStarBtn.IsEnabled = false;
				ThreeStarBtn.IsEnabled = false;
				TwoStarBtn.IsEnabled = false;
				OneStarBtn.IsEnabled = false;
			}
			else	//true
			{
				FiveStarBtn.IsEnabled = true;
				FourStarBtn.IsEnabled = true;
				ThreeStarBtn.IsEnabled = true;
				TwoStarBtn.IsEnabled = true;
				OneStarBtn.IsEnabled = true;
			}
		}

		private void SetStarRank(float value)
		{
			_starrank = value;
			//bool temp = _readonly;
			//_readonly = false;

			//if (_starrank <= 1)
			//	OneStarBtn_Click(this, null);
			//else if (_starrank <= 2)
			//	TwoStarBtn_Click(this, null);
			//else if (_starrank <= 3)
			//	ThreeStarBtn_Click(this, null);
			//else if (_starrank <= 4)
			//	FourStarBtn_Click(this, null);
			//else
			//	FiveStarBtn_Click(this, null);

			//_readonly = temp;
		}

		private void OneStarBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!_readonly)
			{
				//need to figure out how to shade images if possible

				if (StarRank % 10 == 0)
					StarRank = 0.5f;
				else
					StarRank = 1.0f;
			}
		}

		private void TwoStarBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!_readonly)
			{
				//need to figure out how to shade images if possible

				if (StarRank % 10 == 0)
					StarRank = 1.5f;
				else
					StarRank = 2.0f;
			}
		}

		private void ThreeStarBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!_readonly)
			{
				//need to figure out how to shade images if possible

				if (StarRank % 10 == 0)
					StarRank = 2.5f;
				else
					StarRank = 3.0f;
			}
		}

		private void FourStarBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!_readonly)
			{
				//need to figure out how to shade images if possible

				if (StarRank % 10 == 0)
					StarRank = 3.5f;
				else
					StarRank = 4.0f;
			}
		}

		private void FiveStarBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!_readonly)
			{
				//need to figure out how to shade images if possible

				if (StarRank % 10 == 0)
					StarRank = 4.5f;
				else
					StarRank = 5.0f;
			}
		}
	}
}
