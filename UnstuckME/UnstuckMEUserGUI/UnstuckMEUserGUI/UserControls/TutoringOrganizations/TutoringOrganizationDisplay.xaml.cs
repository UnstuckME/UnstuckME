using System;
using System.Windows;
using System.Windows.Controls;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for TutorStickerSubmit.xaml
    /// </summary>
    public partial class TutoringOrganizationDisplay : UserControl, IEquatable<TutoringOrganizationDisplay>
    {
        public Organization TutoringOrg;

        public TutoringOrganizationDisplay(int inOrgID, string inOrgName)
        {
            InitializeComponent();
            TutoringOrg = new Organization
            {
                MentorID = inOrgID,
                OrganizationName = inOrgName
            };
            textBoxOrgName.Text = inOrgName;
        }

        public bool Equals(TutoringOrganizationDisplay other)
        {
            return other != null && TutoringOrg.MentorID == other.TutoringOrg.MentorID && TutoringOrg.OrganizationName == other.TutoringOrg.OrganizationName;
        }

        private void buttonRemoveOrg_Click(object sender, RoutedEventArgs e)
        {
            ((StackPanel)Parent).Children.Remove(this);
        }
    }
}
