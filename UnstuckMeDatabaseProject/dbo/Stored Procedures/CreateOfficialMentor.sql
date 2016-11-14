
--CREATE AN OFFICIAL MENTOR
CREATE PROC [dbo].[CreateOfficialMentor]
    (
    @OrganizationName	NVARCHAR(50)
    )
AS
    BEGIN
        if  (Exists(Select OrganizationName from OfficialMentor 
					where OrganizationName = @OrganizationName))
            return 1;
        else
            BEGIN
                INSERT INTO OfficialMentor
				VALUES(@OrganizationName)
            END

    END
