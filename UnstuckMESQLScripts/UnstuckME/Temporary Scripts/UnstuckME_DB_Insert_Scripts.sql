/**********************************************************
***********************************************************
***********INSERTION STORED PROCEDURE SCRIPTS**************
***********************************************************
**********************************************************/

USE UnstuckME_DB;
GO

/******DROP PROCEDURE STATEMENTS*************************/
--IF OBJECT_ID('CreateOfficialMentor') is not null
--	DROP PROCEDURE CreateOfficialMentor;
IF OBJECT_ID('CreateNewAdminUser') is not null
	DROP PROCEDURE CreateNewAdminUser;
IF OBJECT_ID('AddTutorStarRankToUser') is not null
	DROP PROCEDURE AddTutorStarRankToUser;
IF OBJECT_ID('AddStudentStarRankToUser') is not null
	DROP PROCEDURE AddStudentStarRankToUser;
IF OBJECT_ID('AddOrgToSticker') is not null
	DROP PROCEDURE AddOrgToSticker;
IF OBJECT_ID('CreateServerAdmin') is not null
	DROP PROCEDURE CreateServerAdmin;
IF OBJECT_ID('CreateNewUser') is not null
	DROP PROCEDURE CreateNewUser;
IF OBJECT_ID('InsertStudentIntoClass') is not null
	DROP PROCEDURE InsertStudentIntoClass;
IF OBJECT_ID('CreateNewClass') is not null
	DROP PROCEDURE CreateNewClass;
IF OBJECT_ID('CreateSticker') is not null
	DROP PROCEDURE CreateSticker;
IF OBJECT_ID('CreateMentorOrganization') is not null
	DROP PROCEDURE CreateMentorOrganization;
IF OBJECT_ID('CreateReview') is not null
	DROP PROCEDURE CreateReview;
IF OBJECT_ID('CreateReport') is not null
	DROP PROCEDURE CreateReport;
IF OBJECT_ID('UpdateProfilePicture') is not null
	DROP PROCEDURE UpdateProfilePicture;
--IF OBJECT_ID('ChangeProfilePicture') is not null
--	DROP PROCEDURE ChangeProfilePicture;
IF OBJECT_ID('CreateChat') is not null
	DROP PROCEDURE CreateChat;
IF OBJECT_ID('InsertUserIntoChat') is not null
	DROP PROCEDURE InsertUserIntoChat;
IF OBJECT_ID('InsertMessage') is not null
	DROP PROCEDURE InsertMessage;
--IF OBJECT_ID('InsertFile') is not null
--	DROP PROCEDURE InsertFile;
IF OBJECT_ID('AddFriend') is not null
	DROP PROCEDURE AddFriend;
IF OBJECT_ID('InsertUserIntoMentorProgram') is not null
	DROP PROCEDURE InsertUserIntoMentorProgram;

GO



CREATE PROC [dbo].[AddTutorStarRankToUser]
(
	@UserID INT,
	@StarRanking FLOAT
)
AS
	BEGIN
		IF (NOT EXISTS(SELECT UserID FROM UserProfile WHERE UserID = @UserID))
			RETURN 1;
		ELSE IF (@StarRanking > 5 OR @StarRanking < 0)
			RETURN 1;
		ELSE BEGIN
			DECLARE @AverageTutorRank FLOAT;
			SELECT @AverageTutorRank = (AverageTutorRank * TotalTutorReviews)
			FROM UserProfile
			WHERE UserID = @UserID;

			UPDATE UserProfile
			SET TotalTutorReviews = (TotalTutorReviews + 1)
			WHERE @UserID = UserID;

			UPDATE UserProfile
			SET AverageTutorRank = ((@AverageTutorRank + @StarRanking)/ TotalTutorReviews)
			WHERE @UserID = UserID;
		END
	END
GO

--Adds A rating to a user and updates current value.
CREATE PROC [dbo].[AddStudentStarRankToUser]
(
	@UserID INT,
	@StarRanking FLOAT
)
AS
	BEGIN
		IF (NOT EXISTS(SELECT UserID FROM UserProfile WHERE UserID = @UserID))
			RETURN 1;
		ELSE IF (@StarRanking > 5 OR @StarRanking < 0)
			RETURN 1;
		ELSE BEGIN
			DECLARE @AverageStudentRank FLOAT;
			SELECT @AverageStudentRank = (AverageStudentRank * TotalStudentReviews)
			FROM UserProfile
			WHERE UserID = @UserID;

			UPDATE UserProfile
			SET TotalStudentReviews = (TotalStudentReviews + 1)
			WHERE @UserID = UserID;

			UPDATE UserProfile
			SET AverageStudentRank = ((@AverageStudentRank + @StarRanking)/ TotalStudentReviews)
			WHERE @UserID = UserID;
		END
	END
GO


