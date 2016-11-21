
/*********************************************************
--Update Course Number
*********************************************************/
CREATE PROC [dbo].[UpdateCourseNumberByClassID]
    (
    @ClassID INT,
	@CourseNumber SMALLINT
    )
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Classes
				SET CourseNumber = @CourseNumber
				WHERE ClassID = @ClassID;
                return 0;
            END

    END
