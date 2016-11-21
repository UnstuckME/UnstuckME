create proc ViewAllUsers as
begin
	select DisplayFName + ' ' + DisplayLName as 'User', EmailAddress, Privileges
	from UserProfile
end;

/**************************************************************************
* View all classes
**************************************************************************/
