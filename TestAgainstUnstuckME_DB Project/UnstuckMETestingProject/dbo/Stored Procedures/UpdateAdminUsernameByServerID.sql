
/*********************************************************
--Update Admin Username
*********************************************************/
CREATE PROC [dbo].[UpdateAdminUsernameByServerID]
    (
    @ServerID INT,
	@AdminUsername VARCHAR(30)
    )
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET AdminUsername = @AdminUsername
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
