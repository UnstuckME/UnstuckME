use UnstuckME_Schools;

/*************************************************************************
* Drops the procedures if they already exist on the database
*************************************************************************/
if OBJECT_ID('InsertSchool') is not null
	drop proc InsertSchool;
if OBJECT_ID('InsertServerForSchool') is not null
	drop proc InsertServerForSchool;
if OBJECT_ID('UpdateSchoolInfo') is not null
	drop proc UpdateSchoolInfo;
if OBJECT_ID('UpdateServerInfo') is not null
	drop proc UpdateServerInfo;
if OBJECT_ID('DeleteSchool') is not null
	drop proc DeleteSchool;
if OBJECT_ID('DeleteServer') is not null
	drop proc DeleteServer;
if OBJECT_ID('PreventSchoolsInTableMultipleTimes') is not null
	drop trigger PreventSchoolsInTableMultipleTimes;
if OBJECT_ID('PreventMultipleSchoolsWithSameEmailCredential') is not null
	drop trigger PreventMultipleSchoolsWithSameEmailCredential;
if OBJECT_ID('PreventMultipleSchoolLogos') is not null
	drop trigger PreventMultipleSchoolLogos;

/*************************************************************************
* Inserts a new school, email credentials for that school, and a logo into
* the database
*************************************************************************/
go
create proc InsertSchool
(	@SchooName			nvarchar(128),
	@EmailCredentials	varchar(64),
	@SchoolLogo			varbinary(MAX)
) as
begin
	begin tran InsertingNewSchool
		insert into School
		values (@SchooName, @EmailCredentials);

		insert into SchoolLogo
		values (@@IDENTITY, @SchoolLogo);
	commit tran InsertingNewSchool
end;

/*************************************************************************
* Inserts server information for a specified school. Domain, name, and IP
* address must be specified.
*************************************************************************/
go
create proc InsertServerForSchool
(	@SchoolName	nvarchar(128),
	@Domain		varchar(15),
	@Name		nvarchar(128),
	@IPAddress	varchar(39)
) as
begin
	declare @SchoolID int;
	set @SchoolID = (select SchoolID from School where SchoolName = @SchoolName);

	insert into [Server]
	values (@SchoolID, @Domain, @Name, @IPAddress);
end;

/*************************************************************************
* Updates School name, email credentials and logo; inserts the school if
* it does not exist
*************************************************************************/
go
create proc UpdateSchoolInfo
(	@SchoolName			nvarchar(128),
	@NewSchoolName		nvarchar(128) = null,
	@EmailCredentials	varchar(64) = null,
	@Logo				varbinary(MAX) = null
) as
begin
	if not exists (select SchoolName from School where SchoolName = @SchoolName)
	begin
		if @EmailCredentials is not null and @Logo is not null
			exec InsertSchool @NewSchoolName, @EmailCredentials, @Logo;
		else
			throw 10000, 'Email credentials and a logo must be provided to insert a new school', 1;
	end;
	else
	begin
		declare @SchoolID int;
		set @SchoolID = (select SchoolID from School where SchoolName = @SchoolName);

		update School
		set SchoolName =
		(
			case
				when @NewSchoolName is not null
					then @NewSchoolName
				when @NewSchoolName is null
					then @SchoolName
			end --case
		),
		EmailCredentials =  
		(	case
				when @EmailCredentials is not null
					then @EmailCredentials
				when @EmailCredentials is null
					then EmailCredentials
			end --case
		)
		where SchoolName = @SchoolName;

		update SchoolLogo
		set Logo = 
		(
			case
				when @Logo is not null
					then @Logo
				when @Logo is null
					then Logo
			end --case
		)
		where LogoID = @SchoolID;
	end; --if
end; --proc

/*************************************************************************
* Updates Server domain, name, and IP address; inserts a new server if it
* doesn't exist
*************************************************************************/
go
create proc UpdateServerInfo
(	@ServerName		nvarchar(128),
	@NewServerName	nvarchar(128) = null,
	@Domain			varchar(15) = null,
	@IPAddress		varchar(39) = null
) as
begin
	if not exists (select ServerName from [Server] where ServerName = @ServerName)
	begin
		if @Domain is not null and @ServerName is not null and @IPAddress is not null
		begin
			declare @SchoolName nvarchar(128);
			set @SchoolName = (select SchoolName
							   from School join [Server] on School.SchoolID = [Server].SchoolID
							   where @ServerName = ServerName);

			exec InsertServerForSchool @SchoolName, @Domain, @ServerName, @IPAddress;
		end;
		else
			throw 10000, 'A Domain, ServerName, and IPAddress must be provided in order to insert a new server', 1;
	end;
	else
	begin
		declare @SchoolID int;
		set @SchoolID = (select SchoolID from [Server] where @ServerName = ServerName);

		update [Server]
		set ServerDomain = 
		(
			case
				when @Domain is not null
					then @Domain
				when @Domain is null
					then ServerDomain
			end --case
		),
		ServerName = 
		(
			case
				when @ServerName is not null
					then @ServerName
				when @ServerName is null
					then ServerName
			end --case
		),
		ServerIPAddress = 
		(
			case
				when @IPAddress is not null
					then @IPAddress
				when @IPAddress is null
					then ServerIPAddress
			end --case
		)
		where SchoolID = @SchoolID;
	end; --if
end; --proc

/*************************************************************************
* Deletes a specified school from the database
*************************************************************************/
go
create proc DeleteSchool
(	@SchoolName	nvarchar(128)	)
as
begin
	declare @SchoolID int;
	set @SchoolID = (select SchoolID from School where SchoolName = @SchoolName);

	delete from SchoolLogo
	where LogoID = @SchoolID;

	delete from [Server]
	where SchoolID = @SchoolID;

	delete from School
	where SchoolName = @SchoolName;
end; --proc

/*************************************************************************
* Deletes a specified server from the database
*************************************************************************/
go
create proc DeleteServer
(	@ServerName nvarchar(128)	)
as
begin
	delete from [Server]
	where ServerName = @ServerName;
end; --proc

/*************************************************************************
* Trigger that doesn't allow a school to exist more than once in the school
* table
*************************************************************************/
go
create trigger PreventSchoolsInTableMultipleTimes on School
after insert, update
as
begin
	declare @SchoolName nvarchar(128);
	set @SchoolName = (select SchoolName from inserted);

	if (select count(SchoolName) from School where @SchoolName = SchoolName) > 1
	begin
		print @SchoolName + ' already exists in the database';
		rollback tran;
	end; --if
end; --trigger

/*************************************************************************
* Trigger that doesn't allow email credentials to be available for more
* than one school
*************************************************************************/
go
create trigger PreventMultipleSchoolsWithSameEmailCredential on School
after insert
as
begin
	declare @EmailCredentials varchar(64);
	set @EmailCredentials = (select EmailCredentials from inserted);

	if (select count(EmailCredentials) from School where @EmailCredentials = EmailCredentials) > 1
	begin
		print @EmailCredentials + ' is already in use by another school';
		rollback tran;
	end; --if
end; --trigger

/*************************************************************************
* Trigger that doesn't allow email credentials to be available for more
* than one school
*************************************************************************/
go
create trigger PreventMultipleSchoolLogos on SchoolLogo
after insert, update
as
begin
	declare @Logo varbinary(MAX);
	set @Logo = (select Logo from inserted);

	if (select count(Logo) from SchoolLogo where @Logo = Logo) > 1
	begin
		print 'This logo is already in use by another school';
		rollback tran InsertingNewSchool;
	end; --if
end; --trigger