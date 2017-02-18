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
    /// Interaction logic for AvailableSticker.xaml
    ///
    //public int StickerID { get; set; }
    //public string ProblemDescription { get; set; }
    //public int ClassID { get; set; }
    //public int ChatID { get; set; }
    //public int StudentID { get; set; }
    //public int TutorID { get; set; }
    //public float MinimumStarRanking { get; set; }
    //public List<int> AttachedOrganizations { get; set; }
    //public DateTime SubmitTime { get; set; }
    //public int Timeout { get; set; }
    /// </summary>
    public partial class AvailableSticker : UserControl
    {
        public static UnstuckMESticker Sticker;
        public static UserClass Class;
        public AvailableSticker(UnstuckMESticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            LabelClassName.Content = "Class ID: " + Sticker.ClassID;
        }

        private void BorderCheck_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderCheck.Background = Brushes.ForestGreen;
        }

        private void BorderCheck_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderCheck.Background = null;
        }

        private void BorderX_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderX.Background = UnstuckMEWindow._UnstuckMERed;
        }

        private void BorderX_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderX.Background = null;
        }

        private void GridClassName_MouseEnter(object sender, MouseEventArgs e)
        {
            GridClassName.Background = UnstuckMEWindow._UnstuckMEBlue;
        }

        private void GridClassName_MouseLeave(object sender, MouseEventArgs e)
        {
            GridClassName.Background = null;
        }

        private void GridClassName_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StickerInfoWindow showInfo = new StickerInfoWindow(ref Sticker);
            showInfo.Show();
        }

        private void BorderX_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Removes Sticker From Stack Panel
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
