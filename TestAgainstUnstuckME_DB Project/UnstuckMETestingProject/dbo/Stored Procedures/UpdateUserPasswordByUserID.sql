
/*********************************************************
--Update User Password
*********************************************************/
CREATE PROC [dbo].[UpdateUserPasswordByUserID]
    (
    @UserID INT,
	@UserPassword NVARCHAR(32)
    )
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET UserPassword = @UserPassword
				WHERE UserID = @userID;
                return 0;
            END

    END
