create proc GetUserStickersAndReviews
(	@useremail varchar(50),
	@password nvarchar(30)
) as 
begin
		select Sticker.SubmitTime, Sticker.Timeout, Sticker.ProblemDescription,
			Sticker.MinimumStarRanking, Review.StarRanking, Review.Description
	from UserProfile join Sticker		on Sticker.StudentID = UserProfile.UserID
		join Review						on Review.StickerID = Sticker.StickerID
	where EmailAddress = @useremail and UserPassword = @password;
end;

/**************************************************************************
* Get Organizations user is apart of
**************************************************************************/
