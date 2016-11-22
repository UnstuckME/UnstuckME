
CREATE PROC [dbo].[InsertFile]
    (
	@ChatID		INT,
	@FileData	VARBINARY(MAX)
    )
AS
    BEGIN
        if  (NOT Exists(Select ChatID from Chat 
					WHERE ChatID = @ChatID))
            return 1;
        else
            BEGIN
                INSERT INTO Files
				VALUES(@ChatID, @FileData)
            END

    END
