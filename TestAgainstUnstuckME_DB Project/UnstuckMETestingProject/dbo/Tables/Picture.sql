CREATE TABLE [dbo].[Picture] (
    [UserID] INT             NOT NULL,
    [Photo]  VARBINARY (MAX) NULL,
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[UserProfile] ([UserID])
);

