use UnstuckME_Schools;
go

/********************************************************************
* Removes everything created in the database
********************************************************************/
if object_id('Schools') is not null
	drop table Schools;
if object_id('IPAddress') is not null
	drop table IPAddress;
if object_id('CharToBinaryIPAddress') is not null
	drop function CharToBinaryIPAddress;
if object_id('IPAddress_Split') is not null
	drop function IPAddress_Split;
if object_id('InsertSchool') is not null
	drop procedure InsertSchool;

/********************************************************************
* Creates the table containing the schools and IP addresses of their
* server to connect to.
********************************************************************/
go
create table IPAddress
(
	IPAddress_ID	int				primary key identity(1,1),
	Group8			varbinary(2)	null, --IPv6 only, null for IPv4
	Group7			varbinary(2)	null, --IPv6 only, null for IPv4
	Group6			varbinary(2)	null, --IPv6 only, null for IPv4
	Group5			varbinary(2)	null, --IPv6 only, null for IPv4
	Group4			varbinary(2)	not null,
	Group3			varbinary(2)	not null,
	Group2			varbinary(2)	not null,
	Group1			varbinary(2)	not null
);

go
create table Schools
(
	SchoolID			int				primary key identity(1,1),
	Name				varchar(75)		not null,
	IPAddress_ID		int				not null references IPAddress(IPAddress_ID),
	Logo				varbinary(max)	not null
);

/********************************************************************
* Function that converts binary columns to a readable IP address
********************************************************************/
go
create function CharToBinaryIPAddress (	@schoolname varchar(75)	)
returns varchar(39) as
begin
	declare @group1 varbinary(2);	declare @group2 varbinary(2);
	declare @group3 varbinary(2);	declare @group4 varbinary(2);
	declare @group5 varbinary(2);	declare @group6 varbinary(2);
	declare @group7 varbinary(2);	declare @group8 varbinary(2);
	declare @return_value varchar(39);
	set @group1 = (select Group1 from IPAddress);	set @group2 = (select Group2 from IPAddress)
	set @group3 = (select Group3 from IPAddress);	set @group4 = (select Group4 from IPAddress)
	set @group5 = (select Group5 from IPAddress);	set @group6 = (select Group6 from IPAddress)
	set @group7 = (select Group7 from IPAddress);	set @group8 = (select Group8 from IPAddress)

	if exists (select Name from Schools where Name = @schoolname)
	begin
		if (select Group8 from IPAddress
			join Schools on IPAddress.IPAddress_ID = Schools.IPAddress_ID
							and Schools.Name = @schoolname) is null
		begin	--IPv4 addresses
			set @return_value = (convert(tinyint, @group4) + '.' + convert(tinyint, @group3) + '.' +
								convert(tinyint, @group2) + '.' + convert(tinyint, @group1));
		end;
		else
		begin	--IPv6 addresses
			set @return_value = (lower(convert(varchar(4), @group8, 2) + ':' + convert(varchar(4), @group7, 2) + ':' + 
					convert(varchar(4), @group6, 2) + ':' + convert(varchar(4), @group5, 2) + ':' + 
					convert(varchar(4), @group4, 2) + ':' + convert(varchar(4), @group3, 2) + ':' + 
					convert(varchar(4), @group2, 2) + ':' + convert(varchar(4), @group1, 2)));
		end;
	end;
	else
	begin
		set @return_value = 'Error: ' + @schoolname + ' does not exist in the database.';
	end;

	return @return_value;
end;

/********************************************************************
* Function that converts text IP addresses to binary so they can be
* inserted
********************************************************************/
go
create function IPAddress_Split (	@IPAddress nvarchar(39)	)
returns varchar(35) as
begin
	declare @return_value varchar(35);

	if PATINDEX('%:%', @IPAddress) > 0	--IPv6 address
	begin
		set @return_value = substring(@IPAddress, 0, PATINDEX('%:%', @IPAddress));
	end;
	else if PATINDEX('%.%', @IPAddress) > 0	--IPv4 address
	begin
		set @return_value = substring(@IPAddress, 0, PATINDEX('%.%', @IPAddress));
	end;
	else
	begin
		set @return_value = @IPAddress;
	end;

	return lower(@return_value);
end;

