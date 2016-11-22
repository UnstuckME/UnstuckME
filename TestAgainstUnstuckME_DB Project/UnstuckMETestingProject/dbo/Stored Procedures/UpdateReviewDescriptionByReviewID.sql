
/*********************************************************
--Update Review Description
*********************************************************/
CREATE PROC [dbo].[UpdateReviewDescriptionByReviewID]
    (
    @ReviewID INT,
	@Description NVARCHAR(250)
    )
AS
    BEGIN
        if  (NOT Exists(Select ReviewID from Review where ReviewID = @ReviewID))
            return 1;
        else
            BEGIN
				UPDATE Review
				SET [Description] = @Description
				WHERE ReviewID = @ReviewID;
                return 0;
            END

    END
