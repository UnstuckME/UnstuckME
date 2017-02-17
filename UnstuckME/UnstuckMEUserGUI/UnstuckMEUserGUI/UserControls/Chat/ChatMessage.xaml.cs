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
    /// Interaction logic for ChatMessage.xaml
    /// </summary>
    public partial class ChatMessage : UserControl
    {
        public static UnstuckMEGUIChatMessage Message;
        public ChatMessage(UnstuckMEGUIChatMessage inMessage)
        {
            InitializeComponent();
            TextBoxUserName.Text = inMessage.Username;
            TextBoxChatMessage.Text = inMessage.Message;
            ImageProfilePicture.Source = inMessage.ProfilePic;
            Message = inMessage;
        }
    }
}
