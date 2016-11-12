
/*********************************************************
--Delete Mentor Organization Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteMentorOrganizationByMentorID]
    (
    @MentorID INT
    )
--RETURNS bit/* datatype */
AS
    BEGIN
        if  (NOT Exists(Select MentorID from OfficialMentor where MentorID = @MentorID))
            return 1;
        else
            BEGIN
				DELETE OfficialMentor
				WHERE MentorID = @mentorID;
                return 0;
            END

    END
