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

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for AdminCredChange.xaml
    /// </summary>
    public partial class AdminCredChange : Window
    {
        public static AdminInfo Admin;

        public AdminCredChange(AdminInfo passedInAdmin)
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

            using (UnstuckMEServer_DBEntities db = new UnstuckMEServer_DBEntities())
            {
                MessageBoxResult result = MessageBox.Show("Save Changes?", "Save Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var admin = (from u in db.ServerAdmins
                                     where u.EmailAddress.ToLower() == textBoxCurrentUsername.Text.ToLower()
                                     select u).First();

                        string hashedPassoword = UnstuckMEHashing.RecreateHashedPassword(passwordBoxCurrentPassword.Password, admin.Salt);
                        if (hashedPassoword == admin.Password)
                        {
                            if (passwordBoxNewPassword.Password != passwordBoxConfirm.Password)
                                throw new Exception("Passwords Do Not Match!");
                            if (textBoxNewUsername.Text.Length != 0)
                            {
                                if (textBoxNewUsername.Text.Length < 6)
                                    throw new Exception("Please Enter A Username of 6 Characters or More!");
                                admin.EmailAddress = textBoxNewUsername.Text;
                                UnstuckMEPassword newHashedPassword = UnstuckMEHashing.GetHashedPassword(passwordBoxNewPassword.Password);
                                admin.Password = newHashedPassword.Password;
                                admin.Salt = newHashedPassword.Salt;
                                db.SaveChanges();
                                MessageBox.Show("New Username and Password Saved", "Administrator Update Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                UnstuckMEPassword newHashedPassword = UnstuckMEHashing.GetHashedPassword(passwordBoxNewPassword.Password);
                                admin.Password = newHashedPassword.Password;
                                admin.Salt = newHashedPassword.Salt;
                                db.SaveChanges();
                                MessageBox.Show("New Password Saved", "Password Change Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            this.Close();
                        }
                        else
                        {
                            throw new Exception("Invalid Username/Password");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Credintials Incorrect", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }

        }
    }
}
