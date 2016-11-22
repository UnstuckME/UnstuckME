
/*********************************************************
--Delete Class Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteClassByClassID]
    (
    @ClassID INT
    )
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET ClassID = 1
				WHERE ClassID = @ClassID;
				DELETE UserToClass
				WHERE ClassID = @ClassID
				DELETE Classes
				WHERE ClassID = @classID;
                return 0;
            END

    END
