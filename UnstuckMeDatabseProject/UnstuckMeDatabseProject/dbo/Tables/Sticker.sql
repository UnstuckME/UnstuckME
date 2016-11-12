CREATE TABLE [dbo].[Sticker] (
    [StickerID]          INT            IDENTITY (1, 1) NOT NULL,
    [ProblemDescription] NVARCHAR (500) NOT NULL,
    [ClassID]            INT            NOT NULL,
    [StudentID]          INT            NOT NULL,
    [TutorID]            INT            NOT NULL,
    [StudentReviewID]    INT            NULL,
    [TutorReviewID]      INT            NULL,
    [MinimumStarRanking] FLOAT (53)     DEFAULT ((0.0)) NULL,
    [SubmitTime]         DATETIME       NOT NULL,
    [Timeout]            DATETIME2 (7)  NOT NULL,
    PRIMARY KEY CLUSTERED ([StickerID] ASC),
    FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Classes] ([ClassID]),
    FOREIGN KEY ([StudentID]) REFERENCES [dbo].[UserProfile] ([UserID]),
    FOREIGN KEY ([StudentReviewID]) REFERENCES [dbo].[Review] ([ReviewID]),
    FOREIGN KEY ([TutorID]) REFERENCES [dbo].[UserProfile] ([UserID]),
    FOREIGN KEY ([TutorReviewID]) REFERENCES [dbo].[Review] ([ReviewID])
);

