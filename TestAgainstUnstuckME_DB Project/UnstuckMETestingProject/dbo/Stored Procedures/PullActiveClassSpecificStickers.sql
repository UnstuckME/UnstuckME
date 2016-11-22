create proc PullActiveClassSpecificStickers
(	@Name varchar(50),
	@Code varchar(5),
	@Number smallint,
	@Term tinyint,
	@displayname varchar(61) = null
) as
begin
	select DisplayFName + ' ' + DisplayLName as Student,
		CourseCode /*+ ' ' + convert(varchar, */,CourseNumber/*) + ' - ' + */,CourseName,-- as Course,
		TermOffered, ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
	where CourseName = @Name and CourseCode = @Code and CourseNumber = @Number
		and TermOffered = @Term and
		DisplayFName + ' ' + DisplayLName = case when @displayname is not null
												then @displayname
											end
end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (star ranking, mentor specification, and user
* specification is optional)
**************************************************************************/
