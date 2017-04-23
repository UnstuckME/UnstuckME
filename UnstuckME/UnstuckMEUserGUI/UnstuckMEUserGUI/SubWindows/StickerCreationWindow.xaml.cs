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
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerCreationWindow.xaml
    /// </summary>
    public partial class StickerCreationWindow : Window
    {
        private class ComboboxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        List<int> hourList = new List<int>();
        List<string> minutesList = new List<string>();
        List<string> AMPMList = new List<string>();
        List<string> coursenumberList = new List<string>();
        List<string> courseNameList = new List<string>();
        List<Organization> orgList;
        public StickerCreationWindow()
        {
            InitializeComponent();
            LoadDataIntoComboBoxes();
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
                    //UnstuckMESticker temp = new UnstuckMESticker();
                    UnstuckMEBigSticker newSticker = new UnstuckMEBigSticker();
                    DateTime selectedDate = DatePickerSticker.SelectedDate.Value;
                    TimeSpan timedays = selectedDate - DateTime.Now;
                    int secondsCalc = 0;
                    bool isStickerSubmittedForToday = false;

                    if(selectedDate.DayOfYear == DateTime.Now.DayOfYear)
                    {
                        isStickerSubmittedForToday = true;
                    }

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
                    totalSeconds += (secondsCalc * 3600);

                    //If selected time is has already passed, throw exception.
                    if (selectedDate.Year == DateTime.Now.Year)
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
                    if (isStickerSubmittedForToday)
                    {
                        totalSeconds = (totalSeconds - (int)DateTime.Now.TimeOfDay.TotalSeconds);
                    }

                    newSticker.TimeoutInt = totalSeconds;
                    newSticker.MinimumStarRanking = sliderRating.Value;
                    newSticker.ProblemDescription = ProblemDescription.Text;
                    newSticker.StudentID = UnstuckME.User.UserID;
                    newSticker.StudentRanking = UnstuckME.User.AverageStudentRank;
                    newSticker.SubmitTime = DateTime.Now;
                    newSticker.Timeout = DateTime.Now.AddSeconds(totalSeconds);
                    newSticker.AttachedOrganizations = new List<int>();
                    newSticker.ChatID = 0;
                    newSticker.TutorID = 0;
                    newSticker.TutorRanking = 0;

                    foreach (TutorStickerSubmit a in StackPanelOrganization.Children.OfType<TutorStickerSubmit>())
                    {
                        newSticker.AttachedOrganizations.Add(a.OrganizationID);
                    }
                    try
                    {
                        newSticker.Class = UnstuckME.Server.GetSingleClass(UnstuckME.Server.GetCourseIdNumberByCodeAndNumber(ComboBoxCourseNumber.SelectedValue as string, ComboBoxCourseName.SelectedValue as string));
                    }
                    catch (Exception exp)
                    {
                        UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
                        logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
                        throw new Exception("Sticker Submission Failed, Please make sure you don't already have a Sticker for this class. If problems persists, contact an Administrator.");
                    }
                    try
                    {
                        if (UnstuckME.Server.SubmitSticker(newSticker) < 0) //Returns -1 if failed.
                            throw new Exception("Sticker Submission Failure");
                        UnstuckME.MainWindow.AddStickerToMyStickers(new UnstuckMESticker(newSticker));
                    }
                    catch (Exception exp)
                    {
                        UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
                        logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
                        throw new Exception("Sticker Submission Failed, Please make sure you don't already have a Sticker for this class. If problems persists, contact an Administrator.");
                    }
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message);
                MessageBox.Show(ex.Message, "Sticker Submission Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        public void LoadDataIntoComboBoxes()
        {
            DatePickerSticker.DisplayDateStart = DateTime.Now;
            DatePickerSticker.SelectedDate = DateTime.Now;
            DatePickerSticker.DisplayDateEnd = DateTime.Now.AddDays(56); //56 = 8 weeks

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
            ComboBoxCourseName.IsEnabled = false;

            try
            {
                coursenumberList = UnstuckME.Server.GetCourseCodes();
                orgList = UnstuckME.Server.GetAllOrganizations();
            }
            catch (Exception exp)
            {
                UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
                logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
            }
            ComboboxItem temp1 = new ComboboxItem();
            temp1.Text = "(OfficialMentors)";
            temp1.Value = 0;
            ComboBoxOrgName.Items.Add(temp1);
            foreach (Organization org in orgList)
            {
                ComboboxItem temp = new ComboboxItem();
                temp.Text = org.OrganizationName;
                temp.Value = org.MentorID;
                ComboBoxOrgName.Items.Add(temp);
            }

            coursenumberList[0] = "(Class)";
            ComboBoxCourseNumber.ItemsSource = coursenumberList;
            ComboBoxCourseName.ItemsSource = courseNameList;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelSliderValue.Content = sliderRating.Value;
        }

        private void ComboBoxCourseNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> courseNameList = new List<string>();
            int selected = ComboBoxCourseNumber.SelectedIndex;
            if (selected != 0)
            {
                try
                { 
                    courseNameList = UnstuckME.Server.GetCourseNumbersByCourseCode(ComboBoxCourseNumber.SelectedValue as string);
                    courseNameList.Insert(0, "(Select Class)");
                }
                catch (Exception exp)
                {
                    UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
                    logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
                }
                ComboBoxCourseName.IsEnabled = true;
                ComboBoxCourseName.ItemsSource = courseNameList;
                ComboBoxCourseName.SelectedIndex = 0;
            }
            else
            {
                ComboBoxCourseName.SelectedIndex = 0;
                ComboBoxCourseName.IsEnabled = false;
            }
            
        }

        private void ButtonAddOrganization_Click(object sender, RoutedEventArgs e)
        {
            ComboboxItem temp = ComboBoxOrgName.SelectedItem as ComboboxItem;
            bool exists = false;
            if (ComboBoxOrgName.SelectedIndex == 0) return;
            foreach (TutorStickerSubmit a in StackPanelOrganization.Children.OfType<TutorStickerSubmit>())
            {
                if(temp.Value == a.OrganizationID)
                {
                    exists = true;
                }
            }
            if(!exists)
            {
                StackPanelOrganization.Children.Add(new TutorStickerSubmit(temp.Value, temp.Text));
            }
        }
    }
}
