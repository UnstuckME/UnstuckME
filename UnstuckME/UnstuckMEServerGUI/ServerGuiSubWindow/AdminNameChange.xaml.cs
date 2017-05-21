using System;
using System.Linq;
using System.Windows;
using UnstuckMEServer;
using UnstuckME_Classes;

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for AdminNameChange.xaml
    /// </summary>
    public partial class AdminNameChange : Window
    {
        public static AdminInfo Admin;
        public AdminNameChange(ref AdminInfo currentAdmin)
        {
            InitializeComponent();
            Admin = currentAdmin;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxFirstName.Text.Length == 0)
                    throw new Exception("Please Enter a New First Name");
                if (textBoxLastName.Text.Length == 0)
                    throw new Exception("Please Enter a New Last Name");
                if (textBoxUsername.Text.Length == 0)
                    throw new Exception("Please Enter your Current Username.");
                using (UnstuckME_DBEntities db = new UnstuckME_DBEntities())
                {
                    ServerAdmin queryAdmin = (from u in db.ServerAdmins
                                              where u.EmailAddress == textBoxUsername.Text
                                              select u).First();
                    if (Admin.EmailAddress != queryAdmin.EmailAddress)
                        throw new Exception("UserName Does Not Match!");
                    string testPw = UnstuckMEHashing.RecreateHashedPassword(passwordBox.Password, queryAdmin.Salt);
                    if (testPw != queryAdmin.Password)
                        throw new Exception("Invalid Password");
                    queryAdmin.FirstName = textBoxFirstName.Text;
                    queryAdmin.LastName = textBoxLastName.Text;
                    db.SaveChanges();
                    Admin.FirstName = textBoxFirstName.Text;
                    Admin.LastName = textBoxLastName.Text;
                    MessageBox.Show("Changes Saved", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
