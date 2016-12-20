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
                try
                {
                    var admin = (from u in db.ServerAdmins
                                 where u.EmailAddress.ToLower() == textBoxCurrentUsername.Text.ToLower()
                                 select u).First();

                    //if(inputPassword == admin.Password)
                    //{

                    //    //db.UpdateServerAdmin(admin.ServerAdminID, admin.FirstName, admin.LastName, )
                    //}
                    //else
                    //{
                    //    throw new Exception("Invalid Username/Password");
                    //}
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Credintials Incorrect", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }

        }
    }
}
