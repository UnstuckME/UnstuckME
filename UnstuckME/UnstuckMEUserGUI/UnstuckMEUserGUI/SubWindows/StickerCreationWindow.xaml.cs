using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnstuckME_Classes;
using UnstuckMeLoggers;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerCreationWindow.xaml
    /// </summary>
    public partial class StickerCreationWindow : Window
    {
        private List<int> hourList = new List<int>();
        private List<string> minutesList = new List<string>();
        private List<string> AMPMList = new List<string>();
        private List<string> coursenumberList = new List<string>();
        private List<string> courseNameList = new List<string>();
        private List<Organization> orgList;

        public StickerCreationWindow()
        {
            InitializeComponent();
            LoadDataIntoComboBoxes();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBoxCourseNumber.SelectedIndex == 0)
                    throw new Exception("Please Pick A Course Number.");
                if(ComboBoxCourseName.SelectedIndex == 0)
                    throw new Exception("Please Pick A Course Name.");

                UnstuckMEMessageBox result = new UnstuckMEMessageBox(UnstuckMEBox.YesNo, "Are you sure you want to submit this sticker?", "Sticker Confirmation", UnstuckMEBoxImage.Information);

                bool? open = result.ShowDialog();

                if (open.HasValue && open.Value)
                {
                    //UnstuckMESticker temp = new UnstuckMESticker();
                    UnstuckMEBigSticker newSticker = new UnstuckMEBigSticker();
                    DateTime selectedDate = DatePickerSticker.SelectedDate ?? DateTime.MinValue;
                    TimeSpan timedays = selectedDate - DateTime.Now;
                    int secondsCalc = 0;
                    bool isStickerSubmittedForToday = selectedDate.DayOfYear == DateTime.Now.DayOfYear;

                    //Convert days to Seconds, Set to zero if same day.
                    int totalSeconds = (int)timedays.TotalSeconds;
                    if(totalSeconds <= 0)
                        totalSeconds = 0;

                    //Convert minutes to seconds.
                    for (int i = 0; i < comboBoxMinute.SelectedIndex; i++)
                        secondsCalc += 15;
                    totalSeconds += secondsCalc * 60;
                    secondsCalc = 0;

                    //Convert Hours to seconds, add 12 if PM
                    for (int i = 0; i < comboBoxHour.SelectedIndex; i++)
                        secondsCalc++;
                    if(comboBoxAMPM.SelectedIndex == 1)
                        secondsCalc += 12;
                    totalSeconds += secondsCalc * 3600;

                    //If selected time is has already passed, throw exception.
                    if (selectedDate.Year == DateTime.Now.Year)
                    {
                        if (DateTime.Now.DayOfYear == selectedDate.DayOfYear)
                        {
                            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
                            if (totalSeconds < (DateTime.Now - today).TotalSeconds)
                                throw new Exception("That deadline has already passed, Please choose a future deadline.");
                        }
                    }
                    if (isStickerSubmittedForToday)
                        totalSeconds = totalSeconds - (int)DateTime.Now.TimeOfDay.TotalSeconds;

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

                    foreach (TutoringOrganizationDisplay a in StackPanelOrganization.Children.OfType<TutoringOrganizationDisplay>())
                        newSticker.AttachedOrganizations.Add(a.OrganizationID);
                    try
                    {
                        newSticker.Class = UnstuckME.Server.GetSingleClass(UnstuckME.Server.GetCourseIdNumberByCodeAndNumber(ComboBoxCourseNumber.SelectedValue as string, ComboBoxCourseName.SelectedValue as string));
                    }
                    catch (Exception exp)
                    {
                        UnstuckMEUserEndMasterErrLogger logger = UnstuckMEUserEndMasterErrLogger.GetInstance();
                        logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message, exp.Source);
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
                        logger.WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message, exp.Source);
                        throw new Exception("Sticker Submission Failed, Please make sure you don't already have a Sticker for this class. If problems persists, contact an Administrator.");
                    }
                    Close();
                }
            }
            catch(Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, ex.Source);
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
                hourList.Add(i);

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
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message, exp.Source);
            }

            ComboboxItem temp1 = new ComboboxItem
            {
                Text = "(OfficialMentors)",
                Value = 0
            };

            ComboBoxOrgName.Items.Add(temp1);

            foreach (Organization org in orgList)
            {
                ComboboxItem temp = new ComboboxItem
                {
                    Text = org.OrganizationName,
                    Value = org.MentorID
                };

                ComboBoxOrgName.Items.Add(temp);
            }

            coursenumberList[0] = "(Class)";
            ComboBoxCourseNumber.ItemsSource = coursenumberList;
            ComboBoxCourseName.ItemsSource = courseNameList;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelSliderValue.Content = sliderRating.Value;
        }

        private void ComboBoxCourseNumber_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            courseNameList = new List<string>();
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
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_SERVER_CONNECTION_ERROR, exp.Message);
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

            if (ComboBoxOrgName.SelectedIndex == 0)
                return;

            foreach (TutoringOrganizationDisplay a in StackPanelOrganization.Children.OfType<TutoringOrganizationDisplay>())
            {
                if (temp != null && temp.Value == a.OrganizationID)
                    exists = true;
            }

            if (!exists && temp != null)
                StackPanelOrganization.Children.Add(new TutoringOrganizationDisplay(temp.Value, temp.Text));
        }
    }
}
