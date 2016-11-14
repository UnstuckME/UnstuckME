
/*********************************************************
--Delete Sticker Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteStickerByStickerID]
    (
    @StickerID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				DELETE Sticker
				WHERE StickerID = @stickerID;
                return 0;
            END

    END
