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
        private static List<UnstuckMESchool> schools;
        public LoginWindow()
        { 
            InitializeComponent();
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            _channelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
            Server = _channelFactory.CreateChannel();
            schools = await LoadSchoolsAsync();
            foreach (UnstuckMESchool school in schools)
            {
                comboBoxSchools.Items.Add(new ComboBoxItem().Content = school.SchoolName);
            }
        }

        private Task<List<UnstuckMESchool>> LoadSchoolsAsync()
        {
            return Task.Factory.StartNew(() => LoadSchools());     
        }

        List<UnstuckMESchool> LoadSchools()
        {
            List<UnstuckMESchool> tempSchools = new List<UnstuckMESchool>();
            using (UnstuckMESchools_DBEntities db = new UnstuckMESchools_DBEntities())
            {
                var dbSchools = from s in db.Schools
                                    //join j in db.Servers on s.SchoolID equals j.SchoolID /*No Schools have a server currently*/
                                    //join l in db.SchoolLogoes on s.SchoolID equals l.LogoID /*No Logos need to be pulled*/
                                select new
                                {
                                    SchoolName = s.SchoolName,
                                    EmailCredentials = s.EmailCredentials,
                                    SchoolID = s.SchoolID
                                };
                foreach (var dbschool in dbSchools)
                {
                    UnstuckMESchool newSchool = new UnstuckMESchool();
                    newSchool.SchoolName = dbschool.SchoolName;
                    newSchool.SchoolID = dbschool.SchoolID;
                    newSchool.SchoolEmailCredentials = dbschool.EmailCredentials;
                    tempSchools.Add(newSchool);
                }
            }
            return tempSchools;
        }

        async private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = false;
            
            string emailAttempt = textBoxUserName.Text;
            string passwordAttempt = passwordBox.Password;
            _labelInvalidLogin.Visibility = Visibility.Hidden;
            try
            {
                if (textBoxUserName.Text.Length <= 0)
                    throw new Exception("Enter an Email Address");
                if (passwordBox.Password.Length <= 6)
                    throw new Exception("Enter a Valid Password");
                if (passwordBox.Password.Length >= 32)
                    throw new Exception("Enter a Valid Password");
                isValid = true;
            }
            catch(Exception ex)
            {
                _labelInvalidLogin.Content = ex.Message;
                passwordBox.Password = string.Empty;
                _labelInvalidLogin.Visibility = Visibility.Visible;
                isValid = false;
            }
            if (isValid)
            {
                try
                {
                    UserInfo loggedInUser = await Task.Factory.StartNew(() => ServerLoginAttemptAsynch(emailAttempt, passwordAttempt));
                    if (loggedInUser == null)
                    {
                        _labelInvalidLogin.Content = "Invalid Username/Password";
                        throw new Exception();
                    }
                    else
                    {
                        UnstuckMEWindow mainWindow = new UnstuckMEWindow(ref Server, ref loggedInUser);
                        mainWindow.Show();
                        this.Close();
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show("Please check that you entered the correct credentials.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    isValid = false;
                    _labelInvalidLogin.Visibility = Visibility.Visible;
                    try
                    {
                        _channelFactory.Abort();
                        _channelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
                        Server = _channelFactory.CreateChannel();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There is a problem connecting to the server. Please Contact Your Server Administrator. UnstuckME will now close.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                }
            }
        }

        private UserInfo ServerLoginAttemptAsynch(string emailAttempt, string passwordAttempt)
        {
            UserInfo temp = new UserInfo();
            this.Dispatcher.Invoke(() =>
            {
                temp = Server.UserLoginAttempt(emailAttempt, passwordAttempt);
            });
            return temp;
        }

        async private void buttonCreate_Click(object sender, RoutedEventArgs e)
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
                    if (await Task.Factory.StartNew(() => CreateUserAsynch()))
                    {
                        int userID = await Task.Factory.StartNew(() => GetUserIDAsynch());
                        await Task.Factory.StartNew(() => InsertProfilePictureAsynch(userID));
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
                    try
                    {
                        _channelFactory.Abort();
                        _channelFactory = new DuplexChannelFactory<IUnstuckMEService>(new ClientCallback(), "UnstuckMEServiceEndPoint");
                        Server = _channelFactory.CreateChannel();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There is a problem re-connecting to the server. Please Contact Your Server Administrator. UnstuckME will now close.", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                }
            }
        }

        private bool CreateUserAsynch()
        {
            bool temp = false;
            this.Dispatcher.Invoke(() =>
            {
                temp = Server.CreateNewUser(textBoxCreateFirstName.Text, textBoxCreateLastName.Text, textBoxCreateEmailAddress.Text, passwordBoxCreate.Password);
            });
            return temp;
        }

        private int GetUserIDAsynch()
        {
            int temp = -1;
            this.Dispatcher.Invoke(() =>
            {
                temp = Server.GetUserID(textBoxCreateEmailAddress.Text);
            });
            return temp;
        }

        private void InsertProfilePictureAsynch(int userID)
        {
            this.Dispatcher.Invoke(() =>
            {
                ImageConverter converter = new ImageConverter();
                byte[] avatar = (byte[])converter.ConvertTo(Properties.Resources.UserBlue, typeof(byte[]));
                Server.InsertProfilePicture(userID, avatar);
            });
        }
        private void buttonCreateAccount_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void buttonCreateAccount_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = System.Windows.Media.Brushes.White;
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
            buttonCreateAccount.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void buttonCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = System.Windows.Media.Brushes.White;
        }
        private void buttonCreate_MouseEnter(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = System.Windows.Media.Brushes.Black;
        }

        private void buttonCreate_MouseLeave(object sender, MouseEventArgs e)
        {
            buttonCreateAccount.Foreground = System.Windows.Media.Brushes.White;
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
