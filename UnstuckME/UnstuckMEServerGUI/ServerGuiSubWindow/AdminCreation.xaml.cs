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
using UnstuckME_Classes;
using UnstuckMEServer;

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for AdminCreation.xaml
    /// </summary>
    public partial class AdminCreation : Window
    {
        public static AdminInfo Admin;
        public AdminCreation(ref AdminInfo passedInAdmin)
        {
            InitializeComponent();
            Admin = passedInAdmin;
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxEmailAddress.Text.Length == 0)
                    throw new Exception("Please Enter a Valid Email Address");
                if (textBoxFirstName.Text.Length == 0)
                    throw new Exception("Please Enter a First Name");
                if (textBoxLastName.Text.Length == 0)
                    throw new Exception("Please Enter a Last Name");
                if (passwordBox.Password != passwordBoxConfirm.Password)
                    throw new Exception("Passwords Do Not Match");
                if (passwordBox.Password.Length < 6)
                    throw new Exception("Please Enter a Password of 6-32 Characters");

                using (UnstuckMEServer_DBEntities db = new UnstuckMEServer_DBEntities())
                {
                    UnstuckMEPassword hashedPassword = UnstuckMEHashing.GetHashedPassword(passwordBox.Password);
                    db.CreateServerAdmin(textBoxFirstName.Text, textBoxLastName.Text, textBoxEmailAddress.Text, hashedPassword.Password, hashedPassword.Salt);
                }
                MessageBox.Show("Administrator Successfully Created", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Admin Creation Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
