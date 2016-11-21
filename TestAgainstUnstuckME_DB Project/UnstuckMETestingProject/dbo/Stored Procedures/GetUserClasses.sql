create proc GetUserClasses
(	@useremail varchar(50),
	@password nvarchar(30)
) as
begin
	select Classes.CourseName, Classes.CourseCode, Classes.CourseNumber
	from UserProfile join UserToClass	on UserProfile.UserID = UserToClass.UserID
		join Classes					on UserToClass.ClassID = Classes.ClassID
	where EmailAddress = @useremail and UserPassword = @password;
end;

/**************************************************************************
* Get Stickers with reviews submitted by user
**************************************************************************/
