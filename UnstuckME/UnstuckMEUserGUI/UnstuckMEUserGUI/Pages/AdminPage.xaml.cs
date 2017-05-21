using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UnstuckMEUserGUI.SubWindows;
using UnstuckMEUserGUI.UserControls.Admin;

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
            ClassesContentArea.Children.Add(new ClassesArea());
            UserContentArea.Children.Add(new UserArea());
            ReportsContentArea.Children.Add(new ReportsArea());
            AdminChatsContentArea.Children.Add(new ChatsArea());
            OrgsContentArea.Children.Add(new OrgsArea());

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

        private void ClassesAreaBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void UserAreaBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void ReportAreaBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void OrgsAreaBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

        private void ChatsAreaBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

