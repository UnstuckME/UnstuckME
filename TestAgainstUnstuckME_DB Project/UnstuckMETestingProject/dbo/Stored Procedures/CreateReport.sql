
CREATE PROC [dbo].[CreateReport]
    (
	@ReportDescription		NVARCHAR(200),
	@FlaggerID		INT,
    @ReviewID		FLOAT
    )
AS
    BEGIN
        if  (Exists(Select ReportID from Report 
					where ReviewID = @ReviewID AND FlaggerID = @FlaggerID))
            return 1;
        else
            BEGIN
                INSERT INTO Report
				VALUES(@ReportDescription, @FlaggerID, @ReviewID)
            END

    END
