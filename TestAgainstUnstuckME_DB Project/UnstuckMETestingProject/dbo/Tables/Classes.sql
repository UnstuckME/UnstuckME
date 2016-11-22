CREATE TABLE [dbo].[Classes] (
    [ClassID]      INT          IDENTITY (1, 1) NOT NULL,
    [CourseName]   VARCHAR (50) NOT NULL,
    [CourseCode]   VARCHAR (5)  NOT NULL,
    [CourseNumber] SMALLINT     NOT NULL,
    [TermOffered]  TINYINT      NOT NULL,
    PRIMARY KEY CLUSTERED ([ClassID] ASC)
);


GO


CREATE
	TRIGGER ForbidClass1Delete
	ON Classes
	AFTER DELETE
	AS
		BEGIN
		IF exists(SELECT ClassID FROM deleted WHERE ClassID = 1)
		BEGIN
			ROLLBACK TRANSACTION
			END;
		END;

GO

CREATE
	TRIGGER ForbidClass1Update
	ON Classes
	AFTER INSERT
	AS
		BEGIN
		IF exists(SELECT ClassID FROM inserted WHERE ClassID = 1)
		BEGIN
			ROLLBACK TRANSACTION
			END;
		END;
