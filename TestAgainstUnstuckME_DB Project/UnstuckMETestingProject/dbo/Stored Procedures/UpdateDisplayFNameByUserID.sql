
/*********************************************************
--Update User First Name
*********************************************************/
CREATE PROC [dbo].[UpdateDisplayFNameByUserID]
    (
    @UserID INT,
	@DisplayFName VARCHAR(30)
    )
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET DisplayFName = @DisplayFName
				WHERE UserID = @userID;
                return 0;
            END

    END
