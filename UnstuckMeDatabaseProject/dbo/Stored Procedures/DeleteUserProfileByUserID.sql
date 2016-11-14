
/*********************************************************
--Delete User Profile Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteUserProfileByUserID]
    (
    @UserID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
                DELETE FROM UserProfile
				WHERE UserID = @UserID;
                return 0;
            END

    END
