
/*********************************************************
--Update Admin Password
*********************************************************/
CREATE PROC [dbo].[UpdateAdminPasswordByServerID]
    (
    @ServerID INT,
	@AdminPassword NVARCHAR(32)
    )
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET AdminPassword = @AdminPassword
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
