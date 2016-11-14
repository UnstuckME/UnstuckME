
/*********************************************************
--Delete User Profile Picture Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteUserPictureByUserID]
    (
    @UserID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from Picture where UserID = @UserID))
            return 1;
        else
            BEGIN
                DELETE Picture
				WHERE UserID = @UserID;
                return 0;
            END

    END
