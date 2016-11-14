
/*********************************************************
--Update User Email Address
*********************************************************/
CREATE PROC [dbo].[UpdateEmailAddressByUserID]
    (
    @UserID INT,
	@EmailAddress VARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET EmailAddress = @EmailAddress
				WHERE UserID = @userID;
                return 0;
            END

    END
