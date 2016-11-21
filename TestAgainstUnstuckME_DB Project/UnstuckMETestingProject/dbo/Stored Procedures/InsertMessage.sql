
CREATE PROC [dbo].[InsertMessage]
    (
	@ChatID		INT,
	@Message	NVARCHAR(500)
    )
AS
    BEGIN
        if  (NOT Exists(Select ChatID from Chat 
					WHERE ChatID = @ChatID))
            return 1;
        else
            BEGIN
                INSERT INTO [Messages]
				VALUES(@ChatID, @Message)
            END

    END
