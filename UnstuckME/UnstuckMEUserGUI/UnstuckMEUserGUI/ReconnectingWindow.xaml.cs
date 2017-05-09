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
using System.Windows.Shapes;
using System.Windows.Threading;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ReconnectingWindow.xaml
    /// </summary>
    public partial class ReconnectingWindow : Window
    {
        TimeSpan _time;
        private readonly DispatcherTimer _timer;
        public ReconnectingWindow()
        {
            InitializeComponent();
            _time = TimeSpan.Zero;
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if(_time.Seconds < 10)
                {
                    ReconnectingLabel.Content = string.Format("{0}:0{1}", _time.Minutes, _time.Seconds);
                }
                else
                {
                    ReconnectingLabel.Content = string.Format("{0}:{1}", _time.Minutes, _time.Seconds);
                }
                //if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(1));
            }, Application.Current.Dispatcher);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                while (UnstuckME.ChannelFactory.State != System.ServiceModel.CommunicationState.Opened)
                {
                    UnstuckME.ConnectToServer();
                }
                UserInfo test = new UserInfo();
                test = UnstuckME.Server.UserLoginAttempt(UnstuckME.User.EmailAddress, UnstuckME.UPW);
                this.Close();
                UnstuckME.MainWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void REWindow_ContentRendered(object sender, EventArgs e)
        {

        }
    }
}
