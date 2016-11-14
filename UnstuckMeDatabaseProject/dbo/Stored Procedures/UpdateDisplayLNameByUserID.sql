
/*********************************************************
--Update User Last Name
*********************************************************/
CREATE PROC [dbo].[UpdateDisplayLNameByUserID]
    (
    @UserID INT,
	@DisplayLName VARCHAR(30)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE UserProfile
				SET DisplayLName = @DisplayLName
				WHERE UserID = @userID;
                return 0;
            END

    END
