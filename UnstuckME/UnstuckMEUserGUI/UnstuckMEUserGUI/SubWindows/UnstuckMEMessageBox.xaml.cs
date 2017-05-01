using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for UnstuckMEMessageBox.xaml
    /// DO NOT use the Window.Show() method with this as it will cause an error
    /// </summary>
    public partial class UnstuckMEMessageBox : Window
    {
        private readonly UnstuckMEBox _BoxStyle;
        private UnstuckMEBoxImage _boxImage;
        //WhiteFill Warning: Height = 40 Width = 20 Margin="45,10,0,0"
        //WhiteFill Error & Info: Margin="40,0,0,0" Width="30" Height="40"
        public UnstuckMEMessageBox(UnstuckMEBox BoxStyle, string DisplayMessage, string BoxTitle, UnstuckMEBoxImage BoxImage)
        {
            //Uri uri = new Uri("pack://application:,,,/Resources/Box/Error.png");
            _BoxStyle = BoxStyle;
            _boxImage = BoxImage;
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
                        ImageWhiteFill.Margin = new Thickness(35, 0, 0, 10);
                        Uri uri = new Uri("pack://application:,,,/Resources/Box/Error.png");
                        BitmapImage bitmap = new BitmapImage(uri);
                        ImageMessageBox.Source = bitmap;
                        break;
                    }
                case UnstuckMEBoxImage.Information:
                    {
                        ImageWhiteFill.Height = 40;
                        ImageWhiteFill.Width =30;
                        ImageWhiteFill.Margin = new Thickness(35, 0, 0, 10);
                        Uri uri = new Uri("pack://application:,,,/Resources/Box/Info.png");
                        BitmapImage bitmap = new BitmapImage(uri);
                        ImageMessageBox.Source = bitmap;
                        break;
                    }
                case UnstuckMEBoxImage.Warning:
                    {
                        ImageWhiteFill.Height = 30;
                        ImageWhiteFill.Width = 15;
                        ImageWhiteFill.Margin = new Thickness(42, 10, 0, 12);
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
                        WindowCollection windows = Application.Current.Windows;
                        foreach (Window item in windows)
                        {
                            if (item != this)
                                item.Close();
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
                Close();
            }
            catch (Exception)
            {/*In Case Error is thrown*/ }
        }

        private void ButtonMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonMinimize.Background = Brushes.WhiteSmoke;
            ImageButtonMinimize.Opacity = .85;
        }

        private void ButtonMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonMinimize.Background = Brushes.LightGray;
            ImageButtonMinimize.Opacity = 1;
        }

        private void ButtonMinimize_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ButtonClose_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonClose.Background = Brushes.WhiteSmoke;
            ImageButtonClose.Opacity = .85;
        }

        private void ButtonClose_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonClose.Background = Brushes.LightGray;
            ImageButtonClose.Opacity = 1;
        }

        private void ButtonClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_BoxStyle)
            {
                case UnstuckMEBox.Shutdown:
                    {
                        string unstuckME = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;
                        Process.Start(unstuckME);
                        Application.Current.Shutdown();
                        break;
                    }
                default:
                    break;
            }
            DialogResult = null;
            try
            {
                Close();
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
                        string unstuckME = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;
                        Process.Start(unstuckME);
                        Application.Current.Shutdown();
                        break;
                    }
            }
            try
            {
                Close();
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
                Close();
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
                Close();
            }
            catch (Exception)
            {/*In Case Error is thrown*/ }
        }
    }
}
