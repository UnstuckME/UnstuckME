using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for UnstuckMEMessageBox.xaml
    /// </summary>
    public partial class UnstuckMEMessageBox : Window
    {
        public UnstuckMEMessageBox(int boxStyle, string message, string title)
        {
            InitializeComponent();
            ShowBox(boxStyle, message, title);
        }

        //Legend: 0 - ShutdownAndRestart Application, 1 - OK and Cancel Box, 
        void ShowBox(int boxStyle, string message, string title)
        {
            switch(boxStyle)
            {
                case 0: //ShutdownCurrent/RestartNew Application 
                    {
                        WindowCollection windows = App.Current.Windows;
                        foreach (Window item in windows)
                        {
                            if(item != this)
                            {
                                item.Close();
                            }
                        }
                        _GridServerShutdown.IsEnabled = true;
                        textBoxServerShutdownMessage.Text = message;
                        labelTitleServerShutdown.Content = title;
                        _GridServerShutdown.Visibility = Visibility.Visible;
                        break;
                    }
                case 1: //OK/Cancel Box
                    {
                        _GridOKCancel.IsEnabled = true;
                        labelTitleOKCancel.Content = title;
                        textBoxOKCancel.Text = message;
                        _GridOKCancel.Visibility = Visibility.Visible;
                        break;
                    }
            }
        }

        private void buttonCloseServerShutdown_Click(object sender, RoutedEventArgs e)
        {
            string unstuckME = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
            Process.Start(unstuckME);
            Application.Current.Shutdown();
        }

        private void buttonOKServerShutdown_Click(object sender, RoutedEventArgs e)
        {
            string unstuckME = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
            Process.Start(unstuckME);
            Application.Current.Shutdown();
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonCloseOKCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
