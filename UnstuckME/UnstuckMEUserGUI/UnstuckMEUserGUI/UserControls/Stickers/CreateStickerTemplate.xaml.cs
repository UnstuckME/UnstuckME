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
	public partial class CreateStickerTemplate : UserControl
	{
		private static IUnstuckMEService Server;
		private static int User;

		public CreateStickerTemplate(int UserID, ref IUnstuckMEService OpenServer)
		{
			InitializeComponent();
			Server = OpenServer;
			User = UserID;

			//List<string> organizations = Server.GetAllOrganizations();
			List<string> subjects = Server.GetCourseCodes();
			CourseCodeComboBox.ItemsSource = subjects;

			//if (organizations.Count > 0)
			//{
			//	TutoringOrganizations.Items.Remove(DefaultUserOrganization);

			//	foreach (var organization in organizations)
			//	{
			//		ListBoxItem item = new ListBoxItem();
			//		item.Content = organization;
			//		item.HorizontalAlignment = HorizontalAlignment.Center;
			//		item.VerticalAlignment = VerticalAlignment.Center;

			//		TutoringOrganizations.Items.Add(item);
			//	}
			//}
		}

		private void SubmitStickerBtn_Click(object sender, RoutedEventArgs e)
		{
			UnstuckMESticker sticker = new UnstuckMESticker();

			sticker.ClassID = Server.GetCourseIdNumberByCodeAndNumber(CourseCodeComboBox.SelectedValue as string, CourseNumandNameComboBox.SelectedValue as string);
			sticker.ProblemDescription = ProblemDescriptionBox.Text;
			//sticker.MinimumStarRanking = MinimmumStarRank.StarRank;
			sticker.StudentID = User;

			//are we adding tutoring organizations on stickers into the database?
			//foreach (ListBoxItem organization in TutoringOrganizations.SelectedItems)
			//	sticker.TutoringOrganizations.Add(organization.Content.ToString());

			DateTime timeout = LastDayAvailable.SelectedDate.Value;
			int hour = Convert.ToInt32(HoursLeft.SelectedValue as string);

			if (AM_PM.SelectedValue as string == "PM")
				hour += 12;

			//sticker.Timeout = new DateTime(timeout.Year, timeout.Month, timeout.Day, hour, Convert.ToInt32(MinutesLeft.SelectedValue as string), 0);

			//Server.SubmitSticker(sticker);
			App.Current.MainWindow.Close();
		}

		private void CourseCodeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string selected = CourseCodeComboBox.SelectedValue as string;

			if (selected != null)
			{
				List<string> coursenums = Server.GetCourseNumbersByCourseCode(selected);
				CourseNumandNameComboBox.ItemsSource = coursenums;
			}
		}

		private void CourseNumandNameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			string selected = CourseNumandNameComboBox.SelectedValue as string;

			if (selected != null)
				CourseNameTextblock.Text = Server.GetCourseNameByCodeAndNumber(CourseCodeComboBox.SelectedValue as string, CourseNumandNameComboBox.SelectedValue as string);
		}
	}
}
