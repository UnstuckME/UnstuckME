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

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for AdminCredChange.xaml
    /// </summary>
    public partial class AdminCredChange : Window
    {
        public AdminCredChange()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Server=aura.students.cset.oit.edu;Database=UnstuckME_DB;persist security info=True;user id=UnstuckME_Admin;password=B3$$t-P@$$W0rd");
                conn.Open();

                //Beginnings of username check
                if (System.Data.ConnectionState.Open == conn.State)//If successful connection to database.
                {
                    string dbUsernames = "";
                    string dbPassword = "";
                    string dbSalt = "";
                    bool userFound = false;

                    SqlCommand cmd = new SqlCommand("dbo.GetAdminInfo", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        if (rdr["AdminUsername"].ToString() == textBoxCurrentUsername.Text)
                        {
                            dbUsernames = rdr["AdminUsername"].ToString();
                            dbPassword = rdr["AdminPassword"].ToString();
                            dbSalt = rdr["Salt"].ToString();
                            userFound = true;
                        }
                    }


                    if (userFound)
                    {
                        MessageBox.Show("Admin Found!", "Invalid Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show("Admin Credintials Incorrect!", "Invalid Admin", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Connection Failed!", "Connection Failure", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Connection Failure", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
