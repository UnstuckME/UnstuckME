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
        public static UnstuckMEPages _pages = new UnstuckMEPages();

        private static Brush _UnstuckMEBlue;
        private static Brush _UnstuckMERed;
        public UnstuckMEWindow()
        {
            InitializeComponent();
            _UnstuckMERed = HomeButtonBorder.Background;
            _UnstuckMEBlue = StickerButtonBorder.Background;
 
            _pages.HomePage = new HomePage();
            _pages.StickerPage = new StickerPage();
            _pages.SettingsPage = new SettingsPage();
            _pages.SocialPage = new SocialPage();
            _pages.ClassesPage = new ClassesPage();

            for (int i = 0; i < 30; i++)
            {
                OnlineUsersStack.Children.Add(new OnlineUser("Hello User " + i));
            }

            for (int i = 0; i < 30; i++)
            {
                AvailableStickersStack.Children.Add(new NewMessageNotification("User " +i, ref _pages, this));
                AvailableStickersStack.Children.Add(new AvailableSticker("CST 11" + i));
            }

            MainFrame.Navigate(_pages.HomePage);
            HomeButtonBorder.Background = _UnstuckMERed;
            StickerButtonBorder.Background = _UnstuckMEBlue;
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_pages.HomePage);
            HomeButtonBorder.Background = _UnstuckMERed;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            SocialButtonBorder.Background = _UnstuckMEBlue;
            ClassButtonBorder.Background = _UnstuckMEBlue;
        }

        private void StickerButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_pages.StickerPage);
            HomeButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMERed;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            SocialButtonBorder.Background = _UnstuckMEBlue;
            ClassButtonBorder.Background = _UnstuckMEBlue;
        }

        public void SocialButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_pages.SocialPage);
            HomeButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            SocialButtonBorder.Background = _UnstuckMERed;
            ClassButtonBorder.Background = _UnstuckMEBlue;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_pages.SettingsPage);
            HomeButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMERed;
            SocialButtonBorder.Background = _UnstuckMEBlue;
            ClassButtonBorder.Background = _UnstuckMEBlue;
        }

        private void ClassButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(_pages.ClassesPage);
            HomeButtonBorder.Background = _UnstuckMEBlue;
            StickerButtonBorder.Background = _UnstuckMEBlue;
            SettingButtonBorder.Background = _UnstuckMEBlue;
            SocialButtonBorder.Background = _UnstuckMEBlue;
            ClassButtonBorder.Background = _UnstuckMERed;
        }
    }

    public class UnstuckMEPages
    {
        public  HomePage HomePage { get; set; }
        public  StickerPage StickerPage { get; set; }
        public  SettingsPage SettingsPage { get; set; }
        public  SocialPage SocialPage { get; set; }
        public  ClassesPage ClassesPage { get; set; }
    }
}
