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
	/// Interaction logic for StickerDisplay.xaml
	/// </summary>
	public partial class StickerDisplay : UserControl
	{
		private static IUnstuckMEService Server;
		private static int User;

		public StickerDisplay(int UserID, ref IUnstuckMEService OpenServer, UnstuckMESticker sticker)
		{
			InitializeComponent();
			Server = OpenServer;
			User = UserID;

			UserClass _class = Server.GetSingleClass(sticker.ClassID);
			UserInfo user = Server.GetUserInfo(sticker.StudentID, null);

			SubmittedByLabel.Content += user.FirstName + " " + user.LastName;
			CourseCodeLabel.Content = _class.CourseCode;
			CourseNameLabel.Content = _class.CourseName;
			CourseNumLabel.Content = _class.CourseNumber;
			ProblemDescriptionLabel.Content = sticker.ProblemDescription;
			//MinimmumStarRank.StarRank = sticker.MinimumStarRanking;

			//if (sticker.TutoringOrganizations.Count != 0)
			//{
			//	TutoringOrganizations.Items.Remove(NoOrganizationFilter);
				
			//	//add listviewitems for each tutoring organization in the sticker.TutoringOrganizations list
			//}

			//if (sticker.Timeout.Hour > 12)
			//{
			//	Hours.Content = sticker.Timeout.Hour - 12;
			//	AM_PM.Content = "PM";
			//}
			//else
			//{
			//	Hours.Content = sticker.Timeout.Hour;
			//	AM_PM.Content = "AM";
			//}

			//Minutes.Content = sticker.Timeout.Minute;
			//LastDayAvailable.Text = sticker.Timeout.Month + "/" + sticker.Timeout.Day + "/" + sticker.Timeout.Year;
		}
	}
}
