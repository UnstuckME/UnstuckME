using System.Windows;
using System.Windows.Controls;

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
