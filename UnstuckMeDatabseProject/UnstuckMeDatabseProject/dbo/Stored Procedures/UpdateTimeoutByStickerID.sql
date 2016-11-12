
/*********************************************************
--Update Timeout Time
*********************************************************/
CREATE PROC [dbo].[UpdateTimeoutByStickerID]
    (
    @StickerID INT,
	@Timeout DATETIME2
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET [Timeout] = @Timeout
				WHERE StickerID = @StickerID;
                return 0;
            END

    END
