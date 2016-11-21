using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestAllStoredProcedures
{
    [TestClass()]
    public class SqlServerUnitTest1 : SqlDatabaseTestClass
    {

        public SqlServerUnitTest1()
        {
            InitializeComponent();
        }

        [TestInitialize()]
        public void TestInitialize()
        {
            base.InitializeTest();
        }
        [TestCleanup()]
        public void TestCleanup()
        {
            base.CleanupTest();
        }

        #region Designer support code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AdminPullAllReportsTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerUnitTest1));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ChangeProfilePictureTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ClearReviewDescriptionByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition6;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateChatTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateNewClassTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateNewUserTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition6;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateOfficialMentorTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition7;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateReportTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition8;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateReviewTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition9;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateServerTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition10;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateStickerTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition11;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteClassByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition12;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteFileByFileIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition13;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteMentorOrganizationByMentorIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition14;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteMessageByMessageIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition15;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteReportByReportIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition16;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteServerInformationTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition17;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteServerInformationByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition18;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteUserPictureByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition19;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteUserProfileByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition20;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_FilterUserReviewsByEqualStarRankTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition21;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_FilterUserReviewsByGreaterThanStarRankTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition22;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetActiveStickersWithStarRankOrMentorOrganizationTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition23;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetAdminInfoTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition24;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetAllActiveStickersTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition25;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetAllOrganizationsTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition26;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetAllResolvedStickersTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition27;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetAllStickersTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition28;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetAllStudentReviewsTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition29;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetDisplayNameAndEmailTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition30;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetEmailCredentialsTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition31;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetProfilePictureTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition32;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetSchoolNameTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition33;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetServerDomainTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition34;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetServerIPTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition35;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetServerNameTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition36;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetUserAvgStudentStarRankTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition37;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetUserAvgTutorStarRankTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition38;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetUserClassesTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition39;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetUserOrganizationsTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition40;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetUserStickersAndReviewsTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition41;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_GetUsersWithOverallStarRankTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition42;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_InsertFileTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition43;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_InsertMessageTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition44;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_InsertStudentIntoClassTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition45;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_InsertUserIntoChatTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition46;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_InsertUserIntoMentorProgramTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition47;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_OpenProfilePageTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition48;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_PullActiveClassSpecificStickersTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition49;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_PullChatMessagesAndFilesBetweenUsersTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition50;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_RetrieveLoginTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition51;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateAdminPasswordByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition52;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateAdminUsernameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition53;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseCodeByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition54;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseNameByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition55;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseNumberByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition56;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateDisplayFNameByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition57;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateDisplayLNameByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition58;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateEmailAddressByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition59;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateEmailCredentialsByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition60;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateMentorNameByMentorIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition61;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateMessageByMessageIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition62;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdatePrivilegesByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition63;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateReviewDescriptionByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition64;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateSchoolNameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition65;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerDomainByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition66;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerIPByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition67;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerNameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition68;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateStarRankingByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition69;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition70;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateTermsOfferedByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition71;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition72;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition73;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateUserPasswordByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition74;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ViewAllClassesTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition75;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ViewAllUsersTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition76;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_AdminPullAllReportsTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ChangeProfilePictureTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ChangeProfilePictureTest_PosttestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition scalarValueCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ClearReviewDescriptionByReviewIDTest_PretestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testInitializeAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test4UsersAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test3ClassesAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test5UserToClassAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test3StickersAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test2ReviewsAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test4OfficialMentorsAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test6OmToUserAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test4ChatsAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test5UserToChatsAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition Test2FilesAdded;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction testCleanupAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition rowCountCondition6;
            this.dbo_AdminPullAllReportsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_ChangeProfilePictureTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_ClearReviewDescriptionByReviewIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CreateChatTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CreateNewClassTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CreateNewUserTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CreateOfficialMentorTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CreateReportTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CreateReviewTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CreateServerTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_CreateStickerTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteClassByClassIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteFileByFileIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteMentorOrganizationByMentorIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteMessageByMessageIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteReportByReportIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteServerInformationTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteServerInformationByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteUserPictureByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteUserProfileByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_FilterUserReviewsByEqualStarRankTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_FilterUserReviewsByGreaterThanStarRankTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetActiveStickersWithStarRankOrMentorOrganizationTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetAdminInfoTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetAllActiveStickersTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetAllOrganizationsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetAllResolvedStickersTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetAllStickersTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetAllStudentReviewsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetDisplayNameAndEmailTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetEmailCredentialsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetProfilePictureTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetSchoolNameTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetServerDomainTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetServerIPTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetServerNameTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetUserAvgStudentStarRankTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetUserAvgTutorStarRankTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetUserClassesTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetUserOrganizationsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetUserStickersAndReviewsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_GetUsersWithOverallStarRankTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_InsertFileTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_InsertMessageTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_InsertStudentIntoClassTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_InsertUserIntoChatTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_InsertUserIntoMentorProgramTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_OpenProfilePageTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_PullActiveClassSpecificStickersTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_PullChatMessagesAndFilesBetweenUsersTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_RetrieveLoginTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateAdminPasswordByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateAdminUsernameByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateCourseCodeByClassIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateCourseNameByClassIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateCourseNumberByClassIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateDisplayFNameByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateDisplayLNameByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateEmailAddressByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateEmailCredentialsByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateMentorNameByMentorIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateMessageByMessageIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdatePrivilegesByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateReviewDescriptionByReviewIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateSchoolNameByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateServerDomainByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateServerIPByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateServerNameByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateStarRankingByReviewIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateStickerProblemDescriptionByStickerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateTermsOfferedByClassIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateTimeoutByStickerIDAndSecondsTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateTutorIDByTutorIDAndStickerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateUserPasswordByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_ViewAllClassesTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_ViewAllUsersTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_AdminPullAllReportsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_ChangeProfilePictureTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            rowCountCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            rowCountCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_ClearReviewDescriptionByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            dbo_CreateChatTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateNewClassTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateNewUserTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateOfficialMentorTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition7 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateReportTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition8 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateReviewTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition9 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateServerTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition10 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateStickerTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition11 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteClassByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition12 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteFileByFileIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition13 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteMentorOrganizationByMentorIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition14 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteMessageByMessageIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition15 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteReportByReportIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition16 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteServerInformationTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition17 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteServerInformationByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition18 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteUserPictureByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition19 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteUserProfileByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition20 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_FilterUserReviewsByEqualStarRankTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition21 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_FilterUserReviewsByGreaterThanStarRankTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition22 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetActiveStickersWithStarRankOrMentorOrganizationTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition23 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetAdminInfoTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition24 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetAllActiveStickersTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition25 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetAllOrganizationsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition26 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetAllResolvedStickersTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition27 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetAllStickersTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition28 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetAllStudentReviewsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition29 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetDisplayNameAndEmailTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition30 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetEmailCredentialsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition31 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetProfilePictureTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition32 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetSchoolNameTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition33 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetServerDomainTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition34 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetServerIPTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition35 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetServerNameTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition36 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetUserAvgStudentStarRankTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition37 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetUserAvgTutorStarRankTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition38 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetUserClassesTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition39 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetUserOrganizationsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition40 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetUserStickersAndReviewsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition41 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_GetUsersWithOverallStarRankTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition42 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_InsertFileTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition43 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_InsertMessageTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition44 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_InsertStudentIntoClassTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition45 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_InsertUserIntoChatTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition46 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_InsertUserIntoMentorProgramTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition47 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_OpenProfilePageTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition48 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_PullActiveClassSpecificStickersTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition49 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_PullChatMessagesAndFilesBetweenUsersTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition50 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_RetrieveLoginTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition51 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateAdminPasswordByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition52 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateAdminUsernameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition53 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseCodeByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition54 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseNameByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition55 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseNumberByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition56 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateDisplayFNameByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition57 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateDisplayLNameByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition58 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateEmailAddressByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition59 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateEmailCredentialsByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition60 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateMentorNameByMentorIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition61 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateMessageByMessageIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition62 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdatePrivilegesByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition63 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateReviewDescriptionByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition64 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateSchoolNameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition65 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerDomainByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition66 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerIPByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition67 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerNameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition68 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateStarRankingByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition69 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition70 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateTermsOfferedByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition71 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition72 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition73 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateUserPasswordByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition74 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_ViewAllClassesTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition75 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_ViewAllUsersTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition76 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_AdminPullAllReportsTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_ChangeProfilePictureTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            rowCountCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_ChangeProfilePictureTest_PosttestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            scalarValueCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            scalarValueCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.ScalarValueCondition();
            rowCountCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            rowCountCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            dbo_ClearReviewDescriptionByReviewIDTest_PretestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            testInitializeAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            Test4UsersAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test3ClassesAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test5UserToClassAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test3StickersAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test2ReviewsAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test4OfficialMentorsAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test6OmToUserAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test4ChatsAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test5UserToChatsAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            Test2FilesAdded = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            testCleanupAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            rowCountCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.RowCountCondition();
            // 
            // dbo_AdminPullAllReportsTest_TestAction
            // 
            dbo_AdminPullAllReportsTest_TestAction.Conditions.Add(scalarValueCondition1);
            resources.ApplyResources(dbo_AdminPullAllReportsTest_TestAction, "dbo_AdminPullAllReportsTest_TestAction");
            // 
            // scalarValueCondition1
            // 
            scalarValueCondition1.ColumnNumber = 1;
            scalarValueCondition1.Enabled = true;
            scalarValueCondition1.ExpectedValue = null;
            scalarValueCondition1.Name = "scalarValueCondition1";
            scalarValueCondition1.NullExpected = false;
            scalarValueCondition1.ResultSet = 1;
            scalarValueCondition1.RowNumber = 1;
            // 
            // dbo_ChangeProfilePictureTest_TestAction
            // 
            dbo_ChangeProfilePictureTest_TestAction.Conditions.Add(scalarValueCondition5);
            dbo_ChangeProfilePictureTest_TestAction.Conditions.Add(rowCountCondition4);
            dbo_ChangeProfilePictureTest_TestAction.Conditions.Add(rowCountCondition5);
            resources.ApplyResources(dbo_ChangeProfilePictureTest_TestAction, "dbo_ChangeProfilePictureTest_TestAction");
            // 
            // scalarValueCondition5
            // 
            scalarValueCondition5.ColumnNumber = 1;
            scalarValueCondition5.Enabled = true;
            scalarValueCondition5.ExpectedValue = "0";
            scalarValueCondition5.Name = "scalarValueCondition5";
            scalarValueCondition5.NullExpected = false;
            scalarValueCondition5.ResultSet = 1;
            scalarValueCondition5.RowNumber = 1;
            // 
            // rowCountCondition4
            // 
            rowCountCondition4.Enabled = true;
            rowCountCondition4.Name = "rowCountCondition4";
            rowCountCondition4.ResultSet = 2;
            rowCountCondition4.RowCount = 1;
            // 
            // rowCountCondition5
            // 
            rowCountCondition5.Enabled = true;
            rowCountCondition5.Name = "rowCountCondition5";
            rowCountCondition5.ResultSet = 3;
            rowCountCondition5.RowCount = 1;
            // 
            // dbo_ClearReviewDescriptionByReviewIDTest_TestAction
            // 
            dbo_ClearReviewDescriptionByReviewIDTest_TestAction.Conditions.Add(scalarValueCondition6);
            resources.ApplyResources(dbo_ClearReviewDescriptionByReviewIDTest_TestAction, "dbo_ClearReviewDescriptionByReviewIDTest_TestAction");
            // 
            // scalarValueCondition6
            // 
            scalarValueCondition6.ColumnNumber = 1;
            scalarValueCondition6.Enabled = true;
            scalarValueCondition6.ExpectedValue = "0";
            scalarValueCondition6.Name = "scalarValueCondition6";
            scalarValueCondition6.NullExpected = false;
            scalarValueCondition6.ResultSet = 1;
            scalarValueCondition6.RowNumber = 1;
            // 
            // dbo_CreateChatTest_TestAction
            // 
            dbo_CreateChatTest_TestAction.Conditions.Add(inconclusiveCondition4);
            resources.ApplyResources(dbo_CreateChatTest_TestAction, "dbo_CreateChatTest_TestAction");
            // 
            // inconclusiveCondition4
            // 
            inconclusiveCondition4.Enabled = true;
            inconclusiveCondition4.Name = "inconclusiveCondition4";
            // 
            // dbo_CreateNewClassTest_TestAction
            // 
            dbo_CreateNewClassTest_TestAction.Conditions.Add(inconclusiveCondition5);
            resources.ApplyResources(dbo_CreateNewClassTest_TestAction, "dbo_CreateNewClassTest_TestAction");
            // 
            // inconclusiveCondition5
            // 
            inconclusiveCondition5.Enabled = true;
            inconclusiveCondition5.Name = "inconclusiveCondition5";
            // 
            // dbo_CreateNewUserTest_TestAction
            // 
            dbo_CreateNewUserTest_TestAction.Conditions.Add(inconclusiveCondition6);
            resources.ApplyResources(dbo_CreateNewUserTest_TestAction, "dbo_CreateNewUserTest_TestAction");
            // 
            // inconclusiveCondition6
            // 
            inconclusiveCondition6.Enabled = true;
            inconclusiveCondition6.Name = "inconclusiveCondition6";
            // 
            // dbo_CreateOfficialMentorTest_TestAction
            // 
            dbo_CreateOfficialMentorTest_TestAction.Conditions.Add(inconclusiveCondition7);
            resources.ApplyResources(dbo_CreateOfficialMentorTest_TestAction, "dbo_CreateOfficialMentorTest_TestAction");
            // 
            // inconclusiveCondition7
            // 
            inconclusiveCondition7.Enabled = true;
            inconclusiveCondition7.Name = "inconclusiveCondition7";
            // 
            // dbo_CreateReportTest_TestAction
            // 
            dbo_CreateReportTest_TestAction.Conditions.Add(inconclusiveCondition8);
            resources.ApplyResources(dbo_CreateReportTest_TestAction, "dbo_CreateReportTest_TestAction");
            // 
            // inconclusiveCondition8
            // 
            inconclusiveCondition8.Enabled = true;
            inconclusiveCondition8.Name = "inconclusiveCondition8";
            // 
            // dbo_CreateReviewTest_TestAction
            // 
            dbo_CreateReviewTest_TestAction.Conditions.Add(inconclusiveCondition9);
            resources.ApplyResources(dbo_CreateReviewTest_TestAction, "dbo_CreateReviewTest_TestAction");
            // 
            // inconclusiveCondition9
            // 
            inconclusiveCondition9.Enabled = true;
            inconclusiveCondition9.Name = "inconclusiveCondition9";
            // 
            // dbo_CreateServerTest_TestAction
            // 
            dbo_CreateServerTest_TestAction.Conditions.Add(inconclusiveCondition10);
            resources.ApplyResources(dbo_CreateServerTest_TestAction, "dbo_CreateServerTest_TestAction");
            // 
            // inconclusiveCondition10
            // 
            inconclusiveCondition10.Enabled = true;
            inconclusiveCondition10.Name = "inconclusiveCondition10";
            // 
            // dbo_CreateStickerTest_TestAction
            // 
            dbo_CreateStickerTest_TestAction.Conditions.Add(inconclusiveCondition11);
            resources.ApplyResources(dbo_CreateStickerTest_TestAction, "dbo_CreateStickerTest_TestAction");
            // 
            // inconclusiveCondition11
            // 
            inconclusiveCondition11.Enabled = true;
            inconclusiveCondition11.Name = "inconclusiveCondition11";
            // 
            // dbo_DeleteClassByClassIDTest_TestAction
            // 
            dbo_DeleteClassByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition12);
            resources.ApplyResources(dbo_DeleteClassByClassIDTest_TestAction, "dbo_DeleteClassByClassIDTest_TestAction");
            // 
            // inconclusiveCondition12
            // 
            inconclusiveCondition12.Enabled = true;
            inconclusiveCondition12.Name = "inconclusiveCondition12";
            // 
            // dbo_DeleteFileByFileIDTest_TestAction
            // 
            dbo_DeleteFileByFileIDTest_TestAction.Conditions.Add(inconclusiveCondition13);
            resources.ApplyResources(dbo_DeleteFileByFileIDTest_TestAction, "dbo_DeleteFileByFileIDTest_TestAction");
            // 
            // inconclusiveCondition13
            // 
            inconclusiveCondition13.Enabled = true;
            inconclusiveCondition13.Name = "inconclusiveCondition13";
            // 
            // dbo_DeleteMentorOrganizationByMentorIDTest_TestAction
            // 
            dbo_DeleteMentorOrganizationByMentorIDTest_TestAction.Conditions.Add(inconclusiveCondition14);
            resources.ApplyResources(dbo_DeleteMentorOrganizationByMentorIDTest_TestAction, "dbo_DeleteMentorOrganizationByMentorIDTest_TestAction");
            // 
            // inconclusiveCondition14
            // 
            inconclusiveCondition14.Enabled = true;
            inconclusiveCondition14.Name = "inconclusiveCondition14";
            // 
            // dbo_DeleteMessageByMessageIDTest_TestAction
            // 
            dbo_DeleteMessageByMessageIDTest_TestAction.Conditions.Add(inconclusiveCondition15);
            resources.ApplyResources(dbo_DeleteMessageByMessageIDTest_TestAction, "dbo_DeleteMessageByMessageIDTest_TestAction");
            // 
            // inconclusiveCondition15
            // 
            inconclusiveCondition15.Enabled = true;
            inconclusiveCondition15.Name = "inconclusiveCondition15";
            // 
            // dbo_DeleteReportByReportIDTest_TestAction
            // 
            dbo_DeleteReportByReportIDTest_TestAction.Conditions.Add(inconclusiveCondition16);
            resources.ApplyResources(dbo_DeleteReportByReportIDTest_TestAction, "dbo_DeleteReportByReportIDTest_TestAction");
            // 
            // inconclusiveCondition16
            // 
            inconclusiveCondition16.Enabled = true;
            inconclusiveCondition16.Name = "inconclusiveCondition16";
            // 
            // dbo_DeleteServerInformationTest_TestAction
            // 
            dbo_DeleteServerInformationTest_TestAction.Conditions.Add(inconclusiveCondition17);
            resources.ApplyResources(dbo_DeleteServerInformationTest_TestAction, "dbo_DeleteServerInformationTest_TestAction");
            // 
            // inconclusiveCondition17
            // 
            inconclusiveCondition17.Enabled = true;
            inconclusiveCondition17.Name = "inconclusiveCondition17";
            // 
            // dbo_DeleteServerInformationByServerIDTest_TestAction
            // 
            dbo_DeleteServerInformationByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition18);
            resources.ApplyResources(dbo_DeleteServerInformationByServerIDTest_TestAction, "dbo_DeleteServerInformationByServerIDTest_TestAction");
            // 
            // inconclusiveCondition18
            // 
            inconclusiveCondition18.Enabled = true;
            inconclusiveCondition18.Name = "inconclusiveCondition18";
            // 
            // dbo_DeleteUserPictureByUserIDTest_TestAction
            // 
            dbo_DeleteUserPictureByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition19);
            resources.ApplyResources(dbo_DeleteUserPictureByUserIDTest_TestAction, "dbo_DeleteUserPictureByUserIDTest_TestAction");
            // 
            // inconclusiveCondition19
            // 
            inconclusiveCondition19.Enabled = true;
            inconclusiveCondition19.Name = "inconclusiveCondition19";
            // 
            // dbo_DeleteUserProfileByUserIDTest_TestAction
            // 
            dbo_DeleteUserProfileByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition20);
            resources.ApplyResources(dbo_DeleteUserProfileByUserIDTest_TestAction, "dbo_DeleteUserProfileByUserIDTest_TestAction");
            // 
            // inconclusiveCondition20
            // 
            inconclusiveCondition20.Enabled = true;
            inconclusiveCondition20.Name = "inconclusiveCondition20";
            // 
            // dbo_FilterUserReviewsByEqualStarRankTest_TestAction
            // 
            dbo_FilterUserReviewsByEqualStarRankTest_TestAction.Conditions.Add(inconclusiveCondition21);
            resources.ApplyResources(dbo_FilterUserReviewsByEqualStarRankTest_TestAction, "dbo_FilterUserReviewsByEqualStarRankTest_TestAction");
            // 
            // inconclusiveCondition21
            // 
            inconclusiveCondition21.Enabled = true;
            inconclusiveCondition21.Name = "inconclusiveCondition21";
            // 
            // dbo_FilterUserReviewsByGreaterThanStarRankTest_TestAction
            // 
            dbo_FilterUserReviewsByGreaterThanStarRankTest_TestAction.Conditions.Add(inconclusiveCondition22);
            resources.ApplyResources(dbo_FilterUserReviewsByGreaterThanStarRankTest_TestAction, "dbo_FilterUserReviewsByGreaterThanStarRankTest_TestAction");
            // 
            // inconclusiveCondition22
            // 
            inconclusiveCondition22.Enabled = true;
            inconclusiveCondition22.Name = "inconclusiveCondition22";
            // 
            // dbo_GetActiveStickersWithStarRankOrMentorOrganizationTest_TestAction
            // 
            dbo_GetActiveStickersWithStarRankOrMentorOrganizationTest_TestAction.Conditions.Add(inconclusiveCondition23);
            resources.ApplyResources(dbo_GetActiveStickersWithStarRankOrMentorOrganizationTest_TestAction, "dbo_GetActiveStickersWithStarRankOrMentorOrganizationTest_TestAction");
            // 
            // inconclusiveCondition23
            // 
            inconclusiveCondition23.Enabled = true;
            inconclusiveCondition23.Name = "inconclusiveCondition23";
            // 
            // dbo_GetAdminInfoTest_TestAction
            // 
            dbo_GetAdminInfoTest_TestAction.Conditions.Add(inconclusiveCondition24);
            resources.ApplyResources(dbo_GetAdminInfoTest_TestAction, "dbo_GetAdminInfoTest_TestAction");
            // 
            // inconclusiveCondition24
            // 
            inconclusiveCondition24.Enabled = true;
            inconclusiveCondition24.Name = "inconclusiveCondition24";
            // 
            // dbo_GetAllActiveStickersTest_TestAction
            // 
            dbo_GetAllActiveStickersTest_TestAction.Conditions.Add(inconclusiveCondition25);
            resources.ApplyResources(dbo_GetAllActiveStickersTest_TestAction, "dbo_GetAllActiveStickersTest_TestAction");
            // 
            // inconclusiveCondition25
            // 
            inconclusiveCondition25.Enabled = true;
            inconclusiveCondition25.Name = "inconclusiveCondition25";
            // 
            // dbo_GetAllOrganizationsTest_TestAction
            // 
            dbo_GetAllOrganizationsTest_TestAction.Conditions.Add(inconclusiveCondition26);
            resources.ApplyResources(dbo_GetAllOrganizationsTest_TestAction, "dbo_GetAllOrganizationsTest_TestAction");
            // 
            // inconclusiveCondition26
            // 
            inconclusiveCondition26.Enabled = true;
            inconclusiveCondition26.Name = "inconclusiveCondition26";
            // 
            // dbo_GetAllResolvedStickersTest_TestAction
            // 
            dbo_GetAllResolvedStickersTest_TestAction.Conditions.Add(inconclusiveCondition27);
            resources.ApplyResources(dbo_GetAllResolvedStickersTest_TestAction, "dbo_GetAllResolvedStickersTest_TestAction");
            // 
            // inconclusiveCondition27
            // 
            inconclusiveCondition27.Enabled = true;
            inconclusiveCondition27.Name = "inconclusiveCondition27";
            // 
            // dbo_GetAllStickersTest_TestAction
            // 
            dbo_GetAllStickersTest_TestAction.Conditions.Add(inconclusiveCondition28);
            resources.ApplyResources(dbo_GetAllStickersTest_TestAction, "dbo_GetAllStickersTest_TestAction");
            // 
            // inconclusiveCondition28
            // 
            inconclusiveCondition28.Enabled = true;
            inconclusiveCondition28.Name = "inconclusiveCondition28";
            // 
            // dbo_GetAllStudentReviewsTest_TestAction
            // 
            dbo_GetAllStudentReviewsTest_TestAction.Conditions.Add(inconclusiveCondition29);
            resources.ApplyResources(dbo_GetAllStudentReviewsTest_TestAction, "dbo_GetAllStudentReviewsTest_TestAction");
            // 
            // inconclusiveCondition29
            // 
            inconclusiveCondition29.Enabled = true;
            inconclusiveCondition29.Name = "inconclusiveCondition29";
            // 
            // dbo_GetDisplayNameAndEmailTest_TestAction
            // 
            dbo_GetDisplayNameAndEmailTest_TestAction.Conditions.Add(inconclusiveCondition30);
            resources.ApplyResources(dbo_GetDisplayNameAndEmailTest_TestAction, "dbo_GetDisplayNameAndEmailTest_TestAction");
            // 
            // inconclusiveCondition30
            // 
            inconclusiveCondition30.Enabled = true;
            inconclusiveCondition30.Name = "inconclusiveCondition30";
            // 
            // dbo_GetEmailCredentialsTest_TestAction
            // 
            dbo_GetEmailCredentialsTest_TestAction.Conditions.Add(inconclusiveCondition31);
            resources.ApplyResources(dbo_GetEmailCredentialsTest_TestAction, "dbo_GetEmailCredentialsTest_TestAction");
            // 
            // inconclusiveCondition31
            // 
            inconclusiveCondition31.Enabled = true;
            inconclusiveCondition31.Name = "inconclusiveCondition31";
            // 
            // dbo_GetProfilePictureTest_TestAction
            // 
            dbo_GetProfilePictureTest_TestAction.Conditions.Add(inconclusiveCondition32);
            resources.ApplyResources(dbo_GetProfilePictureTest_TestAction, "dbo_GetProfilePictureTest_TestAction");
            // 
            // inconclusiveCondition32
            // 
            inconclusiveCondition32.Enabled = true;
            inconclusiveCondition32.Name = "inconclusiveCondition32";
            // 
            // dbo_GetSchoolNameTest_TestAction
            // 
            dbo_GetSchoolNameTest_TestAction.Conditions.Add(inconclusiveCondition33);
            resources.ApplyResources(dbo_GetSchoolNameTest_TestAction, "dbo_GetSchoolNameTest_TestAction");
            // 
            // inconclusiveCondition33
            // 
            inconclusiveCondition33.Enabled = true;
            inconclusiveCondition33.Name = "inconclusiveCondition33";
            // 
            // dbo_GetServerDomainTest_TestAction
            // 
            dbo_GetServerDomainTest_TestAction.Conditions.Add(inconclusiveCondition34);
            resources.ApplyResources(dbo_GetServerDomainTest_TestAction, "dbo_GetServerDomainTest_TestAction");
            // 
            // inconclusiveCondition34
            // 
            inconclusiveCondition34.Enabled = true;
            inconclusiveCondition34.Name = "inconclusiveCondition34";
            // 
            // dbo_GetServerIPTest_TestAction
            // 
            dbo_GetServerIPTest_TestAction.Conditions.Add(inconclusiveCondition35);
            resources.ApplyResources(dbo_GetServerIPTest_TestAction, "dbo_GetServerIPTest_TestAction");
            // 
            // inconclusiveCondition35
            // 
            inconclusiveCondition35.Enabled = true;
            inconclusiveCondition35.Name = "inconclusiveCondition35";
            // 
            // dbo_GetServerNameTest_TestAction
            // 
            dbo_GetServerNameTest_TestAction.Conditions.Add(inconclusiveCondition36);
            resources.ApplyResources(dbo_GetServerNameTest_TestAction, "dbo_GetServerNameTest_TestAction");
            // 
            // inconclusiveCondition36
            // 
            inconclusiveCondition36.Enabled = true;
            inconclusiveCondition36.Name = "inconclusiveCondition36";
            // 
            // dbo_GetUserAvgStudentStarRankTest_TestAction
            // 
            dbo_GetUserAvgStudentStarRankTest_TestAction.Conditions.Add(inconclusiveCondition37);
            resources.ApplyResources(dbo_GetUserAvgStudentStarRankTest_TestAction, "dbo_GetUserAvgStudentStarRankTest_TestAction");
            // 
            // inconclusiveCondition37
            // 
            inconclusiveCondition37.Enabled = true;
            inconclusiveCondition37.Name = "inconclusiveCondition37";
            // 
            // dbo_GetUserAvgTutorStarRankTest_TestAction
            // 
            dbo_GetUserAvgTutorStarRankTest_TestAction.Conditions.Add(inconclusiveCondition38);
            resources.ApplyResources(dbo_GetUserAvgTutorStarRankTest_TestAction, "dbo_GetUserAvgTutorStarRankTest_TestAction");
            // 
            // inconclusiveCondition38
            // 
            inconclusiveCondition38.Enabled = true;
            inconclusiveCondition38.Name = "inconclusiveCondition38";
            // 
            // dbo_GetUserClassesTest_TestAction
            // 
            dbo_GetUserClassesTest_TestAction.Conditions.Add(inconclusiveCondition39);
            resources.ApplyResources(dbo_GetUserClassesTest_TestAction, "dbo_GetUserClassesTest_TestAction");
            // 
            // inconclusiveCondition39
            // 
            inconclusiveCondition39.Enabled = true;
            inconclusiveCondition39.Name = "inconclusiveCondition39";
            // 
            // dbo_GetUserOrganizationsTest_TestAction
            // 
            dbo_GetUserOrganizationsTest_TestAction.Conditions.Add(inconclusiveCondition40);
            resources.ApplyResources(dbo_GetUserOrganizationsTest_TestAction, "dbo_GetUserOrganizationsTest_TestAction");
            // 
            // inconclusiveCondition40
            // 
            inconclusiveCondition40.Enabled = true;
            inconclusiveCondition40.Name = "inconclusiveCondition40";
            // 
            // dbo_GetUserStickersAndReviewsTest_TestAction
            // 
            dbo_GetUserStickersAndReviewsTest_TestAction.Conditions.Add(inconclusiveCondition41);
            resources.ApplyResources(dbo_GetUserStickersAndReviewsTest_TestAction, "dbo_GetUserStickersAndReviewsTest_TestAction");
            // 
            // inconclusiveCondition41
            // 
            inconclusiveCondition41.Enabled = true;
            inconclusiveCondition41.Name = "inconclusiveCondition41";
            // 
            // dbo_GetUsersWithOverallStarRankTest_TestAction
            // 
            dbo_GetUsersWithOverallStarRankTest_TestAction.Conditions.Add(inconclusiveCondition42);
            resources.ApplyResources(dbo_GetUsersWithOverallStarRankTest_TestAction, "dbo_GetUsersWithOverallStarRankTest_TestAction");
            // 
            // inconclusiveCondition42
            // 
            inconclusiveCondition42.Enabled = true;
            inconclusiveCondition42.Name = "inconclusiveCondition42";
            // 
            // dbo_InsertFileTest_TestAction
            // 
            dbo_InsertFileTest_TestAction.Conditions.Add(inconclusiveCondition43);
            resources.ApplyResources(dbo_InsertFileTest_TestAction, "dbo_InsertFileTest_TestAction");
            // 
            // inconclusiveCondition43
            // 
            inconclusiveCondition43.Enabled = true;
            inconclusiveCondition43.Name = "inconclusiveCondition43";
            // 
            // dbo_InsertMessageTest_TestAction
            // 
            dbo_InsertMessageTest_TestAction.Conditions.Add(inconclusiveCondition44);
            resources.ApplyResources(dbo_InsertMessageTest_TestAction, "dbo_InsertMessageTest_TestAction");
            // 
            // inconclusiveCondition44
            // 
            inconclusiveCondition44.Enabled = true;
            inconclusiveCondition44.Name = "inconclusiveCondition44";
            // 
            // dbo_InsertStudentIntoClassTest_TestAction
            // 
            dbo_InsertStudentIntoClassTest_TestAction.Conditions.Add(inconclusiveCondition45);
            resources.ApplyResources(dbo_InsertStudentIntoClassTest_TestAction, "dbo_InsertStudentIntoClassTest_TestAction");
            // 
            // inconclusiveCondition45
            // 
            inconclusiveCondition45.Enabled = true;
            inconclusiveCondition45.Name = "inconclusiveCondition45";
            // 
            // dbo_InsertUserIntoChatTest_TestAction
            // 
            dbo_InsertUserIntoChatTest_TestAction.Conditions.Add(inconclusiveCondition46);
            resources.ApplyResources(dbo_InsertUserIntoChatTest_TestAction, "dbo_InsertUserIntoChatTest_TestAction");
            // 
            // inconclusiveCondition46
            // 
            inconclusiveCondition46.Enabled = true;
            inconclusiveCondition46.Name = "inconclusiveCondition46";
            // 
            // dbo_InsertUserIntoMentorProgramTest_TestAction
            // 
            dbo_InsertUserIntoMentorProgramTest_TestAction.Conditions.Add(inconclusiveCondition47);
            resources.ApplyResources(dbo_InsertUserIntoMentorProgramTest_TestAction, "dbo_InsertUserIntoMentorProgramTest_TestAction");
            // 
            // inconclusiveCondition47
            // 
            inconclusiveCondition47.Enabled = true;
            inconclusiveCondition47.Name = "inconclusiveCondition47";
            // 
            // dbo_OpenProfilePageTest_TestAction
            // 
            dbo_OpenProfilePageTest_TestAction.Conditions.Add(inconclusiveCondition48);
            resources.ApplyResources(dbo_OpenProfilePageTest_TestAction, "dbo_OpenProfilePageTest_TestAction");
            // 
            // inconclusiveCondition48
            // 
            inconclusiveCondition48.Enabled = true;
            inconclusiveCondition48.Name = "inconclusiveCondition48";
            // 
            // dbo_PullActiveClassSpecificStickersTest_TestAction
            // 
            dbo_PullActiveClassSpecificStickersTest_TestAction.Conditions.Add(inconclusiveCondition49);
            resources.ApplyResources(dbo_PullActiveClassSpecificStickersTest_TestAction, "dbo_PullActiveClassSpecificStickersTest_TestAction");
            // 
            // inconclusiveCondition49
            // 
            inconclusiveCondition49.Enabled = true;
            inconclusiveCondition49.Name = "inconclusiveCondition49";
            // 
            // dbo_PullChatMessagesAndFilesBetweenUsersTest_TestAction
            // 
            dbo_PullChatMessagesAndFilesBetweenUsersTest_TestAction.Conditions.Add(inconclusiveCondition50);
            resources.ApplyResources(dbo_PullChatMessagesAndFilesBetweenUsersTest_TestAction, "dbo_PullChatMessagesAndFilesBetweenUsersTest_TestAction");
            // 
            // inconclusiveCondition50
            // 
            inconclusiveCondition50.Enabled = true;
            inconclusiveCondition50.Name = "inconclusiveCondition50";
            // 
            // dbo_RetrieveLoginTest_TestAction
            // 
            dbo_RetrieveLoginTest_TestAction.Conditions.Add(inconclusiveCondition51);
            resources.ApplyResources(dbo_RetrieveLoginTest_TestAction, "dbo_RetrieveLoginTest_TestAction");
            // 
            // inconclusiveCondition51
            // 
            inconclusiveCondition51.Enabled = true;
            inconclusiveCondition51.Name = "inconclusiveCondition51";
            // 
            // dbo_UpdateAdminPasswordByServerIDTest_TestAction
            // 
            dbo_UpdateAdminPasswordByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition52);
            resources.ApplyResources(dbo_UpdateAdminPasswordByServerIDTest_TestAction, "dbo_UpdateAdminPasswordByServerIDTest_TestAction");
            // 
            // inconclusiveCondition52
            // 
            inconclusiveCondition52.Enabled = true;
            inconclusiveCondition52.Name = "inconclusiveCondition52";
            // 
            // dbo_UpdateAdminUsernameByServerIDTest_TestAction
            // 
            dbo_UpdateAdminUsernameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition53);
            resources.ApplyResources(dbo_UpdateAdminUsernameByServerIDTest_TestAction, "dbo_UpdateAdminUsernameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition53
            // 
            inconclusiveCondition53.Enabled = true;
            inconclusiveCondition53.Name = "inconclusiveCondition53";
            // 
            // dbo_UpdateCourseCodeByClassIDTest_TestAction
            // 
            dbo_UpdateCourseCodeByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition54);
            resources.ApplyResources(dbo_UpdateCourseCodeByClassIDTest_TestAction, "dbo_UpdateCourseCodeByClassIDTest_TestAction");
            // 
            // inconclusiveCondition54
            // 
            inconclusiveCondition54.Enabled = true;
            inconclusiveCondition54.Name = "inconclusiveCondition54";
            // 
            // dbo_UpdateCourseNameByClassIDTest_TestAction
            // 
            dbo_UpdateCourseNameByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition55);
            resources.ApplyResources(dbo_UpdateCourseNameByClassIDTest_TestAction, "dbo_UpdateCourseNameByClassIDTest_TestAction");
            // 
            // inconclusiveCondition55
            // 
            inconclusiveCondition55.Enabled = true;
            inconclusiveCondition55.Name = "inconclusiveCondition55";
            // 
            // dbo_UpdateCourseNumberByClassIDTest_TestAction
            // 
            dbo_UpdateCourseNumberByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition56);
            resources.ApplyResources(dbo_UpdateCourseNumberByClassIDTest_TestAction, "dbo_UpdateCourseNumberByClassIDTest_TestAction");
            // 
            // inconclusiveCondition56
            // 
            inconclusiveCondition56.Enabled = true;
            inconclusiveCondition56.Name = "inconclusiveCondition56";
            // 
            // dbo_UpdateDisplayFNameByUserIDTest_TestAction
            // 
            dbo_UpdateDisplayFNameByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition57);
            resources.ApplyResources(dbo_UpdateDisplayFNameByUserIDTest_TestAction, "dbo_UpdateDisplayFNameByUserIDTest_TestAction");
            // 
            // inconclusiveCondition57
            // 
            inconclusiveCondition57.Enabled = true;
            inconclusiveCondition57.Name = "inconclusiveCondition57";
            // 
            // dbo_UpdateDisplayLNameByUserIDTest_TestAction
            // 
            dbo_UpdateDisplayLNameByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition58);
            resources.ApplyResources(dbo_UpdateDisplayLNameByUserIDTest_TestAction, "dbo_UpdateDisplayLNameByUserIDTest_TestAction");
            // 
            // inconclusiveCondition58
            // 
            inconclusiveCondition58.Enabled = true;
            inconclusiveCondition58.Name = "inconclusiveCondition58";
            // 
            // dbo_UpdateEmailAddressByUserIDTest_TestAction
            // 
            dbo_UpdateEmailAddressByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition59);
            resources.ApplyResources(dbo_UpdateEmailAddressByUserIDTest_TestAction, "dbo_UpdateEmailAddressByUserIDTest_TestAction");
            // 
            // inconclusiveCondition59
            // 
            inconclusiveCondition59.Enabled = true;
            inconclusiveCondition59.Name = "inconclusiveCondition59";
            // 
            // dbo_UpdateEmailCredentialsByServerIDTest_TestAction
            // 
            dbo_UpdateEmailCredentialsByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition60);
            resources.ApplyResources(dbo_UpdateEmailCredentialsByServerIDTest_TestAction, "dbo_UpdateEmailCredentialsByServerIDTest_TestAction");
            // 
            // inconclusiveCondition60
            // 
            inconclusiveCondition60.Enabled = true;
            inconclusiveCondition60.Name = "inconclusiveCondition60";
            // 
            // dbo_UpdateMentorNameByMentorIDTest_TestAction
            // 
            dbo_UpdateMentorNameByMentorIDTest_TestAction.Conditions.Add(inconclusiveCondition61);
            resources.ApplyResources(dbo_UpdateMentorNameByMentorIDTest_TestAction, "dbo_UpdateMentorNameByMentorIDTest_TestAction");
            // 
            // inconclusiveCondition61
            // 
            inconclusiveCondition61.Enabled = true;
            inconclusiveCondition61.Name = "inconclusiveCondition61";
            // 
            // dbo_UpdateMessageByMessageIDTest_TestAction
            // 
            dbo_UpdateMessageByMessageIDTest_TestAction.Conditions.Add(inconclusiveCondition62);
            resources.ApplyResources(dbo_UpdateMessageByMessageIDTest_TestAction, "dbo_UpdateMessageByMessageIDTest_TestAction");
            // 
            // inconclusiveCondition62
            // 
            inconclusiveCondition62.Enabled = true;
            inconclusiveCondition62.Name = "inconclusiveCondition62";
            // 
            // dbo_UpdatePrivilegesByUserIDTest_TestAction
            // 
            dbo_UpdatePrivilegesByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition63);
            resources.ApplyResources(dbo_UpdatePrivilegesByUserIDTest_TestAction, "dbo_UpdatePrivilegesByUserIDTest_TestAction");
            // 
            // inconclusiveCondition63
            // 
            inconclusiveCondition63.Enabled = true;
            inconclusiveCondition63.Name = "inconclusiveCondition63";
            // 
            // dbo_UpdateReviewDescriptionByReviewIDTest_TestAction
            // 
            dbo_UpdateReviewDescriptionByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition64);
            resources.ApplyResources(dbo_UpdateReviewDescriptionByReviewIDTest_TestAction, "dbo_UpdateReviewDescriptionByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition64
            // 
            inconclusiveCondition64.Enabled = true;
            inconclusiveCondition64.Name = "inconclusiveCondition64";
            // 
            // dbo_UpdateSchoolNameByServerIDTest_TestAction
            // 
            dbo_UpdateSchoolNameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition65);
            resources.ApplyResources(dbo_UpdateSchoolNameByServerIDTest_TestAction, "dbo_UpdateSchoolNameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition65
            // 
            inconclusiveCondition65.Enabled = true;
            inconclusiveCondition65.Name = "inconclusiveCondition65";
            // 
            // dbo_UpdateServerDomainByServerIDTest_TestAction
            // 
            dbo_UpdateServerDomainByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition66);
            resources.ApplyResources(dbo_UpdateServerDomainByServerIDTest_TestAction, "dbo_UpdateServerDomainByServerIDTest_TestAction");
            // 
            // inconclusiveCondition66
            // 
            inconclusiveCondition66.Enabled = true;
            inconclusiveCondition66.Name = "inconclusiveCondition66";
            // 
            // dbo_UpdateServerIPByServerIDTest_TestAction
            // 
            dbo_UpdateServerIPByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition67);
            resources.ApplyResources(dbo_UpdateServerIPByServerIDTest_TestAction, "dbo_UpdateServerIPByServerIDTest_TestAction");
            // 
            // inconclusiveCondition67
            // 
            inconclusiveCondition67.Enabled = true;
            inconclusiveCondition67.Name = "inconclusiveCondition67";
            // 
            // dbo_UpdateServerNameByServerIDTest_TestAction
            // 
            dbo_UpdateServerNameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition68);
            resources.ApplyResources(dbo_UpdateServerNameByServerIDTest_TestAction, "dbo_UpdateServerNameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition68
            // 
            inconclusiveCondition68.Enabled = true;
            inconclusiveCondition68.Name = "inconclusiveCondition68";
            // 
            // dbo_UpdateStarRankingByReviewIDTest_TestAction
            // 
            dbo_UpdateStarRankingByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition69);
            resources.ApplyResources(dbo_UpdateStarRankingByReviewIDTest_TestAction, "dbo_UpdateStarRankingByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition69
            // 
            inconclusiveCondition69.Enabled = true;
            inconclusiveCondition69.Name = "inconclusiveCondition69";
            // 
            // dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction
            // 
            dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction.Conditions.Add(inconclusiveCondition70);
            resources.ApplyResources(dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction, "dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction");
            // 
            // inconclusiveCondition70
            // 
            inconclusiveCondition70.Enabled = true;
            inconclusiveCondition70.Name = "inconclusiveCondition70";
            // 
            // dbo_UpdateTermsOfferedByClassIDTest_TestAction
            // 
            dbo_UpdateTermsOfferedByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition71);
            resources.ApplyResources(dbo_UpdateTermsOfferedByClassIDTest_TestAction, "dbo_UpdateTermsOfferedByClassIDTest_TestAction");
            // 
            // inconclusiveCondition71
            // 
            inconclusiveCondition71.Enabled = true;
            inconclusiveCondition71.Name = "inconclusiveCondition71";
            // 
            // dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction
            // 
            dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction.Conditions.Add(inconclusiveCondition72);
            resources.ApplyResources(dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction, "dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction");
            // 
            // inconclusiveCondition72
            // 
            inconclusiveCondition72.Enabled = true;
            inconclusiveCondition72.Name = "inconclusiveCondition72";
            // 
            // dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction
            // 
            dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction.Conditions.Add(inconclusiveCondition73);
            resources.ApplyResources(dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction, "dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction");
            // 
            // inconclusiveCondition73
            // 
            inconclusiveCondition73.Enabled = true;
            inconclusiveCondition73.Name = "inconclusiveCondition73";
            // 
            // dbo_UpdateUserPasswordByUserIDTest_TestAction
            // 
            dbo_UpdateUserPasswordByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition74);
            resources.ApplyResources(dbo_UpdateUserPasswordByUserIDTest_TestAction, "dbo_UpdateUserPasswordByUserIDTest_TestAction");
            // 
            // inconclusiveCondition74
            // 
            inconclusiveCondition74.Enabled = true;
            inconclusiveCondition74.Name = "inconclusiveCondition74";
            // 
            // dbo_ViewAllClassesTest_TestAction
            // 
            dbo_ViewAllClassesTest_TestAction.Conditions.Add(inconclusiveCondition75);
            resources.ApplyResources(dbo_ViewAllClassesTest_TestAction, "dbo_ViewAllClassesTest_TestAction");
            // 
            // inconclusiveCondition75
            // 
            inconclusiveCondition75.Enabled = true;
            inconclusiveCondition75.Name = "inconclusiveCondition75";
            // 
            // dbo_ViewAllUsersTest_TestAction
            // 
            dbo_ViewAllUsersTest_TestAction.Conditions.Add(inconclusiveCondition76);
            resources.ApplyResources(dbo_ViewAllUsersTest_TestAction, "dbo_ViewAllUsersTest_TestAction");
            // 
            // inconclusiveCondition76
            // 
            inconclusiveCondition76.Enabled = true;
            inconclusiveCondition76.Name = "inconclusiveCondition76";
            // 
            // dbo_AdminPullAllReportsTest_PretestAction
            // 
            resources.ApplyResources(dbo_AdminPullAllReportsTest_PretestAction, "dbo_AdminPullAllReportsTest_PretestAction");
            // 
            // dbo_ChangeProfilePictureTest_PretestAction
            // 
            dbo_ChangeProfilePictureTest_PretestAction.Conditions.Add(scalarValueCondition2);
            dbo_ChangeProfilePictureTest_PretestAction.Conditions.Add(rowCountCondition1);
            resources.ApplyResources(dbo_ChangeProfilePictureTest_PretestAction, "dbo_ChangeProfilePictureTest_PretestAction");
            // 
            // scalarValueCondition2
            // 
            scalarValueCondition2.ColumnNumber = 1;
            scalarValueCondition2.Enabled = true;
            scalarValueCondition2.ExpectedValue = "1";
            scalarValueCondition2.Name = "scalarValueCondition2";
            scalarValueCondition2.NullExpected = false;
            scalarValueCondition2.ResultSet = 1;
            scalarValueCondition2.RowNumber = 1;
            // 
            // rowCountCondition1
            // 
            rowCountCondition1.Enabled = true;
            rowCountCondition1.Name = "rowCountCondition1";
            rowCountCondition1.ResultSet = 1;
            rowCountCondition1.RowCount = 1;
            // 
            // dbo_ChangeProfilePictureTest_PosttestAction
            // 
            dbo_ChangeProfilePictureTest_PosttestAction.Conditions.Add(scalarValueCondition3);
            dbo_ChangeProfilePictureTest_PosttestAction.Conditions.Add(scalarValueCondition4);
            dbo_ChangeProfilePictureTest_PosttestAction.Conditions.Add(rowCountCondition2);
            dbo_ChangeProfilePictureTest_PosttestAction.Conditions.Add(rowCountCondition3);
            resources.ApplyResources(dbo_ChangeProfilePictureTest_PosttestAction, "dbo_ChangeProfilePictureTest_PosttestAction");
            // 
            // scalarValueCondition3
            // 
            scalarValueCondition3.ColumnNumber = 1;
            scalarValueCondition3.Enabled = true;
            scalarValueCondition3.ExpectedValue = "1";
            scalarValueCondition3.Name = "scalarValueCondition3";
            scalarValueCondition3.NullExpected = false;
            scalarValueCondition3.ResultSet = 1;
            scalarValueCondition3.RowNumber = 1;
            // 
            // scalarValueCondition4
            // 
            scalarValueCondition4.ColumnNumber = 1;
            scalarValueCondition4.Enabled = true;
            scalarValueCondition4.ExpectedValue = "1";
            scalarValueCondition4.Name = "scalarValueCondition4";
            scalarValueCondition4.NullExpected = false;
            scalarValueCondition4.ResultSet = 2;
            scalarValueCondition4.RowNumber = 1;
            // 
            // rowCountCondition2
            // 
            rowCountCondition2.Enabled = true;
            rowCountCondition2.Name = "rowCountCondition2";
            rowCountCondition2.ResultSet = 3;
            rowCountCondition2.RowCount = 0;
            // 
            // rowCountCondition3
            // 
            rowCountCondition3.Enabled = true;
            rowCountCondition3.Name = "rowCountCondition3";
            rowCountCondition3.ResultSet = 4;
            rowCountCondition3.RowCount = 0;
            // 
            // dbo_ClearReviewDescriptionByReviewIDTest_PretestAction
            // 
            resources.ApplyResources(dbo_ClearReviewDescriptionByReviewIDTest_PretestAction, "dbo_ClearReviewDescriptionByReviewIDTest_PretestAction");
            // 
            // testInitializeAction
            // 
            testInitializeAction.Conditions.Add(Test4UsersAdded);
            testInitializeAction.Conditions.Add(Test3ClassesAdded);
            testInitializeAction.Conditions.Add(Test5UserToClassAdded);
            testInitializeAction.Conditions.Add(Test3StickersAdded);
            testInitializeAction.Conditions.Add(Test2ReviewsAdded);
            testInitializeAction.Conditions.Add(Test4OfficialMentorsAdded);
            testInitializeAction.Conditions.Add(Test6OmToUserAdded);
            testInitializeAction.Conditions.Add(Test4ChatsAdded);
            testInitializeAction.Conditions.Add(Test5UserToChatsAdded);
            testInitializeAction.Conditions.Add(Test2FilesAdded);
            resources.ApplyResources(testInitializeAction, "testInitializeAction");
            // 
            // Test4UsersAdded
            // 
            Test4UsersAdded.Enabled = true;
            Test4UsersAdded.Name = "Test4UsersAdded";
            Test4UsersAdded.ResultSet = 1;
            Test4UsersAdded.RowCount = 4;
            // 
            // Test3ClassesAdded
            // 
            Test3ClassesAdded.Enabled = true;
            Test3ClassesAdded.Name = "Test3ClassesAdded";
            Test3ClassesAdded.ResultSet = 2;
            Test3ClassesAdded.RowCount = 3;
            // 
            // Test5UserToClassAdded
            // 
            Test5UserToClassAdded.Enabled = true;
            Test5UserToClassAdded.Name = "Test5UserToClassAdded";
            Test5UserToClassAdded.ResultSet = 3;
            Test5UserToClassAdded.RowCount = 5;
            // 
            // Test3StickersAdded
            // 
            Test3StickersAdded.Enabled = true;
            Test3StickersAdded.Name = "Test3StickersAdded";
            Test3StickersAdded.ResultSet = 4;
            Test3StickersAdded.RowCount = 3;
            // 
            // Test2ReviewsAdded
            // 
            Test2ReviewsAdded.Enabled = true;
            Test2ReviewsAdded.Name = "Test2ReviewsAdded";
            Test2ReviewsAdded.ResultSet = 5;
            Test2ReviewsAdded.RowCount = 2;
            // 
            // Test4OfficialMentorsAdded
            // 
            Test4OfficialMentorsAdded.Enabled = true;
            Test4OfficialMentorsAdded.Name = "Test4OfficialMentorsAdded";
            Test4OfficialMentorsAdded.ResultSet = 6;
            Test4OfficialMentorsAdded.RowCount = 4;
            // 
            // Test6OmToUserAdded
            // 
            Test6OmToUserAdded.Enabled = true;
            Test6OmToUserAdded.Name = "Test6OmToUserAdded";
            Test6OmToUserAdded.ResultSet = 7;
            Test6OmToUserAdded.RowCount = 6;
            // 
            // Test4ChatsAdded
            // 
            Test4ChatsAdded.Enabled = true;
            Test4ChatsAdded.Name = "Test4ChatsAdded";
            Test4ChatsAdded.ResultSet = 8;
            Test4ChatsAdded.RowCount = 4;
            // 
            // Test5UserToChatsAdded
            // 
            Test5UserToChatsAdded.Enabled = true;
            Test5UserToChatsAdded.Name = "Test5UserToChatsAdded";
            Test5UserToChatsAdded.ResultSet = 9;
            Test5UserToChatsAdded.RowCount = 5;
            // 
            // Test2FilesAdded
            // 
            Test2FilesAdded.Enabled = true;
            Test2FilesAdded.Name = "Test2FilesAdded";
            Test2FilesAdded.ResultSet = 10;
            Test2FilesAdded.RowCount = 2;
            // 
            // testCleanupAction
            // 
            testCleanupAction.Conditions.Add(rowCountCondition6);
            resources.ApplyResources(testCleanupAction, "testCleanupAction");
            // 
            // rowCountCondition6
            // 
            rowCountCondition6.Enabled = true;
            rowCountCondition6.Name = "rowCountCondition6";
            rowCountCondition6.ResultSet = 1;
            rowCountCondition6.RowCount = 0;
            // 
            // dbo_AdminPullAllReportsTestData
            // 
            this.dbo_AdminPullAllReportsTestData.PosttestAction = null;
            this.dbo_AdminPullAllReportsTestData.PretestAction = dbo_AdminPullAllReportsTest_PretestAction;
            this.dbo_AdminPullAllReportsTestData.TestAction = dbo_AdminPullAllReportsTest_TestAction;
            // 
            // dbo_ChangeProfilePictureTestData
            // 
            this.dbo_ChangeProfilePictureTestData.PosttestAction = dbo_ChangeProfilePictureTest_PosttestAction;
            this.dbo_ChangeProfilePictureTestData.PretestAction = dbo_ChangeProfilePictureTest_PretestAction;
            this.dbo_ChangeProfilePictureTestData.TestAction = dbo_ChangeProfilePictureTest_TestAction;
            // 
            // dbo_ClearReviewDescriptionByReviewIDTestData
            // 
            this.dbo_ClearReviewDescriptionByReviewIDTestData.PosttestAction = null;
            this.dbo_ClearReviewDescriptionByReviewIDTestData.PretestAction = dbo_ClearReviewDescriptionByReviewIDTest_PretestAction;
            this.dbo_ClearReviewDescriptionByReviewIDTestData.TestAction = dbo_ClearReviewDescriptionByReviewIDTest_TestAction;
            // 
            // dbo_CreateChatTestData
            // 
            this.dbo_CreateChatTestData.PosttestAction = null;
            this.dbo_CreateChatTestData.PretestAction = null;
            this.dbo_CreateChatTestData.TestAction = dbo_CreateChatTest_TestAction;
            // 
            // dbo_CreateNewClassTestData
            // 
            this.dbo_CreateNewClassTestData.PosttestAction = null;
            this.dbo_CreateNewClassTestData.PretestAction = null;
            this.dbo_CreateNewClassTestData.TestAction = dbo_CreateNewClassTest_TestAction;
            // 
            // dbo_CreateNewUserTestData
            // 
            this.dbo_CreateNewUserTestData.PosttestAction = null;
            this.dbo_CreateNewUserTestData.PretestAction = null;
            this.dbo_CreateNewUserTestData.TestAction = dbo_CreateNewUserTest_TestAction;
            // 
            // dbo_CreateOfficialMentorTestData
            // 
            this.dbo_CreateOfficialMentorTestData.PosttestAction = null;
            this.dbo_CreateOfficialMentorTestData.PretestAction = null;
            this.dbo_CreateOfficialMentorTestData.TestAction = dbo_CreateOfficialMentorTest_TestAction;
            // 
            // dbo_CreateReportTestData
            // 
            this.dbo_CreateReportTestData.PosttestAction = null;
            this.dbo_CreateReportTestData.PretestAction = null;
            this.dbo_CreateReportTestData.TestAction = dbo_CreateReportTest_TestAction;
            // 
            // dbo_CreateReviewTestData
            // 
            this.dbo_CreateReviewTestData.PosttestAction = null;
            this.dbo_CreateReviewTestData.PretestAction = null;
            this.dbo_CreateReviewTestData.TestAction = dbo_CreateReviewTest_TestAction;
            // 
            // dbo_CreateServerTestData
            // 
            this.dbo_CreateServerTestData.PosttestAction = null;
            this.dbo_CreateServerTestData.PretestAction = null;
            this.dbo_CreateServerTestData.TestAction = dbo_CreateServerTest_TestAction;
            // 
            // dbo_CreateStickerTestData
            // 
            this.dbo_CreateStickerTestData.PosttestAction = null;
            this.dbo_CreateStickerTestData.PretestAction = null;
            this.dbo_CreateStickerTestData.TestAction = dbo_CreateStickerTest_TestAction;
            // 
            // dbo_DeleteClassByClassIDTestData
            // 
            this.dbo_DeleteClassByClassIDTestData.PosttestAction = null;
            this.dbo_DeleteClassByClassIDTestData.PretestAction = null;
            this.dbo_DeleteClassByClassIDTestData.TestAction = dbo_DeleteClassByClassIDTest_TestAction;
            // 
            // dbo_DeleteFileByFileIDTestData
            // 
            this.dbo_DeleteFileByFileIDTestData.PosttestAction = null;
            this.dbo_DeleteFileByFileIDTestData.PretestAction = null;
            this.dbo_DeleteFileByFileIDTestData.TestAction = dbo_DeleteFileByFileIDTest_TestAction;
            // 
            // dbo_DeleteMentorOrganizationByMentorIDTestData
            // 
            this.dbo_DeleteMentorOrganizationByMentorIDTestData.PosttestAction = null;
            this.dbo_DeleteMentorOrganizationByMentorIDTestData.PretestAction = null;
            this.dbo_DeleteMentorOrganizationByMentorIDTestData.TestAction = dbo_DeleteMentorOrganizationByMentorIDTest_TestAction;
            // 
            // dbo_DeleteMessageByMessageIDTestData
            // 
            this.dbo_DeleteMessageByMessageIDTestData.PosttestAction = null;
            this.dbo_DeleteMessageByMessageIDTestData.PretestAction = null;
            this.dbo_DeleteMessageByMessageIDTestData.TestAction = dbo_DeleteMessageByMessageIDTest_TestAction;
            // 
            // dbo_DeleteReportByReportIDTestData
            // 
            this.dbo_DeleteReportByReportIDTestData.PosttestAction = null;
            this.dbo_DeleteReportByReportIDTestData.PretestAction = null;
            this.dbo_DeleteReportByReportIDTestData.TestAction = dbo_DeleteReportByReportIDTest_TestAction;
            // 
            // dbo_DeleteServerInformationTestData
            // 
            this.dbo_DeleteServerInformationTestData.PosttestAction = null;
            this.dbo_DeleteServerInformationTestData.PretestAction = null;
            this.dbo_DeleteServerInformationTestData.TestAction = dbo_DeleteServerInformationTest_TestAction;
            // 
            // dbo_DeleteServerInformationByServerIDTestData
            // 
            this.dbo_DeleteServerInformationByServerIDTestData.PosttestAction = null;
            this.dbo_DeleteServerInformationByServerIDTestData.PretestAction = null;
            this.dbo_DeleteServerInformationByServerIDTestData.TestAction = dbo_DeleteServerInformationByServerIDTest_TestAction;
            // 
            // dbo_DeleteUserPictureByUserIDTestData
            // 
            this.dbo_DeleteUserPictureByUserIDTestData.PosttestAction = null;
            this.dbo_DeleteUserPictureByUserIDTestData.PretestAction = null;
            this.dbo_DeleteUserPictureByUserIDTestData.TestAction = dbo_DeleteUserPictureByUserIDTest_TestAction;
            // 
            // dbo_DeleteUserProfileByUserIDTestData
            // 
            this.dbo_DeleteUserProfileByUserIDTestData.PosttestAction = null;
            this.dbo_DeleteUserProfileByUserIDTestData.PretestAction = null;
            this.dbo_DeleteUserProfileByUserIDTestData.TestAction = dbo_DeleteUserProfileByUserIDTest_TestAction;
            // 
            // dbo_FilterUserReviewsByEqualStarRankTestData
            // 
            this.dbo_FilterUserReviewsByEqualStarRankTestData.PosttestAction = null;
            this.dbo_FilterUserReviewsByEqualStarRankTestData.PretestAction = null;
            this.dbo_FilterUserReviewsByEqualStarRankTestData.TestAction = dbo_FilterUserReviewsByEqualStarRankTest_TestAction;
            // 
            // dbo_FilterUserReviewsByGreaterThanStarRankTestData
            // 
            this.dbo_FilterUserReviewsByGreaterThanStarRankTestData.PosttestAction = null;
            this.dbo_FilterUserReviewsByGreaterThanStarRankTestData.PretestAction = null;
            this.dbo_FilterUserReviewsByGreaterThanStarRankTestData.TestAction = dbo_FilterUserReviewsByGreaterThanStarRankTest_TestAction;
            // 
            // dbo_GetActiveStickersWithStarRankOrMentorOrganizationTestData
            // 
            this.dbo_GetActiveStickersWithStarRankOrMentorOrganizationTestData.PosttestAction = null;
            this.dbo_GetActiveStickersWithStarRankOrMentorOrganizationTestData.PretestAction = null;
            this.dbo_GetActiveStickersWithStarRankOrMentorOrganizationTestData.TestAction = dbo_GetActiveStickersWithStarRankOrMentorOrganizationTest_TestAction;
            // 
            // dbo_GetAdminInfoTestData
            // 
            this.dbo_GetAdminInfoTestData.PosttestAction = null;
            this.dbo_GetAdminInfoTestData.PretestAction = null;
            this.dbo_GetAdminInfoTestData.TestAction = dbo_GetAdminInfoTest_TestAction;
            // 
            // dbo_GetAllActiveStickersTestData
            // 
            this.dbo_GetAllActiveStickersTestData.PosttestAction = null;
            this.dbo_GetAllActiveStickersTestData.PretestAction = null;
            this.dbo_GetAllActiveStickersTestData.TestAction = dbo_GetAllActiveStickersTest_TestAction;
            // 
            // dbo_GetAllOrganizationsTestData
            // 
            this.dbo_GetAllOrganizationsTestData.PosttestAction = null;
            this.dbo_GetAllOrganizationsTestData.PretestAction = null;
            this.dbo_GetAllOrganizationsTestData.TestAction = dbo_GetAllOrganizationsTest_TestAction;
            // 
            // dbo_GetAllResolvedStickersTestData
            // 
            this.dbo_GetAllResolvedStickersTestData.PosttestAction = null;
            this.dbo_GetAllResolvedStickersTestData.PretestAction = null;
            this.dbo_GetAllResolvedStickersTestData.TestAction = dbo_GetAllResolvedStickersTest_TestAction;
            // 
            // dbo_GetAllStickersTestData
            // 
            this.dbo_GetAllStickersTestData.PosttestAction = null;
            this.dbo_GetAllStickersTestData.PretestAction = null;
            this.dbo_GetAllStickersTestData.TestAction = dbo_GetAllStickersTest_TestAction;
            // 
            // dbo_GetAllStudentReviewsTestData
            // 
            this.dbo_GetAllStudentReviewsTestData.PosttestAction = null;
            this.dbo_GetAllStudentReviewsTestData.PretestAction = null;
            this.dbo_GetAllStudentReviewsTestData.TestAction = dbo_GetAllStudentReviewsTest_TestAction;
            // 
            // dbo_GetDisplayNameAndEmailTestData
            // 
            this.dbo_GetDisplayNameAndEmailTestData.PosttestAction = null;
            this.dbo_GetDisplayNameAndEmailTestData.PretestAction = null;
            this.dbo_GetDisplayNameAndEmailTestData.TestAction = dbo_GetDisplayNameAndEmailTest_TestAction;
            // 
            // dbo_GetEmailCredentialsTestData
            // 
            this.dbo_GetEmailCredentialsTestData.PosttestAction = null;
            this.dbo_GetEmailCredentialsTestData.PretestAction = null;
            this.dbo_GetEmailCredentialsTestData.TestAction = dbo_GetEmailCredentialsTest_TestAction;
            // 
            // dbo_GetProfilePictureTestData
            // 
            this.dbo_GetProfilePictureTestData.PosttestAction = null;
            this.dbo_GetProfilePictureTestData.PretestAction = null;
            this.dbo_GetProfilePictureTestData.TestAction = dbo_GetProfilePictureTest_TestAction;
            // 
            // dbo_GetSchoolNameTestData
            // 
            this.dbo_GetSchoolNameTestData.PosttestAction = null;
            this.dbo_GetSchoolNameTestData.PretestAction = null;
            this.dbo_GetSchoolNameTestData.TestAction = dbo_GetSchoolNameTest_TestAction;
            // 
            // dbo_GetServerDomainTestData
            // 
            this.dbo_GetServerDomainTestData.PosttestAction = null;
            this.dbo_GetServerDomainTestData.PretestAction = null;
            this.dbo_GetServerDomainTestData.TestAction = dbo_GetServerDomainTest_TestAction;
            // 
            // dbo_GetServerIPTestData
            // 
            this.dbo_GetServerIPTestData.PosttestAction = null;
            this.dbo_GetServerIPTestData.PretestAction = null;
            this.dbo_GetServerIPTestData.TestAction = dbo_GetServerIPTest_TestAction;
            // 
            // dbo_GetServerNameTestData
            // 
            this.dbo_GetServerNameTestData.PosttestAction = null;
            this.dbo_GetServerNameTestData.PretestAction = null;
            this.dbo_GetServerNameTestData.TestAction = dbo_GetServerNameTest_TestAction;
            // 
            // dbo_GetUserAvgStudentStarRankTestData
            // 
            this.dbo_GetUserAvgStudentStarRankTestData.PosttestAction = null;
            this.dbo_GetUserAvgStudentStarRankTestData.PretestAction = null;
            this.dbo_GetUserAvgStudentStarRankTestData.TestAction = dbo_GetUserAvgStudentStarRankTest_TestAction;
            // 
            // dbo_GetUserAvgTutorStarRankTestData
            // 
            this.dbo_GetUserAvgTutorStarRankTestData.PosttestAction = null;
            this.dbo_GetUserAvgTutorStarRankTestData.PretestAction = null;
            this.dbo_GetUserAvgTutorStarRankTestData.TestAction = dbo_GetUserAvgTutorStarRankTest_TestAction;
            // 
            // dbo_GetUserClassesTestData
            // 
            this.dbo_GetUserClassesTestData.PosttestAction = null;
            this.dbo_GetUserClassesTestData.PretestAction = null;
            this.dbo_GetUserClassesTestData.TestAction = dbo_GetUserClassesTest_TestAction;
            // 
            // dbo_GetUserOrganizationsTestData
            // 
            this.dbo_GetUserOrganizationsTestData.PosttestAction = null;
            this.dbo_GetUserOrganizationsTestData.PretestAction = null;
            this.dbo_GetUserOrganizationsTestData.TestAction = dbo_GetUserOrganizationsTest_TestAction;
            // 
            // dbo_GetUserStickersAndReviewsTestData
            // 
            this.dbo_GetUserStickersAndReviewsTestData.PosttestAction = null;
            this.dbo_GetUserStickersAndReviewsTestData.PretestAction = null;
            this.dbo_GetUserStickersAndReviewsTestData.TestAction = dbo_GetUserStickersAndReviewsTest_TestAction;
            // 
            // dbo_GetUsersWithOverallStarRankTestData
            // 
            this.dbo_GetUsersWithOverallStarRankTestData.PosttestAction = null;
            this.dbo_GetUsersWithOverallStarRankTestData.PretestAction = null;
            this.dbo_GetUsersWithOverallStarRankTestData.TestAction = dbo_GetUsersWithOverallStarRankTest_TestAction;
            // 
            // dbo_InsertFileTestData
            // 
            this.dbo_InsertFileTestData.PosttestAction = null;
            this.dbo_InsertFileTestData.PretestAction = null;
            this.dbo_InsertFileTestData.TestAction = dbo_InsertFileTest_TestAction;
            // 
            // dbo_InsertMessageTestData
            // 
            this.dbo_InsertMessageTestData.PosttestAction = null;
            this.dbo_InsertMessageTestData.PretestAction = null;
            this.dbo_InsertMessageTestData.TestAction = dbo_InsertMessageTest_TestAction;
            // 
            // dbo_InsertStudentIntoClassTestData
            // 
            this.dbo_InsertStudentIntoClassTestData.PosttestAction = null;
            this.dbo_InsertStudentIntoClassTestData.PretestAction = null;
            this.dbo_InsertStudentIntoClassTestData.TestAction = dbo_InsertStudentIntoClassTest_TestAction;
            // 
            // dbo_InsertUserIntoChatTestData
            // 
            this.dbo_InsertUserIntoChatTestData.PosttestAction = null;
            this.dbo_InsertUserIntoChatTestData.PretestAction = null;
            this.dbo_InsertUserIntoChatTestData.TestAction = dbo_InsertUserIntoChatTest_TestAction;
            // 
            // dbo_InsertUserIntoMentorProgramTestData
            // 
            this.dbo_InsertUserIntoMentorProgramTestData.PosttestAction = null;
            this.dbo_InsertUserIntoMentorProgramTestData.PretestAction = null;
            this.dbo_InsertUserIntoMentorProgramTestData.TestAction = dbo_InsertUserIntoMentorProgramTest_TestAction;
            // 
            // dbo_OpenProfilePageTestData
            // 
            this.dbo_OpenProfilePageTestData.PosttestAction = null;
            this.dbo_OpenProfilePageTestData.PretestAction = null;
            this.dbo_OpenProfilePageTestData.TestAction = dbo_OpenProfilePageTest_TestAction;
            // 
            // dbo_PullActiveClassSpecificStickersTestData
            // 
            this.dbo_PullActiveClassSpecificStickersTestData.PosttestAction = null;
            this.dbo_PullActiveClassSpecificStickersTestData.PretestAction = null;
            this.dbo_PullActiveClassSpecificStickersTestData.TestAction = dbo_PullActiveClassSpecificStickersTest_TestAction;
            // 
            // dbo_PullChatMessagesAndFilesBetweenUsersTestData
            // 
            this.dbo_PullChatMessagesAndFilesBetweenUsersTestData.PosttestAction = null;
            this.dbo_PullChatMessagesAndFilesBetweenUsersTestData.PretestAction = null;
            this.dbo_PullChatMessagesAndFilesBetweenUsersTestData.TestAction = dbo_PullChatMessagesAndFilesBetweenUsersTest_TestAction;
            // 
            // dbo_RetrieveLoginTestData
            // 
            this.dbo_RetrieveLoginTestData.PosttestAction = null;
            this.dbo_RetrieveLoginTestData.PretestAction = null;
            this.dbo_RetrieveLoginTestData.TestAction = dbo_RetrieveLoginTest_TestAction;
            // 
            // dbo_UpdateAdminPasswordByServerIDTestData
            // 
            this.dbo_UpdateAdminPasswordByServerIDTestData.PosttestAction = null;
            this.dbo_UpdateAdminPasswordByServerIDTestData.PretestAction = null;
            this.dbo_UpdateAdminPasswordByServerIDTestData.TestAction = dbo_UpdateAdminPasswordByServerIDTest_TestAction;
            // 
            // dbo_UpdateAdminUsernameByServerIDTestData
            // 
            this.dbo_UpdateAdminUsernameByServerIDTestData.PosttestAction = null;
            this.dbo_UpdateAdminUsernameByServerIDTestData.PretestAction = null;
            this.dbo_UpdateAdminUsernameByServerIDTestData.TestAction = dbo_UpdateAdminUsernameByServerIDTest_TestAction;
            // 
            // dbo_UpdateCourseCodeByClassIDTestData
            // 
            this.dbo_UpdateCourseCodeByClassIDTestData.PosttestAction = null;
            this.dbo_UpdateCourseCodeByClassIDTestData.PretestAction = null;
            this.dbo_UpdateCourseCodeByClassIDTestData.TestAction = dbo_UpdateCourseCodeByClassIDTest_TestAction;
            // 
            // dbo_UpdateCourseNameByClassIDTestData
            // 
            this.dbo_UpdateCourseNameByClassIDTestData.PosttestAction = null;
            this.dbo_UpdateCourseNameByClassIDTestData.PretestAction = null;
            this.dbo_UpdateCourseNameByClassIDTestData.TestAction = dbo_UpdateCourseNameByClassIDTest_TestAction;
            // 
            // dbo_UpdateCourseNumberByClassIDTestData
            // 
            this.dbo_UpdateCourseNumberByClassIDTestData.PosttestAction = null;
            this.dbo_UpdateCourseNumberByClassIDTestData.PretestAction = null;
            this.dbo_UpdateCourseNumberByClassIDTestData.TestAction = dbo_UpdateCourseNumberByClassIDTest_TestAction;
            // 
            // dbo_UpdateDisplayFNameByUserIDTestData
            // 
            this.dbo_UpdateDisplayFNameByUserIDTestData.PosttestAction = null;
            this.dbo_UpdateDisplayFNameByUserIDTestData.PretestAction = null;
            this.dbo_UpdateDisplayFNameByUserIDTestData.TestAction = dbo_UpdateDisplayFNameByUserIDTest_TestAction;
            // 
            // dbo_UpdateDisplayLNameByUserIDTestData
            // 
            this.dbo_UpdateDisplayLNameByUserIDTestData.PosttestAction = null;
            this.dbo_UpdateDisplayLNameByUserIDTestData.PretestAction = null;
            this.dbo_UpdateDisplayLNameByUserIDTestData.TestAction = dbo_UpdateDisplayLNameByUserIDTest_TestAction;
            // 
            // dbo_UpdateEmailAddressByUserIDTestData
            // 
            this.dbo_UpdateEmailAddressByUserIDTestData.PosttestAction = null;
            this.dbo_UpdateEmailAddressByUserIDTestData.PretestAction = null;
            this.dbo_UpdateEmailAddressByUserIDTestData.TestAction = dbo_UpdateEmailAddressByUserIDTest_TestAction;
            // 
            // dbo_UpdateEmailCredentialsByServerIDTestData
            // 
            this.dbo_UpdateEmailCredentialsByServerIDTestData.PosttestAction = null;
            this.dbo_UpdateEmailCredentialsByServerIDTestData.PretestAction = null;
            this.dbo_UpdateEmailCredentialsByServerIDTestData.TestAction = dbo_UpdateEmailCredentialsByServerIDTest_TestAction;
            // 
            // dbo_UpdateMentorNameByMentorIDTestData
            // 
            this.dbo_UpdateMentorNameByMentorIDTestData.PosttestAction = null;
            this.dbo_UpdateMentorNameByMentorIDTestData.PretestAction = null;
            this.dbo_UpdateMentorNameByMentorIDTestData.TestAction = dbo_UpdateMentorNameByMentorIDTest_TestAction;
            // 
            // dbo_UpdateMessageByMessageIDTestData
            // 
            this.dbo_UpdateMessageByMessageIDTestData.PosttestAction = null;
            this.dbo_UpdateMessageByMessageIDTestData.PretestAction = null;
            this.dbo_UpdateMessageByMessageIDTestData.TestAction = dbo_UpdateMessageByMessageIDTest_TestAction;
            // 
            // dbo_UpdatePrivilegesByUserIDTestData
            // 
            this.dbo_UpdatePrivilegesByUserIDTestData.PosttestAction = null;
            this.dbo_UpdatePrivilegesByUserIDTestData.PretestAction = null;
            this.dbo_UpdatePrivilegesByUserIDTestData.TestAction = dbo_UpdatePrivilegesByUserIDTest_TestAction;
            // 
            // dbo_UpdateReviewDescriptionByReviewIDTestData
            // 
            this.dbo_UpdateReviewDescriptionByReviewIDTestData.PosttestAction = null;
            this.dbo_UpdateReviewDescriptionByReviewIDTestData.PretestAction = null;
            this.dbo_UpdateReviewDescriptionByReviewIDTestData.TestAction = dbo_UpdateReviewDescriptionByReviewIDTest_TestAction;
            // 
            // dbo_UpdateSchoolNameByServerIDTestData
            // 
            this.dbo_UpdateSchoolNameByServerIDTestData.PosttestAction = null;
            this.dbo_UpdateSchoolNameByServerIDTestData.PretestAction = null;
            this.dbo_UpdateSchoolNameByServerIDTestData.TestAction = dbo_UpdateSchoolNameByServerIDTest_TestAction;
            // 
            // dbo_UpdateServerDomainByServerIDTestData
            // 
            this.dbo_UpdateServerDomainByServerIDTestData.PosttestAction = null;
            this.dbo_UpdateServerDomainByServerIDTestData.PretestAction = null;
            this.dbo_UpdateServerDomainByServerIDTestData.TestAction = dbo_UpdateServerDomainByServerIDTest_TestAction;
            // 
            // dbo_UpdateServerIPByServerIDTestData
            // 
            this.dbo_UpdateServerIPByServerIDTestData.PosttestAction = null;
            this.dbo_UpdateServerIPByServerIDTestData.PretestAction = null;
            this.dbo_UpdateServerIPByServerIDTestData.TestAction = dbo_UpdateServerIPByServerIDTest_TestAction;
            // 
            // dbo_UpdateServerNameByServerIDTestData
            // 
            this.dbo_UpdateServerNameByServerIDTestData.PosttestAction = null;
            this.dbo_UpdateServerNameByServerIDTestData.PretestAction = null;
            this.dbo_UpdateServerNameByServerIDTestData.TestAction = dbo_UpdateServerNameByServerIDTest_TestAction;
            // 
            // dbo_UpdateStarRankingByReviewIDTestData
            // 
            this.dbo_UpdateStarRankingByReviewIDTestData.PosttestAction = null;
            this.dbo_UpdateStarRankingByReviewIDTestData.PretestAction = null;
            this.dbo_UpdateStarRankingByReviewIDTestData.TestAction = dbo_UpdateStarRankingByReviewIDTest_TestAction;
            // 
            // dbo_UpdateStickerProblemDescriptionByStickerIDTestData
            // 
            this.dbo_UpdateStickerProblemDescriptionByStickerIDTestData.PosttestAction = null;
            this.dbo_UpdateStickerProblemDescriptionByStickerIDTestData.PretestAction = null;
            this.dbo_UpdateStickerProblemDescriptionByStickerIDTestData.TestAction = dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction;
            // 
            // dbo_UpdateTermsOfferedByClassIDTestData
            // 
            this.dbo_UpdateTermsOfferedByClassIDTestData.PosttestAction = null;
            this.dbo_UpdateTermsOfferedByClassIDTestData.PretestAction = null;
            this.dbo_UpdateTermsOfferedByClassIDTestData.TestAction = dbo_UpdateTermsOfferedByClassIDTest_TestAction;
            // 
            // dbo_UpdateTimeoutByStickerIDAndSecondsTestData
            // 
            this.dbo_UpdateTimeoutByStickerIDAndSecondsTestData.PosttestAction = null;
            this.dbo_UpdateTimeoutByStickerIDAndSecondsTestData.PretestAction = null;
            this.dbo_UpdateTimeoutByStickerIDAndSecondsTestData.TestAction = dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction;
            // 
            // dbo_UpdateTutorIDByTutorIDAndStickerIDTestData
            // 
            this.dbo_UpdateTutorIDByTutorIDAndStickerIDTestData.PosttestAction = null;
            this.dbo_UpdateTutorIDByTutorIDAndStickerIDTestData.PretestAction = null;
            this.dbo_UpdateTutorIDByTutorIDAndStickerIDTestData.TestAction = dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction;
            // 
            // dbo_UpdateUserPasswordByUserIDTestData
            // 
            this.dbo_UpdateUserPasswordByUserIDTestData.PosttestAction = null;
            this.dbo_UpdateUserPasswordByUserIDTestData.PretestAction = null;
            this.dbo_UpdateUserPasswordByUserIDTestData.TestAction = dbo_UpdateUserPasswordByUserIDTest_TestAction;
            // 
            // dbo_ViewAllClassesTestData
            // 
            this.dbo_ViewAllClassesTestData.PosttestAction = null;
            this.dbo_ViewAllClassesTestData.PretestAction = null;
            this.dbo_ViewAllClassesTestData.TestAction = dbo_ViewAllClassesTest_TestAction;
            // 
            // dbo_ViewAllUsersTestData
            // 
            this.dbo_ViewAllUsersTestData.PosttestAction = null;
            this.dbo_ViewAllUsersTestData.PretestAction = null;
            this.dbo_ViewAllUsersTestData.TestAction = dbo_ViewAllUsersTest_TestAction;
            // 
            // SqlServerUnitTest1
            // 
            this.TestCleanupAction = testCleanupAction;
            this.TestInitializeAction = testInitializeAction;
        }

        #endregion


        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        #endregion

        [TestMethod()]
        public void dbo_AdminPullAllReportsTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_AdminPullAllReportsTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_ChangeProfilePictureTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_ChangeProfilePictureTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_ClearReviewDescriptionByReviewIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_ClearReviewDescriptionByReviewIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CreateChatTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CreateChatTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CreateNewClassTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CreateNewClassTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CreateNewUserTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CreateNewUserTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CreateOfficialMentorTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CreateOfficialMentorTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CreateReportTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CreateReportTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CreateReviewTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CreateReviewTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CreateServerTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CreateServerTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_CreateStickerTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_CreateStickerTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteClassByClassIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteClassByClassIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteFileByFileIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteFileByFileIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteMentorOrganizationByMentorIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteMentorOrganizationByMentorIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteMessageByMessageIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteMessageByMessageIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteReportByReportIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteReportByReportIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteServerInformationTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteServerInformationTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteServerInformationByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteServerInformationByServerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteUserPictureByUserIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteUserPictureByUserIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_DeleteUserProfileByUserIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteUserProfileByUserIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_FilterUserReviewsByEqualStarRankTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_FilterUserReviewsByEqualStarRankTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_FilterUserReviewsByGreaterThanStarRankTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_FilterUserReviewsByGreaterThanStarRankTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetActiveStickersWithStarRankOrMentorOrganizationTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetActiveStickersWithStarRankOrMentorOrganizationTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetAdminInfoTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetAdminInfoTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetAllActiveStickersTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetAllActiveStickersTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetAllOrganizationsTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetAllOrganizationsTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetAllResolvedStickersTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetAllResolvedStickersTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetAllStickersTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetAllStickersTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetAllStudentReviewsTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetAllStudentReviewsTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetDisplayNameAndEmailTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetDisplayNameAndEmailTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetEmailCredentialsTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetEmailCredentialsTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetProfilePictureTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetProfilePictureTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetSchoolNameTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetSchoolNameTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetServerDomainTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetServerDomainTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetServerIPTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetServerIPTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetServerNameTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetServerNameTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetUserAvgStudentStarRankTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetUserAvgStudentStarRankTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetUserAvgTutorStarRankTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetUserAvgTutorStarRankTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetUserClassesTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetUserClassesTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetUserOrganizationsTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetUserOrganizationsTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetUserStickersAndReviewsTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetUserStickersAndReviewsTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_GetUsersWithOverallStarRankTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_GetUsersWithOverallStarRankTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_InsertFileTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_InsertFileTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_InsertMessageTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_InsertMessageTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_InsertStudentIntoClassTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_InsertStudentIntoClassTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_InsertUserIntoChatTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_InsertUserIntoChatTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_InsertUserIntoMentorProgramTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_InsertUserIntoMentorProgramTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_OpenProfilePageTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_OpenProfilePageTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_PullActiveClassSpecificStickersTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_PullActiveClassSpecificStickersTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_PullChatMessagesAndFilesBetweenUsersTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_PullChatMessagesAndFilesBetweenUsersTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_RetrieveLoginTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_RetrieveLoginTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateAdminPasswordByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateAdminPasswordByServerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateAdminUsernameByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateAdminUsernameByServerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateCourseCodeByClassIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateCourseCodeByClassIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateCourseNameByClassIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateCourseNameByClassIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateCourseNumberByClassIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateCourseNumberByClassIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateDisplayFNameByUserIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateDisplayFNameByUserIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateDisplayLNameByUserIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateDisplayLNameByUserIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateEmailAddressByUserIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateEmailAddressByUserIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateEmailCredentialsByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateEmailCredentialsByServerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateMentorNameByMentorIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateMentorNameByMentorIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateMessageByMessageIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateMessageByMessageIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdatePrivilegesByUserIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdatePrivilegesByUserIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateReviewDescriptionByReviewIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateReviewDescriptionByReviewIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateSchoolNameByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateSchoolNameByServerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateServerDomainByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateServerDomainByServerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateServerIPByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateServerIPByServerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateServerNameByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateServerNameByServerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateStarRankingByReviewIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateStarRankingByReviewIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateStickerProblemDescriptionByStickerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateStickerProblemDescriptionByStickerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateTermsOfferedByClassIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateTermsOfferedByClassIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateTimeoutByStickerIDAndSecondsTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateTimeoutByStickerIDAndSecondsTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateTutorIDByTutorIDAndStickerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateTutorIDByTutorIDAndStickerIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_UpdateUserPasswordByUserIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateUserPasswordByUserIDTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_ViewAllClassesTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_ViewAllClassesTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }

        [TestMethod()]
        public void dbo_ViewAllUsersTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_ViewAllUsersTestData;
            // Execute the pre-test script
            // 
            System.Diagnostics.Trace.WriteLineIf((testActions.PretestAction != null), "Executing pre-test script...");
            SqlExecutionResult[] pretestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PretestAction);
            try
            {
                // Execute the test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.TestAction != null), "Executing test script...");
                SqlExecutionResult[] testResults = TestService.Execute(this.ExecutionContext, this.PrivilegedContext, testActions.TestAction);
            }
            finally
            {
                // Execute the post-test script
                // 
                System.Diagnostics.Trace.WriteLineIf((testActions.PosttestAction != null), "Executing post-test script...");
                SqlExecutionResult[] posttestResults = TestService.Execute(this.PrivilegedContext, this.PrivilegedContext, testActions.PosttestAction);
            }
        }
        private SqlDatabaseTestActions dbo_AdminPullAllReportsTestData;
        private SqlDatabaseTestActions dbo_ChangeProfilePictureTestData;
        private SqlDatabaseTestActions dbo_ClearReviewDescriptionByReviewIDTestData;
        private SqlDatabaseTestActions dbo_CreateChatTestData;
        private SqlDatabaseTestActions dbo_CreateNewClassTestData;
        private SqlDatabaseTestActions dbo_CreateNewUserTestData;
        private SqlDatabaseTestActions dbo_CreateOfficialMentorTestData;
        private SqlDatabaseTestActions dbo_CreateReportTestData;
        private SqlDatabaseTestActions dbo_CreateReviewTestData;
        private SqlDatabaseTestActions dbo_CreateServerTestData;
        private SqlDatabaseTestActions dbo_CreateStickerTestData;
        private SqlDatabaseTestActions dbo_DeleteClassByClassIDTestData;
        private SqlDatabaseTestActions dbo_DeleteFileByFileIDTestData;
        private SqlDatabaseTestActions dbo_DeleteMentorOrganizationByMentorIDTestData;
        private SqlDatabaseTestActions dbo_DeleteMessageByMessageIDTestData;
        private SqlDatabaseTestActions dbo_DeleteReportByReportIDTestData;
        private SqlDatabaseTestActions dbo_DeleteServerInformationTestData;
        private SqlDatabaseTestActions dbo_DeleteServerInformationByServerIDTestData;
        private SqlDatabaseTestActions dbo_DeleteUserPictureByUserIDTestData;
        private SqlDatabaseTestActions dbo_DeleteUserProfileByUserIDTestData;
        private SqlDatabaseTestActions dbo_FilterUserReviewsByEqualStarRankTestData;
        private SqlDatabaseTestActions dbo_FilterUserReviewsByGreaterThanStarRankTestData;
        private SqlDatabaseTestActions dbo_GetActiveStickersWithStarRankOrMentorOrganizationTestData;
        private SqlDatabaseTestActions dbo_GetAdminInfoTestData;
        private SqlDatabaseTestActions dbo_GetAllActiveStickersTestData;
        private SqlDatabaseTestActions dbo_GetAllOrganizationsTestData;
        private SqlDatabaseTestActions dbo_GetAllResolvedStickersTestData;
        private SqlDatabaseTestActions dbo_GetAllStickersTestData;
        private SqlDatabaseTestActions dbo_GetAllStudentReviewsTestData;
        private SqlDatabaseTestActions dbo_GetDisplayNameAndEmailTestData;
        private SqlDatabaseTestActions dbo_GetEmailCredentialsTestData;
        private SqlDatabaseTestActions dbo_GetProfilePictureTestData;
        private SqlDatabaseTestActions dbo_GetSchoolNameTestData;
        private SqlDatabaseTestActions dbo_GetServerDomainTestData;
        private SqlDatabaseTestActions dbo_GetServerIPTestData;
        private SqlDatabaseTestActions dbo_GetServerNameTestData;
        private SqlDatabaseTestActions dbo_GetUserAvgStudentStarRankTestData;
        private SqlDatabaseTestActions dbo_GetUserAvgTutorStarRankTestData;
        private SqlDatabaseTestActions dbo_GetUserClassesTestData;
        private SqlDatabaseTestActions dbo_GetUserOrganizationsTestData;
        private SqlDatabaseTestActions dbo_GetUserStickersAndReviewsTestData;
        private SqlDatabaseTestActions dbo_GetUsersWithOverallStarRankTestData;
        private SqlDatabaseTestActions dbo_InsertFileTestData;
        private SqlDatabaseTestActions dbo_InsertMessageTestData;
        private SqlDatabaseTestActions dbo_InsertStudentIntoClassTestData;
        private SqlDatabaseTestActions dbo_InsertUserIntoChatTestData;
        private SqlDatabaseTestActions dbo_InsertUserIntoMentorProgramTestData;
        private SqlDatabaseTestActions dbo_OpenProfilePageTestData;
        private SqlDatabaseTestActions dbo_PullActiveClassSpecificStickersTestData;
        private SqlDatabaseTestActions dbo_PullChatMessagesAndFilesBetweenUsersTestData;
        private SqlDatabaseTestActions dbo_RetrieveLoginTestData;
        private SqlDatabaseTestActions dbo_UpdateAdminPasswordByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateAdminUsernameByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateCourseCodeByClassIDTestData;
        private SqlDatabaseTestActions dbo_UpdateCourseNameByClassIDTestData;
        private SqlDatabaseTestActions dbo_UpdateCourseNumberByClassIDTestData;
        private SqlDatabaseTestActions dbo_UpdateDisplayFNameByUserIDTestData;
        private SqlDatabaseTestActions dbo_UpdateDisplayLNameByUserIDTestData;
        private SqlDatabaseTestActions dbo_UpdateEmailAddressByUserIDTestData;
        private SqlDatabaseTestActions dbo_UpdateEmailCredentialsByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateMentorNameByMentorIDTestData;
        private SqlDatabaseTestActions dbo_UpdateMessageByMessageIDTestData;
        private SqlDatabaseTestActions dbo_UpdatePrivilegesByUserIDTestData;
        private SqlDatabaseTestActions dbo_UpdateReviewDescriptionByReviewIDTestData;
        private SqlDatabaseTestActions dbo_UpdateSchoolNameByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateServerDomainByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateServerIPByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateServerNameByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateStarRankingByReviewIDTestData;
        private SqlDatabaseTestActions dbo_UpdateStickerProblemDescriptionByStickerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateTermsOfferedByClassIDTestData;
        private SqlDatabaseTestActions dbo_UpdateTimeoutByStickerIDAndSecondsTestData;
        private SqlDatabaseTestActions dbo_UpdateTutorIDByTutorIDAndStickerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateUserPasswordByUserIDTestData;
        private SqlDatabaseTestActions dbo_ViewAllClassesTestData;
        private SqlDatabaseTestActions dbo_ViewAllUsersTestData;
    }
}
