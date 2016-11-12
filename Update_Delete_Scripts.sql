--UnstuckME Update/Delete Scripts
--********Delete********
USE UnstuckME_DB
GO
/*********************************************************
--Delete Server Information Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteServerInformationByServerID]
    (
    @ServerID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
                DELETE FROM [Server]
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete User Profile Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteUserProfileByUserID]
    (
    @UserID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
                DELETE FROM UserProfile
				WHERE UserID = @UserID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete User Profile Picture Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteUserPictureByUserID]
    (
    @UserID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from Picture where UserID = @UserID))
            return 1;
        else
            BEGIN
                DELETE Picture
				WHERE UserID = @UserID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete File Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteFileByFileID]
    (
    @FileID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select FileID from Files where FileID = @FileID))
            return 1;
        else
            BEGIN
                DELETE Files
				WHERE FileID = @fileID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete Message Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteMessageByMessageID]
    (
    @MessageID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select MessageID from [Messages] where MessageID = @MessageID))
            return 1;
        else
            BEGIN
                DELETE [Messages]
				WHERE MessageID = @messageID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete Mentor Organization Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteMentorOrganizationByMentorID]
    (
    @MentorID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select MentorID from OfficialMentor where MentorID = @MentorID))
            return 1;
        else
            BEGIN
				DELETE OfficialMentor
				WHERE MentorID = @mentorID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete Class Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteClassByClassID]
    (
    @ClassID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				DELETE Classes
				WHERE ClassID = @classID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete Sticker Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteStickerByStickerID]
    (
    @StickerID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				DELETE Sticker
				WHERE StickerID = @stickerID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete Review Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteReviewByReviewID]
    (
    @ReviewID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ReviewID from Review where ReviewID = @ReviewID))
            return 1;
        else
            BEGIN
				DELETE Review
				WHERE ReviewID = @reviewID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete Review Description Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[ClearReviewDescriptionByReviewID]
    (
    @ReviewID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ReviewID from Review where ReviewID = @ReviewID))
            return 1;
        else
            BEGIN
				UPDATE Review
				SET [Description] = ''
				WHERE ReviewID = @reviewID;
                return 0;
            END

    END
GO

/*********************************************************
--Delete Report Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteReportByReportID]
    (
    @ReportID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ReportID from Report where ReportID = @ReportID))
            return 1;
        else
            BEGIN
				DELETE Report
				WHERE ReportID = @reportID;
                return 0;
            END

    END
GO

--********Update********
/*********************************************************
--Update Server Name
*********************************************************/
CREATE PROC [dbo].[UpdateServerNameByServerID]
    (
    @ServerID INT,
	@ServerName VARCHAR(75)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET ServerName = @ServerName
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Server IP Address
*********************************************************/
CREATE PROC [dbo].[UpdateServerIPByServerID]
    (
    @ServerID INT,
	@ServerIP VARCHAR(15)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET ServerIP = @ServerIP
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Server Domain Name
*********************************************************/
CREATE PROC [dbo].[UpdateServerDomainByServerID]
    (
    @ServerID INT,
	@ServerDomain VARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET ServerDomain = @ServerDomain
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
GO

/*********************************************************
--Update School Name
*********************************************************/
CREATE PROC [dbo].[UpdateSchoolNameByServerID]
    (
    @ServerID INT,
	@SchoolName VARCHAR(75)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET SchoolName = @SchoolName
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Admin Password
*********************************************************/
CREATE PROC [dbo].[UpdateAdminPasswordByServerID]
    (
    @ServerID INT,
	@AdminPassword BINARY(64)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET AdminPassword = @AdminPassword
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Admin Username
*********************************************************/
CREATE PROC [dbo].[UpdateAdminUsernameByServerID]
    (
    @ServerID INT,
	@AdminUsername VARCHAR(30)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET AdminUsername = @AdminUsername
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Server Email Credentials
*********************************************************/
CREATE PROC [dbo].[UpdateEmailCredintialsByServerID]
    (
    @ServerID INT,
	@EmailCredentials NVARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET EmailCredentials = @EmailCredentials
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
GO

--Photo
/********************************NEED MORE INFO ON HOW WE ARE STORING PHOTOS*******************************/

/*********************************************************
--Update Mentor Organization Name
*********************************************************/
CREATE PROC [dbo].[MentorNameByMentorID]
    (
    @MentorID INT,
	@OrganizationName NVARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select MentorID from OfficialMentor where MentorID = @MentorID))
            return 1;
        else
            BEGIN
				UPDATE OfficialMentor
				SET OrganizationName = @OrganizationName
				WHERE MentorID = @MentorID;
                return 0;
            END

    END
GO

/*********************************************************
--Update User First Name
*********************************************************/
CREATE PROC [dbo].[UpdateDisplayFNameByUserID]
    (
    @UserID INT,
	@DisplayFName VARCHAR(30)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET DisplayFName = @DisplayFName
				WHERE UserID = @userID;
                return 0;
            END

    END
GO

/*********************************************************
--Update User Last Name
*********************************************************/
CREATE PROC [dbo].[UpdateDisplayLNameByUserID]
    (
    @UserID INT,
	@DisplayLName VARCHAR(30)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET DisplayLName = @DisplayLName
				WHERE UserID = @userID;
                return 0;
            END

    END
GO

/*********************************************************
--Update User Email Address
*********************************************************/
CREATE PROC [dbo].[UpdateEmailAddressByUserID]
    (
    @UserID INT,
	@EmailAddress VARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET EmailAddress = @EmailAddress
				WHERE UserID = @userID;
                return 0;
            END

    END
GO

/*********************************************************
--Update User Password
*********************************************************/
CREATE PROC [dbo].[UpdateUserPasswordByUserID]
    (
    @UserID INT,
	@UserPassword BINARY(64)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET UserPassword = @UserPassword
				WHERE UserID = @userID;
                return 0;
            END

    END
GO

/*********************************************************
--Update User Privileges
*********************************************************/
CREATE PROC [dbo].[UpdatePrivilegesByUserID]
    (
    @UserID INT,
	@Privileges BINARY(4)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET Privileges = @Privileges
				WHERE UserID = @userID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Sticker Problem Description
*********************************************************/
CREATE PROC [dbo].[UpdateStickerProblemDescriptionByStickerID]
    (
    @StickerID INT,
	@ProblemDescription NVARCHAR(500)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET ProblemDescription = @ProblemDescription
				WHERE StickerID = @StickerID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Timeout Time
*********************************************************/
CREATE PROC [dbo].[UpdateTimeoutByStickerID]
    (
    @StickerID INT,
	@Timeout DATETIME2
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET [Timeout] = @Timeout
				WHERE StickerID = @StickerID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Common Course Name
*********************************************************/
CREATE PROC [dbo].[UpdateCourseNameByClassID]
    (
    @ClassID INT,
	@CourseName VARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Classes
				SET CourseName = @CourseName
				WHERE ClassID = @ClassID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Course Code (WRI, MATH, CST)
*********************************************************/
CREATE PROC [dbo].[UpdateCourseCodeByClassID]
    (
    @ClassID INT,
	@CourseCode VARCHAR(5)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Classes
				SET CourseCode = @CourseCode
				WHERE ClassID = @ClassID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Course Number
*********************************************************/
CREATE PROC [dbo].[UpdateCourseNumberByClassID]
    (
    @ClassID INT,
	@CourseNumber SMALLINT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Classes
				SET CourseNumber = @CourseNumber
				WHERE ClassID = @ClassID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Terms Offered
*********************************************************/
CREATE PROC [dbo].[UpdateTermsOfferedByClassID]
    (
    @ClassID INT,
	@TermOffered TINYINT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Classes
				SET TermOffered = @TermOffered
				WHERE ClassID = @ClassID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Review Star Ranking
*********************************************************/
CREATE PROC [dbo].[UpdateStarRankingByReviewID]
    (
    @ReviewID INT,
	@StarRanking TINYINT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ReviewID from Review where ReviewID = @ReviewID))
            return 1;
        else
            BEGIN
				UPDATE Review
				SET StarRanking = @StarRanking
				WHERE ReviewID = @ReviewID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Review Description
*********************************************************/
CREATE PROC [dbo].[UpdateReviewDescriptionByReviewID]
    (
    @ReviewID INT,
	@Description NVARCHAR(250)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ReviewID from Review where ReviewID = @ReviewID))
            return 1;
        else
            BEGIN
				UPDATE Review
				SET [Description] = @Description
				WHERE ReviewID = @ReviewID;
                return 0;
            END

    END
GO

/*********************************************************
--Update Chat Message
*********************************************************/
CREATE PROC [dbo].[UpdateMessageByMessageID]
    (
    @MessageID INT,
	@MessageData NVARCHAR(500)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select MessageID from [Messages] where MessageID = @MessageID))
            return 1;
        else
            BEGIN
				UPDATE [Messages]
				SET MessageData = @MessageData
				WHERE MessageID = @MessageID;
                return 0;
            END

    END
GO

/******************************************************************
--Update TutorID by TutorID and StickerID Procedure Creation Script
******************************************************************/
CREATE PROC [dbo].[UpdateTutorIDByTutorIDAndStickerID]
    (
	@TutorID	INT,
    @StickerID	INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET TutorID = @TutorID;
                return 0;
            END

    END
GO

/******************************************************************************
--Update TutorReviewID by TutorReviewID and StickerID Procedure Creation Script
******************************************************************************/
CREATE PROC [dbo].[UpdateTutorReviewIDByTutorReviewIDAndStickerID]
    (
	@TutorReviewID	INT,
    @StickerID		INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET TutorReviewID = @TutorReviewID;
                return 0;
            END

    END
GO

/**********************************************************************************
--Update StudentReviewID by StudentReviewID and StickerID Procedure Creation Script
**********************************************************************************/
CREATE PROC [dbo].[UpdateStudentReviewIDByStudentReviewIDAndStickerID]
    (
	@StudentReviewID	INT,
    @StickerID		INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET StudentReviewID = @StudentReviewID;
                return 0;
            END

    END
GO
