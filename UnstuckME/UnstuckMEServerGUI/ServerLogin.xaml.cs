using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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


            byte[] potato = new byte[GetBytes("Password").Length + 1];
            potato = GetBytes("Salt");
            Console.WriteLine(potato);


            byte [] ryan = new byte[GenerateSaltedHash(GetBytes("Password"), GetBytes("Salt")).Length];
            ryan = GenerateSaltedHash(GetBytes("Password"), GetBytes("Salt"));

            //HashAlgorithm algorithm = new SHA256Managed();
            //byte[] ryan = new byte[algorithm.ComputeHash(GetBytes("Password")).Length + 1];
            //ryan = algorithm.ComputeHash(GetBytes("Password"));
            string AJ = "";
            foreach (byte element in ryan)
            {
                 AJ += element;
            }
            Console.WriteLine(AJ);
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
                    List<string> salt = new List<string>();

                    SqlCommand cmd = new SqlCommand("dbo.GetAdminInfo", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();


                    while (rdr.Read())
                    {
                        userNames.Add(rdr["AdminUsername"].ToString());
                        password.Add(rdr["AdminPassword"].ToString());
                        salt.Add(rdr["Salt"].ToString());
                    }

                    MainWindow mainWindow = new MainWindow();
                    App.Current.MainWindow = mainWindow;
                    Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Connection Failed!", "Connection Failure", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                conn.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message, "Connection Failure", MessageBoxButton.OK, MessageBoxImage.Exclamation);


            }
        }

        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }



        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
    }


}
