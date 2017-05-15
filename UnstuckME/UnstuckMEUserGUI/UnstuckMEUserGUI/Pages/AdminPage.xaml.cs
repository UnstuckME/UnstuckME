using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using UnstuckME_Classes;
using UnstuckMeLoggers;
using UnstuckMEUserGUI.SubWindows;

namespace UnstuckMEUserGUI
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private bool isUser = false;
        private bool isModerator = false;
        private bool isAdmin = false;
        private bool isDisabled = false;
        private UserInfo targetUser;
        private int userID = -1;

        public AdminPage()
        {
            InitializeComponent();
            List<Organization> orgList = UnstuckME.Server.GetAllOrganizations();
            foreach (Organization item in orgList)
            // this is going to end up needing to be a different control with the same look so we can remove these on click from the db
                StackPanelOrganization.Children.Add(new TutoringOrganizationDisplay(item.MentorID, item.OrganizationName));
        }

        private void AddRemoveClassesBtn_Click(object sender, RoutedEventArgs e)
        {
            ImportClassesViaFile cheese = new ImportClassesViaFile();
            cheese.ShowDialog();
        }

        private void AddRemoveUserRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            AddUserRoleWindow win = new AddUserRoleWindow();
            win.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            // add mentor orgs
            Window win = new AddMentorOrgsWindow();
            win.Show();
        }

        private void AddOrgBtn_Click(object sender, RoutedEventArgs e)
        {
            string OrgName = this.orgName.Text;

            if (OrgName != string.Empty)
            {
                // call server function to add an org
                try
                {
                    UnstuckME.Server.CreateMentorOrg(OrgName);
                }
                catch (Exception ex)
                {
                    var trace = new System.Diagnostics.StackTrace(ex, true).GetFrame(0).GetMethod();
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "Error occured while creating mentor org, Source = " + trace.Name);
                }
            }
        }

        private void UpdateRoleBtn_Click(object sender, RoutedEventArgs e)
        {
            bool userInfoUpdated = false;
            if (isUser)
            {
                try
                {
                    UnstuckME.Server.SetUserPrivileges(Privileges.User, userID);
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "An Error Occured while updating a user role. " + "User = " + userID);
                }

            }
            else if (isModerator)
            {
                try
                {
                    UnstuckME.Server.SetUserPrivileges(Privileges.Moderator, userID);
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "An Error Occured while updating a user role. " + "User = " + userID);
                }
            }
            else if (isAdmin)
            {
                try
                { 
                    UnstuckME.Server.SetUserPrivileges(Privileges.Admin, userID);
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "An Error Occured while updating a user role. " + "User = " + userID);
                }
            }
            else if (isDisabled)
            {
                try
                { 
                    UnstuckME.Server.SetUserPrivileges(Privileges.InvalidUser, userID);
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "An Error Occured while updating a user role. " + "User = " + userID);
                }
            }
            if (textBoxFirstName.Text != targetUser.FirstName && textBoxFirstName.Text != string.Empty)
                userInfoUpdated = true;
            if (textBoxLastName.Text != targetUser.LastName && textBoxLastName.Text != string.Empty)
                userInfoUpdated = true;
            if (textBoxEmailAddress.Text != targetUser.EmailAddress && textBoxEmailAddress.Text != string.Empty)
                userInfoUpdated = true;
            if (userInfoUpdated)
            {
                try
                {
                    UnstuckME.Server.ChangeUserName(targetUser.EmailAddress, textBoxFirstName.Text, textBoxLastName.Text);
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "An Error Occured while updating a user name. " + "User = " + userID);
                }
                // Add code to update email
                //UnstuckME.Server.
            }
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
                try
                { 
                    targetUser = UnstuckME.Server.GetUserInfo(userID, UserEmailTxtBx.Text);
                }
                catch (Exception ex)
                {
                    UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "An Error Occured while searching a user " + "User = " + userID);
                }
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
                else if (targetUser.Privileges == Privileges.InvalidUser)
                {
                    isDisabled = true;
                    DisabledBtn.IsChecked = true;
                }
                textBoxFirstName.Text = targetUser.FirstName;
                textBoxLastName.Text = targetUser.LastName;
                textBoxEmailAddress.Text = targetUser.EmailAddress;
                AverageStudentRankTxt.Content = targetUser.AverageStudentRank;
                NumberOfStudentReviews.Content = targetUser.TotalStudentReviews;
                AverageTutorRankTxt.Content = targetUser.AverageTutorRank;
                NumberOfTutorReviews.Content = targetUser.TotalTutorReviews;
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

        private void AddSingleClassBtn_Click(object sender, RoutedEventArgs e)
        {
            //UnstuckME.Server.AddClassToServer();
        }

        private void ResetPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UnstuckME.Server.ChangePassword(targetUser, "Password");
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "An Error Occured while updating a users password. " + "User = " + userID);
            }
        }

        private void CreateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            // Add checks
            try
            { 
                UnstuckME.Server.CreateNewUser(textBoxFirstName.Text, textBoxLastName.Text, textBoxEmailAddress.Text, "Password");
            }
            catch (Exception ex)
            {
                UnstuckMEUserEndMasterErrLogger.GetInstance().WriteError(ERR_TYPES.USER_GUI_INTERACTION_ERROR, ex.Message, "An Error Occured while Admin creating a user ");
            }

            if (isUser)
                UnstuckME.Server.SetUserPrivileges(Privileges.User, userID);
            else if (isModerator)
                UnstuckME.Server.SetUserPrivileges(Privileges.Moderator, userID);
            else if (isAdmin)
                UnstuckME.Server.SetUserPrivileges(Privileges.Admin, userID);
            else if (isDisabled)
                UnstuckME.Server.SetUserPrivileges(Privileges.InvalidUser, userID);
        }
    }


}

