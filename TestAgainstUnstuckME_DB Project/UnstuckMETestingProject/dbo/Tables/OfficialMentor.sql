CREATE TABLE [dbo].[OfficialMentor] (
    [MentorID]         INT           IDENTITY (1, 1) NOT NULL,
    [OrganizationName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([MentorID] ASC)
);

