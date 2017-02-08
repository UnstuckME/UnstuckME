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

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerCreationWindow.xaml
    /// </summary>
    public partial class StickerCreationWindow : Window
    {
        public StickerCreationWindow()
        {
            InitializeComponent();
            DatePickerSticker.DisplayDateStart = DateTime.Now;
            DatePickerSticker.SelectedDate = DateTime.Now;
            List<string> hourList = new List<string>();
            List<string> minutesList = new List<string>();
            List<string> AMPMList = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                hourList.Add(i.ToString());
            }
            minutesList.Add("00");
            minutesList.Add("15");
            minutesList.Add("30");
            minutesList.Add("45");
            AMPMList.Add("A.M.");
            AMPMList.Add("P.M.");
            comboBoxAMPM.ItemsSource = AMPMList;
            comboBoxHour.ItemsSource = hourList;
            comboBoxMinute.ItemsSource = minutesList;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelSliderValue.Content = sliderRating.Value;
        }
    }
}
