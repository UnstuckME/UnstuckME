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
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for TutorStickerSubmit.xaml
    /// </summary>
    public partial class TutorStickerSubmit : UserControl
    {
        public string OrganizationName { get; set; }
        public int OrganizationID { get; set; }
        public TutorStickerSubmit(int inOrgID, string inOrgName)
        {
            InitializeComponent();
            OrganizationID = inOrgID;
            OrganizationName = inOrgName;
            textBoxOrgName.Text = inOrgName;
        }

        private void buttonRemoveOrg_Click(object sender, RoutedEventArgs e)
        {
            ((StackPanel)Parent).Children.Remove(this);
        }
    }
}
