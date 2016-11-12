
/*********************************************************
--Delete Report Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteReportByReportID]
    (
    @ReportID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ReportID from Report where ReportID = @ReportID))
            return 1;
        else
            BEGIN
				DELETE Report
				WHERE ReportID = @reportID;
                return 0;
            END

    END
