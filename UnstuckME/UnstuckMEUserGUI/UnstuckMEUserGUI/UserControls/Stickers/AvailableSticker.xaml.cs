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
        public UnstuckMEAvailableSticker Sticker;
        public UserClass Class;
        public AvailableSticker(UnstuckMEAvailableSticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            LabelClassName.Content = Sticker.CourseCode + " " + Sticker.CourseNumber + " " + Sticker.CourseName + "   " + "Student Rating = " + Sticker.StudentRanking;
        }

        public void RemoveFromStackPanel()
        {
            //Removes Sticker From Stack Panel
            ((StackPanel)this.Parent).Children.Remove(this);
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

        private void ButtonAccept_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonAccept.Background = Brushes.LimeGreen;
        }

        private void ButtonAccept_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonAccept.Background = Brushes.ForestGreen;
        }

        private void ButtonAccept_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UnstuckMEWindow.Server.AcceptSticker(UnstuckMEWindow.User.UserID, Sticker.StickerID);
                Application.Current.Windows.OfType<UnstuckMEWindow>().SingleOrDefault().StickerAcceptedStartConversation(Sticker.StudentID, UnstuckMEWindow.User.UserID);
                ((StackPanel)this.Parent).Children.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Accept Failed In Available Sticker User Control. " + ex.Message);
            }
        }
    }
}
