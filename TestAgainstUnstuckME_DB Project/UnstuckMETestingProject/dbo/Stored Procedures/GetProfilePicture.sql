create proc GetProfilePicture
(	@useremail varchar(50) ) as
begin
	select Photo
	from Picture join UserProfile	on UserProfile.UserID = Picture.UserID
	where EmailAddress = @useremail
end;

/**************************************************************************
* Get username, email address
**************************************************************************/
