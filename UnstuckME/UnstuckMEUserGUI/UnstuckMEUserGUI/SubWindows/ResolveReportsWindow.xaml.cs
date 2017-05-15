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

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for ResolveReportsWindow.xaml
    /// </summary>
    public partial class ResolveReportsWindow : Window
    {
        List<int> reports;
        public ResolveReportsWindow()
        {
            InitializeComponent();
            //get the list of reports that need resolved
            reports = UnstuckME.Server.GetAllReportedReviewIDs();
            //populate the stack panel with each of the reports
            // show content of the review (stars, words)
            if (reports != null)
            {
                foreach (var item in reports)
                {
                    stackPan.Children.Add(new UserControls.Reviews.ResolveReportsDisplay(item));
                }
            }
            else
            {
                TextBox txt = new TextBox();
                txt.Text = "Nothing to see here... Move along...";
                stackPan.Children.Add(txt);
            }
            
        }
    }
}
