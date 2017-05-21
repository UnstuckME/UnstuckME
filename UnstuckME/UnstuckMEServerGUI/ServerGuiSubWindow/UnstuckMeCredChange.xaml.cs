using System;
using System.Linq;
using System.Windows;
using UnstuckMEServer;

namespace UnstuckMEServerGUI.ServerGuiSubWindow
{
    /// <summary>
    /// Interaction logic for UnstuckMeCredChange.xaml
    /// </summary>
    public partial class UnstuckMeCredChange : Window
    {
        public static int _schoolID;
        public UnstuckMeCredChange(int schoolID)
        {
            _schoolID = schoolID;
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(textBoxNewUsername.Text.Length < 4)
                    throw new Exception("You must enter a new Username that is at least three characters");
                if (passwordBoxNewPassword.Password.Length < 6)
                {
                    passwordBoxNewPassword.Password = "";
                    passwordBoxConfirm.Password = "";
                    throw new Exception("Please enter a New Password that is at least 6 characters long");
                }
                if (passwordBoxNewPassword.Password != passwordBoxConfirm.Password)
                {
                    passwordBoxNewPassword.Password = "";
                    passwordBoxConfirm.Password = "";
                    throw new Exception("Please make sure your new passwords matches");
                }

                UnstuckMEPassword newHashedPassword = UnstuckMEHashing.GetHashedPassword(passwordBoxNewPassword.Password);
                using (UnstuckME_SchoolsEntities db = new UnstuckME_SchoolsEntities())
                {
                    var school = (from u in db.Schools where u.SchoolID == _schoolID select u).First();

                    school.SchoolAdminUsername = textBoxNewUsername.Text;
                    school.SchoolAdminPassword = newHashedPassword.Password;
                    school.Salt = newHashedPassword.Salt;

                    db.SaveChanges();
                }

                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Credentials Incorrect", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
