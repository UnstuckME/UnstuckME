
/* INSERTS A STUDENT AND A CLASS INTO UserToClass */
CREATE PROC [dbo].[InsertStudentIntoClass]
    (
    @UserID		INT,
	@ClassID	INT
    )
AS
    BEGIN
        if  (Exists(Select UserID, ClassID from UserToClass 
					where UserID = @UserID AND ClassID = @ClassID))
            return 1;
        else
            BEGIN
                INSERT INTO UserToClass
				VALUES(@UserID, @ClassID)
            END

    END
