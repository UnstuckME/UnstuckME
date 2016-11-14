
--Create Sticker
CREATE PROC [dbo].[CreateSticker]
    (
    @ProblemDescription		NVARCHAR(500),
	@ClassID				INT,
	@StudentID				INT,
	@MinimumStarRanking		FLOAT,
	@Timeout				INT
    )
AS
    BEGIN
        if  (Exists(Select StudentID, ClassID from Sticker 
					where StudentID = @StudentID AND ClassID = @ClassID))
            return 1;
        else
            BEGIN
                INSERT INTO Sticker (ProblemDescription, ClassID, StudentID, MinimumStarRanking, SubmitTime, [Timeout])
				VALUES(@ProblemDescription, @ClassID, @StudentID, @MinimumStarRanking, GETDATE(), DATEADD(second, @Timeout, GETDATE()))
            END

    END
