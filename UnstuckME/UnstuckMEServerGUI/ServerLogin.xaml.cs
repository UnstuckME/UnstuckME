using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace UnstuckMEServerGUI
{
    /// <summary>
    /// Interaction logic for ServerLogin.xaml
    /// </summary>
    public partial class ServerLogin : Window
    {
        public ServerLogin()
        {
            InitializeComponent();
        }

        private void buttonServerLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection("Server=aura.students.cset.oit.edu;Database=UnstuckME_DB;persist security info=True;user id=UnstuckME_Admin;password=B3$$t-P@$$W0rd");
                conn.Open();

                //Beginnings of username check
                if (System.Data.ConnectionState.Open == conn.State)
                {
                    List<string> userNames = new List<string>();
                    List<string> password = new List<string>();
                    SqlCommand cmd = new SqlCommand("dbo.GetAdminInfo", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        userNames.Add(rdr["AdminUsername"].ToString());
                        password.Add(rdr["AdminPassword"].ToString());
                    }

                    conn.Close();
                    MainWindow mainWindow = new MainWindow();
                    App.Current.MainWindow = mainWindow;
                    this.Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Connection Failed!", "Connection Failure", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    conn.Close();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Connection Failure", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}
