/**********************************************************
***********************************************************
***********INSERTION STORED PROCEDURE SCRIPTS**************
***********************************************************
**********************************************************/

USE UnstuckME_DB;
GO

/******DROP PROCEDURE STATEMENTS*************************/
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
IF OBJECT_ID('CreateOfficialMentor') is not null
	DROP PROCEDURE CreateOfficialMentor;
IF OBJECT_ID('CreateReview') is not null
	DROP PROCEDURE CreateReview;
IF OBJECT_ID('CreateReport') is not null
	DROP PROCEDURE CreateReport;
IF OBJECT_ID('InsertProfilePicture') is not null
	DROP PROCEDURE InsertProfilePicture;
IF OBJECT_ID('ChangeProfilePicture') is not null
	DROP PROCEDURE ChangeProfilePicture;
IF OBJECT_ID('CreateChat') is not null
	DROP PROCEDURE CreateChat;
IF OBJECT_ID('InsertUserIntoChat') is not null
	DROP PROCEDURE InsertUserIntoChat;
IF OBJECT_ID('InsertMessage') is not null
	DROP PROCEDURE InsertMessage;
IF OBJECT_ID('InsertFile') is not null
	DROP PROCEDURE InsertFile;
IF OBJECT_ID('AddFriend') is not null
	DROP PROCEDURE AddFriend;
IF OBJECT_ID('InsertUserIntoMentorProgram') is not null
	DROP PROCEDURE [InsertUserIntoMentorProgram];

GO
/****************BEGIN CREATION SCRIPTS******************/
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
		ELSE
			INSERT INTO ServerAdmins
			VALUES(@EmailAddress, @FirstName, @LastName, @Password, @Salt)
			RETURN 0;
	END
GO

--CREATE NEW USER
CREATE PROC [dbo].[CreateNewUser]
    (
    @FirstName VARCHAR(30),
	@LastName VARCHAR(30),
	@EmailAddress VARCHAR(50),
	@Password NVARCHAR(256),
	@Privileges NVARCHAR(32),
	@Salt NVARCHAR(256)
    )
AS
    BEGIN
        if  (Exists(Select EmailAddress from UserProfile where EmailAddress = @EmailAddress))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
                INSERT INTO UserProfile
				VALUES(@FirstName, @LastName, @EmailAddress, @Password, @Privileges, @Salt)
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
        if  (Exists(Select UserID, ClassID from UserToClass 
					where UserID = @UserID AND ClassID = @ClassID))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
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
	@CourseNumber SMALLINT,
	@TermOffered TINYINT
    )
AS
    BEGIN
        if  (Exists(Select CourseCode, CourseNumber from Classes 
					where CourseCode = @CourseCode AND CourseNumber = @CourseNumber))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
                INSERT INTO Classes
				VALUES(@CourseName, @CourseCode, @CourseNumber, @TermOffered)
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
        if  (Exists(Select StudentID, ClassID from Sticker 
					where StudentID = @StudentID AND ClassID = @ClassID))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
                INSERT INTO Sticker (ProblemDescription, ClassID, StudentID, MinimumStarRanking, SubmitTime, [Timeout])
				VALUES(@ProblemDescription, @ClassID, @StudentID, @MinimumStarRanking, GETDATE(), DATEADD(second, @Timeout, GETDATE()))
				RETURN 0;
            END

    END
GO

--CREATE AN OFFICIAL MENTOR
CREATE PROC [dbo].[CreateOfficialMentor]
    (
    @OrganizationName	NVARCHAR(100)
    )
AS
    BEGIN
        if  (Exists(Select OrganizationName from OfficialMentor 
					where OrganizationName = @OrganizationName))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
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
        if  (Exists(Select StickerID from Review 
					where StickerID = @StickerID AND ReviewerID = @ReviewerID))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
                INSERT INTO Review
				VALUES(@StickerID, @ReviewerID, @StarRanking, @Description)
			END

    END
GO

CREATE PROC [dbo].[CreateReport]
    (
	@ReportDescription		NVARCHAR(200),
	@FlaggerID		INT,
    @ReviewID		FLOAT
    )
AS
    BEGIN
        if  (Exists(Select ReportID from Report 
					where ReviewID = @ReviewID AND FlaggerID = @FlaggerID))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
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
        if  (Exists(Select * from OmToUser 
					WHERE UserID = @UserID AND MentorID = @MentorID))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
                INSERT INTO OmToUser
				VALUES(@UserID, @MentorID)
				RETURN 0;
            END

    END
GO

CREATE PROC [dbo].[InsertProfilePicture]
    (
	@UserID		INT,
	@Photo		VARBINARY(MAX)
    )
AS
    BEGIN
        if  (NOT Exists(Select UserID from Picture
					WHERE UserID = @UserID))
            BEGIN
				INSERT INTO Picture
				VALUES (@UserID, @Photo);
			END
        else
            BEGIN
				UPDATE Picture
				SET Photo = @Photo
				WHERE @UserID = UserID;
				RETURN 0;
            END

    END
GO

CREATE PROC [dbo].[ChangeProfilePicture]
    (
	@UserID		INT,
	@Photo		VARBINARY(MAX)
    )
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile
					WHERE UserID = @UserID))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
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
        if  (Exists(Select ChatID, UserID from UserToChat 
					WHERE UserID = @UserID AND ChatID = @ChatID))
            BEGIN
				RETURN 1;
			END
        else
            BEGIN
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
	@UserID		INT
    )
AS
    BEGIN
        if  (NOT Exists(Select ChatID from UserToChat 
					WHERE ChatID = @ChatID and UserID = @UserID))
            BEGIN
				return 1;
			END
        else
            BEGIN
               INSERT INTO [Messages]
				VALUES(@ChatID, @Message, @UserID, GETDATE())
				RETURN 0;
            END
    END
GO

CREATE PROC [dbo].[InsertFile]
    (
	@ChatID		INT,
	@FileData	VARBINARY(MAX),
	@UserID		INT
    )
AS
    BEGIN
        if  (NOT Exists(Select ChatID from UserToChat 
					WHERE ChatID = @ChatID and UserID = @UserID))
            BEGIN
				return 1;
			END
        else
            BEGIN
                INSERT INTO Files
				VALUES(@ChatID, @FileData, @UserID, GETDATE())
				RETURN 0;
            END
    END
GO

CREATE PROC [dbo].[AddFriend]
	(
	@CurrentUserID	INT,
	@NewFriendUserID	INT
	)
AS
	BEGIN
		IF	(EXISTS(SELECT UserID, FriendUserID	FROM Friends 
						WHERE UserID = @CurrentUserID AND @NewFriendUserID = FriendUserID))
						BEGIN
							RETURN 1;
						END
		ELSE
			BEGIN
				INSERT INTO Friends
					VALUES (@CurrentUserID, @NewFriendUserID)
					RETURN 0;
			END
	END
GO