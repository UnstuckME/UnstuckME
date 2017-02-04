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
    /// Interaction logic for UnstuckMEWindow.xaml
    /// </summary>
    public partial class UnstuckMEWindow : Window
    {
        private static HomePage _HomePage;
        private static StickerPage _stickerPage;
        private static Brush _UnstuckMEBlue;
        public UnstuckMEWindow()
        {
            InitializeComponent();
            _UnstuckMEBlue = HomeButtonBorder.Background;
            _HomePage = new HomePage();
            _stickerPage = new StickerPage();
            for (int i = 0; i < 30; i++)
            {
                OnlineUsersStack.Children.Add(new OnlineUser("Hello User " + i ));
            }
            OnlineUsersLabel.Content = "Online Users: " + OnlineUsersStack.Children.Count;

            for (int i = 0; i < 30; i++)
            {
                AvailableStickersStack.Children.Add(new AvailableSticker("CST 11" + i));
            }
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_HomePage);
            HomeButtonBorder.Background = Brushes.White;
            StickerButtonBorder.Background = _UnstuckMEBlue;
        }

        private void StickerButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_stickerPage);
            StickerButtonBorder.Background = Brushes.White;
            HomeButtonBorder.Background = _UnstuckMEBlue;
        }
    }
}
