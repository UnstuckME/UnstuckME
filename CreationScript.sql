USE UnstuckME_DB;
GO

--DROP TRIGGER ON UserPRofile UserPassword
IF OBJECT_ID ('HashPassword', 'TR') IS NOT NULL  
	DROP TRIGGER HashPassword;  
	GO

--DROP ANY PRE-EXISTING TABLES
IF OBJECT_ID('Sticker', 'U') IS NOT NULL
	DROP TABLE Sticker;
IF OBJECT_ID('Review', 'U') IS NOT NULL
	DROP TABLE Review;
IF OBJECT_ID('Report', 'U') IS NOT NULL
	DROP TABLE Report;
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
IF OBJECT_ID('UserProfile', 'U') IS NOT NULL
	DROP TABLE UserProfile;
IF OBJECT_ID('OfficialMentor', 'U') IS NOT NULL
	DROP TABLE OfficialMentor;
IF OBJECT_ID('Chat', 'U') IS NOT NULL
	DROP TABLE Chat;
IF OBJECT_ID('Files', 'U') IS NOT NULL
	DROP TABLE Files;
IF OBJECT_ID('Messages', 'U') IS NOT NULL
	DROP TABLE [Messages];
IF OBJECT_ID('Server', 'U') IS NOT NULL
	DROP TABLE [Server];

--Create Server Table
CREATE TABLE [Server]
	(ServerID			INT				PRIMARY KEY IDENTITY(1,1),
	ServerName			VARCHAR(75)			NOT NULL,
	ServerIP			VARCHAR(15)			NOT NULL,	
	ServerDomain			VARCHAR(50)			DEFAULT NULL,
	SchoolName			VARCHAR(75)			NOT NULL,
	AdminUsername			VARCHAR(30)			DEFAULT 'Admin',	
	AdminPassword			VARCHAR(64)			DEFAULT 'Password',
	EmailCredentials		NVARCHAR(50)			DEFAULT NULL)

--Create Messages Table
CREATE TABLE [Messages]
	(MessageID			INT				PRIMARY KEY IDENTITY(1,1),
	MessageData			NVARCHAR(500)			NOT NULL)

--Create File Table
CREATE TABLE Files
	(FileID				INT				PRIMARY KEY IDENTITY(1,1),
	FileData			VARBINARY(MAX) 			NOT NULL);

--Create Chat Table
CREATE TABLE Chat
	(ChatID				INT				PRIMARY KEY IDENTITY(1,1),
	MessageID			INT				DEFAULT NULL REFERENCES [Messages](MessageID),
	FileID				INT				DEFAULT NULL REFERENCES Files(FileID))
		
--Create Official Mentor Table
CREATE TABLE OfficialMentor
	(MentorID			INT				PRIMARY KEY IDENTITY(1,1),
	OrganizationName		NVARCHAR(50)			NOT NULL)

--Create UserProfile Table
CREATE TABLE UserProfile
	(UserID					INT				PRIMARY KEY IDENTITY(1,1),
	DisplayFName			VARCHAR(30)			NOT NULL,
	DisplayLName			VARCHAR(30)			NOT NULL,
	EmailAddress			VARCHAR(50)			NOT	NULL UNIQUE, 
	UserPassword			NVARCHAR(32)		NOT NULL,
	Privileges				BINARY(4)			NOT NULL)

--Create Official Mentor Table
CREATE TABLE OmToUser
	(UserID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	MentorID			INT				NOT NULL	REFERENCES OfficialMentor(MentorID)
	PRIMARY KEY(UserID, MentorID))

--Create UserToChat Table
CREATE TABLE UserToChat
	(UserID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	 ChatID				INT				NOT NULL	REFERENCES Chat(ChatID)
	PRIMARY KEY (UserID, ChatID))

--Create Classes Table
CREATE TABLE Picture
	(UserID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	Photo				VARBINARY(MAX)			NULL)	

--Create Classes Table
CREATE TABLE Classes
	(ClassID			INT				PRIMARY KEY IDENTITY(1,1),
	CourseName			VARCHAR(50)			NOT NULL,	--Common Name
	CourseCode			VARCHAR(5)			NOT NULL,	--Ex. WRI
	CourseNumber			SMALLINT			NOT NULL,	--Ex. 121
	TermOffered			TINYINT				NOT NULL)

--Create User To Class Table
CREATE TABLE UserToClass
	(UserID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	ClassID				INT				NOT NULL	REFERENCES Classes(ClassID)
	PRIMARY KEY (UserID, ClassID))

--Create Report Table
CREATE TABLE Report
	(ReportID			INT				PRIMARY KEY IDENTITY(1,1),
	 FlaggerID			INT				DEFAULT NULL	REFERENCES UserProfile(UserID))

--Create Review Table
CREATE TABLE Review
	(ReviewID			INT				PRIMARY KEY IDENTITY(1,1),
	ReportID			INT				DEFAULT NULL	REFERENCES Report(ReportID),
	StarRanking			FLOAT				DEFAULT NULL,
	[Description]			NVARCHAR(250)			NULL)

--Create Sticker Table
CREATE TABLE Sticker
	(StickerID			INT				PRIMARY KEY IDENTITY(1,1),
	ProblemDescription		NVARCHAR(500)			NOT NULL,
	ClassID				INT				NOT NULL	REFERENCES Classes(ClassID),
	StudentID			INT				NOT NULL	REFERENCES UserProfile(UserID),
	TutorID				INT				NOT NULL	REFERENCES UserProfile(UserID),
	StudentReviewID			INT				NULL		REFERENCES Review(ReviewID),
	TutorReviewID			INT				NULL		REFERENCES Review(ReviewID),
	MinimumStarRanking		FLOAT				DEFAULT 0.0,
	SubmitTime			DATETIME			NOT NULL,
	[Timeout]			DATETIME2			NOT NULL)

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
		End;
GO