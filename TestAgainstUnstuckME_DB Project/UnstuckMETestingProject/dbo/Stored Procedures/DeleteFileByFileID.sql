
/*********************************************************
--Delete File Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteFileByFileID]
    (
    @FileID INT
    )
AS
    BEGIN
        if  (NOT Exists(Select FileID from Files where FileID = @FileID))
            return 1;
        else
            BEGIN
                DELETE Files
				WHERE FileID = @fileID;
                return 0;
            END

    END
