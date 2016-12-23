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
	/// Interaction logic for UserLoginSettingsPage.xaml
	/// </summary>
	public partial class UserLoginSettingsPage : Page
	{
		public UserLoginSettingsPage()
		{
			InitializeComponent();
		}
	
		private void button_Click(object sender, RoutedEventArgs e)
		{
			// get the data selected from the drop down
			// write the associated info to the config file
			NavigationService.GoBack();
		}
	}
}
