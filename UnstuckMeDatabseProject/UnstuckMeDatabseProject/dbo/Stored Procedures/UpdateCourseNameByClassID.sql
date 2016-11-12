
/*********************************************************
--Update Common Course Name
*********************************************************/
CREATE PROC [dbo].[UpdateCourseNameByClassID]
    (
    @ClassID INT,
	@CourseName VARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Classes
				SET CourseName = @CourseName
				WHERE ClassID = @ClassID;
                return 0;
            END

    END
