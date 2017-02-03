USE UnstuckME_DB
GO

-- Drop Triggers
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
IF OBJECT_ID ('DisallowSameTutorAndStudentID', 'TR') IS NOT NULL
	DROP TRIGGER DisallowSameTutorAndStudentID
	GO

IF OBJECT_ID ('DisallowDeletionOfAllAdmin', 'TR') IS NOT NULL
	DROP TRIGGER DisallowDeletionOfAllAdmin
	GO

--DROP ANY PRE-EXISTING TABLES
IF OBJECT_ID('ServerAdmins', 'U') IS NOT NULL
	DROP TABLE ServerAdmins;
IF OBJECT_ID('StickerToMentor', 'U') IS NOT NULL
	DROP TABLE StickerToMentor;
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
--IF OBJECT_ID('Files', 'U') IS NOT NULL
--	DROP TABLE Files;
IF OBJECT_ID('Friends', 'U') IS NOT NULL
	DROP TABLE Friends;
IF OBJECT_ID('UserProfile', 'U') IS NOT NULL
	DROP TABLE UserProfile;
IF OBJECT_ID('Chat', 'U') IS NOT NULL
	DROP TABLE Chat;

CREATE TABLE ServerAdmins
	(ServerAdminID			INT				PRIMARY KEY IDENTITY(1,1),
	EmailAddress			VARCHAR(50)		NOT NULL UNIQUE,
	FirstName				VARCHAR(32)		NOT NULL,
	LastName				VARCHAR(32)		NOT NULL,
	[Password]				NVARCHAR(256)	NOT NULL,
	Salt					NVARCHAR(256)	NOT NULL UNIQUE)
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
	UserPassword			NVARCHAR(256)		NOT NULL,
	AverageStudentRank		FLOAT				DEFAULT(5)  NOT NULL,
	AverageTutorRank		FLOAT				DEFAULT(5)	NOT NULL,
	TotalTutorReviews       INT                 DEFAULT(1)  NOT NULL,
	TotalStudentReviews		INT					DEFAULT(1)  NOT NULL,
	Privileges				INT					DEFAULT(3)	NOT NULL,
	Salt					NVARCHAR(256)		NOT NULL UNIQUE)
GO

--Create Friends List Table
CREATE TABLE Friends
	(FriendID				INT				PRIMARY KEY IDENTITY(1,1),
	UserID					INT				NOT NULL REFERENCES UserProfile(UserID),
	FriendUserID			INT				NOT NULL)

--Create Messages Table
CREATE TABLE [Messages]
	(MessageID				INT				PRIMARY KEY IDENTITY(1,1),
	ChatID					INT				NOT NULL REFERENCES Chat(ChatID),
	MessageData				NVARCHAR(500)	DEFAULT NULL, 
	FilePath				VARCHAR(MAX)	DEFAULT NULL,  
	IsFile                  BIT				NOT NULL DEFAULT(0),                
	SentBy					INT				NOT NULL REFERENCES UserProfile(UserID),
	SentTime				SMALLDATETIME	NOT NULL)
GO

--Create File Table
--CREATE TABLE Files
--	(FileID				INT					PRIMARY KEY IDENTITY(1,1),
--	ChatID				INT					NOT NULL REFERENCES Chat(ChatID),
--	FileData			VARBINARY(MAX) 		NOT NULL,
--	SentBy				INT					NOT NULL REFERENCES UserProfile(UserID),
--	SentTime			SMALLDATETIME		NOT NULL);
--GO

--Create Official Mentor Table
CREATE TABLE OfficialMentor
	(MentorID			INT				PRIMARY KEY IDENTITY(1,1),
	OrganizationName	NVARCHAR(100)			NOT NULL)
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
	CourseName			VARCHAR(100)		NOT NULL,
	CourseCode			VARCHAR(5)			NOT NULL,	--Ex. WRI
	CourseNumber		SMALLINT			NOT NULL)
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
	ChatID				INT				DEFAULT NULL	REFERENCES Chat(ChatID),
	StudentID			INT				NOT NULL		REFERENCES UserProfile(UserID),
	TutorID				INT				DEFAULT NULL	REFERENCES UserProfile(UserID),
	MinimumStarRanking	FLOAT			DEFAULT 0.0,
	SubmitTime			DATETIME		NOT NULL,
	[Timeout]			DATETIME2		NOT NULL)
GO

CREATE TABLE StickerToMentor
(	StickerID			INT				NOT NULL		REFERENCES Sticker(StickerID),
	MentorID			INT				NOT NULL		REFERENCES OfficialMentor(MentorID)
	PRIMARY KEY (StickerID, MentorID))
GO
	--Create Review Table
CREATE TABLE Review
	(ReviewID			INT					PRIMARY KEY IDENTITY(1,1),
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
CREATE INDEX MessagesSentByUserIndex on [Messages] (SentBy);
CREATE INDEX MessageChatIDIndex on [Messages] (ChatID);
CREATE INDEX ReportFlaggerIDIndex on Report(FlaggerID);
CREATE INDEX ReportReviewIDIndex on Report (ReviewID);
CREATE INDEX ReviewStickerIDIndex on Review (StickerID);
CREATE INDEX ReviewReviewerIDIndex on Review (ReviewerID);
CREATE INDEX StickerClassIDIndex on Sticker (ClassID);
CREATE INDEX StickerStudentIDIndex on Sticker (StudentID);
CREATE INDEX StickerTutorIDIndex on Sticker (TutorID);
CREATE INDEX UserProfileEmailAddress on UserProfile (EmailAddress);
GO

/*******************************************************************************
INSERT DEFAULT USER
*******************************************************************************/
INSERT INTO UserProfile
VALUES ('Unknown', 'User', 'Invalid Email Address', 'NO_PASSWORD', DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT, 0);
GO
/*******************************************************************************
INSERT DEFAULT CLASS
*******************************************************************************/
INSERT INTO Classes
VALUES ('Class Unavailable', 'NA', 0);
GO

/*******************************************************************************
INSERT DEFAULT ServerAdmin
*******************************************************************************/
INSERT INTO ServerAdmins
VALUES ('Admin', 'John', 'Doe', '20024519787285415818223418423415125157246207175561872721374197129248198221206128122231254', 'salt');
GO

/*******************************************************************************
Create DB TRIGGERS
*******************************************************************************/

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

CREATE 
	TRIGGER DisallowSameTutorAndStudentID
	ON Sticker
	AFTER INSERT, UPDATE
	AS
		BEGIN
			IF EXISTS (SELECT * FROM inserted WHERE StudentID = TutorID)
			BEGIN
				ROLLBACK TRANSACTION;
			END;
		END;
GO

CREATE
	TRIGGER DisallowDeletionOfAllAdmin
	ON ServerAdmins
	AFTER DELETE
	AS
		BEGIN
			If NOT EXISTS (SELECT * FROM ServerAdmins)
			BEGIN
				ROLLBACK TRANSACTION;
			END;
		END;
GO
