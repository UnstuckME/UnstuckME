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
        private static StickerPage _StickerPage;
        private static SettingsPage _SettingsPage;
        private static SocialPage _SocialPage;
        private static ClassesPage _ClassesPage;

        private static Brush _UnstuckMEBlue;
        private static Brush _UnstuckMERed;
        public UnstuckMEWindow()
        {
            InitializeComponent();
            _UnstuckMERed = HomeButtonBorder.Background;
            _UnstuckMEBlue = StickerButtonBorder.Background;

            _HomePage = new HomePage();
            _StickerPage = new StickerPage();
            _SettingsPage = new SettingsPage();
            _SocialPage = new SocialPage();
            _ClassesPage = new ClassesPage();

            for (int i = 0; i < 30; i++)
            {
                OnlineUsersStack.Children.Add(new OnlineUser("Hello User " + i ));
            }

            for (int i = 0; i < 30; i++)
            {
                AvailableStickersStack.Children.Add(new AvailableSticker("CST 11" + i));
            }

            MainFrame.Navigate(_HomePage);
            HomeButtonBorder.Background = _UnstuckMERed;
            StickerButtonBorder.Background = _UnstuckMEBlue;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_HomePage);
            HomeButtonBorder.Background = _UnstuckMERed;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            SocialButtonBorder.Background = _UnstuckMEBlue;
            ClassButtonBorder.Background = _UnstuckMEBlue;
        }

        private void StickerButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_StickerPage);
            HomeButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMERed;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            SocialButtonBorder.Background = _UnstuckMEBlue;
            ClassButtonBorder.Background = _UnstuckMEBlue;
        }

        private void SocialButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_SocialPage);
            HomeButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            SocialButtonBorder.Background = _UnstuckMERed;
            ClassButtonBorder.Background = _UnstuckMEBlue;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_SettingsPage);
            HomeButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMERed;
            SocialButtonBorder.Background = _UnstuckMEBlue;
            ClassButtonBorder.Background = _UnstuckMEBlue;
        }

        private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_ClassesPage);
            HomeButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            SocialButtonBorder.Background = _UnstuckMEBlue;
            ClassButtonBorder.Background = _UnstuckMERed;
        }
    }
}
