CREATE TABLE [dbo].[Files] (
    [FileID]   INT             IDENTITY (1, 1) NOT NULL,
    [ChatID]   INT             NOT NULL,
    [FileData] VARBINARY (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([FileID] ASC),
    FOREIGN KEY ([ChatID]) REFERENCES [dbo].[Chat] ([ChatID])
);

