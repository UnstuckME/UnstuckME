create proc GetAllActiveStickers
(	@displayname varchar(61) = null	) as
begin
	select DisplayFName + ' ' + DisplayLName as Student,
		CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		TermOffered, ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
	where datediff(minute, getdate(), Timeout) > 0 and
		DisplayFName + ' ' + DisplayLName = case when @displayname is not null
												then @displayname
											end
end;

/**************************************************************************
* Pull active Stickers available for a certain class a user may be associated
* with (user specification is optional)
**************************************************************************/
