
/*********************************************************
--Update Course Code (WRI, MATH, CST)
*********************************************************/
CREATE PROC [dbo].[UpdateCourseCodeByClassID]
    (
    @ClassID INT,
	@CourseCode VARCHAR(5)
    )
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Classes
				SET CourseCode = @CourseCode
				WHERE ClassID = @ClassID;
                return 0;
            END

    END
