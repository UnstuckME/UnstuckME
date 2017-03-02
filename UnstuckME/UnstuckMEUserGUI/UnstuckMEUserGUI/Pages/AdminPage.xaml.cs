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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnstuckMEInterfaces;
using UnstuckMEUserGUI.SubWindows;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {

        public AdminPage()
        {
            InitializeComponent();
        }

        private void AddRemoveClassesBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddRemoveUserRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUserRoleWindow win = new AddUserRoleWindow();
            win.Show();
        }
    }
}
