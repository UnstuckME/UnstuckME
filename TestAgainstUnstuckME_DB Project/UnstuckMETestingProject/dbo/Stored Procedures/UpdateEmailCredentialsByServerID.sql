
/*********************************************************
--Update Server Email Credentials
*********************************************************/
CREATE PROC [dbo].[UpdateEmailCredentialsByServerID]
    (
    @ServerID INT,
	@EmailCredentials NVARCHAR(50)
    )
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET EmailCredentials = @EmailCredentials
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
