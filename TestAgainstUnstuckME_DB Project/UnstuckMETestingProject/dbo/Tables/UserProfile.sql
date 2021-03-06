﻿CREATE TABLE [dbo].[UserProfile] (
    [UserID]       INT           IDENTITY (1, 1) NOT NULL,
    [DisplayFName] VARCHAR (30)  NOT NULL,
    [DisplayLName] VARCHAR (30)  NOT NULL,
    [EmailAddress] VARCHAR (50)  NOT NULL,
    [UserPassword] NVARCHAR (32) NOT NULL,
    [Privileges]   NVARCHAR (32) NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([EmailAddress] ASC)
);


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
