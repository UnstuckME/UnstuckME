CREATE TABLE [dbo].[OmToUser] (
    [UserID]   INT NOT NULL,
    [MentorID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC, [MentorID] ASC),
    FOREIGN KEY ([MentorID]) REFERENCES [dbo].[OfficialMentor] ([MentorID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[UserProfile] ([UserID])
);

