create proc GetAllStickers as
begin
	select DisplayFName + ' ' + DisplayLName as Student, EmailAddress,
		CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Classes.ClassID = Sticker.ClassID
end;

/**************************************************************************
* Gets all Student Reviews for Admin
**************************************************************************/
