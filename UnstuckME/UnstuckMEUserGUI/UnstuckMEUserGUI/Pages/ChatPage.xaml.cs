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
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        public ChatPage()
        {
            InitializeComponent();
            for (int i = 0; i < 30; i++)
            {
                //ConversationsStack.Children.Add(new OnlineUser("User " + i));
            }
        }

        //This Box Determines what happens when a user clicks a new message notification.
        public void NotificationCall(string userName)
        {
            MessageTextBox.Text = "Message From " + userName;
        }

        private void MessageTextBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MessageTextBox.CaretIndex = MessageTextBox.Text.Length;
        }

    }
}
