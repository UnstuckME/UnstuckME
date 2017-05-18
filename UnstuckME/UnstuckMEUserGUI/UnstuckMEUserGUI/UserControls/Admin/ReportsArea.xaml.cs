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

namespace UnstuckMEUserGUI.UserControls.Admin
{
    /// <summary>
    /// Interaction logic for ReportsArea.xaml
    /// </summary>
    public partial class ReportsArea : UserControl
    {
        public ReportsArea()
        {
            InitializeComponent();
        }
        private void ResolveReportsBtn_Click(object sender, RoutedEventArgs e)
        {
            Window win = new SubWindows.ResolveReportsWindow();
            win.Show();
        }
    }
}
