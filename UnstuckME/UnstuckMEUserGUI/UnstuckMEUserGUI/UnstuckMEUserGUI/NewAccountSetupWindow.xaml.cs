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
using System.Security;

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
            try
            {
                if (FNameTxtBx.Text.Length == 0)
                    throw new Exception("First Name Required!");
                else if (LNameTxtBx.Text.Length == 0)
                    throw new Exception("Last Name Required!");
                else if (EmailTxtBx.Text.Length == 0)
                    throw new Exception("Email Address Required!");
                else if (passwordBox.Password.Length < 6)
                    throw new Exception("Please enter a password with 6 or more charcters!");
                else if (passwordBox.Password != passwordBoxConfirm.Password)
                    throw new Exception("Passwords Not Match!");
                else
                {
                    if(Server.CreateNewUser(FNameTxtBx.Text, LNameTxtBx.Text, EmailTxtBx.Text, passwordBox.Password))
                    {
                        Window disp = new MainWindow(Server.GetUserID(EmailTxtBx.Text), Server);
                        disp.Show();
                        this.Close();
                    }
                    else
                    {
                        throw new Exception("Error Occured Creating New User, Please Contact Your Server Administrator");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "New User Creation Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
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
