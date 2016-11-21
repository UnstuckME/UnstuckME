
CREATE PROC [dbo].[CreateReview]
    (
	@StickerID		INT,
	@ReviewerID		INT,
    @StarRanking	FLOAT,
	@Description	NVARCHAR(250)
    )
AS
    BEGIN
        if  (Exists(Select StickerID from Review 
					where StickerID = @StickerID AND ReviewerID = @ReviewerID))
            return 1;
        else
            BEGIN
                INSERT INTO Review
				VALUES(@StickerID, @ReviewerID, @StarRanking, @Description)
            END

    END
