CREATE TABLE [dbo].[Server] (
    [ServerID]         INT           IDENTITY (1, 1) NOT NULL,
    [ServerName]       VARCHAR (75)  NOT NULL,
    [ServerIP]         VARCHAR (15)  NOT NULL,
    [ServerDomain]     VARCHAR (50)  DEFAULT (NULL) NULL,
    [SchoolName]       VARCHAR (75)  NOT NULL,
    [AdminUsername]    VARCHAR (30)  DEFAULT ('Admin') NULL,
    [AdminPassword]    VARCHAR (64)  DEFAULT ('Password') NULL,
    [EmailCredentials] NVARCHAR (50) DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([ServerID] ASC)
);

