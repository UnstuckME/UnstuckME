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
    /// Interaction logic for Conversation.xaml
    /// </summary>
    public partial class Conversation : UserControl
    { 
        public Conversation()
        {
            InitializeComponent();

        }

        private void ConversationUserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = Brushes.LightGray;
        }

        private void ConversationUserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = null;
        }

        private void ConversationUserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ConversationLabel.Content = "Clicked";
        }
    }
}
