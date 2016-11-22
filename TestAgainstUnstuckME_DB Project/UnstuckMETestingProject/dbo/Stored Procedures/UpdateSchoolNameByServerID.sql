
/*********************************************************
--Update School Name
*********************************************************/
CREATE PROC [dbo].[UpdateSchoolNameByServerID]
    (
    @ServerID INT,
	@SchoolName VARCHAR(75)
    )
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
				UPDATE [Server]
				SET SchoolName = @SchoolName
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
