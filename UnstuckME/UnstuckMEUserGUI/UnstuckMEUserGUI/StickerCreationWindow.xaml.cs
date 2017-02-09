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
using UnstuckME_Classes;
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerCreationWindow.xaml
    /// </summary>
    public partial class StickerCreationWindow : Window
    {
        public static IUnstuckMEService Server;
        public static UserInfo User;
        public StickerCreationWindow(ref IUnstuckMEService inServer, ref UserInfo inUser)
        {
            InitializeComponent();

            Server = inServer;
            User = inUser;

            DatePickerSticker.DisplayDateStart = DateTime.Now;
            DatePickerSticker.SelectedDate = DateTime.Now;
            DatePickerSticker.DisplayDateEnd = DateTime.Now.AddDays(56);
            List<int> hourList = new List<int>();
            List<string> minutesList = new List<string>();
            List<string> AMPMList = new List<string>();
            hourList.Add(12);
            for (int i = 1; i < 12; i++)
            {
                hourList.Add(i);
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

            List<string> coursenumberList = new List<string>();
            List<string> courseNameList = new List<string>();

            courseNameList.Add("(Course Name)");
            coursenumberList.Add("(CRN)");

            for (int i = 0; i < 10; i++)
            {
                courseNameList.Add(i.ToString());
                coursenumberList.Add(i.ToString());
            }
            ComboBoxCourseName.MaxDropDownHeight = 200;
            ComboBoxCourseNumber.MaxDropDownHeight = 200;
            comboBoxAMPM.MaxDropDownHeight = 75;
            comboBoxHour.MaxDropDownHeight = 200;
            comboBoxMinute.MaxDropDownHeight = 125;
            ComboBoxCourseNumber.ItemsSource = coursenumberList;
            ComboBoxCourseName.ItemsSource = courseNameList;

        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(ComboBoxCourseNumber.SelectedIndex == 0)
                {
                    throw new Exception("Please Pick A Course Number.");
                }
                if(ComboBoxCourseName.SelectedIndex == 0)
                {
                    throw new Exception("Please Pick A Course Name.");
                }
                MessageBoxResult result = MessageBox.Show("Are you sure you want to submit this sticker?", "Sticker Submission Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                if(result == MessageBoxResult.Yes)
                {
                    UnstuckMESticker temp = new UnstuckMESticker();
                    DateTime selectedDate = DatePickerSticker.SelectedDate.Value;
                    TimeSpan timedays = selectedDate - DateTime.Now;
                    int secondsCalc = 0;

                    //Convert days to Seconds, Set to zero if same day.
                    int totalSeconds = (int)timedays.TotalSeconds;
                    if(totalSeconds <= 0)
                    {
                        totalSeconds = 0;
                    }

                    //Convert minutes to seconds.
                    for (int i = 0; i < comboBoxMinute.SelectedIndex; i++)
                    {
                        secondsCalc += 15;
                    }
                    totalSeconds += (secondsCalc * 60);
                    secondsCalc = 0;

                    //Convert Hours to seconds, add 12 if PM
                    for (int i = 0; i < comboBoxHour.SelectedIndex; i++)
                    {
                        secondsCalc++;
                    }
                    if(comboBoxAMPM.SelectedIndex == 1)
                    {
                        secondsCalc += 12;
                    }
                    totalSeconds += (secondsCalc * 60 * 60);

                    //If selected time is has already passed, throw exception.
                    if(selectedDate.Year == DateTime.Now.Year)
                    {
                        if(DateTime.Now.DayOfYear == selectedDate.DayOfYear)
                        {
                            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                            if (totalSeconds < ((DateTime.Now - today).TotalSeconds))
                            {
                                throw new Exception("That deadline has already passed, Please choose a future deadline.");
                            }
                        }
                    }

                    temp.Timeout = totalSeconds;
                    temp.MinimumStarRanking = (float)sliderRating.Value;
                    temp.ProblemDescription = ProblemDescription.Text;
                    temp.StudentID = User.UserID;
                    temp.ClassID = 2; ////////////////////////////////////THIS NEEDS TO CHANGE EVENTUALLY///////////////////////////////////////////////
                    Server.SubmitSticker(temp);
                    MessageBox.Show("Sticker Submitted Successfully");
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Sticker Submission Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
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
