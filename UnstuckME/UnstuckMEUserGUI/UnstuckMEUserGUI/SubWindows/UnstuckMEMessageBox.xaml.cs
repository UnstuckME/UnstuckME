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
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UnstuckMEMessageBox.xaml
    /// </summary>
    public partial class UnstuckMEMessageBox : Window
    {
        private UnstuckMEBox _BoxStyle;
        private UnstuckMEBoxImage _BoxImage;
        //WhiteFill Warning: Height = 40 Width = 20 Margin="45,10,0,0"
        //WhiteFill Error & Info: Margin="40,0,0,0" Width="30" Height="40"
        public UnstuckMEMessageBox(UnstuckMEBox BoxStyle, string DisplayMessage, string BoxTitle, UnstuckMEBoxImage BoxImage)
        {
            Uri uri = new Uri("pack://application:,,,/Resources/Box/Error.png");
            _BoxStyle = BoxStyle;
            _BoxImage = BoxImage;
            InitializeComponent();
            ShowBox(BoxStyle, DisplayMessage, BoxTitle, BoxImage);   
        }

        //Legend: 0 - ShutdownAndRestart Application, 1 - OK and Cancel Box, 
        void ShowBox(UnstuckMEBox boxStyle, string message, string title, UnstuckMEBoxImage boxImage)
        {
            switch(boxImage)
            {
                case UnstuckMEBoxImage.Error:
                    {
                        ImageWhiteFill.Height = 40;
                        ImageWhiteFill.Width = 30;
                        ImageWhiteFill.Margin = new Thickness(40, 0, 0, 0);
                        Uri uri = new Uri("pack://application:,,,/Resources/Box/Error.png");
                        BitmapImage bitmap = new BitmapImage(uri);
                        ImageMessageBox.Source = bitmap;
                        break;
                    }
                case UnstuckMEBoxImage.Information:
                    {
                        ImageWhiteFill.Height = 40;
                        ImageWhiteFill.Width = 30;
                        ImageWhiteFill.Margin = new Thickness(40, 0, 0, 0);
                        Uri uri = new Uri("pack://application:,,,/Resources/Box/Info.png");
                        BitmapImage bitmap = new BitmapImage(uri);
                        ImageMessageBox.Source = bitmap;
                        break;
                    }
                case UnstuckMEBoxImage.Warning:
                    {
                        ImageWhiteFill.Height = 40;
                        ImageWhiteFill.Width = 20;
                        ImageWhiteFill.Margin = new Thickness(45, 10, 0, 0);
                        Uri uri = new Uri("pack://application:,,,/Resources/Box/Warning.png");
                        BitmapImage bitmap = new BitmapImage(uri);
                        ImageMessageBox.Source = bitmap;
                        break;
                    }
            }

            switch(boxStyle)
            {
                case UnstuckMEBox.Shutdown:
                    {
                        WindowCollection windows = App.Current.Windows;
                        foreach (Window item in windows)
                        {
                            if(item != this)
                            {
                                item.Close();
                            }
                        }
                        ButtonOK.Visibility = Visibility.Visible;
                        ButtonOK.IsEnabled = true;
                        LabelTitle.Content = title;
                        TextBoxMessage.Text = message;
                        break;
                    }
                case UnstuckMEBox.OKCancel:
                    {
                        ButtonOK.Visibility = Visibility.Visible;
                        ButtonOK.IsEnabled = true;
                        ButtonCancel.Visibility = Visibility.Visible;
                        ButtonCancel.IsEnabled = true;
                        LabelTitle.Content = title;
                        TextBoxMessage.Text = message;
                        break;
                    }
                case UnstuckMEBox.OK:
                    {
                        ButtonOK.Visibility = Visibility.Visible;
                        ButtonOK.IsEnabled = true;
                        LabelTitle.Content = title;
                        TextBoxMessage.Text = message;
                        break;
                    }
                case UnstuckMEBox.YesNo:
                    {
                        ButtonYes.Visibility = Visibility.Visible;
                        ButtonYes.IsEnabled = true;
                        ButtonNo.Visibility = Visibility.Visible;
                        ButtonNo.IsEnabled = true;
                        LabelTitle.Content = title;
                        TextBoxMessage.Text = message;
                        break;
                    }
                case UnstuckMEBox.YesNoCancel:
                    {
                        ButtonYes.Visibility = Visibility.Visible;
                        ButtonYes.IsEnabled = true;
                        ButtonNo.Visibility = Visibility.Visible;
                        ButtonNo.IsEnabled = true;
                        ButtonYNCancel.Visibility = Visibility.Visible;
                        ButtonYNCancel.IsEnabled = true;
                        LabelTitle.Content = title;
                        TextBoxMessage.Text = message;
                        break;
                    }
            }
        }

        private void ButtonCancel_MouseEnter(object sender, MouseEventArgs e)
        { 
            ButtonYNCancel.Background = Brushes.LightSteelBlue;
        }

        private void ButtonCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonYNCancel.Background = Brushes.SteelBlue;
        }

        private void ButtonCancel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_BoxStyle)
            {
                case UnstuckMEBox.OKCancel:
                    {
                        DialogResult = null;
                        break;
                    }
                case UnstuckMEBox.YesNoCancel:
                    {
                        DialogResult = null;
                        break;
                    }
            }
            try
            {
                this.Close();
            }
            catch (Exception)
            {/*In Case Error is thrown*/ }
        }

        private void ButtonMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonMinimize.Background = Brushes.White;
        }

        private void ButtonMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonMinimize.Background = Brushes.LightGray;
        }

        private void ButtonMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonClose_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonClose.Background = Brushes.White;
        }

        private void ButtonClose_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonClose.Background = Brushes.LightGray;
        }

        private void ButtonClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_BoxStyle)
            {
                case UnstuckMEBox.Shutdown:
                    {
                        string unstuckME = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
                        Process.Start(unstuckME);
                        Application.Current.Shutdown();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            DialogResult = null;
            try
            {
                this.Close();
            }
            catch (Exception)
            {/*In Case Error is thrown*/ }
        }

        private void ButtonOK_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonOK.Background = Brushes.SteelBlue;
        }

        private void ButtonOK_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonOK.Background = UnstuckME.Blue;
        }

        private void ButtonOK_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_BoxStyle)
            {
                case UnstuckMEBox.OKCancel:
                    {
                        DialogResult = true;
                        break;
                    }
                case UnstuckMEBox.OK:
                    {
                        DialogResult = true;
                        break;
                    }
                case UnstuckMEBox.Shutdown:
                    {
                        DialogResult = true;
                        string unstuckME = System.AppDomain.CurrentDomain.BaseDirectory + System.AppDomain.CurrentDomain.FriendlyName;
                        Process.Start(unstuckME);
                        Application.Current.Shutdown();
                        break;
                    }
            }
            try
            {
                this.Close();
            }
            catch (Exception)
            {/*In Case of App Shutdown and Error is thrown*/ }

        }

        private void ButtonNo_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonNo.Background = Brushes.IndianRed;
        }

        private void ButtonNo_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonNo.Background = UnstuckME.Red;
        }

        private void ButtonNo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_BoxStyle)
            {
                case UnstuckMEBox.YesNo:
                    {
                        DialogResult = false;
                        break;
                    }
                case UnstuckMEBox.YesNoCancel:
                    {
                        DialogResult = false;
                        break;
                    }
            }
            try
            {
                this.Close();
            }
            catch (Exception)
            {/*In Case Error is thrown*/ }
        }

        private void ButtonYes_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonYes.Background = Brushes.SteelBlue;
        }

        private void ButtonYes_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonYes.Background = UnstuckME.Blue;
        }

        private void ButtonYes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_BoxStyle)
            {
                case UnstuckMEBox.YesNo:
                    {
                        DialogResult = true;
                        break;
                    }
                case UnstuckMEBox.YesNoCancel:
                    {
                        DialogResult = true;
                        break;
                    }
            }
            try
            {
                this.Close();
            }
            catch (Exception)
            {/*In Case Error is thrown*/ }
        }
    }
}
