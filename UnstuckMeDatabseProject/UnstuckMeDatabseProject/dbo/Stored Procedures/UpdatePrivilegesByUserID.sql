
/*********************************************************
--Update User Privileges
*********************************************************/
CREATE PROC [dbo].[UpdatePrivilegesByUserID]
    (
    @UserID INT,
	@Privileges BINARY(4)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET Privileges = @Privileges
				WHERE UserID = @userID;
                return 0;
            END

    END
