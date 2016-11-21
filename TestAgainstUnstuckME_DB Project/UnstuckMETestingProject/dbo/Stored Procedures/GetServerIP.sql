create proc GetServerIP as
begin
	select ServerIP
	from Server;
end;

/**************************************************************************
* Gets server domain
**************************************************************************/
