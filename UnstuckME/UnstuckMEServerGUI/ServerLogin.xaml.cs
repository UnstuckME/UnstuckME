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
            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow = mainWindow;
            this.Close();
            mainWindow.Show();
        }
    }
}
