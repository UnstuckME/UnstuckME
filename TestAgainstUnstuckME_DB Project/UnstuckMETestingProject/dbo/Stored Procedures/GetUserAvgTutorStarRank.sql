create proc GetUserAvgTutorStarRank
(	@displayname varchar(61)	) as
begin
	select avg(StarRanking) as AvgStarRank
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentID = Review.ReviewerID
	where DisplayFName + ' ' + DisplayLName = @displayname
end;

/**************************************************************************
* Pull chat messages and files between two users
**************************************************************************/
