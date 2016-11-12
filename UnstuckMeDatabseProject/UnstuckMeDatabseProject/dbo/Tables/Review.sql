CREATE TABLE [dbo].[Review] (
    [ReviewID]    INT            IDENTITY (1, 1) NOT NULL,
    [ReportID]    INT            DEFAULT (NULL) NULL,
    [StarRanking] FLOAT (53)     DEFAULT (NULL) NULL,
    [Description] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([ReviewID] ASC),
    FOREIGN KEY ([ReportID]) REFERENCES [dbo].[Report] ([ReportID])
);

