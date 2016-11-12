CREATE TABLE [dbo].[UserProfile] (
    [UserID]       INT          IDENTITY (1, 1) NOT NULL,
    [DisplayFName] VARCHAR (30) NOT NULL,
    [DisplayLName] VARCHAR (30) NOT NULL,
    [EmailAddress] VARCHAR (50) NOT NULL,
    [UserPassword] BINARY (64)  NOT NULL,
    [Privileges]   BINARY (4)   NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

