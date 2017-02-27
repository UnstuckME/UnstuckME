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
using UnstuckMEInterfaces;
using UnstuckMeLoggers;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddUserRoleWindow.xaml
    /// </summary>
    public partial class AddUserRoleWindow : Window
    {
        IUnstuckMEService Server;
        bool isUser = false;
        bool isModerator = false;
        bool isAdmin = false;
        UserInfo targetUser;
        int userId = -1;
        public AddUserRoleWindow(ref IUnstuckMEService server)
        {
            Server = server;
            InitializeComponent();
        }

        private void UpdateRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isUser)
            {
                Server.SetUserPrivileges(Privileges.User, userId);
            }
            else if (isModerator)
            {
                Server.SetUserPrivileges(Privileges.Moderator, userId);
            }
            else if (isAdmin)
            {
                Server.SetUserPrivileges(Privileges.Admin, userId);
            }
        }

        private void FindUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userId = Server.GetUserID(UserEmailTxtBx.Text);
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "While attempting a change to the user role an bad email was entered");
            }
            if (userId != -1)
            {
                targetUser = Server.GetUserInfo(userId, UserEmailTxtBx.Text);
                if (targetUser.Privileges == (int)Privileges.User)
                {
                    isUser = true;
                    UserBtn.IsChecked = true;
                }
                else if (targetUser.Privileges == (int)Privileges.Moderator)
                {
                    isModerator = true;
                    ModeratorBtn.IsChecked = true;
                }
                else if (targetUser.Privileges == (int)Privileges.Admin)
                {
                    isAdmin = true;
                    AdminBtn.IsChecked = true;
                }
                FirstNameTxt.Text = targetUser.FirstName;
                LastNameTxt.Text = targetUser.LastName;
            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs e)
        {
            UserBtn.IsChecked = true;
            isUser = true;

            isModerator = false;
            ModeratorBtn.IsChecked = false;

            isAdmin = false;
            AdminBtn.IsChecked = false;
        }

        private void ModeratorBtn_Checked(object sender, RoutedEventArgs e)
        {
            UserBtn.IsChecked = false;
            isUser = false;

            isModerator = true;
            ModeratorBtn.IsChecked = true;

            isAdmin = false;
            AdminBtn.IsChecked = false;
        }

        private void AdminBtn_Checked(object sender, RoutedEventArgs e)
        {
            UserBtn.IsChecked = false;
            isUser = false;

            isModerator = false;
            ModeratorBtn.IsChecked = false;

            isAdmin = true;
            AdminBtn.IsChecked = true;
        }
    }
}