/****************BEGIN Insert/Create SCRIPTS******************/
--Add Sticker/Mentor Organization Relationship
CREATE PROC [dbo].[AddOrgToSticker]
(
	@StickerID INT,
	@OrganizationID INT
)
AS
	BEGIN
		IF(EXISTS(SELECT StickerID, MentorID FROM StickerToMentor WHERE StickerID = @StickerID AND MentorID = @OrganizationID))
			RETURN 1;
		ELSE BEGIN
			INSERT INTO StickerToMentor
			VALUES(@StickerID, @OrganizationID);
		END
	END
GO

--CREATE NEW SERVER ADMIN
CREATE PROC [dbo].[CreateServerAdmin]
	(
	@FirstName		VARCHAR(32),
	@LastName		VARCHAR(32),
	@EmailAddress	VARCHAR(50),
	@Password		NVARCHAR(256),
	@Salt			NVARCHAR(256)
	)
AS	
	BEGIN
		IF (EXISTS(SELECT EmailAddress FROM ServerAdmins WHERE EmailAddress = @EmailAddress))
			RETURN 1;	
		ELSE BEGIN
			INSERT INTO ServerAdmins
			VALUES(@EmailAddress, @FirstName, @LastName, @Password, @Salt)
			RETURN 0;
		END
	END
GO

CREATE PROC [dbo].[CreateNewAdminUser]
    (
    @FirstName VARCHAR(30),
	@LastName VARCHAR(30),
	@EmailAddress VARCHAR(50),
	@Password NVARCHAR(256),
	@Salt NVARCHAR(256)
    )
AS
    BEGIN
        IF (EXISTS(Select EmailAddress from UserProfile where EmailAddress = @EmailAddress))
				RETURN 1;
        ELSE BEGIN
			INSERT INTO UserProfile
			VALUES(@FirstName, @LastName, @EmailAddress, @Password, DEFAULT, DEFAULT, DEFAULT, DEFAULT, 1, @Salt)
			RETURN 0;
		END
    END
GO

CREATE PROC [dbo].[CreateNewUser]
    (
    @FirstName VARCHAR(30),
	@LastName VARCHAR(30),
	@EmailAddress VARCHAR(50),
	@Password NVARCHAR(256),
	@Salt NVARCHAR(256)
    )
AS
    BEGIN
        IF (EXISTS(Select EmailAddress from UserProfile where EmailAddress = @EmailAddress))
			RETURN 1;
        ELSE BEGIN
            INSERT INTO UserProfile
			VALUES(@FirstName, @LastName, @EmailAddress, @Password, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT, @Salt)
			RETURN 0;
		END
    END
GO

/* INSERTS A STUDENT AND A CLASS INTO UserToClass */
CREATE PROC [dbo].[InsertStudentIntoClass]
    (
    @UserID		INT,
	@ClassID	INT
    )
AS
    BEGIN
        IF (EXISTS(Select UserID, ClassID from UserToClass where UserID = @UserID AND ClassID = @ClassID))
			RETURN 1;
        ELSE BEGIN
            INSERT INTO UserToClass
			VALUES(@UserID, @ClassID)
			RETURN 0;
        END
    END
GO

--Create New Class
CREATE PROC [dbo].[CreateNewClass]
    (
    @CourseName VARCHAR(50),
	@CourseCode VARCHAR(5),
	@CourseNumber SMALLINT
    )
AS
    BEGIN
        IF (EXISTS(Select CourseCode, CourseNumber from Classes where CourseCode = @CourseCode AND CourseNumber = @CourseNumber))
			RETURN 1;
        ELSE BEGIN
            INSERT INTO Classes
			VALUES(@CourseName, @CourseCode, @CourseNumber)
			RETURN 0;
        END
    END
GO

--Create Sticker
CREATE PROC [dbo].[CreateSticker]
    (
    @ProblemDescription		NVARCHAR(500),
	@ClassID				INT,
	@StudentID				INT,
	@MinimumStarRanking		FLOAT,
	@Timeout				INT
    )
AS
    BEGIN
		DECLARE @Return INT
        IF (EXISTS(Select StudentID, ClassID from Sticker where StudentID = @StudentID AND ClassID = @ClassID))
			SELECT 0;
		ELSE IF (select count(*) from (select StudentID from Sticker where StudentID = @StudentID and DATEDIFF(second, GETDATE(), Timeout) > 0) as ActiveStickers) > 3
			SELECT 0;
        ELSE BEGIN
            INSERT INTO Sticker
			VALUES(@ProblemDescription, @ClassID, DEFAULT, @StudentID, DEFAULT, @MinimumStarRanking, GETDATE(), DATEADD(second, @Timeout, GETDATE()))
			SET @Return = @@IDENTITY
			SELECT @Return
        END
    END
GO

--CREATE AN OFFICIAL MENTOR
CREATE PROC [dbo].[CreateMentorOrganization]
    (
    @OrganizationName	NVARCHAR(100)
    )
