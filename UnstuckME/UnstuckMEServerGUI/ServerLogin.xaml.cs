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
            try
            {
                if(textBoxEmailAddress.Text.Length == 0)
                {
                    throw new Exception();
                }

                using (UnstuckMEServer_DBEntities db = new UnstuckMEServer_DBEntities())
                {
                    var admin = (from u in db.ServerAdmins
                                 where u.EmailAddress.ToLower() == textBoxEmailAddress.Text.ToLower()
                                 select u).First();

                    string stringOfPassword = UnstuckMEHashing.RecreateHashedPassword(passwordBoxInput.Password, admin.Salt);

                    if (stringOfPassword == admin.Password)
                    {
                        Admin.EmailAddress = admin.EmailAddress;
                        Admin.FirstName = admin.FirstName;
                        Admin.LastName = admin.LastName;
                        Admin.ServerAdminID = admin.ServerAdminID;

                        if (Admin.EmailAddress.ToLower() == "admin" && UnstuckMEHashing.RecreateHashedPassword("password", admin.Salt) == admin.Password)
                        {
                            AdminCredChange changeloginCreds = new AdminCredChange(ref Admin);
                            App.Current.MainWindow = changeloginCreds;
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
                                App.Current.MainWindow = mainWindow;
                                Close();
                                mainWindow.Show();
                            }
                            else
                            {
                                ServerRunning window = new ServerRunning(ref Admin);
                                App.Current.MainWindow = window;
                                this.Close();
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

        private void buttonSetting_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Whatsup mani");
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
