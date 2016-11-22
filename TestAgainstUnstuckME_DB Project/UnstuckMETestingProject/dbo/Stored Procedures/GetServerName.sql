create proc GetServerName as
begin
	select ServerName
	from Server;
end;

/**************************************************************************
* Gets server IP address
**************************************************************************/
