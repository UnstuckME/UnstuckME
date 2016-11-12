CREATE TABLE [dbo].[Report] (
    [ReportID]  INT IDENTITY (1, 1) NOT NULL,
    [FlaggerID] INT DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([ReportID] ASC),
    FOREIGN KEY ([FlaggerID]) REFERENCES [dbo].[UserProfile] ([UserID])
);

