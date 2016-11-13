using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting;
using Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestStoredProcedures
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
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_ClearReviewDescriptionByReviewIDTest_TestAction;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SqlServerUnitTest1));
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteClassByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition2;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteFileByFileIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition3;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteMentorOrganizationByMentorIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition4;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteMessageByMessageIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition5;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteReportByReportIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition6;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteReviewByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition7;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteServerInformationTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition8;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteServerInformationByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition9;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteStickerByStickerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition10;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteUserPictureByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition11;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_DeleteUserProfileByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition12;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_MentorNameByMentorIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition13;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateAdminPasswordByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition14;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateAdminUsernameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition15;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseCodeByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition16;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseNameByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition17;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateCourseNumberByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition18;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateDisplayFNameByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition19;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateDisplayLNameByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition20;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateEmailAddressByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition21;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateEmailCredintialsByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition22;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateMessageByMessageIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition23;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdatePrivilegesByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition24;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateReviewDescriptionByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition25;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateSchoolNameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition26;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerDomainByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition27;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerIPByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition28;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateServerNameByServerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition29;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateStarRankingByReviewIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition30;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition31;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateTermsOfferedByClassIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition32;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateTimeoutByStickerIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition33;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction dbo_UpdateUserPasswordByUserIDTest_TestAction;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition inconclusiveCondition34;
            Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition emptyResultSetCondition1;
            this.dbo_ClearReviewDescriptionByReviewIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
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
            this.dbo_MentorNameByMentorIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateAdminPasswordByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateAdminUsernameByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateCourseCodeByClassIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateCourseNameByClassIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateCourseNumberByClassIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateDisplayFNameByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateDisplayLNameByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateEmailAddressByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateEmailCredintialsByServerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
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
            this.dbo_UpdateTimeoutByStickerIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            this.dbo_UpdateUserPasswordByUserIDTestData = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestActions();
            dbo_ClearReviewDescriptionByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            dbo_DeleteClassByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition2 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteFileByFileIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition3 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteMentorOrganizationByMentorIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition4 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteMessageByMessageIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition5 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteReportByReportIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition6 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteReviewByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition7 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteServerInformationTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition8 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteServerInformationByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition9 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteStickerByStickerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition10 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteUserPictureByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition11 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_DeleteUserProfileByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition12 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_MentorNameByMentorIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition13 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateAdminPasswordByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition14 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateAdminUsernameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition15 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseCodeByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition16 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseNameByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition17 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateCourseNumberByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition18 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateDisplayFNameByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition19 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateDisplayLNameByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition20 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateEmailAddressByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition21 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateEmailCredintialsByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition22 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateMessageByMessageIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition23 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdatePrivilegesByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition24 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateReviewDescriptionByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition25 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateSchoolNameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition26 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerDomainByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition27 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerIPByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition28 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateServerNameByServerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition29 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateStarRankingByReviewIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition30 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition31 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateTermsOfferedByClassIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition32 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateTimeoutByStickerIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition33 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            dbo_UpdateUserPasswordByUserIDTest_TestAction = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.SqlDatabaseTestAction();
            inconclusiveCondition34 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.InconclusiveCondition();
            emptyResultSetCondition1 = new Microsoft.Data.Tools.Schema.Sql.UnitTesting.Conditions.EmptyResultSetCondition();
            // 
            // dbo_ClearReviewDescriptionByReviewIDTest_TestAction
            // 
            dbo_ClearReviewDescriptionByReviewIDTest_TestAction.Conditions.Add(emptyResultSetCondition1);
            resources.ApplyResources(dbo_ClearReviewDescriptionByReviewIDTest_TestAction, "dbo_ClearReviewDescriptionByReviewIDTest_TestAction");
            // 
            // dbo_DeleteClassByClassIDTest_TestAction
            // 
            dbo_DeleteClassByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition2);
            resources.ApplyResources(dbo_DeleteClassByClassIDTest_TestAction, "dbo_DeleteClassByClassIDTest_TestAction");
            // 
            // inconclusiveCondition2
            // 
            inconclusiveCondition2.Enabled = true;
            inconclusiveCondition2.Name = "inconclusiveCondition2";
            // 
            // dbo_DeleteFileByFileIDTest_TestAction
            // 
            dbo_DeleteFileByFileIDTest_TestAction.Conditions.Add(inconclusiveCondition3);
            resources.ApplyResources(dbo_DeleteFileByFileIDTest_TestAction, "dbo_DeleteFileByFileIDTest_TestAction");
            // 
            // inconclusiveCondition3
            // 
            inconclusiveCondition3.Enabled = true;
            inconclusiveCondition3.Name = "inconclusiveCondition3";
            // 
            // dbo_DeleteMentorOrganizationByMentorIDTest_TestAction
            // 
            dbo_DeleteMentorOrganizationByMentorIDTest_TestAction.Conditions.Add(inconclusiveCondition4);
            resources.ApplyResources(dbo_DeleteMentorOrganizationByMentorIDTest_TestAction, "dbo_DeleteMentorOrganizationByMentorIDTest_TestAction");
            // 
            // inconclusiveCondition4
            // 
            inconclusiveCondition4.Enabled = true;
            inconclusiveCondition4.Name = "inconclusiveCondition4";
            // 
            // dbo_DeleteMessageByMessageIDTest_TestAction
            // 
            dbo_DeleteMessageByMessageIDTest_TestAction.Conditions.Add(inconclusiveCondition5);
            resources.ApplyResources(dbo_DeleteMessageByMessageIDTest_TestAction, "dbo_DeleteMessageByMessageIDTest_TestAction");
            // 
            // inconclusiveCondition5
            // 
            inconclusiveCondition5.Enabled = true;
            inconclusiveCondition5.Name = "inconclusiveCondition5";
            // 
            // dbo_DeleteReportByReportIDTest_TestAction
            // 
            dbo_DeleteReportByReportIDTest_TestAction.Conditions.Add(inconclusiveCondition6);
            resources.ApplyResources(dbo_DeleteReportByReportIDTest_TestAction, "dbo_DeleteReportByReportIDTest_TestAction");
            // 
            // inconclusiveCondition6
            // 
            inconclusiveCondition6.Enabled = true;
            inconclusiveCondition6.Name = "inconclusiveCondition6";
            // 
            // dbo_DeleteReviewByReviewIDTest_TestAction
            // 
            dbo_DeleteReviewByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition7);
            resources.ApplyResources(dbo_DeleteReviewByReviewIDTest_TestAction, "dbo_DeleteReviewByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition7
            // 
            inconclusiveCondition7.Enabled = true;
            inconclusiveCondition7.Name = "inconclusiveCondition7";
            // 
            // dbo_DeleteServerInformationTest_TestAction
            // 
            dbo_DeleteServerInformationTest_TestAction.Conditions.Add(inconclusiveCondition8);
            resources.ApplyResources(dbo_DeleteServerInformationTest_TestAction, "dbo_DeleteServerInformationTest_TestAction");
            // 
            // inconclusiveCondition8
            // 
            inconclusiveCondition8.Enabled = true;
            inconclusiveCondition8.Name = "inconclusiveCondition8";
            // 
            // dbo_DeleteServerInformationByServerIDTest_TestAction
            // 
            dbo_DeleteServerInformationByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition9);
            resources.ApplyResources(dbo_DeleteServerInformationByServerIDTest_TestAction, "dbo_DeleteServerInformationByServerIDTest_TestAction");
            // 
            // inconclusiveCondition9
            // 
            inconclusiveCondition9.Enabled = true;
            inconclusiveCondition9.Name = "inconclusiveCondition9";
            // 
            // dbo_DeleteStickerByStickerIDTest_TestAction
            // 
            dbo_DeleteStickerByStickerIDTest_TestAction.Conditions.Add(inconclusiveCondition10);
            resources.ApplyResources(dbo_DeleteStickerByStickerIDTest_TestAction, "dbo_DeleteStickerByStickerIDTest_TestAction");
            // 
            // inconclusiveCondition10
            // 
            inconclusiveCondition10.Enabled = true;
            inconclusiveCondition10.Name = "inconclusiveCondition10";
            // 
            // dbo_DeleteUserPictureByUserIDTest_TestAction
            // 
            dbo_DeleteUserPictureByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition11);
            resources.ApplyResources(dbo_DeleteUserPictureByUserIDTest_TestAction, "dbo_DeleteUserPictureByUserIDTest_TestAction");
            // 
            // inconclusiveCondition11
            // 
            inconclusiveCondition11.Enabled = true;
            inconclusiveCondition11.Name = "inconclusiveCondition11";
            // 
            // dbo_DeleteUserProfileByUserIDTest_TestAction
            // 
            dbo_DeleteUserProfileByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition12);
            resources.ApplyResources(dbo_DeleteUserProfileByUserIDTest_TestAction, "dbo_DeleteUserProfileByUserIDTest_TestAction");
            // 
            // inconclusiveCondition12
            // 
            inconclusiveCondition12.Enabled = true;
            inconclusiveCondition12.Name = "inconclusiveCondition12";
            // 
            // dbo_MentorNameByMentorIDTest_TestAction
            // 
            dbo_MentorNameByMentorIDTest_TestAction.Conditions.Add(inconclusiveCondition13);
            resources.ApplyResources(dbo_MentorNameByMentorIDTest_TestAction, "dbo_MentorNameByMentorIDTest_TestAction");
            // 
            // inconclusiveCondition13
            // 
            inconclusiveCondition13.Enabled = true;
            inconclusiveCondition13.Name = "inconclusiveCondition13";
            // 
            // dbo_UpdateAdminPasswordByServerIDTest_TestAction
            // 
            dbo_UpdateAdminPasswordByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition14);
            resources.ApplyResources(dbo_UpdateAdminPasswordByServerIDTest_TestAction, "dbo_UpdateAdminPasswordByServerIDTest_TestAction");
            // 
            // inconclusiveCondition14
            // 
            inconclusiveCondition14.Enabled = true;
            inconclusiveCondition14.Name = "inconclusiveCondition14";
            // 
            // dbo_UpdateAdminUsernameByServerIDTest_TestAction
            // 
            dbo_UpdateAdminUsernameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition15);
            resources.ApplyResources(dbo_UpdateAdminUsernameByServerIDTest_TestAction, "dbo_UpdateAdminUsernameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition15
            // 
            inconclusiveCondition15.Enabled = true;
            inconclusiveCondition15.Name = "inconclusiveCondition15";
            // 
            // dbo_UpdateCourseCodeByClassIDTest_TestAction
            // 
            dbo_UpdateCourseCodeByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition16);
            resources.ApplyResources(dbo_UpdateCourseCodeByClassIDTest_TestAction, "dbo_UpdateCourseCodeByClassIDTest_TestAction");
            // 
            // inconclusiveCondition16
            // 
            inconclusiveCondition16.Enabled = true;
            inconclusiveCondition16.Name = "inconclusiveCondition16";
            // 
            // dbo_UpdateCourseNameByClassIDTest_TestAction
            // 
            dbo_UpdateCourseNameByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition17);
            resources.ApplyResources(dbo_UpdateCourseNameByClassIDTest_TestAction, "dbo_UpdateCourseNameByClassIDTest_TestAction");
            // 
            // inconclusiveCondition17
            // 
            inconclusiveCondition17.Enabled = true;
            inconclusiveCondition17.Name = "inconclusiveCondition17";
            // 
            // dbo_UpdateCourseNumberByClassIDTest_TestAction
            // 
            dbo_UpdateCourseNumberByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition18);
            resources.ApplyResources(dbo_UpdateCourseNumberByClassIDTest_TestAction, "dbo_UpdateCourseNumberByClassIDTest_TestAction");
            // 
            // inconclusiveCondition18
            // 
            inconclusiveCondition18.Enabled = true;
            inconclusiveCondition18.Name = "inconclusiveCondition18";
            // 
            // dbo_UpdateDisplayFNameByUserIDTest_TestAction
            // 
            dbo_UpdateDisplayFNameByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition19);
            resources.ApplyResources(dbo_UpdateDisplayFNameByUserIDTest_TestAction, "dbo_UpdateDisplayFNameByUserIDTest_TestAction");
            // 
            // inconclusiveCondition19
            // 
            inconclusiveCondition19.Enabled = true;
            inconclusiveCondition19.Name = "inconclusiveCondition19";
            // 
            // dbo_UpdateDisplayLNameByUserIDTest_TestAction
            // 
            dbo_UpdateDisplayLNameByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition20);
            resources.ApplyResources(dbo_UpdateDisplayLNameByUserIDTest_TestAction, "dbo_UpdateDisplayLNameByUserIDTest_TestAction");
            // 
            // inconclusiveCondition20
            // 
            inconclusiveCondition20.Enabled = true;
            inconclusiveCondition20.Name = "inconclusiveCondition20";
            // 
            // dbo_UpdateEmailAddressByUserIDTest_TestAction
            // 
            dbo_UpdateEmailAddressByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition21);
            resources.ApplyResources(dbo_UpdateEmailAddressByUserIDTest_TestAction, "dbo_UpdateEmailAddressByUserIDTest_TestAction");
            // 
            // inconclusiveCondition21
            // 
            inconclusiveCondition21.Enabled = true;
            inconclusiveCondition21.Name = "inconclusiveCondition21";
            // 
            // dbo_UpdateEmailCredintialsByServerIDTest_TestAction
            // 
            dbo_UpdateEmailCredintialsByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition22);
            resources.ApplyResources(dbo_UpdateEmailCredintialsByServerIDTest_TestAction, "dbo_UpdateEmailCredintialsByServerIDTest_TestAction");
            // 
            // inconclusiveCondition22
            // 
            inconclusiveCondition22.Enabled = true;
            inconclusiveCondition22.Name = "inconclusiveCondition22";
            // 
            // dbo_UpdateMessageByMessageIDTest_TestAction
            // 
            dbo_UpdateMessageByMessageIDTest_TestAction.Conditions.Add(inconclusiveCondition23);
            resources.ApplyResources(dbo_UpdateMessageByMessageIDTest_TestAction, "dbo_UpdateMessageByMessageIDTest_TestAction");
            // 
            // inconclusiveCondition23
            // 
            inconclusiveCondition23.Enabled = true;
            inconclusiveCondition23.Name = "inconclusiveCondition23";
            // 
            // dbo_UpdatePrivilegesByUserIDTest_TestAction
            // 
            dbo_UpdatePrivilegesByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition24);
            resources.ApplyResources(dbo_UpdatePrivilegesByUserIDTest_TestAction, "dbo_UpdatePrivilegesByUserIDTest_TestAction");
            // 
            // inconclusiveCondition24
            // 
            inconclusiveCondition24.Enabled = true;
            inconclusiveCondition24.Name = "inconclusiveCondition24";
            // 
            // dbo_UpdateReviewDescriptionByReviewIDTest_TestAction
            // 
            dbo_UpdateReviewDescriptionByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition25);
            resources.ApplyResources(dbo_UpdateReviewDescriptionByReviewIDTest_TestAction, "dbo_UpdateReviewDescriptionByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition25
            // 
            inconclusiveCondition25.Enabled = true;
            inconclusiveCondition25.Name = "inconclusiveCondition25";
            // 
            // dbo_UpdateSchoolNameByServerIDTest_TestAction
            // 
            dbo_UpdateSchoolNameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition26);
            resources.ApplyResources(dbo_UpdateSchoolNameByServerIDTest_TestAction, "dbo_UpdateSchoolNameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition26
            // 
            inconclusiveCondition26.Enabled = true;
            inconclusiveCondition26.Name = "inconclusiveCondition26";
            // 
            // dbo_UpdateServerDomainByServerIDTest_TestAction
            // 
            dbo_UpdateServerDomainByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition27);
            resources.ApplyResources(dbo_UpdateServerDomainByServerIDTest_TestAction, "dbo_UpdateServerDomainByServerIDTest_TestAction");
            // 
            // inconclusiveCondition27
            // 
            inconclusiveCondition27.Enabled = true;
            inconclusiveCondition27.Name = "inconclusiveCondition27";
            // 
            // dbo_UpdateServerIPByServerIDTest_TestAction
            // 
            dbo_UpdateServerIPByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition28);
            resources.ApplyResources(dbo_UpdateServerIPByServerIDTest_TestAction, "dbo_UpdateServerIPByServerIDTest_TestAction");
            // 
            // inconclusiveCondition28
            // 
            inconclusiveCondition28.Enabled = true;
            inconclusiveCondition28.Name = "inconclusiveCondition28";
            // 
            // dbo_UpdateServerNameByServerIDTest_TestAction
            // 
            dbo_UpdateServerNameByServerIDTest_TestAction.Conditions.Add(inconclusiveCondition29);
            resources.ApplyResources(dbo_UpdateServerNameByServerIDTest_TestAction, "dbo_UpdateServerNameByServerIDTest_TestAction");
            // 
            // inconclusiveCondition29
            // 
            inconclusiveCondition29.Enabled = true;
            inconclusiveCondition29.Name = "inconclusiveCondition29";
            // 
            // dbo_UpdateStarRankingByReviewIDTest_TestAction
            // 
            dbo_UpdateStarRankingByReviewIDTest_TestAction.Conditions.Add(inconclusiveCondition30);
            resources.ApplyResources(dbo_UpdateStarRankingByReviewIDTest_TestAction, "dbo_UpdateStarRankingByReviewIDTest_TestAction");
            // 
            // inconclusiveCondition30
            // 
            inconclusiveCondition30.Enabled = true;
            inconclusiveCondition30.Name = "inconclusiveCondition30";
            // 
            // dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction
            // 
            dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction.Conditions.Add(inconclusiveCondition31);
            resources.ApplyResources(dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction, "dbo_UpdateStickerProblemDescriptionByStickerIDTest_TestAction");
            // 
            // inconclusiveCondition31
            // 
            inconclusiveCondition31.Enabled = true;
            inconclusiveCondition31.Name = "inconclusiveCondition31";
            // 
            // dbo_UpdateTermsOfferedByClassIDTest_TestAction
            // 
            dbo_UpdateTermsOfferedByClassIDTest_TestAction.Conditions.Add(inconclusiveCondition32);
            resources.ApplyResources(dbo_UpdateTermsOfferedByClassIDTest_TestAction, "dbo_UpdateTermsOfferedByClassIDTest_TestAction");
            // 
            // inconclusiveCondition32
            // 
            inconclusiveCondition32.Enabled = true;
            inconclusiveCondition32.Name = "inconclusiveCondition32";
            // 
            // dbo_UpdateTimeoutByStickerIDTest_TestAction
            // 
            dbo_UpdateTimeoutByStickerIDTest_TestAction.Conditions.Add(inconclusiveCondition33);
            resources.ApplyResources(dbo_UpdateTimeoutByStickerIDTest_TestAction, "dbo_UpdateTimeoutByStickerIDTest_TestAction");
            // 
            // inconclusiveCondition33
            // 
            inconclusiveCondition33.Enabled = true;
            inconclusiveCondition33.Name = "inconclusiveCondition33";
            // 
            // dbo_UpdateUserPasswordByUserIDTest_TestAction
            // 
            dbo_UpdateUserPasswordByUserIDTest_TestAction.Conditions.Add(inconclusiveCondition34);
            resources.ApplyResources(dbo_UpdateUserPasswordByUserIDTest_TestAction, "dbo_UpdateUserPasswordByUserIDTest_TestAction");
            // 
            // inconclusiveCondition34
            // 
            inconclusiveCondition34.Enabled = true;
            inconclusiveCondition34.Name = "inconclusiveCondition34";
            // 
            // dbo_ClearReviewDescriptionByReviewIDTestData
            // 
            this.dbo_ClearReviewDescriptionByReviewIDTestData.PosttestAction = null;
            this.dbo_ClearReviewDescriptionByReviewIDTestData.PretestAction = null;
            this.dbo_ClearReviewDescriptionByReviewIDTestData.TestAction = dbo_ClearReviewDescriptionByReviewIDTest_TestAction;
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
            // dbo_MentorNameByMentorIDTestData
            // 
            this.dbo_MentorNameByMentorIDTestData.PosttestAction = null;
            this.dbo_MentorNameByMentorIDTestData.PretestAction = null;
            this.dbo_MentorNameByMentorIDTestData.TestAction = dbo_MentorNameByMentorIDTest_TestAction;
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
            // dbo_UpdateEmailCredintialsByServerIDTestData
            // 
            this.dbo_UpdateEmailCredintialsByServerIDTestData.PosttestAction = null;
            this.dbo_UpdateEmailCredintialsByServerIDTestData.PretestAction = null;
            this.dbo_UpdateEmailCredintialsByServerIDTestData.TestAction = dbo_UpdateEmailCredintialsByServerIDTest_TestAction;
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
            // dbo_UpdateTimeoutByStickerIDTestData
            // 
            this.dbo_UpdateTimeoutByStickerIDTestData.PosttestAction = null;
            this.dbo_UpdateTimeoutByStickerIDTestData.PretestAction = null;
            this.dbo_UpdateTimeoutByStickerIDTestData.TestAction = dbo_UpdateTimeoutByStickerIDTest_TestAction;
            // 
            // dbo_UpdateUserPasswordByUserIDTestData
            // 
            this.dbo_UpdateUserPasswordByUserIDTestData.PosttestAction = null;
            this.dbo_UpdateUserPasswordByUserIDTestData.PretestAction = null;
            this.dbo_UpdateUserPasswordByUserIDTestData.TestAction = dbo_UpdateUserPasswordByUserIDTest_TestAction;
            // 
            // emptyResultSetCondition1
            // 
            emptyResultSetCondition1.Enabled = true;
            emptyResultSetCondition1.Name = "emptyResultSetCondition1";
            emptyResultSetCondition1.ResultSet = 1;
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
        public void dbo_MentorNameByMentorIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_MentorNameByMentorIDTestData;
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
        public void dbo_UpdateEmailCredintialsByServerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateEmailCredintialsByServerIDTestData;
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
        public void dbo_UpdateTimeoutByStickerIDTest()
        {
            SqlDatabaseTestActions testActions = this.dbo_UpdateTimeoutByStickerIDTestData;
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
        private SqlDatabaseTestActions dbo_ClearReviewDescriptionByReviewIDTestData;
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
        private SqlDatabaseTestActions dbo_MentorNameByMentorIDTestData;
        private SqlDatabaseTestActions dbo_UpdateAdminPasswordByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateAdminUsernameByServerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateCourseCodeByClassIDTestData;
        private SqlDatabaseTestActions dbo_UpdateCourseNameByClassIDTestData;
        private SqlDatabaseTestActions dbo_UpdateCourseNumberByClassIDTestData;
        private SqlDatabaseTestActions dbo_UpdateDisplayFNameByUserIDTestData;
        private SqlDatabaseTestActions dbo_UpdateDisplayLNameByUserIDTestData;
        private SqlDatabaseTestActions dbo_UpdateEmailAddressByUserIDTestData;
        private SqlDatabaseTestActions dbo_UpdateEmailCredintialsByServerIDTestData;
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
        private SqlDatabaseTestActions dbo_UpdateTimeoutByStickerIDTestData;
        private SqlDatabaseTestActions dbo_UpdateUserPasswordByUserIDTestData;
    }
}
