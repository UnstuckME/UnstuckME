using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
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
using UnstuckMEInterfaces;
using UnstuckME_Classes;
using System.ServiceModel.Channels;
using UnstuckMEServer;
using UnstuckMEServerGUI;

namespace UnstuckMEServerGUI.ServerGuiSubWindow
{
    /// <summary>
    /// Interaction logic for AdminSchoolChange.xaml
    /// </summary>
    public partial class AdminSchoolChange : Window
    {
        private static List<UnstuckMESchool> schools;

        public AdminSchoolChange()
        {
            InitializeComponent();
        }
  

        private Task<List<UnstuckMESchool>> LoadSchoolsAsync()
        {
            return Task.Factory.StartNew(() => LoadSchools());
        }

        List<UnstuckMESchool> LoadSchools()
        {
            List<UnstuckMESchool> tempSchools = new List<UnstuckMESchool>();
            try
            {
                using (UnstuckMEServerGUI.UnstuckME_SchoolsEntities db = new UnstuckMEServerGUI.UnstuckME_SchoolsEntities())
                {

                    var dbSchools = from s in db.Schools select new {SchoolName = s.SchoolName, SchoolID = s.SchoolID};

                    foreach (var dbschool in dbSchools)
                    {
                        UnstuckMESchool newSchool = new UnstuckMESchool();
                        newSchool.SchoolName = dbschool.SchoolName;
                        newSchool.SchoolID = dbschool.SchoolID;
                        tempSchools.Add(newSchool);
                    }
                }
            }
            catch
            {
                MessageBox.Show("We seem to experiencing some difficulties connecting to the UnstuckMe Server", "Unable To Connect to UnstuckME Server", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return tempSchools;
        }


        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxSchools.SelectedIndex != -1)
            {
                int selectedSchoolID = schools[(comboBoxSchools.SelectedIndex) - 1].SchoolID;

                using (UnstuckMEServerGUI.UnstuckME_SchoolsEntities db = new UnstuckMEServerGUI.UnstuckME_SchoolsEntities())
                {
                    var admin = (from u in db.Schools
                                 where u.SchoolID == selectedSchoolID
                                 select u).First();
                    if (textBoxUnstuckMEUsername.Text == admin.SchoolAdminUsername)
                    {


                        string stringOfPassword =
                            UnstuckMEHashing.RecreateHashedPassword(passwordBoxUnstuckMEPassword.Password, admin.Salt);
                        if (stringOfPassword == admin.SchoolAdminPassword)
                        {
                            System.Configuration.Configuration config =
                                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            config.AppSettings.Settings["SchoolName"].Value =
                                comboBoxSchools.SelectionBoxItem.ToString();
                            config.Save(ConfigurationSaveMode.Modified);

                            if ((admin.SchoolAdminUsername == "admin" &&
                                 UnstuckMEHashing.RecreateHashedPassword("password", admin.Salt) ==
                                 admin.SchoolAdminPassword) || (admin.Salt == "salt"))
                            {
                                MessageBox.Show(
                                    "It looks like you are new to the UnstuckME program, please update your UnstuckME Credentials",
                                    "New To UnstuckME", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                this.Close();
                                UnstuckMeCredChange unstuckMECredChangeWindow = new UnstuckMeCredChange(selectedSchoolID);
                                Application.Current.MainWindow = unstuckMECredChangeWindow;
                                unstuckMECredChangeWindow.ShowDialog();
                            }

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Login");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Login");
                    }
                }
            }
            else
            {
                MessageBox.Show("You need to select a school first");
            }
            
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AdminSchoolChange_OnContentRendered(object sender, EventArgs e)
        {
            schools = await LoadSchoolsAsync();
            foreach (UnstuckMESchool school in schools)
            {
                comboBoxSchools.Items.Add(new ComboBoxItem().Content = school.SchoolName);
            }
        }

     
    }
}
