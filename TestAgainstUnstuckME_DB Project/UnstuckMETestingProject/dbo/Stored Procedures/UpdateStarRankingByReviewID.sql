
/*********************************************************
--Update Review Star Ranking
*********************************************************/
CREATE PROC [dbo].[UpdateStarRankingByReviewID]
    (
    @ReviewID INT,
	@StarRanking TINYINT
    )
AS
    BEGIN
        if  (NOT Exists(Select ReviewID from Review where ReviewID = @ReviewID))
            return 1;
        else
            BEGIN
				UPDATE Review
				SET StarRanking = @StarRanking
				WHERE ReviewID = @ReviewID;
                return 0;
            END

    END
