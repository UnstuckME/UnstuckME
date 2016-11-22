create proc ViewAllClasses as 
begin
	select CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		TermOffered
	from Classes
end;

/**************************************************************************
* View all organizations
**************************************************************************/
