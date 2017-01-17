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
	/// </summary>
	public partial class Sticker_UC : UserControl
	{
		//private static UnstuckMESticker sticker;
		private static IUnstuckMEService Server;

		public Sticker_UC(IUnstuckMEService OpenServer)
		{
			InitializeComponent();
			Server = OpenServer;
		}

		private void SubmitStickerBtn_Click(object sender, RoutedEventArgs e)
		{
			//Server.SubmitSticker(sticker);
		}
	}
}
