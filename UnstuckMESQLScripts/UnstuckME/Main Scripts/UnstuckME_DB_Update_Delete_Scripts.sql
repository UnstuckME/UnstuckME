--UnstuckME Update/Delete Scripts
--********Delete********
USE UnstuckME_DB
GO

/***************************************************
DROP STORED PROCEDURES
***************************************************/
IF OBJECT_ID('DeleteUserFromClass') is not null
	DROP PROCEDURE [DeleteUserFromClass];
IF OBJECT_ID('DeleteServerAdmin') is not null
	DROP PROCEDURE [DeleteServerAdmin];
IF OBJECT_ID('DeleteUserProfileByUserID') is not null
	DROP PROCEDURE [DeleteUserProfileByUserID];
IF OBJECT_ID('DeleteUserPictureByUserID') is not null
	DROP PROCEDURE [DeleteUserPictureByUserID];
IF OBJECT_ID('DeleteMessageByMessageID') is not null
	DROP PROCEDURE [DeleteMessageByMessageID];
IF OBJECT_ID('DeleteMentorOrganizationByMentorID') is not null
	DROP PROCEDURE [DeleteMentorOrganizationByMentorID];
IF OBJECT_ID('DeleteClassByClassID') is not null
	DROP PROCEDURE [DeleteClassByClassID];
IF OBJECT_ID('DeleteStickerByStickerID') is not null
	DROP PROCEDURE DeleteStickerByStickerID;
IF OBJECT_ID('DeleteReviewByReviewID') is not null
	DROP PROCEDURE DeleteReviewByReviewID;
IF OBJECT_ID('ClearReviewDescriptionByReviewID') is not null
	DROP PROCEDURE ClearReviewDescriptionByReviewID;
IF OBJECT_ID('DeleteReportByReportID') is not null
	DROP PROCEDURE DeleteReportByReportID;
IF OBJECT_ID('DeleteFriend') is not null
	DROP PROCEDURE DeleteFriend;
IF OBJECT_ID('RemoveUserFromMentorProgram') is not null
	DROP PROCEDURE RemoveUserFromMentorProgram;

--Photo
/********************************NEED MORE INFO ON HOW WE ARE STORING PHOTOS*******************************/
IF OBJECT_ID('UpdateServerAdmin') is not null
	DROP PROCEDURE UpdateServerAdmin;
IF OBJECT_ID('UpdateMentorNameByMentorID') is not null
	DROP PROCEDURE UpdateMentorNameByMentorID;
IF OBJECT_ID('UpdateDisplayFNameByUserID') is not null
	DROP PROCEDURE UpdateDisplayFNameByUserID;
IF OBJECT_ID('UpdateDisplayLNameByUserID') is not null
	DROP PROCEDURE UpdateDisplayLNameByUserID;
IF OBJECT_ID('UpdateEmailAddressByUserID') is not null
	DROP PROCEDURE UpdateEmailAddressByUserID;
IF OBJECT_ID('UpdateUserPasswordByUserID') is not null
	DROP PROCEDURE UpdateUserPasswordByUserID;
IF OBJECT_ID('UpdatePrivilegesByUserID') is not null
	DROP PROCEDURE UpdatePrivilegesByUserID;
IF OBJECT_ID('UpdateStickerProblemDescriptionByStickerID') is not null
	DROP PROCEDURE UpdateStickerProblemDescriptionByStickerID;
IF OBJECT_ID('UpdateTimeoutByStickerIDAndSeconds') is not null
	DROP PROCEDURE UpdateTimeoutByStickerIDAndSeconds;
IF OBJECT_ID('MarkStickerAsResolved') is not null
	DROP PROCEDURE MarkStickerAsResolved;
IF OBJECT_ID('UpdateCourseNameByClassID') is not null
	DROP PROCEDURE UpdateCourseNameByClassID;
IF OBJECT_ID('UpdateCourseCodeByClassID') is not null
	DROP PROCEDURE UpdateCourseCodeByClassID;
IF OBJECT_ID('UpdateCourseNumberByClassID') is not null
	DROP PROCEDURE UpdateCourseNumberByClassID;
IF OBJECT_ID('UpdateStarRankingByReviewID') is not null
	DROP PROCEDURE UpdateStarRankingByReviewID;
IF OBJECT_ID('UpdateReviewDescriptionByReviewID') is not null
	DROP PROCEDURE UpdateReviewDescriptionByReviewID;
