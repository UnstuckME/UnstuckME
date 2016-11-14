using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject1
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ChangeProfilePictureTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerUnitTest1));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition1;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ClearReviewDescriptionByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateNewClassTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateNewUserTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateOfficialMentorTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateReportTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition6;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateReviewTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition7;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateServerTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition8;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_CreateStickerTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition9;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteClassByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition10;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteFileByFileIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition11;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteMentorOrganizationByMentorIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition12;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteMessageByMessageIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition13;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteReportByReportIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition14;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteReviewByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition15;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteServerInformationTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition16;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteServerInformationByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition17;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteStickerByStickerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition18;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteUserPictureByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition19;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteUserProfileByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition20;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_InsertStudentIntoClassTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition21;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_InsertUserIntoMentorProgramTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition22;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateAdminPasswordByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition23;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateAdminUsernameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition24;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseCodeByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition25;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseNameByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition26;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseNumberByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition27;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateDisplayFNameByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition28;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateDisplayLNameByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition29;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateEmailAddressByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition30;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateEmailCredentialsByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition31;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateMentorNameByMentorIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition32;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateMessageByMessageIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition33;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdatePrivilegesByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition34;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateReviewDescriptionByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition35;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateSchoolNameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition36;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerDomainByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition37;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerIPByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition38;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerNameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition39;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateStarRankingByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition40;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition41;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateTermsOfferedByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition42;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition43;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition44;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateUserPasswordByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition45;
            this.dbo_ChangeProfilePictureTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_ClearReviewDescriptionByReviewIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
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
            this.dbo_DeleteReviewByReviewIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteServerInformationTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteServerInformationByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteStickerByStickerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteUserPictureByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_DeleteUserProfileByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_InsertStudentIntoClassTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_InsertUserIntoMentorProgramTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
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
            dbo_ChangeProfilePictureTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_ClearReviewDescriptionByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateNewClassTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateNewUserTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateOfficialMentorTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateReportTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateReviewTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition7 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateServerTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition8 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_CreateStickerTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition9 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteClassByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition10 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteFileByFileIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition11 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteMentorOrganizationByMentorIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition12 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteMessageByMessageIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition13 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteReportByReportIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition14 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteReviewByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition15 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteServerInformationTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition16 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteServerInformationByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition17 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteStickerByStickerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition18 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteUserPictureByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition19 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteUserProfileByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition20 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_InsertStudentIntoClassTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition21 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_InsertUserIntoMentorProgramTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition22 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateAdminPasswordByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition23 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateAdminUsernameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition24 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseCodeByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition25 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseNameByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition26 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseNumberByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition27 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateDisplayFNameByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition28 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateDisplayLNameByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition29 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateEmailAddressByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition30 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateEmailCredentialsByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition31 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateMentorNameByMentorIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition32 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateMessageByMessageIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition33 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdatePrivilegesByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition34 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateReviewDescriptionByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition35 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateSchoolNameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition36 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerDomainByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition37 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerIPByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition38 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerNameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition39 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateStarRankingByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition40 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition41 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateTermsOfferedByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition42 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition43 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition44 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateUserPasswordByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition45 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            // 
            // dbo_ChangeProfilePictureTest_TestAction
            // 
            dbo_ChangeProfilePictureTest_TestAction.Conditions.Add(inconclusiveCondition1);
            resources.ApplyResources(dbo_ChangeProfilePictureTest_TestAction, "dbo_ChangeProfilePictureTest_TestAction");
            // 
            // inconclusiveCondition1
            // 
            inconclusiveCondition1.Enabled = true;
            inconclusiveCondition1.Name = "inconclusiveCondition1";
            // 
            // dbo_ClearReviewDescriptionByReviewIDTest_TestAction
            // 
            dbo_ClearReviewDescriptionByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition2);
            resources.ApplyResources(dbo_ClearReviewDescriptionByReviewIDTest_TestAction, "dbo_ClearReviewDescriptionByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition2
            // 
            inconclusiveCondition2.Enabled = true;
            inconclusiveCondition2.Name = "inconclusiveCondition2";
            // 
            // dbo_CreateNewClassTest_TestAction
            // 
            dbo_CreateNewClassTest_TestAction.Conditions.Add(inconclusiveCondition3);
            resources.ApplyResources(dbo_CreateNewClassTest_TestAction, "dbo_CreateNewClassTest_TestAction");
            // 
            // inconclusiveCondition3
            // 
            inconclusiveCondition3.Enabled = true;
            inconclusiveCondition3.Name = "inconclusiveCondition3";
            // 
            // dbo_CreateNewUserTest_TestAction
            // 
            dbo_CreateNewUserTest_TestAction.Conditions.Add(inconclusiveCondition4);
            resources.ApplyResources(dbo_CreateNewUserTest_TestAction, "dbo_CreateNewUserTest_TestAction");
            // 
            // inconclusiveCondition4
            // 
            inconclusiveCondition4.Enabled = true;
            inconclusiveCondition4.Name = "inconclusiveCondition4";
            // 
            // dbo_CreateOfficialMentorTest_TestAction
            // 
            dbo_CreateOfficialMentorTest_TestAction.Conditions.Add(inconclusiveCondition5);
            resources.ApplyResources(dbo_CreateOfficialMentorTest_TestAction, "dbo_CreateOfficialMentorTest_TestAction");
            // 
            // inconclusiveCondition5
            // 
            inconclusiveCondition5.Enabled = true;
            inconclusiveCondition5.Name = "inconclusiveCondition5";
            // 
            // dbo_CreateReportTest_TestAction
            // 
            dbo_CreateReportTest_TestAction.Conditions.Add(inconclusiveCondition6);
            resources.ApplyResources(dbo_CreateReportTest_TestAction, "dbo_CreateReportTest_TestAction");
            // 
            // inconclusiveCondition6
            // 
            inconclusiveCondition6.Enabled = true;
            inconclusiveCondition6.Name = "inconclusiveCondition6";
            // 
            // dbo_CreateReviewTest_TestAction
            // 
            dbo_CreateReviewTest_TestAction.Conditions.Add(inconclusiveCondition7);
            resources.ApplyResources(dbo_CreateReviewTest_TestAction, "dbo_CreateReviewTest_TestAction");
            // 
            // inconclusiveCondition7
            // 
            inconclusiveCondition7.Enabled = true;
            inconclusiveCondition7.Name = "inconclusiveCondition7";
            // 
            // dbo_CreateServerTest_TestAction
            // 
            dbo_CreateServerTest_TestAction.Conditions.Add(inconclusiveCondition8);
            resources.ApplyResources(dbo_CreateServerTest_TestAction, "dbo_CreateServerTest_TestAction");
            // 
            // inconclusiveCondition8
            // 
            inconclusiveCondition8.Enabled = true;
            inconclusiveCondition8.Name = "inconclusiveCondition8";
            // 
            // dbo_CreateStickerTest_TestAction
            // 
            dbo_CreateStickerTest_TestAction.Conditions.Add(inconclusiveCondition9);
            resources.ApplyResources(dbo_CreateStickerTest_TestAction, "dbo_CreateStickerTest_TestAction");
            // 
            // inconclusiveCondition9
            // 
            inconclusiveCondition9.Enabled = true;
            inconclusiveCondition9.Name = "inconclusiveCondition9";
            // 
            // dbo_DeleteClassByClassIDTest_TestAction
            // 
            dbo_DeleteClassByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition10);
            resources.ApplyResources(dbo_DeleteClassByClassIDTest_TestAction, "dbo_DeleteClassByClassIDTest_TestAction");
            // 
            // inconclusiveCondition10
            // 
            inconclusiveCondition10.Enabled = true;
            inconclusiveCondition10.Name = "inconclusiveCondition10";
            // 
            // dbo_DeleteFileByFileIDTest_TestAction
            // 
            dbo_DeleteFileByFileIDTest_TestAction.Conditions.Add(inconclusiveCondition11);
            resources.ApplyResources(dbo_DeleteFileByFileIDTest_TestAction, "dbo_DeleteFileByFileIDTest_TestAction");
            // 
            // inconclusiveCondition11
            // 
            inconclusiveCondition11.Enabled = true;
            inconclusiveCondition11.Name = "inconclusiveCondition11";
            // 
            // dbo_DeleteMentorOrganizationByMentorIDTest_TestAction
            // 
            dbo_DeleteMentorOrganizationByMentorIDTest_TestAction.Conditions.Add(inconclusiveCondition12);
            resources.ApplyResources(dbo_DeleteMentorOrganizationByMentorIDTest_TestAction, "dbo_DeleteMentorOrganizationByMentorIDTest_TestAction");
            // 
            // inconclusiveCondition12
            // 
            inconclusiveCondition12.Enabled = true;
            inconclusiveCondition12.Name = "inconclusiveCondition12";
            // 
            // dbo_DeleteMessageByMessageIDTest_TestAction
            // 
            dbo_DeleteMessageByMessageIDTest_TestAction.Conditions.Add(inconclusiveCondition13);
            resources.ApplyResources(dbo_DeleteMessageByMessageIDTest_TestAction, "dbo_DeleteMessageByMessageIDTest_TestAction");
            // 
            // inconclusiveCondition13
            // 
            inconclusiveCondition13.Enabled = true;
            inconclusiveCondition13.Name = "inconclusiveCondition13";
            // 
            // dbo_DeleteReportByReportIDTest_TestAction
            // 
            dbo_DeleteReportByReportIDTest_TestAction.Conditions.Add(inconclusiveCondition14);
            resources.ApplyResources(dbo_DeleteReportByReportIDTest_TestAction, "dbo_DeleteReportByReportIDTest_TestAction");
            // 
            // inconclusiveCondition14
            // 
            inconclusiveCondition14.Enabled = true;
            inconclusiveCondition14.Name = "inconclusiveCondition14";
            // 
            // dbo_DeleteReviewByReviewIDTest_TestAction
            // 
            dbo_DeleteReviewByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition15);
            resources.ApplyResources(dbo_DeleteReviewByReviewIDTest_TestAction, "dbo_DeleteReviewByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition15
            // 
            inconclusiveCondition15.Enabled = true;
            inconclusiveCondition15.Name = "inconclusiveCondition15";
            // 
            // dbo_DeleteServerInformationTest_TestAction
            // 
            dbo_DeleteServerInformationTest_TestAction.Conditions.Add(inconclusiveCondition16);
            resources.ApplyResources(dbo_DeleteServerInformationTest_TestAction, "dbo_DeleteServerInformationTest_TestAction");
            // 
            // inconclusiveCondition16
            // 
            inconclusiveCondition16.Enabled = true;
            inconclusiveCondition16.Name = "inconclusiveCondition16";
            // 
            // dbo_DeleteServerInformationByServerIDTest_TestAction
            // 
            dbo_DeleteServerInformationByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition17);
            resources.ApplyResources(dbo_DeleteServerInformationByServerIDTest_TestAction, "dbo_DeleteServerInformationByServerIDTest_TestAction");
            // 
            // inconclusiveCondition17
            // 
            inconclusiveCondition17.Enabled = true;
            inconclusiveCondition17.Name = "inconclusiveCondition17";
            // 
            // dbo_DeleteStickerByStickerIDTest_TestAction
            // 
            dbo_DeleteStickerByStickerIDTest_TestAction.Conditions.Add(inconclusiveCondition18);
            resources.ApplyResources(dbo_DeleteStickerByStickerIDTest_TestAction, "dbo_DeleteStickerByStickerIDTest_TestAction");
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
            // dbo_InsertStudentIntoClassTest_TestAction
            // 
            dbo_InsertStudentIntoClassTest_TestAction.Conditions.Add(inconclusiveCondition21);
            resources.ApplyResources(dbo_InsertStudentIntoClassTest_TestAction, "dbo_InsertStudentIntoClassTest_TestAction");
            // 
            // inconclusiveCondition21
            // 
            inconclusiveCondition21.Enabled = true;
            inconclusiveCondition21.Name = "inconclusiveCondition21";
            // 
            // dbo_InsertUserIntoMentorProgramTest_TestAction
            // 
            dbo_InsertUserIntoMentorProgramTest_TestAction.Conditions.Add(inconclusiveCondition22);
            resources.ApplyResources(dbo_InsertUserIntoMentorProgramTest_TestAction, "dbo_InsertUserIntoMentorProgramTest_TestAction");
            // 
            // inconclusiveCondition22
            // 
            inconclusiveCondition22.Enabled = true;
            inconclusiveCondition22.Name = "inconclusiveCondition22";
            // 
            // dbo_UpdateAdminPasswordByServerIDTest_TestAction
            // 
            dbo_UpdateAdminPasswordByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition23);
            resources.ApplyResources(dbo_UpdateAdminPasswordByServerIDTest_TestAction, "dbo_UpdateAdminPasswordByServerIDTest_TestAction");
            // 
            // inconclusiveCondition23
            // 
            inconclusiveCondition23.Enabled = true;
            inconclusiveCondition23.Name = "inconclusiveCondition23";
            // 
            // dbo_UpdateAdminUsernameByServerIDTest_TestAction
            // 
            dbo_UpdateAdminUsernameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition24);
            resources.ApplyResources(dbo_UpdateAdminUsernameByServerIDTest_TestAction, "dbo_UpdateAdminUsernameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition24
            // 
            inconclusiveCondition24.Enabled = true;
            inconclusiveCondition24.Name = "inconclusiveCondition24";
            // 
            // dbo_UpdateCourseCodeByClassIDTest_TestAction
            // 
            dbo_UpdateCourseCodeByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition25);
            resources.ApplyResources(dbo_UpdateCourseCodeByClassIDTest_TestAction, "dbo_UpdateCourseCodeByClassIDTest_TestAction");
            // 
            // inconclusiveCondition25
            // 
            inconclusiveCondition25.Enabled = true;
            inconclusiveCondition25.Name = "inconclusiveCondition25";
            // 
            // dbo_UpdateCourseNameByClassIDTest_TestAction
            // 
            dbo_UpdateCourseNameByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition26);
            resources.ApplyResources(dbo_UpdateCourseNameByClassIDTest_TestAction, "dbo_UpdateCourseNameByClassIDTest_TestAction");
            // 
            // inconclusiveCondition26
            // 
            inconclusiveCondition26.Enabled = true;
            inconclusiveCondition26.Name = "inconclusiveCondition26";
            // 
            // dbo_UpdateCourseNumberByClassIDTest_TestAction
            // 
            dbo_UpdateCourseNumberByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition27);
            resources.ApplyResources(dbo_UpdateCourseNumberByClassIDTest_TestAction, "dbo_UpdateCourseNumberByClassIDTest_TestAction");
            // 
            // inconclusiveCondition27
            // 
            inconclusiveCondition27.Enabled = true;
            inconclusiveCondition27.Name = "inconclusiveCondition27";
            // 
            // dbo_UpdateDisplayFNameByUserIDTest_TestAction
            // 
            dbo_UpdateDisplayFNameByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition28);
            resources.ApplyResources(dbo_UpdateDisplayFNameByUserIDTest_TestAction, "dbo_UpdateDisplayFNameByUserIDTest_TestAction");
            // 
            // inconclusiveCondition28
            // 
            inconclusiveCondition28.Enabled = true;
            inconclusiveCondition28.Name = "inconclusiveCondition28";
            // 
            // dbo_UpdateDisplayLNameByUserIDTest_TestAction
            // 
            dbo_UpdateDisplayLNameByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition29);
            resources.ApplyResources(dbo_UpdateDisplayLNameByUserIDTest_TestAction, "dbo_UpdateDisplayLNameByUserIDTest_TestAction");
            // 
            // inconclusiveCondition29
            // 
            inconclusiveCondition29.Enabled = true;
            inconclusiveCondition29.Name = "inconclusiveCondition29";
            // 
            // dbo_UpdateEmailAddressByUserIDTest_TestAction
            // 
            dbo_UpdateEmailAddressByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition30);
            resources.ApplyResources(dbo_UpdateEmailAddressByUserIDTest_TestAction, "dbo_UpdateEmailAddressByUserIDTest_TestAction");
            // 
            // inconclusiveCondition30
            // 
            inconclusiveCondition30.Enabled = true;
            inconclusiveCondition30.Name = "inconclusiveCondition30";
            // 
            // dbo_UpdateEmailCredentialsByServerIDTest_TestAction
            // 
            dbo_UpdateEmailCredentialsByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition31);
            resources.ApplyResources(dbo_UpdateEmailCredentialsByServerIDTest_TestAction, "dbo_UpdateEmailCredentialsByServerIDTest_TestAction");
            // 
            // inconclusiveCondition31
            // 
            inconclusiveCondition31.Enabled = true;
            inconclusiveCondition31.Name = "inconclusiveCondition31";
            // 
            // dbo_UpdateMentorNameByMentorIDTest_TestAction
            // 
            dbo_UpdateMentorNameByMentorIDTest_TestAction.Conditions.Add(inconclusiveCondition32);
            resources.ApplyResources(dbo_UpdateMentorNameByMentorIDTest_TestAction, "dbo_UpdateMentorNameByMentorIDTest_TestAction");
            // 
            // inconclusiveCondition32
            // 
            inconclusiveCondition32.Enabled = true;
            inconclusiveCondition32.Name = "inconclusiveCondition32";
            // 
            // dbo_UpdateMessageByMessageIDTest_TestAction
            // 
            dbo_UpdateMessageByMessageIDTest_TestAction.Conditions.Add(inconclusiveCondition33);
            resources.ApplyResources(dbo_UpdateMessageByMessageIDTest_TestAction, "dbo_UpdateMessageByMessageIDTest_TestAction");
            // 
            // inconclusiveCondition33
            // 
            inconclusiveCondition33.Enabled = true;
            inconclusiveCondition33.Name = "inconclusiveCondition33";
            // 
            // dbo_UpdatePrivilegesByUserIDTest_TestAction
            // 
            dbo_UpdatePrivilegesByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition34);
            resources.ApplyResources(dbo_UpdatePrivilegesByUserIDTest_TestAction, "dbo_UpdatePrivilegesByUserIDTest_TestAction");
            // 
            // inconclusiveCondition34
            // 
            inconclusiveCondition34.Enabled = true;
            inconclusiveCondition34.Name = "inconclusiveCondition34";
            // 
            // dbo_UpdateReviewDescriptionByReviewIDTest_TestAction
            // 
            dbo_UpdateReviewDescriptionByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition35);
            resources.ApplyResources(dbo_UpdateReviewDescriptionByReviewIDTest_TestAction, "dbo_UpdateReviewDescriptionByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition35
            // 
            inconclusiveCondition35.Enabled = true;
            inconclusiveCondition35.Name = "inconclusiveCondition35";
            // 
            // dbo_UpdateSchoolNameByServerIDTest_TestAction
            // 
            dbo_UpdateSchoolNameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition36);
            resources.ApplyResources(dbo_UpdateSchoolNameByServerIDTest_TestAction, "dbo_UpdateSchoolNameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition36
            // 
            inconclusiveCondition36.Enabled = true;
            inconclusiveCondition36.Name = "inconclusiveCondition36";
            // 
            // dbo_UpdateServerDomainByServerIDTest_TestAction
            // 
            dbo_UpdateServerDomainByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition37);
            resources.ApplyResources(dbo_UpdateServerDomainByServerIDTest_TestAction, "dbo_UpdateServerDomainByServerIDTest_TestAction");
            // 
            // inconclusiveCondition37
            // 
            inconclusiveCondition37.Enabled = true;
            inconclusiveCondition37.Name = "inconclusiveCondition37";
            // 
            // dbo_UpdateServerIPByServerIDTest_TestAction
            // 
            dbo_UpdateServerIPByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition38);
            resources.ApplyResources(dbo_UpdateServerIPByServerIDTest_TestAction, "dbo_UpdateServerIPByServerIDTest_TestAction");
            // 
            // inconclusiveCondition38
            // 
            inconclusiveCondition38.Enabled = true;
            inconclusiveCondition38.Name = "inconclusiveCondition38";
            // 
            // dbo_UpdateServerNameByServerIDTest_TestAction
            // 
            dbo_UpdateServerNameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition39);
            resources.ApplyResources(dbo_UpdateServerNameByServerIDTest_TestAction, "dbo_UpdateServerNameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition39
            // 
            inconclusiveCondition39.Enabled = true;
            inconclusiveCondition39.Name = "inconclusiveCondition39";
            // 
            // dbo_UpdateStarRankingByReviewIDTest_TestAction
            // 
            dbo_UpdateStarRankingByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition40);
            resources.ApplyResources(dbo_UpdateStarRankingByReviewIDTest_TestAction, "dbo_UpdateStarRankingByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition40
            // 
            inconclusiveCondition40.Enabled = true;
            inconclusiveCondition40.Name = "inconclusiveCondition40";
            // 
            // dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction
            // 
            dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction.Conditions.Add(inconclusiveCondition41);
            resources.ApplyResources(dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction, "dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction");
            // 
            // inconclusiveCondition41
            // 
            inconclusiveCondition41.Enabled = true;
            inconclusiveCondition41.Name = "inconclusiveCondition41";
            // 
            // dbo_UpdateTermsOfferedByClassIDTest_TestAction
            // 
            dbo_UpdateTermsOfferedByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition42);
            resources.ApplyResources(dbo_UpdateTermsOfferedByClassIDTest_TestAction, "dbo_UpdateTermsOfferedByClassIDTest_TestAction");
            // 
            // inconclusiveCondition42
            // 
            inconclusiveCondition42.Enabled = true;
            inconclusiveCondition42.Name = "inconclusiveCondition42";
            // 
            // dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction
            // 
            dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction.Conditions.Add(inconclusiveCondition43);
            resources.ApplyResources(dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction, "dbo_UpdateTimeoutByStickerIDAndSecondsTest_TestAction");
            // 
            // inconclusiveCondition43
            // 
            inconclusiveCondition43.Enabled = true;
            inconclusiveCondition43.Name = "inconclusiveCondition43";
            // 
            // dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction
            // 
            dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction.Conditions.Add(inconclusiveCondition44);
            resources.ApplyResources(dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction, "dbo_UpdateTutorIDByTutorIDAndStickerIDTest_TestAction");
            // 
            // inconclusiveCondition44
            // 
            inconclusiveCondition44.Enabled = true;
            inconclusiveCondition44.Name = "inconclusiveCondition44";
            // 
            // dbo_UpdateUserPasswordByUserIDTest_TestAction
            // 
            dbo_UpdateUserPasswordByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition45);
            resources.ApplyResources(dbo_UpdateUserPasswordByUserIDTest_TestAction, "dbo_UpdateUserPasswordByUserIDTest_TestAction");
            // 
            // inconclusiveCondition45
            // 
            inconclusiveCondition45.Enabled = true;
            inconclusiveCondition45.Name = "inconclusiveCondition45";
            // 
            // dbo_ChangeProfilePictureTestData
            // 
            this.dbo_ChangeProfilePictureTestData.PosttestAction = null;
            this.dbo_ChangeProfilePictureTestData.PretestAction = null;
            this.dbo_ChangeProfilePictureTestData.TestAction = dbo_ChangeProfilePictureTest_TestAction;
            // 
            // dbo_ClearReviewDescriptionByReviewIDTestData
            // 
            this.dbo_ClearReviewDescriptionByReviewIDTestData.PosttestAction = null;
            this.dbo_ClearReviewDescriptionByReviewIDTestData.PretestAction = null;
            this.dbo_ClearReviewDescriptionByReviewIDTestData.TestAction = dbo_ClearReviewDescriptionByReviewIDTest_TestAction;
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
            // dbo_DeleteReviewByReviewIDTestData
            // 
            this.dbo_DeleteReviewByReviewIDTestData.PosttestAction = null;
            this.dbo_DeleteReviewByReviewIDTestData.PretestAction = null;
            this.dbo_DeleteReviewByReviewIDTestData.TestAction = dbo_DeleteReviewByReviewIDTest_TestAction;
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
            // dbo_DeleteStickerByStickerIDTestData
            // 
            this.dbo_DeleteStickerByStickerIDTestData.PosttestAction = null;
            this.dbo_DeleteStickerByStickerIDTestData.PretestAction = null;
            this.dbo_DeleteStickerByStickerIDTestData.TestAction = dbo_DeleteStickerByStickerIDTest_TestAction;
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
            // dbo_InsertStudentIntoClassTestData
            // 
            this.dbo_InsertStudentIntoClassTestData.PosttestAction = null;
            this.dbo_InsertStudentIntoClassTestData.PretestAction = null;
            this.dbo_InsertStudentIntoClassTestData.TestAction = dbo_InsertStudentIntoClassTest_TestAction;
            // 
            // dbo_InsertUserIntoMentorProgramTestData
            // 
            this.dbo_InsertUserIntoMentorProgramTestData.PosttestAction = null;
            this.dbo_InsertUserIntoMentorProgramTestData.PretestAction = null;
            this.dbo_InsertUserIntoMentorProgramTestData.TestAction = dbo_InsertUserIntoMentorProgramTest_TestAction;
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
        public void dbo_DeleteReviewByReviewIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteReviewByReviewIDTestData;
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
        public void dbo_DeleteStickerByStickerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_DeleteStickerByStickerIDTestData;
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
        private SqlDatabaseTestActions dbo_ChangeProfilePictureTestData;
        private SqlDatabaseTestActions dbo_ClearReviewDescriptionByReviewIDTestData;
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
        private SqlDatabaseTestActions dbo_DeleteReviewByReviewIDTestData;
        private SqlDatabaseTestActions dbo_DeleteServerInformationTestData;
        private SqlDatabaseTestActions dbo_DeleteServerInformationByServerIDTestData;
        private SqlDatabaseTestActions dbo_DeleteStickerByStickerIDTestData;
        private SqlDatabaseTestActions dbo_DeleteUserPictureByUserIDTestData;
        private SqlDatabaseTestActions dbo_DeleteUserProfileByUserIDTestData;
        private SqlDatabaseTestActions dbo_InsertStudentIntoClassTestData;
        private SqlDatabaseTestActions dbo_InsertUserIntoMentorProgramTestData;
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
    }
}
