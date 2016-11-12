CREATE TABLE [dbo].[Files] (
    [FileID]   INT             IDENTITY (1, 1) NOT NULL,
    [FileData] VARBINARY (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([FileID] ASC)
);

