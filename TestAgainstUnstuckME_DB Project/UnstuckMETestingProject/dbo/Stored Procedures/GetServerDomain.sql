create proc GetServerDomain as
begin
	select ServerDomain
	from Server;
end;

/**************************************************************************
* Gets school name
**************************************************************************/
