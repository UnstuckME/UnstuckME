
/*********************************************************
--Delete Message Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteMessageByMessageID]
    (
    @MessageID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select MessageID from [Messages] where MessageID = @MessageID))
            return 1;
        else
            BEGIN
                DELETE [Messages]
				WHERE MessageID = @messageID;
                return 0;
            END

    END
