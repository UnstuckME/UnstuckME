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
using System.Windows.Shapes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UnstuckMEWindow.xaml
    /// </summary>
    public partial class UnstuckMEWindow : Window
    {
        public UnstuckMEWindow()
        {
            InitializeComponent();
            for (int i = 0; i < 30; i++)
            {
                OnlineUsersStack.Children.Add(new OnlineUser("Hello User " + i ));
            }
            OnlineUsersLabel.Content = "Online Users: " + OnlineUsersStack.Children.Count;

            for (int i = 0; i < 10; i++)
            {
                AvailableStickersStack.Children.Add(new AvailableSticker("CST 11" + i));
            }
        }
    }
}
