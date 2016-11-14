
CREATE PROC [dbo].[ChangeProfilePicture]
    (
	@UserID		INT,
	@Photo		VARBINARY(MAX)
    )
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile 
					WHERE UserID = @UserID))
            return 1;
        else
            BEGIN
                INSERT INTO Picture
				VALUES(@UserID, @Photo)
            END

    END