AS
    BEGIN
        IF (EXISTS(Select OrganizationName from OfficialMentor where OrganizationName = @OrganizationName))
			RETURN 1;
        ELSE BEGIN
            INSERT INTO OfficialMentor
			VALUES(@OrganizationName)
			RETURN 0;
        END
    END
GO

CREATE PROC [dbo].[CreateReview]
    (
	@StickerID		INT,
	@ReviewerID		INT,
    @StarRanking	FLOAT,
	@Description	NVARCHAR(250)
    )
AS
    BEGIN
        IF (EXISTS(Select StickerID from Review where StickerID = @StickerID AND ReviewerID = @ReviewerID))
			RETURN 1;
        ELSE BEGIN
            INSERT INTO Review
			VALUES(@StickerID, @ReviewerID, @StarRanking, @Description)
		END
    END
GO

CREATE PROC [dbo].[CreateReport]
    (
	@ReportDescription		NVARCHAR(200),
	@FlaggerID		INT,
    @ReviewID		INT
    )
AS
    BEGIN
        IF (EXISTS(Select ReportID from Report where ReviewID = @ReviewID AND FlaggerID = @FlaggerID))
			RETURN 1;
        ELSE BEGIN
            INSERT INTO Report
			VALUES(@ReportDescription, @FlaggerID, @ReviewID)
			RETURN 0;
        END
    END
GO

CREATE PROC [dbo].[InsertUserIntoMentorProgram]
    (
	@UserID		INT,
	@MentorID	INT
    )
AS
    BEGIN
        if  (EXISTS(Select * from OmToUser WHERE UserID = @UserID AND MentorID = @MentorID))
			RETURN 1;
        ELSE BEGIN
            INSERT INTO OmToUser
			VALUES(@UserID, @MentorID)
			RETURN 0;
        END
    END
GO

CREATE PROC [dbo].[UpdateProfilePicture]
    (
	@UserID		INT,
	@Photo		VARBINARY(MAX)
    )
AS
    BEGIN
        IF (NOT Exists(Select UserID from Picture WHERE UserID = @UserID))
        BEGIN
			INSERT INTO Picture
			VALUES (@UserID, @Photo);
		END
        ELSE BEGIN
			UPDATE Picture
			SET Photo = @Photo
			WHERE @UserID = UserID;
			RETURN 0;
		END
	END
GO

CREATE PROC [dbo].[CreateChat]
    (
	@UserID	INT
    )
AS
	DECLARE @NewChatID INT;
	BEGIN TRY
		BEGIN TRAN;
			INSERT INTO Chat
			DEFAULT VALUES
			
			SET @NewChatID = @@IDENTITY
			
			INSERT INTO UserToChat
			VALUES(@UserID, @NewChatID);
		COMMIT TRAN;
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN;
	END CATCH
	BEGIN
		SELECT @NewChatID
	END
GO

CREATE PROC [dbo].[InsertUserIntoChat]
    (
	@UserID	INT,
	@ChatID	INT
    )
AS
    BEGIN
        IF (Exists(Select ChatID, UserID from UserToChat WHERE UserID = @UserID AND ChatID = @ChatID))
			RETURN 1;
        ELSE BEGIN
            INSERT INTO UserToChat
			VALUES(@UserID, @ChatID)
			RETURN 0;
        END
    END
GO

CREATE PROC [dbo].[InsertMessage]
    (
	@ChatID		INT,
	@Message	NVARCHAR(500),
	@FilePath	VARCHAR(MAX) = null,
	@UserID		INT
    )
AS
    BEGIN
		DECLARE @NewMessageID INT;

        IF (NOT Exists(Select ChatID from UserToChat WHERE ChatID = @ChatID and UserID = @UserID))
			SELECT 1;
        ELSE BEGIN
			INSERT INTO [Messages]
			VALUES (@ChatID, @Message, @FilePath, 0, @UserID, GETDATE())
			SET @NewMessageID = @@IDENTITY
			SELECT @NewMessageID
		END
    END
GO

--CREATE PROC [dbo].[InsertFile]
--    (
--	@ChatID		INT,
--	@FilePath	VARCHAR(MAX),
--	@UserID		INT
--    )
--AS
--    BEGIN
--        IF (NOT Exists(Select ChatID from UserToChat WHERE ChatID = @ChatID and UserID = @UserID))
--			RETURN 1;
--        ELSE BEGIN
--            INSERT INTO [Messages]
--			VALUES(@ChatID, DEFAULT, @FilePath, 1, @UserID, GETDATE())
--			RETURN 0;
--        END
--    END
--GO

CREATE PROC [dbo].[AddFriend]
	(
	@CurrentUserID	INT,
	@NewFriendUserID	INT
	)
AS
	BEGIN
		IF (EXISTS(SELECT UserID, FriendUserID	FROM Friends WHERE UserID = @CurrentUserID AND @NewFriendUserID = FriendUserID))
			RETURN 1;
		ELSE BEGIN
			INSERT INTO Friends
			VALUES (@CurrentUserID, @NewFriendUserID)
			RETURN 0;
		END
	END
GO