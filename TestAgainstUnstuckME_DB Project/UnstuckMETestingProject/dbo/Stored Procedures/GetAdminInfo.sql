create proc GetAdminInfo as
begin
	select AdminUsername, AdminPassword
	from Server;
end;

/**************************************************************************
* Gets all Stickers for Admin
**************************************************************************/
