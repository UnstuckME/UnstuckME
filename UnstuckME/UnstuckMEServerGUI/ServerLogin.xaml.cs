using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
using UnstuckMEServer;
using UnstuckMEServerGUI.ServerGuiSubWindow;

namespace UnstuckMEServerGUI
{

    /// <summary>
    /// Interaction logic for ServerLogin.xaml
    /// </summary>
    public partial class ServerLogin : Window
    {
        public static AdminInfo Admin;
        public ServerLogin()
        {
            InitializeComponent();
            Admin = new AdminInfo();
        }

        private void buttonServerLogin_Click(object sender, RoutedEventArgs e)
        {

            if (System.Configuration.ConfigurationManager.AppSettings["SchoolName"] == "" || System.Configuration.ConfigurationManager.AppSettings["DatabaseName"] == "")
            {
                MessageBox.Show("It Looks like you have not configured your login settings on this machine before, press the gear icon to configure your connection settings.", "Unable To Login", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                try
                {
                    if (textBoxEmailAddress.Text.Length == 0)
                    {
                        throw new Exception();
                    }

                    using (UnstuckMEServer_DBEntities db = new UnstuckMEServer_DBEntities())
                    {
                        var admin = (from u in db.ServerAdmins
                            where u.EmailAddress.ToLower() == textBoxEmailAddress.Text.ToLower()
                            select u).First();

                        string stringOfPassword = UnstuckMEHashing.RecreateHashedPassword(passwordBoxInput.Password,
                            admin.Salt);

                        if (stringOfPassword == admin.Password)
                        {
                            Admin.EmailAddress = admin.EmailAddress;
                            Admin.FirstName = admin.FirstName;
                            Admin.LastName = admin.LastName;
                            Admin.ServerAdminID = admin.ServerAdminID;

                            if (Admin.EmailAddress.ToLower() == "admin" &&
                                UnstuckMEHashing.RecreateHashedPassword("password", admin.Salt) == admin.Password)
                            {
                                AdminCredChange changeloginCreds = new AdminCredChange(ref Admin);
                                Application.Current.MainWindow = changeloginCreds;
                                changeloginCreds.ShowDialog();

                                using (UnstuckMEServer_DBEntities db2 = new UnstuckMEServer_DBEntities())
                                {
                                    admin = (from u in db2.ServerAdmins
                                        where u.ServerAdminID == Admin.ServerAdminID
                                        select u).First();

                                    Admin.EmailAddress = admin.EmailAddress;

                                    textBoxEmailAddress.Text = Admin.EmailAddress.ToLower();
                                    passwordBoxInput.Password = "";
                                }

                                labelInvalidUsernamePassword.Visibility = Visibility.Hidden;
                            }
                            else
                            {
                                Process[] pname = Process.GetProcessesByName("UnstuckMEServer");
                                if (pname.Length == 0)
                                {
                                    MainWindow mainWindow = new MainWindow(ref Admin);
                                    Application.Current.MainWindow = mainWindow;
                                    Close();
                                    mainWindow.Show();
                                }
                                else
                                {
                                    ServerRunning window = new ServerRunning(ref Admin);
                                    Application.Current.MainWindow = window;
                                    Close();
                                    window.Show();
                                }
                            }
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                }
                catch (Exception)
                {
                    labelInvalidUsernamePassword.Visibility = Visibility.Visible;
                }
            }
        }

        private void buttonSetting_Click(object sender, RoutedEventArgs e)
        {
           

            if (System.Configuration.ConfigurationManager.AppSettings["SchoolName"] == "")
            {
                MessageBox.Show("It Looks like you have not stated what school you are trying to configure on this machine before. Select your school and login with the credentials provided to you by an UnstuckME associate.\n\n NOTE: Your login information will be updated to use the same credentials as your MSSQL database once you connect one.", "School Information Not Set", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                AdminSchoolChange changeSchool = new AdminSchoolChange();
                Application.Current.MainWindow = changeSchool;
                changeSchool.ShowDialog();
            }

            if (System.Configuration.ConfigurationManager.AppSettings["DatabaseName"] == "" && System.Configuration.ConfigurationManager.AppSettings["SchoolName"] != "")
            {
                MessageBox.Show("It looks like you have not configured your MSSQL database settings on this machine before, please configure them before continuing", "Configure MSSQL Database", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                ChangeDabaseConnectionSettings changeDBString = new ChangeDabaseConnectionSettings();
                Application.Current.MainWindow = changeDBString;
                changeDBString.ShowDialog();
            }

            if (System.Configuration.ConfigurationManager.AppSettings["SchoolName"] != "" &&
                System.Configuration.ConfigurationManager.AppSettings["DatabaseName"] != "" &&
                System.Configuration.ConfigurationManager.AppSettings["UnstuckMEServerIP"] == "")
            {
                MessageBox.Show("It looks like you have not configured your UnstuckME Server on this machine before, please configuring your UnstuckME server settings before continuing", "Configure UnstuckME Server", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                ChangeUnstuckMEServerIP changeIPString = new ChangeUnstuckMEServerIP();
                Application.Current.MainWindow = changeIPString;
                changeIPString.ShowDialog();
            }
            if (System.Configuration.ConfigurationManager.AppSettings["SchoolName"] != "" && System.Configuration.ConfigurationManager.AppSettings["DatabaseName"] != "" && System.Configuration.ConfigurationManager.AppSettings["UnstuckMEServerIP"] != "")
            {
                ChangeDBSchoolInfo schoolInfoWindow = new ChangeDBSchoolInfo();
                Application.Current.MainWindow = schoolInfoWindow;
                schoolInfoWindow.ShowDialog();
            }
        }

        private void OnKeyDownPasswordHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                buttonServerLogin_Click(sender, e);
            }
        }
    }
}
