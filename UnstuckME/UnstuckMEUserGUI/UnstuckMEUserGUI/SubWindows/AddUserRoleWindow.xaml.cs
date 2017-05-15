using System;
using System.Windows;
using UnstuckMeLoggers;
using UnstuckME_Classes;

namespace UnstuckMEUserGUI.SubWindows
{
    /// <summary>
    /// Interaction logic for AddUserRoleWindow.xaml
    /// </summary>
    public partial class AddUserRoleWindow : Window
    {
        private bool isUser = false;
        private bool isModerator = false;
        private bool isAdmin = false;
        private bool isDisabled = false;
        private UserInfo targetUser;
        private int userID = -1;

        public AddUserRoleWindow()
        {
            InitializeComponent();
        }

        private void UpdateRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isUser)
                UnstuckME.Server.SetUserPrivileges(Privileges.User, userID);
            else if (isModerator)
                UnstuckME.Server.SetUserPrivileges(Privileges.Moderator, userID);
            else if (isAdmin)
                UnstuckME.Server.SetUserPrivileges(Privileges.Admin, userID);
            else if (isDisabled)
                UnstuckME.Server.SetUserPrivileges(Privileges.InvalidUser, userID);
        }

        private void FindUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userID = UnstuckME.Server.GetUserID(UserEmailTxtBx.Text);
            }
            catch (Exception ex)
            {
                var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "While attempting a change to the user role an bad email was entered, Source = " + trace.Name);
            }
            if (userID != -1)
            {
                targetUser = UnstuckME.Server.GetUserInfo(userID, UserEmailTxtBx.Text);
                if (targetUser.Privileges == Privileges.User)
                {
                    isUser = true;
                    UserBtn.IsChecked = true;
                }
                else if (targetUser.Privileges == Privileges.Moderator)
                {
                    isModerator = true;
                    ModeratorBtn.IsChecked = true;
                }
                else if (targetUser.Privileges == Privileges.Admin)
                {
                    isAdmin = true;
                    AdminBtn.IsChecked = true;
                }
                else if (targetUser.Privileges == (int)Privileges.InvalidUser)
                {
                    isDisabled = true;
                    DisabledBtn.IsChecked = true;
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

            isDisabled = false;
            DisabledBtn.IsChecked = false;
        }

        private void ModeratorBtn_Checked(object sender, RoutedEventArgs e)
        {
            UserBtn.IsChecked = false;
            isUser = false;

            isModerator = true;
            ModeratorBtn.IsChecked = true;

            isAdmin = false;
            AdminBtn.IsChecked = false;

            isDisabled = false;
            DisabledBtn.IsChecked = false;
        }

        private void AdminBtn_Checked(object sender, RoutedEventArgs e)
        {
            UserBtn.IsChecked = false;
            isUser = false;

            isModerator = false;
            ModeratorBtn.IsChecked = false;

            isAdmin = true;
            AdminBtn.IsChecked = true;

            isDisabled = false;
            DisabledBtn.IsChecked = false;
        }

        private void DisabledBtn_Checked(object sender, RoutedEventArgs e)
        {
            UserBtn.IsChecked = false;
            isUser = false;

            isModerator = false;
            ModeratorBtn.IsChecked = false;

            isAdmin = false;
            AdminBtn.IsChecked = false;

            isDisabled = true;
            DisabledBtn.IsChecked = true;
        }
    }
}
