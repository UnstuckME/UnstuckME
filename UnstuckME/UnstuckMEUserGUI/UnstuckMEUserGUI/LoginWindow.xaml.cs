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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private static List<string> schools = new List<string>();
        public LoginWindow()
        {
            schools = SchoolDB_Connection.GetSchoolNames();
            InitializeComponent();
        }
        public LoginWindow(bool invalidUser)
        {
            schools = SchoolDB_Connection.GetSchoolNames();
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            foreach (string school in schools)
            {
                comboBoxSchools.Items.Add(new ComboBoxItem().Content = school);
            }
        }

        private void buttonCreateAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = Brushes.Black;
        }

        private void buttonCreateAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = Brushes.White;
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            _LoginGrid.IsEnabled = false;
            _LoginGrid.Visibility = Visibility.Hidden;
            _AccountCreationGrid.IsEnabled = true;
            _AccountCreationGrid.Visibility = Visibility.Visible;
        }

        private void buttonCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = Brushes.Black;
        }

        private void buttonCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = Brushes.White;
        }
        private void buttonCreate_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = Brushes.Black;
        }

        private void buttonCreate_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = Brushes.White;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            _AccountCreationGrid.IsEnabled = false;
            _AccountCreationGrid.Visibility = Visibility.Hidden;
            _LoginGrid.IsEnabled = true;
            _LoginGrid.Visibility = Visibility.Visible;
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
