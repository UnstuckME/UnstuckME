using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UnstuckME_Classes;
using UnstuckMeLoggers;
using UnstuckMEUserGUI.SubWindows;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        bool classesVisible;
        bool usersVisible;
        bool reportsVisible;
        bool chatsVisible;
        bool orgsVisible;

        public AdminPage()
        {
            InitializeComponent();
            ClassesContentArea.Children.Add(new UserControls.Admin.ClassesArea());
            UserContentArea.Children.Add(new UserControls.Admin.UserArea());
            ReportsContentArea.Children.Add(new UserControls.Admin.ReportsArea());
            AdminChatsContentArea.Children.Add(new UserControls.Admin.ChatsArea());
            OrgsContentArea.Children.Add(new UserControls.Admin.OrgsArea());

            classesVisible = false;
            usersVisible = false;
            reportsVisible = false;
            chatsVisible = false;
            orgsVisible = false;

            ClassesContentArea.Visibility = Visibility.Collapsed;
            UserContentArea.Visibility = Visibility.Collapsed;
            ReportsContentArea.Visibility = Visibility.Collapsed;
            AdminChatsContentArea.Visibility = Visibility.Collapsed;
            OrgsContentArea.Visibility = Visibility.Collapsed;
        }



      

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // add mentor orgs
            Window win = new AddMentorOrgsWindow();
            win.Show();
        }

        private void ClassesAreaBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (classesVisible)
            {
                classesVisible = false;
                ClassesContentArea.Visibility = Visibility.Collapsed;
            }
            else
            {
                classesVisible = true;
                ClassesContentArea.Visibility = Visibility.Visible;
            }
        }

        private void UserAreaBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (usersVisible)
            {
                usersVisible = false;
                UserContentArea.Visibility = Visibility.Collapsed;
            }
            else
            {
                usersVisible = true;
                UserContentArea.Visibility = Visibility.Visible;
            }
        }

        private void ReportAreaBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (reportsVisible)
            {
                reportsVisible = false;
                ReportsContentArea.Visibility = Visibility.Collapsed;
            }
            else
            {
                reportsVisible = true;
                ReportsContentArea.Visibility = Visibility.Visible;
            }
        }

        private void OrgsAreaBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (orgsVisible)
            {
                orgsVisible = false;
                OrgsContentArea.Visibility = Visibility.Collapsed;
            }
            else
            {
                orgsVisible = true;
                OrgsContentArea.Visibility = Visibility.Visible;
            }
        }

        private void ChatsAreaBtn_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (chatsVisible)
            {
                chatsVisible = false;
                AdminChatsContentArea.Visibility = Visibility.Collapsed;
            }
            else
            {
                chatsVisible = true;
                AdminChatsContentArea.Visibility = Visibility.Visible;
            }
        }
    }


}

