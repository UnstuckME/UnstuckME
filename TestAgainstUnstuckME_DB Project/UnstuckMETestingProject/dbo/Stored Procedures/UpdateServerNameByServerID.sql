
--********Update********
/*********************************************************
--Update Server Name
*********************************************************/
CREATE PROC [dbo].[UpdateServerNameByServerID]
    (
    @ServerID INT,
	@ServerName VARCHAR(75)
    )
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET ServerName = @ServerName
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
