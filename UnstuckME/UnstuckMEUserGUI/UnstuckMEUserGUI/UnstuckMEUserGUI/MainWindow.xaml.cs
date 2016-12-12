using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using UnstuckMEInterfaces;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(int UserID)
        {
            //Opens a connection to UnstuckME Server.
            ChannelFactory<IUnstuckMEService> channelFactory = new ChannelFactory<IUnstuckMEService>("UnstuckMEClient");
            IUnstuckMEService proxy = channelFactory.CreateChannel();

            int UsersID = UserID;
            UserNameAndEmail userInfo = proxy.GetUserDisplayInfo(UsersID);
            InitializeComponent();
            FNameTxtBx.Text = userInfo.FirstName; // get teh name from the server and insert it
            LNameTxtBx.Text = userInfo.LastName;
            EmailtextBlock.Text = userInfo.EmailAddress; // get the email and show it
            for (int i = 0; i < 50; i++)
            {
                TextBlock test = new TextBlock();
                test.Text = "test text" + i;
                ClassesStack.Children.Add(test);
            }
            
        }
    }
}
