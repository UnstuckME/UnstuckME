using System;
using System.Windows;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerInfoWindow.xaml
    /// </summary>
    public partial class StickerInfoWindow : Window
    {
        public UnstuckMEAvailableSticker Sticker;
        public static UserClass Class;

        public StickerInfoWindow(ref UnstuckMEAvailableSticker inSticker)
        {
            InitializeComponent();
            Sticker = inSticker;
            Topmost = true;

            TextBoxProblemDescription.Text = Sticker.ProblemDescription == string.Empty ? "(No Description Given)" : Sticker.ProblemDescription;

            LabelLongTimeout.Content = "Timeout: " + DateTime.Now.AddSeconds((Sticker.Timeout - DateTime.Now).TotalSeconds).ToLongDateString() + " " + DateTime.Now.AddSeconds((Sticker.Timeout - DateTime.Now).TotalSeconds).ToShortTimeString();
            LabelClassName.Content = "Class: " + Sticker.CourseCode  + " " + Sticker.CourseNumber + " " + Sticker.CourseName;

        }

        private void MoveBottomRightEdgeOfWindowToMousePosition()
        {
            PresentationSource presentationSource = PresentationSource.FromVisual(this);
            if (presentationSource?.CompositionTarget != null)
            {
                var transform = presentationSource.CompositionTarget.TransformFromDevice;
                var mouse = transform.Transform(GetMousePosition());
                Left = mouse.X;
                Top = mouse.Y;
            }
        }

        public Point GetMousePosition()
        {
            System.Drawing.Point point = System.Windows.Forms.Control.MousePosition;
            return new Point(point.X, point.Y);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MoveBottomRightEdgeOfWindowToMousePosition();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Close();
        }
    }
}
