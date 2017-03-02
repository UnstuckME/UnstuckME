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

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        IUnstuckMEService Server;
        public AdminPage(ref IUnstuckMEService inServer)
        {
            Server = inServer;
            InitializeComponent();
        }

        private void AddRemoveClassesBtn_Click(object sender, RoutedEventArgs e)
        {
            Window win = new SubWindows.AddClassToServerWindow();
            win.Show();
        }

        private void AddRemoveUserRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            Window win = new SubWindows.AddUserRoleWindow(ref Server);
            win.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
