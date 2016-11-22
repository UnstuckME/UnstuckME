
/******************************************************************
--Update TutorID by TutorID and StickerID Procedure Creation Script
******************************************************************/
CREATE PROC [dbo].[UpdateTutorIDByTutorIDAndStickerID]
    (
	@TutorID	INT,
    @StickerID	INT
    )
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET TutorID = @TutorID
				WHERE StickerID = @StickeriD;
                return 0;
            END

    END
