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
using UnstuckMEInterfaces;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        public static UserInfo User;
        private static ImageSource UserImage;
        public static IUnstuckMEService Server;
        public static UnstuckMEChat currentChat;
        public static List<UnstuckMEChat> allChats;
        public static ImageSourceConverter ic;
        public ChatPage(ref UserInfo inUser, ref IUnstuckMEService inServer)
        {
            InitializeComponent();
            Server = inServer;
            User = inUser;
            ic = new ImageSourceConverter();
            UserImage = ic.ConvertFrom(User.UserProfilePictureBytes) as ImageSource;

            for (int i = 0; i < 10; i++)
            {
                StackPanelConversations.Children.Add(new Conversation());
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

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageTextBox.Text != string.Empty)
            {
                SendMessage(MessageTextBox.Text);
                MessageTextBox.Text = string.Empty;
            }

        }

        public void AddMessage(UnstuckMESendChatMessage message)
        {
            UnstuckMEGUIChatMessage test2 = new UnstuckMEGUIChatMessage();
            test2.Message = message.Message;
            test2.ProfilePic = UserImage;
            test2.Username = message.SenderID.ToString();
            StackPanelMessages.Children.Add(new ChatMessage(test2));
        }

        private void ScrollViewerConversationBox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ScrollViewerMessagesBox.ScrollToBottom();
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendButton_Click(null, null);
            }
        }

        private void SendMessage(string message)
        {
            try
            {
                UnstuckMESendChatMessage temp = new UnstuckMESendChatMessage();
                temp.UsersInConvo = new List<int>();
                temp.ChatID = 5;
                temp.Message = message;
                temp.SenderID = User.UserID;
                temp.UsersInConvo.Add(12);
                Server.SendMessage(temp);
                UnstuckMEGUIChatMessage temp2 = new UnstuckMEGUIChatMessage();
                temp2.Message = message;
                temp2.Username = User.FirstName;
                temp2.ProfilePic = UserImage;
                StackPanelMessages.Children.Add(new ChatMessage(temp2));
            }
            catch(Exception ex)
            {
                MessageBox.Show("Chat Send Failed. Error: " + ex.Message, "Failed Message Send", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ButtonCreateChat_Click(object sender, RoutedEventArgs e)
        {
            UnstuckMEChat temp = new UnstuckMEChat();
            try
            {
                temp.ChatID = Server.CreateChat(User.UserID);
                if (temp.ChatID == -1)
                    throw new Exception("Chat Creation Failed!");/////////////////////////////////////////////////////THIS NEEDS TO BE CHANGED LATER.
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Server.InsertUserInToChat(12 /*Default Ryan*/, temp.ChatID);
        }
    }
}
