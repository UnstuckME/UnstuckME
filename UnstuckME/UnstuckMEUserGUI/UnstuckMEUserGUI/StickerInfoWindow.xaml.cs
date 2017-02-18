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
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerInfoWindow.xaml
    /// </summary>
    public partial class StickerInfoWindow : Window
    {
        public static UnstuckMESticker Sticker;
        public static UserClass Class;
        public StickerInfoWindow(ref UnstuckMESticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            Topmost = true;
            if(Sticker.ProblemDescription == string.Empty)
            {
                TextBoxProblemDescription.Text = "(No Description Given)";
            }
            else
            {
                TextBoxProblemDescription.Text = Sticker.ProblemDescription;
            }
        }

        private void MoveBottomRightEdgeOfWindowToMousePosition()
        {
            var transform = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var mouse = transform.Transform(GetMousePosition());
            Left = mouse.X;
            Top = mouse.Y;
        }

        public System.Windows.Point GetMousePosition()
        {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new System.Windows.Point(point.X, point.Y);
        }

         private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveBottomRightEdgeOfWindowToMousePosition();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
