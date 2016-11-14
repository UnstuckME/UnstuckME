CREATE PROC [dbo].[UpdateEmailCredentialsByServerID]
    (
    @ServerID INT,
	@EmailCredentials NVARCHAR(50)
    )
--RETURNS bit/* datatype */
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
