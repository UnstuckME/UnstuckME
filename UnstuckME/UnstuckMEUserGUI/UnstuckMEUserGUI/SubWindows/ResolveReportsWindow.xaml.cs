using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UnstuckMEUserGUI.UserControls.Reviews;

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
            if (reports != null && reports.Count > 0)
            {
                foreach (var item in reports)
                {
                    stackPan.Children.Add(new ResolveReportsDisplay(item));
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
