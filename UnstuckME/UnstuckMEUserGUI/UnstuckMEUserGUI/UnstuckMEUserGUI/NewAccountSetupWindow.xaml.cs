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
using UnstuckME_Classes;
using System.Security.Cryptography;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for NewAccountSetupWindow.xaml
    /// </summary>
    public partial class NewAccountSetupWindow : Window
    {
        public static IUnstuckMEService Server;
        public NewAccountSetupWindow(IUnstuckMEService OpenServer)
        {
            InitializeComponent();
            Server = OpenServer;
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
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

                byte[] saltedPassword = UnstuckMEHashing.GenerateSaltedHash(Password, token);

                string newPassword = "";
                foreach (byte element in saltedPassword)
                {
                     newPassword += element;
                }

                int result = Server.CreateNewUser(FName, LName, Email, newPassword, "User", token);
                if (result == 1)
                {
                    Window disp = new MainWindow(Server.GetUserID(Email), Server);
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
    }
}
