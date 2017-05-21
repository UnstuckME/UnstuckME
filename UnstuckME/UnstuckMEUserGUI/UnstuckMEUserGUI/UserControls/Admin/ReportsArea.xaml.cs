using System.Windows;
using System.Windows.Controls;
using UnstuckMEUserGUI.SubWindows;

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
            Window win = new ResolveReportsWindow();
            win.Topmost = true;
            win.Show();
        }
    }
}
