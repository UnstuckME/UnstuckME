create proc GetDisplayNameAndEmail
(	@useremail varchar(50),
	@password nvarchar(30)
) as
begin
	select DisplayFName + ' ' + DisplayLName, EmailAddress
	from UserProfile
	where EmailAddress = @useremail and UserPassword = @password;
end;

/**************************************************************************
* Get user's classes
**************************************************************************/
