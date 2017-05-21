using System.Windows;
using System.Windows.Controls;
using UnstuckMEUserGUI.SubWindows;

namespace UnstuckMEUserGUI.UserControls.Admin
{
    /// <summary>
    /// Interaction logic for ClassesArea.xaml
    /// </summary>
    public partial class ClassesArea : UserControl
    {
        public ClassesArea()
        {
            InitializeComponent();
        }

        private void AddSingleClassBtn_Click(object sender, RoutedEventArgs e)
        {
            //UnstuckME.Server.AddClassToServer();
        }

        private void AddRemoveClassesBtn_Click(object sender, RoutedEventArgs e)
        {
            ImportClassesViaFile cheese = new ImportClassesViaFile();
            cheese.ShowDialog();
        }
    }
}
