CREATE TABLE [dbo].[Server] (
    [ServerID]         INT           IDENTITY (1, 1) NOT NULL,
    [ServerName]       VARCHAR (75)  NOT NULL,
    [ServerIP]         VARCHAR (15)  NOT NULL,
    [ServerDomain]     VARCHAR (50)  DEFAULT (NULL) NULL,
    [SchoolName]       VARCHAR (75)  NOT NULL,
    [AdminUsername]    VARCHAR (30)  DEFAULT ('Admin') NULL,
    [AdminPassword]    NVARCHAR (32) DEFAULT ('Password') NULL,
    [EmailCredentials] NVARCHAR (50) DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([ServerID] ASC)
);


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
