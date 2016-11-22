create proc GetAllOrganizations as
begin
	select OrganizationName
	from OfficialMentor
end;

/**************************************************************************
* Get Profile Picture
**************************************************************************/
