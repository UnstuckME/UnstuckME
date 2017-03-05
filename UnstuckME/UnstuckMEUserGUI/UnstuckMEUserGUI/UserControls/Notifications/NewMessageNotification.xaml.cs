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
    /// Interaction logic for NewMessageNotification.xaml
    /// </summary>
    public partial class NewMessageNotification : UserControl
    {
        public UnstuckMEMessage Message { get; set; }
        public int NotificationCount { get; set; }
        public NewMessageNotification(UnstuckMEMessage inMessage)
        {
            InitializeComponent();
            NewMessageButton.Content = "Message From: " + inMessage.Username;
            Message = inMessage;
            NotificationCount = 0;
        }

        public NewMessageNotification(UnstuckMEMessage inMessage, int count)
        {
            NotificationCount = count;
            InitializeComponent();
            NewMessageButton.Content = "Message From: " + inMessage.Username + " (" + count + ")";
            Message = inMessage;
        }

        public void RemoveFromNotifications()
        {
            ((StackPanel)this.Parent).Children.Remove(this);
        }

        private void NewMessageButton_Click(object sender, RoutedEventArgs e)
        {
            UnstuckME.MainWindow.SwitchToChatTab();
            UnstuckME.Pages.ChatPage.NotificationCall(Message);
        }
    }
}
