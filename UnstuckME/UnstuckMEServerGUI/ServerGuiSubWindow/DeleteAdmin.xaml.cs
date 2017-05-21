using System;
using System.Linq;
using System.Windows;
using UnstuckMEServer;
using UnstuckME_Classes;

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for DeleteAdmin.xaml
    /// </summary>
    public partial class DeleteAdmin : Window
    {
        public static AdminInfo Admin;
        public DeleteAdmin(AdminInfo psdAdmin)
        {
            InitializeComponent();
            Admin = psdAdmin;
        }

        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    if (textBoxTargetDelete.Text == Admin.EmailAddress)
                        throw new Exception("You Cannot Delete The Admin You are Logged in as");
                    if (textBoxTargetDelete.Text.Length == 0)
                        throw new Exception("Please Enter a Username to Delete");
                    if (passwordBox.Password.Length == 0)
                        throw new Exception("Please Enter Your Password");

                    var admin = (from u in db.ServerAdmins
                                 where u.ServerAdminID == Admin.ServerAdminID
                                 select u).First();
                    string enteredPassword = UnstuckMEHashing.RecreateHashedPassword(passwordBox.Password, admin.Salt);

                    if (enteredPassword != admin.Password)
                        throw new Exception("Invalid Password");

                    var target = (from u in db.ServerAdmins
                                 where u.EmailAddress == textBoxTargetDelete.Text
                                 select u).First();
                    db.DeleteServerAdmin(target.ServerAdminID);
                    MessageBox.Show("Successful!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Failed!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

}
