create proc PullChatMessagesAndFilesBetweenUsers
(	@user varchar(61),
	@tutor varchar(61)
) as
begin
	select MessageData, FileData
	from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
		join Chat						on UserToChat.ChatID = Chat.ChatID
		join Files						on Chat.ChatID = Files.ChatID
		join Messages					on Chat.ChatID = Messages.ChatID
	where DisplayFName + ' ' + DisplayLName = @user;

		select MessageData, FileData
	from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
		join Chat						on UserToChat.ChatID = Chat.ChatID
		join Files						on Chat.ChatID = Files.ChatID
		join Messages					on Chat.ChatID = Messages.ChatID
	where DisplayFName + ' ' + DisplayLName = @tutor;
end;