IF OBJECT_ID('UpdateMessageByMessageID') is not null
	DROP PROCEDURE UpdateMessageByMessageID;
IF OBJECT_ID('UpdateTutorIDByTutorIDAndStickerID') is not null
	DROP PROCEDURE UpdateTutorIDByTutorIDAndStickerID;
IF OBJECT_ID('UpdateUserAverageRank') is not null
	DROP PROCEDURE UpdateUserAverageRank;
IF OBJECT_ID('UpdateUserTotalReviews') is not null
	DROP PROCEDURE UpdateUserTotalReviews;
IF OBJECT_ID('UpdateStickerByChatID') is not null
	DROP PROCEDURE UpdateStickerByChatID;
GO
--START CREATION SCRIPTS
/*********************************************************/
/*********************************************************
--Delete User From Class Stored Procedure
*********************************************************/
CREATE PROC [dbo].[DeleteUserFromClass]
	(
		@UserID INT,
		@ClassID INT
	)
AS
	BEGIN
		IF (NOT EXISTS(SELECT UserID FROM UserToClass WHERE UserID = @UserID AND ClassID = @ClassID))
			RETURN 1;
		ELSE BEGIN
			DELETE FROM UserToClass 
			WHERE UserID = @UserID AND @ClassID = ClassID
			RETURN 0;
		END
	END
GO
 
/*********************************************************
--Delete ServerAdmin PROCEDURE Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteServerAdmin]
	(
	@AdminID INT
	)
AS
	BEGIN
		IF (NOT EXISTS(SELECT ServerAdminID FROM ServerAdmins WHERE ServerAdminID = @AdminID))
			RETURN 1;
		ELSE BEGIN
			DELETE FROM ServerAdmins
			WHERE ServerAdminID = @AdminID
			RETURN 0;
		END
	END
GO

/*********************************************************
--Delete User Profile PROCEDURE Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteUserProfileByUserID]
    (
    @UserID INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT UserID FROM UserProfile WHERE UserID = @UserID))
            RETURN 1;
        ELSE BEGIN
			UPDATE Sticker
			SET StudentID = 1
			WHERE StudentID = @UserID;

			UPDATE Sticker
			SET TutorID = 1
			WHERE TutorID = @UserID;

			UPDATE Report
			SET FlaggerID = 1
			WHERE FlaggerID = @UserID;

			UPDATE Review
			SET ReviewerID = 1
			WHERE ReviewerID = @UserID;

			DELETE FROM UserToClass
			WHERE UserID = @UserID;

			DELETE FROM Picture
			WHERE UserID = @UserID;

			DELETE FROM OmToUser
			WHERE UserID = @UserID;

			DELETE FROM UserToChat
			WHERE UserID = @UserID;

			DECLARE @ChatID INT
			DECLARE my_cursor CURSOR LOCAL STATIC READ_ONLY FORWARD_ONLY FOR
			SELECT * FROM Chat

			OPEN my_cursor
			FETCH NEXT FROM my_cursor INTO @ChatID
			WHILE @@FETCH_STATUS = 0
			BEGIN
				IF (SELECT COUNT(UserID) from UserToChat WHERE ChatID = @ChatID) = 1
				BEGIN
					DELETE FROM UserToChat
					WHERE ChatID = @ChatID
				END
				FETCH NEXT FROM my_cursor INTO @ChatID
			END
			CLOSE my_cursor
			DEALLOCATE my_cursor

			DELETE FROM [Messages]
			WHERE SentBy = @UserID;

			DELETE FROM Friends
			WHERE UserID = @UserID OR FriendUserID = @UserID;

            DELETE FROM UserProfile
			WHERE UserID = @UserID;

            RETURN 0;
        END
    END
GO

/*********************************************************
--Delete User Profile Picture PROCEDURE Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteUserPictureByUserID]
    (
    @UserID INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT UserID FROM Picture WHERE UserID = @UserID))
            RETURN 1;
        ELSE BEGIN
            DELETE Picture
			WHERE UserID = @UserID;
            RETURN 0;
        END
    END
GO

/*********************************************************
--Delete Message PROCEDURE Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteMessageByMessageID]
    (
    @MessageID INT
    )
AS
    BEGIN
        IF (NOT Exists(SELECT MessageID FROM [Messages] WHERE MessageID = @MessageID))
            RETURN 1;
        ELSE BEGIN
            DELETE [Messages]
			WHERE MessageID = @messageID;
            RETURN 0;
        END
    END
GO

/*********************************************************
--Delete Mentor Organization PROCEDURE Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteMentorOrganizationByMentorID]
    (
    @MentorID INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT MentorID FROM OfficialMentor WHERE MentorID = @MentorID))
            RETURN 1;
        ELSE BEGIN
			DELETE FROM StickerToMentor
			WHERE @MentorID = MentorID;

			DELETE OmToUser
			WHERE @MentorID = MentorID;

			DELETE OfficialMentor
			WHERE MentorID = @MentorID;

            RETURN 0;
        END
    END
GO

/*********************************************************
--Delete Class PROCEDURE Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteClassByClassID]
    (
    @ClassID INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT ClassID FROM Classes WHERE ClassID = @ClassID))
            RETURN 1;
        ELSE BEGIN
			UPDATE Sticker
			SET ClassID = 1
			WHERE ClassID = @ClassID;

			DELETE UserToClass
			WHERE ClassID = @ClassID;

			DELETE Classes
			WHERE ClassID = @classID;

            RETURN 0;
		END
    END
GO

/*********************************************************
--Delete Sticker PROCEDURE Creation Script NOT NEEDED AS OF NOW
*********************************************************/
CREATE PROC [dbo].[DeleteStickerByStickerID]
    (
    @StickerID INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT StickerID FROM Sticker WHERE StickerID = @StickerID))
            RETURN 1;
        ELSE
            BEGIN
				DELETE FROM StickerToMentor
				WHERE StickerID = @StickerID;
				DELETE Sticker
				WHERE StickerID = @stickerID;
                RETURN 0;
            END
    END
