
/*********************************************************
--Update Terms Offered
*********************************************************/
CREATE PROC [dbo].[UpdateTermsOfferedByClassID]
    (
    @ClassID INT,
	@TermOffered TINYINT
    )
AS
    BEGIN
        if  (NOT Exists(Select ClassID from Classes where ClassID = @ClassID))
            return 1;
        else
            BEGIN
				UPDATE Classes
				SET TermOffered = @TermOffered
				WHERE ClassID = @ClassID;
                return 0;
            END

    END
