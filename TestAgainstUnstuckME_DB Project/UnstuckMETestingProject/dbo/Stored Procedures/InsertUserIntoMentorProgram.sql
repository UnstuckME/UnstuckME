
CREATE PROC [dbo].[InsertUserIntoMentorProgram]
    (
	@UserID		INT,
	@MentorID	INT
    )
AS
    BEGIN
        if  (Exists(Select * from OmToUser 
					WHERE UserID = @UserID AND MentorID = @MentorID))
            return 1;
        else
            BEGIN
                INSERT INTO OmToUser
				VALUES(@UserID, @MentorID)
            END

    END
