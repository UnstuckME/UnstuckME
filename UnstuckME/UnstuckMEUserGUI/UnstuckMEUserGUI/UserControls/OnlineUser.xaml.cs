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

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for OnlineUser.xaml
    /// </summary>
    public partial class OnlineUser : UserControl
    {
        public OnlineUser(string username)
        {
            InitializeComponent();
            UserButton.Content = username;
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UserButton.Content = "Clicked";
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {
            UserButton.Content = "Clicked";
        }

        private void UserButton_MouseEnter(object sender, MouseEventArgs e)
        {
            UserButton.Foreground = Brushes.Black;
        }

        private void UserButton_MouseLeave(object sender, MouseEventArgs e)
        {
            UserButton.Foreground = Brushes.White;
        }
    }
}
