
--Photo
/********************************NEED MORE INFO ON HOW WE ARE STORING PHOTOS*******************************/

/*********************************************************
--Update Mentor Organization Name
*********************************************************/
CREATE PROC [dbo].[MentorNameByMentorID]
    (
    @MentorID INT,
	@OrganizationName NVARCHAR(50)
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select MentorID from OfficialMentor where MentorID = @MentorID))
            return 1;
        else
            BEGIN
				UPDATE OfficialMentor
				SET OrganizationName = @OrganizationName
				WHERE MentorID = @MentorID;
                return 0;
            END

    END
