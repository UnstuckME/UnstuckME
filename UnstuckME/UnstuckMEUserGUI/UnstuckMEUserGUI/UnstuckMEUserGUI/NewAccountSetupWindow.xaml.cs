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
using System.ServiceModel;
using UnstuckMEInterfaces;
using System.Security.Cryptography;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for NewAccountSetupWindow.xaml
    /// </summary>
    public partial class NewAccountSetupWindow : Window
    {
        public NewAccountSetupWindow()
        {
            InitializeComponent();
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            //Opens a connection to UnstuckME Server.
            ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");
            IUnstuckMEService proxy = channelFactory.CreateChannel();

            string errors = "Please Correct the Following:\n";
            bool errFlag = false;
            string FName = "";
            string LName = "";
            string Email = "";
            string Password = "";

            if (FNameTxtBx.Text != "")
            {
                FName = FNameTxtBx.Text;
            }
            else
            {
                // display no first name error
                errors = errors + "No First Name \n";
                errFlag = true;
            }
            if (LNameTxtBx.Text != "")
            {
                LName = LNameTxtBx.Text;
            }
            else
            {
                // display no last name error
                errors = errors + "No Last Name \n";
                errFlag = true;
            }
            if (EmailTxtBx.Text != "")
            {
                Email = EmailTxtBx.Text;
            }
            else
            {
                // display no email error
                errors = errors + "No Email\n";
                errFlag = true;
            }
            if (passwordBox.Password == passwordBoxConfirm.Password)
            {
                if (passwordBox.Password == "")
                {
                    errors = errors + "No Password";
                    errFlag = true;
                }
                else
                {
                    Password = passwordBox.Password;
                }
            }
            else
            {
                // display pass words do no match error
                errors = errors + "Passwords Dont Match\n";
                errFlag = true;
            }

            if (errFlag == true)
            {
                MessageBox.Show(errors);
            }
            else
            {
                //create the account
                RandomNumberGenerator rng = new RNGCryptoServiceProvider();

                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);

                string token = Convert.ToBase64String(tokenData);

                byte[] saltedPassword = GenerateSaltedHash(GetBytes(Password), GetBytes(token));

                string newPassword = "";
                foreach (byte element in saltedPassword)
                {
                     newPassword += element;
                }

                int result = proxy.CreateNewUser(FName, LName, Email, newPassword, "User", token);
                if (result == 1)
                {
                    Window disp = new MainWindow(proxy.GetUserID(Email));
                    disp.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("An Error Occurred. Please Contact your UnstuckME Admin");
                    this.Close();
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Window disp = new StartWindow();
            disp.Show();
            this.Close();
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
    }
}
