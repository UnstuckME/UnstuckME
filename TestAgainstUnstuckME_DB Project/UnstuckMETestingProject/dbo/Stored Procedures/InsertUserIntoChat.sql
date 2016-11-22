
CREATE PROC [dbo].[InsertUserIntoChat]
    (
	@UserID	INT,
	@ChatID	INT
    )
AS
    BEGIN
        if  (Exists(Select ChatID, UserID from UserToChat 
					WHERE UserID = @UserID AND ChatID = @ChatID))
            return 1;
        else
            BEGIN
                INSERT INTO UserToChat
				VALUES(@UserID, @ChatID)
            END

    END
