create proc GetAllStudentReviews as
begin
	select DisplayFName + ' ' + DisplayLName as Reviewer, StarRanking, Description, Sticker.StickerID, UserID, ReviewID
	from Sticker join UserProfile	on (UserProfile.UserID = Sticker.StudentID) 
		join Review					on Review.ReviewerID = UserProfile.UserID
		WHERE STICKER.StudentID = Review.ReviewerID
		ORDER BY Sticker.StickerID
end;