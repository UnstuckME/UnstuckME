CREATE TABLE [dbo].[Review] (
    [ReviewID]    INT            IDENTITY (1, 1) NOT NULL,
    [StickerID]   INT            NOT NULL,
    [ReviewerID]  INT            NOT NULL,
    [StarRanking] FLOAT (53)     DEFAULT (NULL) NULL,
    [Description] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([ReviewID] ASC),
    FOREIGN KEY ([ReviewerID]) REFERENCES [dbo].[UserProfile] ([UserID]),
    FOREIGN KEY ([StickerID]) REFERENCES [dbo].[Sticker] ([StickerID])
);

