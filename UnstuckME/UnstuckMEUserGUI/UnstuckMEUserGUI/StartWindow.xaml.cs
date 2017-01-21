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
using System.ServiceModel;
using UnstuckMEInterfaces;
using System.Configuration;
using System.Threading;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class StartWindow : Window
	{
		public static IUnstuckMEService Server;

		public StartWindow(ref IUnstuckMEService inboundServer, ref UserInfo inboundUser, ref byte [] inboundImg)
		{
			InitializeComponent();
            Server = inboundServer;
			_mainFrame.Navigate(new MainPage(ref Server, ref inboundUser, ref inboundImg));
		}

        public void ForceCloseStart()
        {
            Task.Factory.StartNew(() => ForceClose()).ContinueWith(t => t.Result.Show(), TaskScheduler.FromCurrentSynchronizationContext());
            Task.Factory.StartNew(() => returnCurrent()).ContinueWith(t => t.Result.Close(), TaskScheduler.FromCurrentSynchronizationContext());
        }
        UnstuckMEMessageBox ForceClose()
        {
            return new UnstuckMEMessageBox(0, "Server has shutdown, Please contact your Server Administrator.");
        }

        StartWindow returnCurrent()
        {
            return this;
        }

		private void UnstuckME_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				this.Hide();
				Server.Logout();
			}
			catch (Exception)
			{
			}
		}
	}
}
