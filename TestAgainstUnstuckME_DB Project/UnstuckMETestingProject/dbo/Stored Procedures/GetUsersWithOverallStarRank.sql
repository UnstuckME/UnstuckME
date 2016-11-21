create proc GetUsersWithOverallStarRank
(	@starrank float	) as
begin
	declare @totalrank float;
	set @totalrank = (select StarRanking
					  from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
						join Review					on Sticker.StudentID = Review.ReviewerID)
									+
					(select StarRanking
					 from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
						join Review					on Sticker.StudentID = Review.ReviewerID);

	select DisplayFName + ' ' + DisplayLName as 'User', EmailAddress,
		avg(@totalrank) as AvgStarRank
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentID = Review.ReviewerID
	group by DisplayFName + ' ' + DisplayLName, EmailAddress
	having avg(@totalrank) >= 3--@starrank
end;

/**************************************************************************
* View a user's avg student rating
**************************************************************************/
