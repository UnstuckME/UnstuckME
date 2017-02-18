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
    /// Interaction logic for AvailableStickerNotification.xaml
    /// </summary>
    public partial class AvailableStickerNotification : UserControl
    {
        public AvailableStickerNotification(string inClassName)
        {
            InitializeComponent();
            ClassNameInfoButton.Content = inClassName;
        }
        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ClassNameInfoButton.Foreground = Brushes.Black;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ClassNameInfoButton.Foreground = Brushes.White;
        }

        private void ClassNameInfoButton_Click(object sender, RoutedEventArgs e)
        {
            ClassNameInfoButton.Content = "Clicked";
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {

            //Removes from stack panel.
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {

            //Removes from stack panel.
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
