CREATE TABLE [dbo].[UserToChat] (
    [UserID] INT NOT NULL,
    [ChatID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC, [ChatID] ASC),
    FOREIGN KEY ([ChatID]) REFERENCES [dbo].[Chat] ([ChatID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[UserProfile] ([UserID])
);

