
/*********************************************************
--Delete Review Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteReviewByReviewID]
    (
    @ReviewID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ReviewID from Review where ReviewID = @ReviewID))
            return 1;
        else
            BEGIN
				DELETE Review
				WHERE ReviewID = @reviewID;
                return 0;
            END

    END
