CREATE TABLE [dbo].[Messages] (
    [MessageID]   INT            IDENTITY (1, 1) NOT NULL,
    [ChatID]      INT            NOT NULL,
    [MessageData] NVARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([MessageID] ASC),
    FOREIGN KEY ([ChatID]) REFERENCES [dbo].[Chat] ([ChatID])
);

