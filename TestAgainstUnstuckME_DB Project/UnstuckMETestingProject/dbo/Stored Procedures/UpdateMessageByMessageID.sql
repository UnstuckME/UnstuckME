
/*********************************************************
--Update Chat Message
*********************************************************/
CREATE PROC [dbo].[UpdateMessageByMessageID]
    (
    @MessageID INT,
	@MessageData NVARCHAR(500)
    )
AS
    BEGIN
        if  (NOT Exists(Select MessageID from [Messages] where MessageID = @MessageID))
            return 1;
        else
            BEGIN
				UPDATE [Messages]
				SET MessageData = @MessageData
				WHERE MessageID = @MessageID;
                return 0;
            END

    END
