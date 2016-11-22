create proc GetUserOrganizations
(	@useremail varchar(50),
	@password nvarchar(30)
) as
begin
	select OrganizationName
	from UserProfile join OmToUser		on UserProfile.UserID = OmToUser.UserID
		join OfficialMentor				on OfficialMentor.MentorID = OmToUser.MentorID
	where EmailAddress = @useremail and UserPassword = @password;
end;

/**************************************************************************
* User opens profile page
**************************************************************************/
