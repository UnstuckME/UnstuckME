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
    /// Interaction logic for NewAccountSetupWindow.xaml
    /// </summary>
    public partial class NewAccountSetupWindow : Window
    {
        public NewAccountSetupWindow()
        {
            InitializeComponent();
        }

        private void CreateAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (passwordBox.Password == passwordBoxConfirm.Password)
            {
                if (FNameTxtBx.Text != null)
                {

                }
                else
                {
                    // display no first name error
                }
                if (LNameTxtBx.Text != null)
                {

                }
                else
                {
                    // display no last name error
                }
                if (EmailTxtBx.Text != null)
                {

                }
                else
                {
                    // display no email error
                }

            }
            else
            {
                // display pass words do no match error
            }
        }
    }
}
