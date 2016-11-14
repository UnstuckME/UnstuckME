/****************BEGIN CREATION SCRIPTS******************/
--CREATE NEW USER
CREATE PROC [dbo].[CreateNewUser]
    (
    @FirstName VARCHAR(30),
	@LastName VARCHAR(30),
	@EmailAddress VARCHAR(50),
	@Password NVARCHAR(32),
	@Privileges NVARCHAR(32)
    )
AS
    BEGIN
        if  (Exists(Select EmailAddress from UserProfile where EmailAddress = @EmailAddress))
            return 1;
        else
            BEGIN
                INSERT INTO UserProfile
				VALUES(@FirstName, @LastName, @EmailAddress, @Password, @Privileges)
            END

    END
