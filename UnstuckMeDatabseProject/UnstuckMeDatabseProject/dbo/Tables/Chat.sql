CREATE TABLE [dbo].[Chat] (
    [ChatID]    INT IDENTITY (1, 1) NOT NULL,
    [MessageID] INT DEFAULT (NULL) NULL,
    [FileID]    INT DEFAULT (NULL) NULL,
    PRIMARY KEY CLUSTERED ([ChatID] ASC),
    FOREIGN KEY ([FileID]) REFERENCES [dbo].[Files] ([FileID]),
    FOREIGN KEY ([MessageID]) REFERENCES [dbo].[Messages] ([MessageID])
);

