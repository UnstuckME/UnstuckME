
CREATE PROC [dbo].[CreateServer]
    (
	@ServerName			VARCHAR(75),
	@ServerIP			VARCHAR(15),
	@ServerDomain		VARCHAR(50),
	@SchoolName			VARCHAR(75),
	@AdminUsername		VARCHAR(30),
	@AdminPassword		NVARCHAR(32),
	@EmailCredentials	NVARCHAR(50)
    )
AS
    BEGIN
        if  (Exists(Select ServerName from Server 
					WHERE ServerName = @ServerName))
            return 1;
        else
            BEGIN
                INSERT INTO [Server]
				VALUES(@ServerName, @ServerIP, @ServerDomain, @SchoolName,
						 @AdminUsername, @AdminPassword, @EmailCredentials)
            END

    END
