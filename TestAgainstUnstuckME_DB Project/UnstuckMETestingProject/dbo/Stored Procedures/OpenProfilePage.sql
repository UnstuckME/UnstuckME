create proc OpenProfilePage
(	@useremail varchar(50),
	@password nvarchar(30)
) as
begin
	exec GetProfilePicture @useremail;

	exec GetDisplayNameAndEmail @useremail, @password;

	exec GetUserClasses @useremail, @password;

	exec GetUserStickersAndReviews @useremail, @password;

	exec GetUserOrganizations @useremail, @password;
end;

/**************************************************************************
* Filter reviews based on a >= star ranking
**************************************************************************/
