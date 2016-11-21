create proc GetUserAvgStudentStarRank
(	@displayname varchar(61)	) as
begin
	select avg(StarRanking) as AvgStarRank
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentID = Review.ReviewerID
	where DisplayFName + ' ' + DisplayLName = @displayname
end;

/**************************************************************************
* View a user's avg tutor rating
**************************************************************************/
