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
        public UserInfo User;
        public ImageSource UserImage;
        public static IUnstuckMEService Server;
        public UnstuckMEChat currentChat;
        public List<UnstuckMEChat> allChats;
        public ImageSourceConverter ic;
        public ChatPage(ref UserInfo inUser, ref IUnstuckMEService inServer)
        {
            InitializeComponent();
            Server = inServer;
            User = inUser;
            ic = new ImageSourceConverter();
            UserImage = ic.ConvertFrom(User.UserProfilePictureBytes) as ImageSource;
            currentChat = new UnstuckMEChat();
            currentChat.ChatID = -1;
            for (int i = 0; i < 10; i++)
            {
                StackPanelAddContacts.Children.Add(new ContactAddToConversation());
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

        public void AddConversation(UnstuckMEChat inChat)
        {
            StackPanelConversations.Children.Add(new Conversation(inChat, this, ref Server));
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageTextBox.Text != string.Empty)
            {
                SendMessage(MessageTextBox.Text);
                MessageTextBox.Text = string.Empty;
            }

        }

        public void AddMessage(UnstuckMEMessage message)
        {
            try
            {
                var temp = from a in allChats
                           where a.ChatID == message.ChatID
                           select a;
                UnstuckMEChat chat = temp.First();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Add Message Failed" + ": " + ex.Message);
            }
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
                if(currentChat.ChatID < 0) { throw new Exception(); }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Chat Send Failed. Error: " + ex.Message, "Failed Message Send", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ButtonCreateChat_Click(object sender, RoutedEventArgs e)
        {
            ButtonCreateChat.Visibility = Visibility.Hidden;
            ButtonCreateChat.IsEnabled = false;
            GridConversations.Visibility = Visibility.Hidden;
            GridConversations.IsEnabled = false;
            GridNewConversation.Visibility = Visibility.Visible;
            GridNewConversation.IsEnabled = true;
        }

        private void ButtonAddUserDone_Click(object sender, RoutedEventArgs e)
        {
            ButtonCreateChat.Visibility = Visibility.Visible;
            ButtonCreateChat.IsEnabled = true;
            GridConversations.Visibility = Visibility.Visible;
            GridConversations.IsEnabled = true;
            GridNewConversation.Visibility = Visibility.Hidden;
            GridNewConversation.IsEnabled = false;
            TextBoxManualUserNameSearch.Text = string.Empty;
        }

        private void ImageAddUserToConvo_MouseEnter(object sender, MouseEventArgs e)
        {
            BorderAddUserToConvo.Background = Brushes.LightBlue;
        }

        private void ImageAddUserToConvo_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderAddUserToConvo.Background = Brushes.SteelBlue;
        }

        private void ImageAddUserToConvo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Button Clicked");
        }
    }
}
