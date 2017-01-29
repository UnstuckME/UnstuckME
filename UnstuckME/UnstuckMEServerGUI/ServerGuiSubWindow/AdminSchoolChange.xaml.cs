using System;
using System.Collections.Generic;
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
using UnstuckMEUserGUI;

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
        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            schools = await LoadSchoolsAsync();
            foreach (UnstuckMESchool school in schools)
            {
                comboBoxSchools.Items.Add(new ComboBoxItem().Content = school.SchoolName);
            }
        }

        private Task<List<UnstuckMESchool>> LoadSchoolsAsync()
        {
            return Task.Factory.StartNew(() => LoadSchools());
        }

        List<UnstuckMESchool> LoadSchools()
        {
            List<UnstuckMESchool> tempSchools = new List<UnstuckMESchool>();
            using (UnstuckMESchools_DBEntities db = new UnstuckMESchools_DBEntities())
            {
                var dbSchools = from s in db.Schools select new {
                                                                    SchoolName = s.SchoolName,
                                                                    EmailCredentials = s.EmailCredentials,
                                                                    SchoolID = s.SchoolID
                                                                };

                foreach (var dbschool in dbSchools)
                {
                    UnstuckMESchool newSchool = new UnstuckMESchool();
                    newSchool.SchoolName = dbschool.SchoolName;
                    newSchool.SchoolID = dbschool.SchoolID;
                    newSchool.SchoolEmailCredentials = dbschool.EmailCredentials;
                    tempSchools.Add(newSchool);
                }
            }
            return tempSchools;
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