/********************************************************************
* Function that converts text IP addresses to binary so they can be
* inserted
********************************************************************/
go
create procedure InsertSchool
(	@schoolname varchar(75), @TextIPAddress nvarchar(39), @logo varbinary(max)	) as
begin
	declare @group1 varchar(4);			declare @group2 varchar(4);
	declare @group3 varchar(4);			declare @group4 varchar(4);
	declare @group5 varchar(4);			declare @group6 varchar(4);
	declare @group7 varchar(4);			declare @group8 varchar(4);
	declare @len varchar(35)

	exec @group1 = IPAddress_Split @TextIPAddress;
	set @len = len(@group1);
	--print @group1
	
	exec @group2 = IPAddress_Split substring(select @TextIPAddress, @len + 1, 39);
	set @len += len(@group2);
	--print @group2
	
	exec @group3 = IPAddress_Split substring(select @TextIPAddress, @len + 1, 39);
	set @len += len(@group3);
	--print @group3
	
	exec @group4 = IPAddress_Split substring(select @TextIPAddress, @len + 1, 39);
	set @len += len(@group4);
	--print @group4
	
	exec @group5 = IPAddress_Split substring(select @TextIPAddress, @len + 1, 39);
	set @len += len(@group5);
	--print @group5
	
	exec @group6 = IPAddress_Split substring(select @TextIPAddress, @len + 1, 39);
	set @len += len(@group6);
	--print @group6
	
	exec @group7 = IPAddress_Split substring(select @TextIPAddress, @len + 1, 39);
	set @len += len(@group7);
	--print @group7
	
	exec @group8 = IPAddress_Split substring(select @TextIPAddress, @len + 1, 39);
	--print @group8

	--Insert school and IP address into the database
	insert into IPAddress
	values (convert(varbinary(2), @group8), convert(varbinary(2), @group7),
			convert(varbinary(2), @group6), convert(varbinary(2), @group5),
			convert(varbinary(2), @group4), convert(varbinary(2), @group3),
			convert(varbinary(2), @group2), convert(varbinary(2), @group1));
	insert into Schools
	values (@schoolname, @@IDENTITY, @logo);
end;

/********************************************************************
* Trigger that updates the school information if the school already
* exists
********************************************************************/
go
create trigger UpdateSchoolAndIPIfAlreadyExists
on Schools
instead of insert
as
	update Schools
	set Logo = (select Logo from inserted where Name = (select Name from inserted));
	
	delete from IPAddress
	where IPAddress_ID = @@IDENTITY

	declare @group1 varchar(4);			declare @group2 varchar(4);
	declare @group3 varchar(4);			declare @group4 varchar(4);
	declare @group5 varchar(4);			declare @group6 varchar(4);
	declare @group7 varchar(4);			declare @group8 varchar(4);
	declare @len varchar(35);			declare @IPAddress nvarchar(39);
	exec @IPAddress = CharToBinaryIPAddress (select Name from inserted);

	exec @group1 = IPAddress_Split @IPAddress;
	set @len = len(@group1);
	--print @group1
	
	exec @group2 = IPAddress_Split substring(select @IPAddress, @len + 1, 39);
	set @len += len(@group2);
	--print @group2
	
	exec @group3 = IPAddress_Split substring(select @IPAddress, @len + 1, 39);
	set @len += len(@group3);
	--print @group3
	
	exec @group4 = IPAddress_Split substring(select @IPAddress, @len + 1, 39);
	set @len += len(@group4);
	--print @group4
	
	exec @group5 = IPAddress_Split substring(select @IPAddress, @len + 1, 39);
	set @len += len(@group5);
	--print @group5
	
	exec @group6 = IPAddress_Split substring(select @IPAddress, @len + 1, 39);
	set @len += len(@group6);
	--print @group6
	
	exec @group7 = IPAddress_Split substring(select @IPAddress, @len + 1, 39);
	set @len += len(@group7);
	--print @group7
	
	exec @group8 = IPAddress_Split substring(select @IPAddress, @len + 1, 39);
	--print @group8

	update IPAddress
	set Group8 = convert(varbinary(2), @group8),
		Group7 = convert(varbinary(2), @group7),
		Group6 = convert(varbinary(2), @group6),
		Group5 = convert(varbinary(2), @group5),
		Group4 = convert(varbinary(2), @group4),
		Group3 = convert(varbinary(2), @group3),
		Group2 = convert(varbinary(2), @group2),
		Group1 = convert(varbinary(2), @group1);