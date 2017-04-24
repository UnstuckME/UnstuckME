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
using System.Windows.Threading;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for AvailableSticker.xaml
    /// </summary>
    public partial class AvailableSticker : UserControl
    {
        public UnstuckMEAvailableSticker Sticker;
        private DispatcherTimer _timer;
        private TimeSpan _time;
        public AvailableSticker(UnstuckMEAvailableSticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            LabelClassName.Content = Sticker.CourseCode + " " + Sticker.CourseNumber + " " + Sticker.CourseName;
            StarRatingValue.Value = (inSticker.StudentRanking / 5);
            ProblemDescription.Text = "Problem Description:\n" + Sticker.ProblemDescription;

            _time = Sticker.Timeout - DateTime.Now;
            _timer = new DispatcherTimer(new TimeSpan(0,0,0,1), DispatcherPriority.Normal, delegate
            {
                CountdownTimer.Content = String.Format("D: {0} H: {1} M: {2} S: {3}", _time.Days, _time.Hours, _time.Minutes, _time.Seconds);
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
        }

        public void RemoveFromStackPanel()
        {
            //Removes Sticker From Stack Panel
            ((StackPanel)Parent).Children.Remove(this);
        }

        private void PeelTab_MouseEnter(object sender, MouseEventArgs e)
        {
            PeelTab.Height = 100;
            PeelTab.Width = 100;
            PeelTab.Background = Brushes.ForestGreen;
            AcceptLabel.Visibility = Visibility.Visible;
        }

        private void PeelTab_MouseLeave(object sender, MouseEventArgs e)
        {
            AcceptLabel.Visibility = Visibility.Hidden;
            PeelTab.Background = Brushes.LightGray;
            PeelTab.Height = 50;
            PeelTab.Width = 50;
        }

        private void PeelTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UnstuckME.Server.AcceptSticker(UnstuckME.User.UserID, Sticker.StickerID);
                Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().StickerAcceptedStartConversation(Sticker, UnstuckME.User.UserID);
                RemoveFromStackPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Accept Failed In Available Sticker User Control. " + ex.Message);
            }
        }
    }
}
