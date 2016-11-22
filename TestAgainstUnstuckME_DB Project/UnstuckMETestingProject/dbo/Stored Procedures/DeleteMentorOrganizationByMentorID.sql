
/*********************************************************
--Delete Mentor Organization Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteMentorOrganizationByMentorID]
    (
    @MentorID INT
    )
AS
    BEGIN
        if  (NOT Exists(Select MentorID from OfficialMentor where MentorID = @MentorID))
            return 1;
        else
            BEGIN
				DELETE OmToUser
				WHERE @MentorID = MentorID;
				DELETE OfficialMentor
				WHERE MentorID = @MentorID;
                return 0;
            END

    END
