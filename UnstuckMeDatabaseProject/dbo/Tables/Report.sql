CREATE TABLE [dbo].[Report] (
    [ReportID]          INT            IDENTITY (1, 1) NOT NULL,
    [ReportDescription] NVARCHAR (200) DEFAULT (NULL) NULL,
    [FlaggerID]         INT            DEFAULT (NULL) NULL,
    [ReviewID]          INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ReportID] ASC),
    FOREIGN KEY ([FlaggerID]) REFERENCES [dbo].[UserProfile] ([UserID]),
    FOREIGN KEY ([ReviewID]) REFERENCES [dbo].[Review] ([ReviewID])
);

