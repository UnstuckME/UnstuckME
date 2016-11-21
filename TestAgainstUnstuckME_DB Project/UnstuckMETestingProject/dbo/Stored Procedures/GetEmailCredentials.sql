create proc GetEmailCredentials as
begin
	select EmailCredentials
	from Server;
end;

/**************************************************************************
* Gets Administrator information
**************************************************************************/
