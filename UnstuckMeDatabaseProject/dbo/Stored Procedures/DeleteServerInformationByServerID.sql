
--START CREATION SCRIPTS
/*********************************************************/
CREATE PROC [dbo].[DeleteServerInformationByServerID]
    (
    @ServerID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select ServerID from [Server] where ServerID = @ServerID))
            return 1;
        else
            BEGIN
                DELETE FROM [Server]
				WHERE ServerID = @ServerID;
                return 0;
            END

    END
