
/*********************************************************
--Update Server IP Address
*********************************************************/
CREATE PROC [dbo].[UpdateServerIPByServerID]
    (
    @ServerID INT,
	@ServerIP VARCHAR(15)
    )
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET ServerIP = @ServerIP
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
