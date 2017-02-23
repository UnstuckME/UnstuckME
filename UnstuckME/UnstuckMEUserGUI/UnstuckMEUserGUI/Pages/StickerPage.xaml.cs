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

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for StickerPage.xaml
    /// </summary>
    public partial class StickerPage : Page
    {
        public List<UnstuckMEAvailableSticker> AvailableStickers;
        public List<UnstuckMESticker> OpenStickers;
        public List<UnstuckMESticker> MyStickers;
		public List<UnstuckMESticker> History;
        public StickerPage()
        {
            InitializeComponent();

        }

    }

}
