USE UnstuckME_Schools;
GO

/*************************************************************************
* Drops the tables if they already exist on the database
*************************************************************************/
IF OBJECT_ID('SchoolLogo', 'U') IS NOT NULL
	DROP TABLE SchoolLogo;
IF OBJECT_ID('Database', 'U') IS NOT NULL
	DROP TABLE [Database];
IF OBJECT_ID('Server', 'U') IS NOT NULL
	DROP TABLE [Server];
IF OBJECT_ID('School', 'U') IS NOT NULL
	DROP TABLE School;



/*************************************************************************
* School table with name and email credentials
*************************************************************************/
CREATE TABLE School	(
	SchoolID				INT				PRIMARY KEY IDENTITY(1,1),
	SchoolName 				NVARCHAR(128)	NOT NULL,
	EmailCredentials		VARCHAR(64)		DEFAULT NULL, 
	SchoolAdminUsername		NVARCHAR(256)   DEFAULT 'admin',
	SchoolAdminPassword		NVARCHAR(256)	DEFAULT  '20024519787285415818223418423415125157246207175561872721374197129248198221206128122231254',
	Salt					NVARCHAR(256)	NOT NULL DEFAULT 'salt')

/*************************************************************************
* Server table containing the domain, identifier for the school it belongs
* to, the name, and the IP address. Schools can have multiple servers, but
* a server cannot belong to multiple schools
*************************************************************************/
CREATE TABLE [Server]	(
	ServerID			INT				PRIMARY KEY IDENTITY(1,1),
	SchoolID			INT				NOT NULL REFERENCES School(SchoolID),
	ServerDomain		VARCHAR(15)		DEFAULT NULL,
	ServerName			NVARCHAR(128)	NOT NULL UNIQUE,
	ServerIPAddress		VARCHAR(39)		NOT NULL UNIQUE)

/*************************************************************************
* Databse table containing the domain, identifier for the school it belongs
* to, the name, and the IP address. Schools can have multiple servers, but
* a server cannot belong to multiple schools
*************************************************************************/
CREATE TABLE [Database]	
(
	DatabaseID					INT				PRIMARY KEY IDENTITY(1,1),
	SchoolID					INT				NOT NULL REFERENCES School(SchoolID),
	DatabaseName				NVARCHAR(128)	NOT NULL,
	DatabaseIP					VARCHAR(39)		NOT NULL UNIQUE,
	DatabaseAdminUsername		NVARCHAR(256)	NOT NULL,
	DatabaseUsingWindowsAuthen	BIT				DEFAULT (0)	/*toggle this to 1 if you want to use windows authenication*/
)

/*************************************************************************
* Logo table containing the logo of the school. A school can only have one
* logo, and a logo can belong to only one school
*************************************************************************/
CREATE TABLE SchoolLogo	
(
	LogoID		INT				NOT NULL UNIQUE REFERENCES School(SchoolID),
	Logo		VARBINARY(MAX)	NOT NULL,
	LastModified DATETIME		NULL	DEFAULT(SYSDATETIME()),		
	PRIMARY KEY(LogoID)
)
GO

CREATE TRIGGER updateToLogo
ON [SchoolLogo]
AFTER UPDATE
AS
	UPDATE [SchoolLogo]
	SET LastModified = SYSDATETIME()
	FROM Inserted i
	WHERE SchoolLogo.LogoID = i.LogoID