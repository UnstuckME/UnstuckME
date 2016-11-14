/**********************************************************
***********************************************************
***********INSERTION STORED PROCEDURE SCRIPTS**************
***********************************************************
**********************************************************/

USE UnstuckME_DB;
GO

/******DROP PROCEDURE STATEMENTS*************************/
DROP PROC [dbo].[InsertNewUser];
GO

/****************BEGIN CREATION SCRIPTS******************/
CREATE PROC [dbo].[InsertNewUser]
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
GO

CREATE PROC [dbo].[InsertNewClass]
    (
    @CourseName VARCHAR(50),
	@CourseCode VARCHAR(5),
	@CourseNumber SMALLINT,
	@TermOffered TINYINT
    )
AS
    BEGIN
        if  (Exists(Select CourseCode, CourseNumber from Classes 
					where CourseCode = @CourseCode AND CourseNumber = @CourseNumber))
            return 1;
        else
            BEGIN
                INSERT INTO Classes
				VALUES(@CourseName, @LastName, @EmailAddress, @Password, @Privileges)
            END

    END
GO
