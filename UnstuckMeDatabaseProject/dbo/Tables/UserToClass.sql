CREATE TABLE [dbo].[UserToClass] (
    [UserID]  INT NOT NULL,
    [ClassID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC, [ClassID] ASC),
    FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Classes] ([ClassID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[UserProfile] ([UserID])
);

