CREATE PROC [dbo].[UpdateTimeoutByStickerIDAndSeconds]
    (
    @StickerID INT,
	@Seconds INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET [Timeout] = DATEADD(second, @Seconds, GETDATE())
				WHERE StickerID = @StickerID;
                return 0;
            END

    END
