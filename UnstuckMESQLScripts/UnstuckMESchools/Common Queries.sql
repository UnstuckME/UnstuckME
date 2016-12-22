Use UnstuckME_Schools;

/*************************************************************************
* Gathers information for all schools and their servers
*************************************************************************/
go
create proc GetAllSchools as
begin
	select School.SchoolID, SchoolName, EmailCredentials, Logo, ServerID,
			ServerDomain, ServerName, ServerIPAddress
	from School join SchoolLogo		on School.SchoolID = SchoolLogo.LogoID
				join [Server]		on [Server].SchoolID = School.SchoolID;
end;

/*************************************************************************
* Gathers all the logos for schools
*************************************************************************/
go
create proc GetAllSchoolLogos as
begin
	select SchoolID, SchoolName, Logo
	from School join SchoolLogo		on School.SchoolID = SchoolLogo.LogoID;
end;

/*************************************************************************
* Gets the SchoolID for a specified school
*************************************************************************/
go
create proc GetSchoolID
(	@SchoolName nvarchar(128)	)
as
begin
	select SchoolID
	from School
	where SchoolName = @SchoolName;
end;

/*************************************************************************
* Gets the email credentials for a specified school
*************************************************************************/
go
create proc GetSchoolEmailCredentials
(	@SchoolName nvarchar(128)	)
as
begin
	select EmailCredentials
	from School
	where SchoolName = @SchoolName;
end;

/*************************************************************************
* Gets the Domain, Name, and IP address of a specified school's server(s)
*************************************************************************/
go
create proc GetSchoolServerInfo
(	@SchoolName nvarchar(128)	)
as
begin
	select ServerDomain, ServerName, ServerIPAddress
	from [Server] join School	on [Server].SchoolID = School.SchoolID
	where SchoolName = @SchoolName;
end;