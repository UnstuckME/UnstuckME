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
	/// Interaction logic for Sticker_UC.xaml
	/// Object used to create a new Sticker
	/// Should change the CourseCode and CourseNumber objects so that it implements the same functionality as searching through all available classes
	/// </summary>
	public partial class Sticker_UC : UserControl
	{
		private static IUnstuckMEService Server;
		private static int User;

		public Sticker_UC(int UserID, ref IUnstuckMEService OpenServer)
		{
			InitializeComponent();
			Server = OpenServer;
			User = UserID;

			List<string> organizations = Server.GetAllOrganizations();

			if (organizations.Any())
			{
				TutoringOrganizations.Items.Remove(DefaultUserOrganization);

				foreach (var organization in organizations)
				{
					ListBoxItem item = new ListBoxItem();
					item.Name = organization + "Box";
					item.Content = organization;
					item.HorizontalAlignment = HorizontalAlignment.Center;
					item.VerticalAlignment = VerticalAlignment.Center;

					TutoringOrganizations.Items.Add(item);
				}
			}
		}

		private void SubmitStickerBtn_Click(object sender, RoutedEventArgs e)
		{
			UnstuckMESticker sticker = new UnstuckMESticker();

			sticker.ClassID = Server.GetCourseIdNumberByCodeAndNumber(CourseCodeBox.Text, CourseNumberBox.Text);
			sticker.ProblemDescription = ProblemDescriptionBox.Text;
			sticker.MinimumStarRanking = MinimmumStarRank.StarRank;
			sticker.StudentID = User;

			DateTime timeout = LastDayAvailable.SelectedDate.Value;
			sticker.Timeout = new DateTime(timeout.Year, timeout.Month, timeout.Day, int.Parse(((ListBoxItem)HoursLeft.SelectedItem).Content.ToString()),
											int.Parse(((ListBoxItem)MinutesLeft.SelectedItem).Content.ToString()), 0);

			Server.SubmitSticker(sticker);
		}
	}
}
