
--Create New Class
CREATE PROC [dbo].[CreateNewClass]
    (
    @CourseName VARCHAR(50),
	@CourseCode VARCHAR(5),
	@CourseNumber SMALLINT,
	@TermOffered TINYINT
    )
AS
    BEGIN
        if  (Exists(Select CourseCode, CourseNumber from Classes 
					where CourseCode = @CourseCode AND CourseNumber = @CourseNumber))
            return 1;
        else
            BEGIN
                INSERT INTO Classes
				VALUES(@CourseName, @CourseCode, @CourseNumber, @TermOffered)
            END

    END
