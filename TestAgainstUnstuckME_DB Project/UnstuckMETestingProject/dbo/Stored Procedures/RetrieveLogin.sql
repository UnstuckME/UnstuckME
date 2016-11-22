create proc RetrieveLogin
(	@useremail varchar(50)	) as
begin
	select UserPassword
	from UserProfile
	where EmailAddress = @useremail;
end;

/**************************************************************************
* Pull all active Stickers (user specification is optional)
**************************************************************************/
