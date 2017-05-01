using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using UnstuckME_Classes;
using UnstuckMEServer;

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

            //DuplexChannelFactory<IUnstuckMEServer> channelFactory = new DuplexChannelFactory<IUnstuckMEServer>(new ServerCallback(), "UnstuckMEServerEndPoint");
            //IUnstuckMEServer testingChannel = channelFactory.CreateChannel();

            //testingChannel.UploadDocument();
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
                using (UnstuckME_SchoolsEntities db = new UnstuckME_SchoolsEntities())
                {

                    var dbSchools = from s in db.Schools select new {s.SchoolName, s.SchoolID};

                    foreach (var dbschool in dbSchools)
                    {
                        UnstuckMESchool newSchool = new UnstuckMESchool
                        {
                            SchoolName = dbschool.SchoolName,
                            SchoolID = dbschool.SchoolID
                        };

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
                int selectedSchoolID = schools[comboBoxSchools.SelectedIndex - 1].SchoolID;

                using (UnstuckME_SchoolsEntities db = new UnstuckME_SchoolsEntities())
                {
                    var admin = (from u in db.Schools
                                 where u.SchoolID == selectedSchoolID
                                 select u).First();
                    if (textBoxUnstuckMEUsername.Text == admin.SchoolAdminUsername)
                    {
                        string stringOfPassword = UnstuckMEHashing.RecreateHashedPassword(passwordBoxUnstuckMEPassword.Password, admin.Salt);
                        if (stringOfPassword == admin.SchoolAdminPassword)
                        {
                            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                            config.AppSettings.Settings["SchoolName"].Value = comboBoxSchools.SelectionBoxItem.ToString();
                            config.Save(ConfigurationSaveMode.Modified);

                            if (admin.SchoolAdminUsername == "admin" &&
                                UnstuckMEHashing.RecreateHashedPassword("password", admin.Salt) ==
                                admin.SchoolAdminPassword || admin.Salt == "salt")
                            {
                                MessageBox.Show(
                                    "It looks like you are new to the UnstuckME program, please update your UnstuckME Credentials",
                                    "New To UnstuckME", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                Close();
                                UnstuckMeCredChange unstuckMECredChangeWindow = new UnstuckMeCredChange(selectedSchoolID);
                                Application.Current.MainWindow = unstuckMECredChangeWindow;
                                unstuckMECredChangeWindow.ShowDialog();
                            }

                            Close();
                        }
                        else
                            MessageBox.Show("Invalid Login");
                    }
                    else
                        MessageBox.Show("Invalid Login");
                }
            }
            else
                MessageBox.Show("You need to select a school first");
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void AdminSchoolChange_OnContentRendered(object sender, EventArgs e)
        {
            schools = await LoadSchoolsAsync();
            foreach (UnstuckMESchool school in schools)
                comboBoxSchools.Items.Add(new ComboBoxItem().Content = school.SchoolName);
        }
    }
}
