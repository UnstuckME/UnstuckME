
/*********************************************************
--Update Sticker Problem Description
*********************************************************/
CREATE PROC [dbo].[UpdateStickerProblemDescriptionByStickerID]
    (
    @StickerID INT,
	@ProblemDescription NVARCHAR(500)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select StickerID from Sticker where StickerID = @StickerID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET ProblemDescription = @ProblemDescription
				WHERE StickerID = @StickerID;
                return 0;
            END

    END
