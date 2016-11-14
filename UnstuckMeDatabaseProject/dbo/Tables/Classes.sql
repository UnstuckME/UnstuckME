CREATE TABLE [dbo].[Classes] (
    [ClassID]      INT          IDENTITY (1, 1) NOT NULL,
    [CourseName]   VARCHAR (50) NOT NULL,
    [CourseCode]   VARCHAR (5)  NOT NULL,
    [CourseNumber] SMALLINT     NOT NULL,
    [TermOffered]  TINYINT      NOT NULL,
    PRIMARY KEY CLUSTERED ([ClassID] ASC)
);

