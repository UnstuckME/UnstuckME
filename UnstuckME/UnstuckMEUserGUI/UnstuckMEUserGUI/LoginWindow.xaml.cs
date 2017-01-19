using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static IUnstuckMEService Server;
        private static DuplexChannelFactory<IUnstuckMEService> _channelFactory;
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
            _channelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
            Server = _channelFactory.CreateChannel();
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            _LoadingGrid.Visibility = Visibility.Visible;
            _LoginGrid.Visibility = Visibility.Hidden;
            bool isValid = false;
            int userID = 0;
            string emailAttempt = textBoxUserName.Text;
            string passwordAttempt = passwordBox.Password;
            _LoginGrid.IsEnabled = false;
            _LoadingGrid.IsEnabled = true;
            _labelLoadingPercent.Content = "0%";
            _LoadingProgressBar.Value = 0;
            
            try
            {
                if (textBoxUserName.Text.Length <= 0)
                    throw new Exception("Enter an Email Address");
                _LoadingProgressBar.Value = 10;
                _labelLoadingPercent.Content = "10%";
                if (passwordBox.Password.Length <= 6)
                    throw new Exception("Enter a Valid Password");
                _LoadingProgressBar.Value = 30;
                _labelLoadingPercent.Content = "30%";
                if (passwordBox.Password.Length >= 32)
                    throw new Exception("Enter a Valid Password");
                _LoadingProgressBar.Value = 50;
                _labelLoadingPercent.Content = "50%";
            }
            catch(Exception ex)
            {
                _LoginGrid.IsEnabled = true;
                _labelInvalidLogin.Content = ex.Message;
                _AccountCreationGrid.IsEnabled = false;
                _AccountCreationGrid.Visibility = Visibility.Hidden;
                _LoadingGrid.Visibility = Visibility.Hidden;
                _LoadingGrid.IsEnabled = false;
                passwordBox.Password = string.Empty;
                textBoxUserName.Text = string.Empty;
                _LoginGrid.Visibility = Visibility.Visible;
            }
            try
            {
                isValid = Server.UserLoginAttempt(emailAttempt, passwordAttempt);
                _LoadingProgressBar.Value = 60;
                _labelLoadingPercent.Content = "60%";
                userID = Server.GetUserID(emailAttempt);
                _LoadingProgressBar.Value = 70;
                _labelLoadingPercent.Content = "70%";
                UserInfo loggedInUser = Server.GetUserInfo(userID);
                _LoadingProgressBar.Value = 80;
                _labelLoadingPercent.Content = "80%";
                byte[] img = Server.GetProfilePicture(loggedInUser.UserID);
                _LoadingProgressBar.Value = 90;
                _labelLoadingPercent.Content = "90%";
                StartWindow mainWindow = new StartWindow(ref Server, ref loggedInUser, ref img);
                _LoadingProgressBar.Value = 100;
                _labelLoadingPercent.Content = "100%";
                mainWindow.Show();
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + " Please Contact Your Server Administrator", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            bool validCredentials = false;
            try
            {
                Exception invalidCreds = new Exception("Invalid Credintials!");
                if (textBoxCreateEmailAddress.Text.Length <= 0)
                    throw invalidCreds;
                if (textBoxCreateFirstName.Text.Length <= 0)
                    throw invalidCreds;
                if (textBoxCreateLastName.Text.Length <= 0)
                    throw invalidCreds;
                if (passwordBoxCreate.Password.Length <= 6)
                    throw invalidCreds;
                if (passwordBoxCreate.Password.Length >= 32)
                    throw invalidCreds;
                if (passwordBoxCreate.Password != passwordBoxCreateConfirm.Password)
                    throw invalidCreds;

                validCredentials = true;
            }
            catch(Exception)
            {
                validCredentials = false;
                labelCreateIncorrectCreds.Visibility = Visibility.Visible;
            }
            if (validCredentials)
            {
                try
                {
                    if (Server.CreateNewUser(textBoxCreateFirstName.Text, textBoxCreateLastName.Text, textBoxCreateEmailAddress.Text, passwordBoxCreate.Password))
                    {
                        int userID = Server.GetUserID(textBoxCreateEmailAddress.Text);
                        ImageConverter converter = new ImageConverter();
                        byte[] avatar = (byte[])converter.ConvertTo(Properties.Resources.SimpleAvatar, typeof(byte[]));
                        Server.InsertProfilePicture(userID, avatar);
                    }
                    else
                    {
                        throw new Exception("Error occurred creating a new user, Please Contact Your Server Administrator");
                    }
                    _LoginGrid.IsEnabled = true;
                    textBoxUserName.Text = textBoxCreateEmailAddress.Text;
                    passwordBox.Password = passwordBoxCreate.Password;
                    _AccountCreationGrid.IsEnabled = false;
                    _AccountCreationGrid.Visibility = Visibility.Hidden;
                    _LoginGrid.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Account Creation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonCreateAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            //buttonCreateAccount.Foreground = Brushes.Black;
        }

        private void buttonCreateAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            //buttonCreateAccount.Foreground = Brushes.White;
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
            //buttonCreateAccount.Foreground = Brushes.Black;
        }

        private void buttonCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            //buttonCreateAccount.Foreground = Brushes.White;
        }
        private void buttonCreate_MouseEnter(object sender, MouseEventArgs e)
        {
            //buttonCreateAccount.Foreground = Brushes.Black;
        }

        private void buttonCreate_MouseLeave(object sender, MouseEventArgs e)
        {
            //buttonCreateAccount.Foreground = Brushes.White;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            _AccountCreationGrid.IsEnabled = false;
            _AccountCreationGrid.Visibility = Visibility.Hidden;
            _LoginGrid.IsEnabled = true;
            _LoginGrid.Visibility = Visibility.Visible;
        }

    }
}
