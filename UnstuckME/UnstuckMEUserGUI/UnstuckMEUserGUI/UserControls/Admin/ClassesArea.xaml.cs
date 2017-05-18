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
