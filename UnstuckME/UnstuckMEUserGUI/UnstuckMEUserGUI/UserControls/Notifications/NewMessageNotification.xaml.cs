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
    /// Interaction logic for NewMessageNotification.xaml
    /// </summary>
    public partial class NewMessageNotification : UserControl
    {
        UnstuckMEPages _pages;
        UnstuckMEWindow _Window;
        string _username;
        public NewMessageNotification(string inUserName, ref UnstuckMEPages inPages, UnstuckMEWindow inWindow)
        {
            InitializeComponent();
            NewMessageButton.Content = "New Message From: " + inUserName;
            _pages = inPages;
            _Window = inWindow;
            _username = inUserName;
        }

        private void NewMessageButton_Click(object sender, RoutedEventArgs e)
        {
            _Window.SocialButton_Click(sender, e);
            _pages.SocialPage.NotificationCall(_username);
            //Removes from stack panel.
            ((StackPanel)this.Parent).Children.Remove(this);
        }
    }
}
