USE UnstuckME_DB
GO

--DROP TRIGGER ON UserPRofile UserPassword
IF OBJECT_ID ('HashPassword', 'TR') IS NOT NULL  
	DROP TRIGGER HashPassword;  
	GO
IF OBJECT_ID ('HashPasswordAdmin', 'TR') IS NOT NULL  
	DROP TRIGGER HashPasswordAdmin;  
	GO
IF OBJECT_ID ('ForbidUser1Update', 'TR') IS NOT NULL  
	DROP TRIGGER ForbidUser1Update;  
	GO
IF OBJECT_ID ('ForbidUser1Delete', 'TR') IS NOT NULL  
	DROP TRIGGER ForbidUser1Delete;  
	GO
IF OBJECT_ID ('ForbidClass1Delete', 'TR') IS NOT NULL  
	DROP TRIGGER ForbidClass1Delete;  
	GO
IF OBJECT_ID ('ForbidClass1Update', 'TR') IS NOT NULL  
	DROP TRIGGER ForbidClass1Update;  
	GO
IF OBJECT_ID ('OnlyAllowCorrectReviews', 'TR') IS NOT NULL
	DROP TRIGGER OnlyAllowCorrectReviews
	GO

--Drop Views
IF OBJECT_ID('AllUsers_View', 'VIEW') IS NOT NULL
	DROP VIEW AllUsers_View
	GO
IF OBJECT_ID('AllStickers_View', 'VIEW') IS NOT NULL
	DROP VIEW AllStickers_View
	GO
IF OBJECT_ID('AllMentorPrograms_View', 'VIEW') IS NOT NULL
	DROP VIEW AllMentorPrograms_View
	GO

--DROP ANY PRE-EXISTING TABLES
IF OBJECT_ID('Report', 'U') IS NOT NULL
	DROP TABLE Report;
IF OBJECT_ID('Review', 'U') IS NOT NULL
	DROP TABLE Review;
IF OBJECT_ID('Sticker', 'U') IS NOT NULL
	DROP TABLE Sticker;
IF OBJECT_ID('UserToClass', 'U') IS NOT NULL
	DROP TABLE UserToClass;
IF OBJECT_ID('Classes', 'U') IS NOT NULL
	DROP TABLE Classes;
IF OBJECT_ID('Picture', 'U') IS NOT NULL
	DROP TABLE Picture;
IF OBJECT_ID('OmToUser', 'U') IS NOT NULL
	DROP TABLE OmToUser;
IF OBJECT_ID('UserToChat', 'U') IS NOT NULL
	DROP TABLE UserToChat;
IF OBJECT_ID('OfficialMentor', 'U') IS NOT NULL
	DROP TABLE OfficialMentor;
IF OBJECT_ID('Messages', 'U') IS NOT NULL
	DROP TABLE [Messages];
IF OBJECT_ID('Files', 'U') IS NOT NULL
	DROP TABLE Files;
IF OBJECT_ID('UserProfile', 'U') IS NOT NULL
	DROP TABLE UserProfile;
IF OBJECT_ID('Chat', 'U') IS NOT NULL
	DROP TABLE Chat;
IF OBJECT_ID('Server', 'U') IS NOT NULL
	DROP TABLE [Server];

--Create Server Table
CREATE TABLE [Server]
	(ServerID			INT					PRIMARY KEY IDENTITY(1,1),
	ServerName			VARCHAR(75)			NOT NULL,
	ServerIP			VARCHAR(15)			NOT NULL,	
	ServerDomain		VARCHAR(50)			DEFAULT NULL,
	SchoolName			VARCHAR(75)			NOT NULL,
	AdminUsername		VARCHAR(30)			DEFAULT 'Admin',	
	AdminPassword		NVARCHAR(32)		DEFAULT 'Password',
	EmailCredentials	NVARCHAR(50)		DEFAULT NULL,
	Salt				VARBINARY(256)		NOT NULL UNIQUE)
GO

--Create Chat Table
CREATE TABLE Chat
	(ChatID				INT				PRIMARY KEY IDENTITY(1,1))
GO

--Create UserProfile Table
CREATE TABLE UserProfile
	(UserID					INT					PRIMARY KEY IDENTITY(1,1),
	DisplayFName			VARCHAR(32)			NOT NULL,
	DisplayLName			VARCHAR(32)			NOT NULL,
	EmailAddress			VARCHAR(50)			NOT NULL UNIQUE, 
	UserPassword			NVARCHAR(32)		NOT NULL,
	Privileges				NVARCHAR(32)		NOT NULL,
	Salt					VARBINARY(256)		NOT NULL UNIQUE)
