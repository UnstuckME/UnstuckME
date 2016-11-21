
/*********************************************************
--Delete User Profile Procedure Creation Script
*********************************************************/
CREATE PROC [dbo].[DeleteUserProfileByUserID]
    (
    @UserID INT
    )
AS
    BEGIN
        if  (NOT Exists(Select UserID from UserProfile where UserID = @UserID))
            return 1;
        else
            BEGIN
				UPDATE Sticker
				SET StudentID = 1
				WHERE StudentID = @UserID;
				UPDATE Sticker
				SET TutorID = 1
				WHERE TutorID = @UserID;
				UPDATE Report
				SET FlaggerID = 1
				WHERE FlaggerID = @UserID;
				UPDATE Review
				SET ReviewerID = 1
				WHERE ReviewerID = @UserID;
				DELETE FROM UserToClass
				WHERE UserID = @UserID;
				DELETE FROM Picture
				WHERE UserID =@UserID;
				DELETE FROM OmToUser
				WHERE UserID = @UserID;
				DELETE FROM UserToChat
				WHERE UserID = @UserID;
                DELETE FROM UserProfile
				WHERE UserID = @UserID;
                return 0;
            END

    END
