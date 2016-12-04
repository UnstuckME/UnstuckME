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
        }

        private void buttonServerLogin_Click(object sender, RoutedEventArgs e)
        {
            labelInvalidUsernamePassword.Visibility = Visibility.Collapsed;

            try
            {
                SqlConnection conn = new SqlConnection("Server=aura.students.cset.oit.edu;Database=UnstuckME_DB;persist security info=True;user id=UnstuckME_Admin;password=B3$$t-P@$$W0rd");
                conn.Open();

                //Beginnings of username check
                if (System.Data.ConnectionState.Open == conn.State)//If successful connection to database.
                {
                    List<string> dbUsernames = new List<string>();
                    List<string> dbPassword = new List<string>();
                    List<string> dbSalt = new List<string>();

                    SqlCommand cmd = new SqlCommand("dbo.GetAdminInfo", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        dbUsernames.Add(rdr["AdminUsername"].ToString());
                        dbPassword.Add(rdr["AdminPassword"].ToString());
                        dbSalt.Add(rdr["Salt"].ToString());
                    }

                    string userEmailAddressInput = textBoxEmailAddress.Text;
                    int listCount = 0;
                    bool isUser = false;

                    foreach (var userName in dbUsernames)
                    {
                        if (string.Equals(userName, userEmailAddressInput, StringComparison.OrdinalIgnoreCase) == true)
                        {
                            string dbStringSalt = dbSalt[listCount];
                            byte[] databasePassword = GenerateSaltedHash(GetBytes(passwordBoxInput.Password), GetBytes(dbSalt[listCount]));
                            string stringOfPassword = "";
                            foreach (byte element in databasePassword)
                            {
                                stringOfPassword += element;
                            }
                            
                            if(stringOfPassword == dbPassword[listCount])
                            {
                                isUser = true;
                            }
                        }
                        listCount++;
                    }

                    if (isUser)
                    {
                        MainWindow mainWindow = new MainWindow();
                        App.Current.MainWindow = mainWindow;
                        Close();
                        mainWindow.Show();
                    }
                    else
                    {
                        labelInvalidUsernamePassword.Visibility = Visibility.Visible;
                        ChangeLoginCanvas.Visibility = Visibility.Visible;
                    }
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
