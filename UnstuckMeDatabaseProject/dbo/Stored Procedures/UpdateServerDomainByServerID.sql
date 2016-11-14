
/*********************************************************
--Update Server Domain Name
*********************************************************/
CREATE PROC [dbo].[UpdateServerDomainByServerID]
    (
    @ServerID INT,
	@ServerDomain VARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET ServerDomain = @ServerDomain
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
