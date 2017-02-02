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
using System.Data.SqlClient;
using UnstuckMEServer;
using UnstuckME_Classes;
using System.Diagnostics;

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for AdminCredChange.xaml
    /// </summary>
    public partial class AdminCredChange : Window
    {
        public static AdminInfo Admin;

        public AdminCredChange(ref AdminInfo passedInAdmin)
        {
            InitializeComponent();
            Admin = passedInAdmin;

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (passwordBoxNewPassword.Password.Length < 6)
                    throw new Exception("Password Too Short, 6-32 characters.");
                if (passwordBoxNewPassword.Password.Length > 32)
                    throw new Exception("Password Too Long, 6-32 characters.");
                using (UnstuckMEServer_DBEntities db = new UnstuckMEServer_DBEntities())
                    {
                        MessageBoxResult result = MessageBox.Show("Save Changes?", "Save Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {
                                if (textBoxCurrentUsername.Text != Admin.EmailAddress)
                                    throw new Exception("You cannot change the credentials of another Admin.");
                                var admin = (from u in db.ServerAdmins
                                             where u.EmailAddress.ToLower() == textBoxCurrentUsername.Text.ToLower()
                                             select u).First();

                                string hashedPassoword = UnstuckMEHashing.RecreateHashedPassword(passwordBoxCurrentPassword.Password, admin.Salt);
                                if (hashedPassoword == admin.Password)
                                {
                                    if (passwordBoxNewPassword.Password != passwordBoxConfirm.Password)
                                        throw new Exception("Passwords Do Not Match!");
                                    if (textBoxNewUsername.Text.Length != 0) //Changes username/password
                                    {
                                        if (textBoxNewUsername.Text.Length < 6)
                                            throw new Exception("Please Enter A Username of 6 Characters or More!");
                                        admin.EmailAddress = textBoxNewUsername.Text;
                                        UnstuckMEPassword newHashedPassword = UnstuckMEHashing.GetHashedPassword(passwordBoxNewPassword.Password);
                                        admin.Password = newHashedPassword.Password;
                                        admin.Salt = newHashedPassword.Salt;
                                        db.SaveChanges();
                                        Admin.EmailAddress = textBoxNewUsername.Text;
                                        MessageBox.Show("New Username and Password Saved", "Administrator Update Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                    else //Only changes password
                                    {
                                        UnstuckMEPassword newHashedPassword = UnstuckMEHashing.GetHashedPassword(passwordBoxNewPassword.Password);
                                        admin.Password = newHashedPassword.Password;
                                        admin.Salt = newHashedPassword.Salt;
                                        db.SaveChanges();
                                        MessageBox.Show("New Password Saved", "Password Change Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                    string unstuckME = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
                                    Process.Start(unstuckME);
                                    Application.Current.Shutdown();
                                }
                                else
                                {
                                    throw new Exception("Invalid Username/Password");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Credentials Incorrect", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                    }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Invalid Changes", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