GO

--Create Messages Table
CREATE TABLE [Messages]
	(MessageID				INT				PRIMARY KEY IDENTITY(1,1),
	ChatID					INT				NOT NULL REFERENCES Chat(ChatID),
	MessageData				NVARCHAR(500)	NOT NULL,
	SentBy					int				NOT NULL REFERENCES UserProfile(UserID),
	SentTime				smalldatetime	NOT NULL)
GO

--Create File Table
CREATE TABLE Files
	(FileID				INT					PRIMARY KEY IDENTITY(1,1),
	ChatID				INT					NOT NULL REFERENCES Chat(ChatID),
	FileData			VARBINARY(MAX) 		NOT NULL,
	SentBy				int					NOT NULL REFERENCES UserProfile(UserID),
	SentTime			smalldatetime		NOT NULL);
GO

--Create Official Mentor Table
CREATE TABLE OfficialMentor
	(MentorID			INT				PRIMARY KEY IDENTITY(1,1),
	OrganizationName	NVARCHAR(50)			NOT NULL)
GO



--Create Official Mentor Table
CREATE TABLE OmToUser
	(UserID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	MentorID			INT				NOT NULL	REFERENCES OfficialMentor(MentorID)
	PRIMARY KEY(UserID, MentorID))
GO

--Create UserToChat Table
CREATE TABLE UserToChat
	(UserID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	 ChatID				INT				NOT NULL	REFERENCES Chat(ChatID)
	PRIMARY KEY (UserID, ChatID))
GO

--Create Classes Table
CREATE TABLE Picture
	(UserID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	Photo				VARBINARY(MAX)			NULL
	PRIMARY KEY (UserID))	
GO

--Create Classes Table
CREATE TABLE Classes
	(ClassID			INT				PRIMARY KEY IDENTITY(1,1),
	CourseName			VARCHAR(50)			NOT NULL,	--Common Name
	CourseCode			VARCHAR(5)			NOT NULL,	--Ex. WRI
	CourseNumber		SMALLINT			NOT NULL,	--Ex. 121
	TermOffered			TINYINT				NOT NULL)
GO

--Create User To Class Table
CREATE TABLE UserToClass
	(UserID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	ClassID				INT				NOT NULL	REFERENCES Classes(ClassID)
	PRIMARY KEY (UserID, ClassID))
GO

--Create Sticker Table
CREATE TABLE Sticker
	(StickerID			INT								PRIMARY KEY IDENTITY(1,1),
	ProblemDescription	NVARCHAR(500)	NOT NULL,
	ClassID				INT				NOT NULL		REFERENCES Classes(ClassID),
	StudentID			INT				NOT NULL		REFERENCES UserProfile(UserID),
	TutorID				INT				DEFAULT NULL	REFERENCES UserProfile(UserID),
	--StudentReviewID		INT				NULL			REFERENCES Review(ReviewID),
	--TutorReviewID		INT				NULL			REFERENCES Review(ReviewID),
	MinimumStarRanking	FLOAT			DEFAULT 0.0,
	SubmitTime			DATETIME		NOT NULL,
	[Timeout]			DATETIME2		NOT NULL)
GO

	--Create Review Table
CREATE TABLE Review
	(ReviewID			INT					PRIMARY KEY IDENTITY(1,1),
	--ReportID			INT					DEFAULT NULL	REFERENCES Report(ReportID),
	StickerID			INT					NOT NULL	REFERENCES Sticker(StickerID),
	ReviewerID			INT					NOT NULL	REFERENCES UserProfile(UserID),
	StarRanking			FLOAT				DEFAULT NULL,
	[Description]		NVARCHAR(250)		NULL)
GO

--Create Report Table
CREATE TABLE Report
	(ReportID			INT				PRIMARY KEY IDENTITY(1,1),
	ReportDescription	NVARCHAR(200)	DEFAULT NULL,
	FlaggerID			INT				DEFAULT NULL	REFERENCES UserProfile(UserID),
	ReviewID			INT				NOT NULL		REFERENCES Review(ReviewID))
GO

--Create Indexes on tables
CREATE INDEX MessagesSentByUserIndex on Messages (SentBy);
CREATE INDEX MessageChatIDIndex on Messages (ChatID);
CREATE INDEX FilesSentByUserIndex on Files (SentBy);
CREATE INDEX FileChatIDIndex on Files (ChatID);
CREATE INDEX ReportFlaggerIDIndex on Report(FlaggerID);
CREATE INDEX ReportReviewIDIndex on Report (ReviewID);
CREATE INDEX ReviewStickerIDIndex on Review (StickerID);
CREATE INDEX ReviewReviewerIDIndex on Review (ReviewerID);
CREATE INDEX StickerClassIDIndex on Sticker (ClassID);
CREATE INDEX StickerStudentIDIndex on Sticker (StudentID);
CREATE INDEX StickerTutorIDIndex on Sticker (TutorID);
GO

