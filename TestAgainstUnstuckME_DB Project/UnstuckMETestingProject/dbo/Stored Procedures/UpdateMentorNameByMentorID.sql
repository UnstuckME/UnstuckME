
--Photo
/********************************NEED MORE INFO ON HOW WE ARE STORING PHOTOS*******************************/

/*********************************************************
--Update Mentor Organization Name
*********************************************************/
CREATE PROC [dbo].[UpdateMentorNameByMentorID]
    (
    @MentorID INT,
	@OrganizationName NVARCHAR(50)
    )
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
