
/*********************************************************
--Delete Review Description Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[ClearReviewDescriptionByReviewID]
    (
    @ReviewID INT
    )
AS
    BEGIN
        if  (NOT Exists(Select ReviewID from Review where ReviewID = @ReviewID))
            return 1;
        else
            BEGIN
				UPDATE Review
				SET [Description] = ''
				WHERE ReviewID = @reviewID;
                return 0;
            END

    END