--Create Views
CREATE VIEW AllUsers_View AS
	select DisplayFName + ' ' + DisplayLName as 'User', EmailAddress, Privileges
	from UserProfile
GO

CREATE VIEW AllStickers_View AS
	select DisplayFName + ' ' + DisplayLName as Student, EmailAddress,
		CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Classes.ClassID = Sticker.ClassID
GO

CREATE VIEW AllMentorPrograms_View AS
	select OrganizationName
	from OfficialMentor
GO

/*******************************************************************************
INSERT DEFAULT USER
*******************************************************************************/
INSERT INTO UserProfile
VALUES ('Unknown', 'User', 'Invalid Email Address', 'NO_PASSWORD', 'NO_PRIVILEGES', CONVERT(varbinary(256), 0));
GO
/*******************************************************************************
INSERT DEFAULT CLASS
*******************************************************************************/
INSERT INTO Classes
VALUES ('Class Unavailable', 'NA', 0, 0);
GO
--Create HashingPassword trigger on UserProfile Password
CREATE 
	TRIGGER HashPassword
	ON UserProfile
	AFTER INSERT, UPDATE
	AS
		Begin
			UPDATE UserProfile
			Set UserPassword = CONVERT(NVARCHAR(32),HashBytes('MD5', (SELECT UserPassword from UserProfile WHERE UserID IN (SELECT UserID from inserted))),2)
			WHERE  UserID IN (SELECT UserID from inserted)
		End;
GO

CREATE 
	TRIGGER HashPasswordAdmin
	ON Server
	AFTER INSERT, UPDATE
	AS
		Begin
			UPDATE Server
			Set AdminPassword = CONVERT(NVARCHAR(32),HashBytes('MD5', (SELECT AdminPassword from [Server] WHERE ServerID IN (SELECT ServerID from inserted))),2)
			WHERE ServerID IN (SELECT ServerID from inserted)
		End;
GO

CREATE
	TRIGGER ForbidUser1Delete
	ON UserProfile
	AFTER DELETE
	AS
		BEGIN
		IF exists(SELECT UserID FROM deleted WHERE UserID = 1 )
			BEGIN
				ROLLBACK TRANSACTION;
			END;
		END;
GO

CREATE
	TRIGGER ForbidUser1Update
	ON UserProfile
	AFTER UPDATE
	AS
		BEGIN
		IF exists(SELECT UserID FROM inserted WHERE UserID = 1 )
			BEGIN
				ROLLBACK TRANSACTION;
			END;
		END;
GO


CREATE
	TRIGGER ForbidClass1Delete
	ON Classes
	AFTER DELETE
	AS
		BEGIN
		IF exists(SELECT ClassID FROM deleted WHERE ClassID = 1)
		BEGIN
			ROLLBACK TRANSACTION
			END;
		END;
GO

CREATE
	TRIGGER ForbidClass1Update
	ON Classes
	AFTER INSERT
	AS
		BEGIN
		IF exists(SELECT ClassID FROM inserted WHERE ClassID = 1)
		BEGIN
			ROLLBACK TRANSACTION
			END;
		END;
GO

CREATE
	TRIGGER OnlyAllowCorrectReviews
	ON Review
	AFTER INSERT
	AS
		BEGIN --BEGIN TRIGGER
			DECLARE @tempStickerID as INT, @insertedReviewerID as INT;
			SELECT @tempStickerID = (SELECT StickerID FROM inserted);
			SELECT @insertedReviewerID = (SELECT ReviewerID FROM inserted);

			IF NOT (((SELECT tutorID FROM Sticker WHERE StickerID = @tempStickerID) is NOT NULL)
				and
				((@insertedReviewerID = (SELECT StudentID FROM Sticker WHERE StickerID = @tempStickerID)) or (@insertedReviewerID = (SELECT TutorID FROM Sticker WHERE StickerID = @tempStickerID)))
				and
				((SELECT COUNT(*) FROM Review WHERE ReviewerID = @insertedReviewerID and StickerID = @tempStickerID) = 1))
				BEGIN
					PRINT('There is not asscoaiated tutor with this sticker yet');
					ROLLBACK TRANSACTION;
				END;
		END;-- END TRIGGER
GO