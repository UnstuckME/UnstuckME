create proc FilterUserReviewsByEqualStarRank
(	@starfilter float,
	@displayname varchar(61) = null
) as	
begin
	select DisplayFName + ' ' + DisplayLName as 'User', StarRanking, Description
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StickerID = Review.StickerID
	where StarRanking = @starfilter and
		@displayname = case when @displayname is not null
						then 'User'
					end
end;

/**************************************************************************
* User forgot login information
**************************************************************************/