GO

/*********************************************************
--Delete Review PROCEDURE Creation Script NOT NEEDED AS OF NOW
*********************************************************/
CREATE PROC [dbo].[DeleteReviewByReviewID]
    (
    @ReviewID INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT ReviewID FROM Review WHERE ReviewID = @ReviewID))
            RETURN 1;
        ELSE
            BEGIN
				DELETE Review
				WHERE ReviewID = @reviewID;
                RETURN 0;
            END
    END
GO

/*********************************************************
--Delete Review Description PROCEDURE Creation Script
*********************************************************/
CREATE PROC [dbo].[ClearReviewDescriptionByReviewID]
    (
    @ReviewID INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT ReviewID FROM Review WHERE ReviewID = @ReviewID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Review
				SET [Description] = 'This Review Has Been Removed'
				WHERE ReviewID = @reviewID;
                RETURN 0;
            END
    END
GO

/*********************************************************
--Delete Report PROCEDURE Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteReportByReportID]
    (
    @ReportID INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT ReportID FROM Report WHERE ReportID = @ReportID))
            RETURN 1;
        ELSE
            BEGIN
				DELETE Report
				WHERE ReportID = @reportID;
                RETURN 0;
            END
    END
GO
/*********************************************************
--Delete Friend
*********************************************************/
CREATE PROC [dbo].[DeleteFriend]
	(
	@CurrentUserID			INT,
	@TargetFriendUserID		INT
	)
AS
	BEGIN
		IF (NOT EXISTS(SELECT UserID, FriendUserID
						FROM Friends
						WHERE UserID = @CurrentUserID AND FriendUserID = @TargetFriendUserID))
						RETURN 1;
		ELSE
			BEGIN
				DELETE FROM Friends
				WHERE UserID = @CurrentUserID AND FriendUserID = @TargetFriendUserID;
				RETURN 0;
			END
	END
GO

/*********************************************************
--Remove user from mentoring organization
*********************************************************/
CREATE PROC [dbo].[RemoveUserFromMentorProgram]
    (
	@UserID		INT,
	@MentorID	INT
    )
AS
    BEGIN
        if  (NOT EXISTS(Select * from OmToUser WHERE UserID = @UserID AND MentorID = @MentorID))
			SELECT -1;
        ELSE BEGIN
            DELETE FROM OmToUser
			WHERE UserID = @UserID and MentorID = @MentorID
			SELECT 0;
        END
    END
GO

--********Update********

--Photo
/********************************NEED MORE INFO ON HOW WE ARE STORING PHOTOS*******************************/
CREATE PROC [dbo].[UpdateServerAdmin]
	(
	@ServerAdminID INT,
	@FirstName	VARCHAR(32) = NULL,
	@LastName	VARCHAR(32) = NULL,
	@Password	NVARCHAR(256) = NULL,
	@Salt		NVARCHAR(256) = NULL
	)
AS
	BEGIN
		IF(NOT EXISTS(SELECT ServerAdminID FROM ServerAdmins WHERE ServerAdminID = @ServerAdminID))
			RETURN 1;
		ELSE
			IF(@FirstName IS NOT NULL)
			BEGIN
				UPDATE ServerAdmins
				SET FirstName = @FirstName
				WHERE @ServerAdminID = ServerAdminID
			END
			IF(@LastName IS NOT NULL)
			BEGIN
				UPDATE ServerAdmins
				SET LastName = @LastName
				WHERE @ServerAdminID = ServerAdminID
			END
			IF(@Password IS NOT NULL AND @Salt IS NOT NULL)
			BEGIN
				UPDATE ServerAdmins
				SET [Password] = @Password, Salt = @Salt
				WHERE @ServerAdminID = ServerAdminID
			END
	END
GO
				
/*********************************************************
--Update Mentor Organization Name
*********************************************************/
CREATE PROC [dbo].[UpdateMentorNameByMentorID]
    (
    @MentorID INT,
	@OrganizationName NVARCHAR(50)
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT MentorID FROM OfficialMentor WHERE MentorID = @MentorID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE OfficialMentor
				SET OrganizationName = @OrganizationName
				WHERE MentorID = @MentorID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT UserID FROM UserProfile WHERE UserID = @UserID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE UserProfile
				SET DisplayFName = @DisplayFName
				WHERE UserID = @userID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT UserID FROM UserProfile WHERE UserID = @UserID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE UserProfile
				SET DisplayLName = @DisplayLName
				WHERE UserID = @userID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT UserID FROM UserProfile WHERE UserID = @UserID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE UserProfile
				SET EmailAddress = @EmailAddress
				WHERE UserID = @userID;
                RETURN 0;
            END
    END
GO

/*********************************************************
--Update User Password
*********************************************************/
CREATE PROC [dbo].[UpdateUserPasswordByUserID]
    (
    @UserID INT,
	@UserPassword NVARCHAR(32)
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT UserID FROM UserProfile WHERE UserID = @UserID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE UserProfile
				SET UserPassword = @UserPassword
				WHERE UserID = @userID;
                RETURN 0;
            END
    END
GO

/*********************************************************
--Update User Privileges
*********************************************************/
CREATE PROC [dbo].[UpdatePrivilegesByUserID]
    (
    @UserID INT,
	@Privileges INT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT UserID FROM UserProfile WHERE UserID = @UserID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE UserProfile
				SET Privileges = @Privileges
				WHERE UserID = @userID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT StickerID FROM Sticker WHERE StickerID = @StickerID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Sticker
				SET ProblemDescription = @ProblemDescription
				WHERE StickerID = @StickerID;
                RETURN 0;
            END
    END
GO

/*********************************************************
--Update Timeout Time
*********************************************************/
CREATE PROC [dbo].[UpdateTimeoutByStickerIDAndSeconds]
    (
    @StickerID INT,
	@Seconds INT
    )
AS
    BEGIN
        IF (NOT Exists(SELECT StickerID FROM Sticker WHERE StickerID = @StickerID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Sticker
				SET [Timeout] = DATEADD(second, @Seconds, GETDATE())
				WHERE StickerID = @StickerID;
                RETURN 0;
            END
    END
GO

/*********************************************************
--Update Timeout Time
*********************************************************/
CREATE PROC [dbo].[MarkStickerAsResolved]
	(
	@StickerID INT
	)
AS
	BEGIN
        IF (NOT Exists(SELECT StickerID FROM Sticker WHERE StickerID = @StickerID))
            RETURN 1;
		ELSE IF (SELECT count(StickerID) FROM Review WHERE StickerID = @StickerID) <> 2
			RETURN 1;
        ELSE
            BEGIN
				UPDATE Sticker
				SET [Timeout] = GETDATE()
				WHERE StickerID = @StickerID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT ClassID FROM Classes WHERE ClassID = @ClassID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Classes
				SET CourseName = @CourseName
				WHERE ClassID = @ClassID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT ClassID FROM Classes WHERE ClassID = @ClassID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Classes
				SET CourseCode = @CourseCode
				WHERE ClassID = @ClassID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT ClassID FROM Classes WHERE ClassID = @ClassID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Classes
				SET CourseNumber = @CourseNumber
				WHERE ClassID = @ClassID;
                RETURN 0;
            END
    END
GO

/*********************************************************
--Update Terms Offered
*********************************************************/
--CREATE PROC [dbo].[UpdateTermsOfferedByClassID]
--    (
--    @ClassID INT,
--	@TermOffered TINYINT
--    )
--AS
--    BEGIN
--        IF  (NOT Exists(SELECT ClassID FROM Classes WHERE ClassID = @ClassID))
--            RETURN 1;
--        ELSE
--            BEGIN
--				UPDATE Classes
--				SET TermOffered = @TermOffered
--				WHERE ClassID = @ClassID;
--                RETURN 0;
--            END

--    END
--GO

/*********************************************************
--Update Review Star Ranking
*********************************************************/
CREATE PROC [dbo].[UpdateStarRankingByReviewID]
    (
    @ReviewID INT,
	@StarRanking TINYINT
    )
AS
    BEGIN
        IF  (NOT Exists(SELECT ReviewID FROM Review WHERE ReviewID = @ReviewID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Review
				SET StarRanking = @StarRanking
				WHERE ReviewID = @ReviewID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT ReviewID FROM Review WHERE ReviewID = @ReviewID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Review
				SET [Description] = @Description
				WHERE ReviewID = @ReviewID;
                RETURN 0;
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
AS
    BEGIN
        IF  (NOT Exists(SELECT MessageID FROM [Messages] WHERE MessageID = @MessageID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE [Messages]
				SET MessageData = @MessageData
				WHERE MessageID = @MessageID;
                RETURN 0;
            END
    END
GO

/******************************************************************
--Update TutorID by TutorID and StickerID PROCEDURE Creation Script
******************************************************************/
CREATE PROC [dbo].[UpdateTutorIDByTutorIDAndStickerID]
    (
	@TutorID	INT,
    @StickerID	INT
    )
AS
    BEGIN
        IF (NOT Exists(SELECT StickerID FROM Sticker WHERE StickerID = @StickerID))
            RETURN 1;
        ELSE
            BEGIN
				UPDATE Sticker
				SET TutorID = @TutorID
				WHERE StickerID = @StickerID;
                RETURN 0;
            END
    END
GO

/******************************************************************
--Update AverageStudentRank and AverageTutorRank
******************************************************************/
CREATE PROC UpdateUserAverageRank
(	@UserID INT	) AS
BEGIN
	IF (NOT EXISTS (SELECT UserID FROM UserProfile WHERE UserID = @UserID))
		RETURN 1;
	ELSE BEGIN
		DECLARE @StudentRank FLOAT, @TutorRank FLOAT;
		EXEC @StudentRank = GetUserAvgStudentStarRank @UserID;
		EXEC @TutorRank = GetUserAvgTutorStarRank @UserID;

		UPDATE UserProfile
		SET AverageStudentRank = @StudentRank,
			AverageTutorRank = @TutorRank
		WHERE UserID = @UserID;
	END
END

GO

/******************************************************************
--Update AverageStudentRank and AverageTutorRank
******************************************************************/
CREATE PROC UpdateUserTotalReviews
(	@UserID INT	) AS
BEGIN
	IF (NOT EXISTS (SELECT UserID FROM UserProfile WHERE UserID = @UserID))
		RETURN 1;
	ELSE BEGIN
		DECLARE @StudentReviews INT, @TutorReviews INT;
		EXEC GetUserStudentReviews @UserID;
		SET @StudentReviews = @@ROWCOUNT
		EXEC GetUserTutorReviews @UserID;
		SET @TutorReviews = @@ROWCOUNT;

		UPDATE UserProfile
		SET TotalStudentReviews = @StudentReviews,
			TotalTutorReviews = @TutorReviews
		WHERE UserID = @UserID;
	END
END

GO

/******************************************************************
--Update the ChatID for a given sticker
******************************************************************/
CREATE PROC UpdateStickerByChatID
(	@ChatID int,
	@StickerID int
) as
BEGIN
	IF (NOT EXISTS(SELECT StickerID, ChatID from Sticker where StickerID = @StickerID and (ChatID is null or ChatID <> @ChatID)))
		RETURN -1;
	ELSE BEGIN
		UPDATE Sticker
		SET ChatID = @ChatID
		WHERE StickerID = @StickerID
	END
END