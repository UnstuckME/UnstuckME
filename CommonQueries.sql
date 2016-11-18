/**************************************************************************
* UnstuckME Common Queries
**************************************************************************/
use UnstuckME_DB
go

/**************************************************************************
* Drop existing stored procedures
**************************************************************************/
if object_id('GetServerName') is not null
	drop procedure GetServerName;
if object_id('GetServerIP') is not null
	drop procedure GetServerIP;
if object_id('GetServerDomain') is not null
	drop procedure GetServerDomain;
if object_id('GetSchoolName') is not null
	drop procedure GetSchoolName;
if object_id('GetEmailCredentials') is not null
	drop procedure GetEmailCredentials;
if object_id('GetAdminInfo') is not null
	drop procedure GetAdminInfo;
if object_id('GetAllStickers') is not null
	drop procedure GetAllStickers;
if object_id('GetAllStudentReviews') is not null
	drop procedure GetAllStudentReviews;
if object_id('GetAllTutorReviews') is not null
	drop procedure GetAllTutorReviews;
if object_id('AdminPullReportsForUser') is not null
	drop procedure AdminPullReportsForUser;
if object_id('AdminPullAllReports') is not null
	drop procedure AdminPullAllReports;
if object_id('ViewAllUsers') is not null
	drop procedure ViewAllUsers;
if object_id('ViewAllClasses') is not null
	drop procedure ViewAllClasses;
if object_id('GetAllOrganizations') is not null
	drop procedure GetAllOrganizations;
if object_id('GetProfilePicture') is not null
	drop procedure GetProfilePicture;
if object_id('GetDisplayNameAndEmail') is not null
	drop procedure GetDisplayNameAndEmail;
if object_id('GetUserClasses') is not null
	drop procedure GetUserClasses;
if object_id('GetUserStickersAndReviews') is not null
	drop procedure GetUserStickersAndReviews;
if object_id('GetUserOrganizations') is not null
	drop procedure GetUserOrganizations;
if object_id('OpenProfilePage') is not null
	drop procedure OpenProfilePage;
if object_id('FilterUserReviewsByGreaterThanStarRank') is not null
	drop procedure FilterUserReviewsByGreaterThanStarRank;
if object_id('FilterUserReviewsByEqualStarRank') is not null
	drop procedure FilterUserReviewsByEqualStarRank;
if object_id('RetrieveLogin') is not null
	drop procedure RetrieveLogin;
if object_id('GetAllActiveStickers') is not null
	drop procedure GetAllActiveStickers;
if object_id('PullActiveClassSpecificStickers') is not null
	drop procedure PullActiveClassSpecificStickers;
if object_id('GetActiveStickersWithStarRankOrMentorOrganization') is not null
	drop procedure GetActiveStickersWithStarRankOrMentorOrganization;
if object_id('GetAllResolvedStickers') is not null
	drop procedure GetAllResolvedStickers;
if object_id('GetUsersWithOverallStarRank') is not null
	drop procedure GetUsersWithOverallStarRank;
if object_id('GetUserAvgStudentStarRank') is not null
	drop procedure GetUserAvgStudentStarRank;
if object_id('GetUserAvgTutorStarRank') is not null
	drop procedure GetUserAvgTutorStarRank;
if object_id('PullChatMessagesAndFilesBetweenUsers') is not null
	drop procedure PullChatMessagesAndFilesBetweenUsers;

/**************************************************************************
* Gets server name
**************************************************************************/
go
create proc GetServerName as
begin
	select ServerName
	from Server;
end;

/**************************************************************************
* Gets server IP address
**************************************************************************/
go
create proc GetServerIP as
begin
	select ServerIP
	from Server;
end;

/**************************************************************************
* Gets server domain
**************************************************************************/
go
create proc GetServerDomain as
begin
	select ServerDomain
	from Server;
end;

/**************************************************************************
* Gets school name
**************************************************************************/
go
create proc GetSchoolName as
begin
	select SchoolName
	from Server;
end;

/**************************************************************************
* Gets email credentials
**************************************************************************/
go
create proc GetEmailCredentials as
begin
	select EmailCredentials
	from Server;
end;

/**************************************************************************
* Gets Administrator information
**************************************************************************/
go
create proc GetAdminInfo as
begin
	select AdminUsername, AdminPassword
	from Server;
end;

/**************************************************************************
* Gets all Stickers for Admin
**************************************************************************/
go
create proc GetAllStickers as
begin
	select DisplayFName + ' ' + DisplayLName as Student, EmailAddress,
		CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Classes.ClassID = Sticker.ClassID
end;

/**************************************************************************
* Gets all Student Reviews for Admin
**************************************************************************/
go
--create proc GetAllStudentReviews as
--begin
--	select DisplayFName + ' ' + DisplayLName as Reviewer, StarRanking, Description
--	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
--		join Review					on Review.ReviewerID = Sticker.StudentID
--end;

/**************************************************************************
* Gets all Tutor Reviews for Admin
**************************************************************************/
go
--create proc GetAllTutorReviews as
--begin
	
--end;

/**************************************************************************
* Pull Reports for a specific user (Admins only)
**************************************************************************/
go
create proc AdminPullReportsForUser
(	@user varchar(61)	) as
begin
	select (select DisplayFName + ' ' + DisplayLName
				from UserProfile join Review	on UserProfile.UserID = Review.ReviewerID
					join Report					on Review.ReviewID = Report.ReviewID
				where Report.FlaggerID is not null)
			as 'Reported User',
			(select DisplayFName + ' ' + DisplayLName
				from UserProfile join Report	on UserProfile.UserID = Report.FlaggerID
					join Review					on Report.ReviewID = Review.ReviewID)
			as Reporter,
			StarRanking as ReviewStarRanking, Description as ReviewDescription, ReportDescription
	from Report join Review		on Report.ReviewID = Review.ReviewID
	where 'Reported User' = @user or Reporter = @user
end;

/**************************************************************************
* Pull reports for all users (Admins only)
**************************************************************************/
go
create proc AdminPullAllReports as
begin
	select (select DisplayFName + ' ' + DisplayLName
				from UserProfile join Review	on UserProfile.UserID = Review.ReviewerID
					join Report					on Review.ReviewID = Report.ReviewID
				where Report.FlaggerID is not null)
			as 'Reported User',
			(select DisplayFName + ' ' + DisplayLName
				from UserProfile join Report	on UserProfile.UserID = Report.FlaggerID
					join Review					on Report.ReviewID = Review.ReviewID)
			as Reporter,
			StarRanking as ReviewStarRanking, Description as ReviewDescription, ReportDescription
	from Report join Review		on Report.ReviewID = Review.ReviewID
end;

/**************************************************************************
* Admin wants to view all users
**************************************************************************/
go
create proc ViewAllUsers as
begin
	select DisplayFName + ' ' + DisplayLName as 'User', EmailAddress, Privileges
	from UserProfile
end;

/**************************************************************************
* View all classes
**************************************************************************/
go
create proc ViewAllClasses as 
begin
	select CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		TermOffered
	from Classes
end;

/**************************************************************************
* View all organizations
**************************************************************************/
go
create proc GetAllOrganizations as
begin
	select OrganizationName
	from OfficialMentor
end;

/**************************************************************************
* Get Profile Picture
**************************************************************************/
go
create proc GetProfilePicture
(	@useremail varchar(50) ) as
begin
	select Photo
	from Picture join UserProfile	on UserProfile.UserID = Picture.UserID
	where EmailAddress = @useremail
end;

/**************************************************************************
* Get username, email address
**************************************************************************/
go
create proc GetDisplayNameAndEmail
(	@useremail varchar(50),
	@password nvarchar(30)
) as
begin
	select DisplayFName + ' ' + DisplayLName, EmailAddress
	from UserProfile
	where EmailAddress = @useremail and UserPassword = @password;
end;

/**************************************************************************
* Get user's classes
**************************************************************************/
go
create proc GetUserClasses
(	@useremail varchar(50),
	@password nvarchar(30)
) as
begin
	select Classes.CourseName, Classes.CourseCode, Classes.CourseNumber
	from UserProfile join UserToClass	on UserProfile.UserID = UserToClass.UserID
		join Classes					on UserToClass.ClassID = Classes.ClassID
	where EmailAddress = @useremail and UserPassword = @password;
end;

/**************************************************************************
* Get Stickers with reviews submitted by user
**************************************************************************/
go
create proc GetUserStickersAndReviews
(	@useremail varchar(50),
	@password nvarchar(30)
) as 
begin
		select Sticker.SubmitTime, Sticker.Timeout, Sticker.ProblemDescription,
			Sticker.MinimumStarRanking, Review.StarRanking, Review.Description
	from UserProfile join Sticker		on Sticker.StudentID = UserProfile.UserID
		join Review						on Review.StickerID = Sticker.StickerID
	where EmailAddress = @useremail and UserPassword = @password;
end;

/**************************************************************************
* Get Organizations user is apart of
**************************************************************************/
go
create proc GetUserOrganizations
(	@useremail varchar(50),
	@password nvarchar(30)
) as
begin
	select OrganizationName
	from UserProfile join OmToUser		on UserProfile.UserID = OmToUser.UserID
		join OfficialMentor				on OfficialMentor.MentorID = OmToUser.MentorID
	where EmailAddress = @useremail and UserPassword = @password;
end;

/**************************************************************************
* User opens profile page
**************************************************************************/
go
create proc OpenProfilePage
(	@useremail varchar(50),
	@password nvarchar(30)
) as
begin
	exec GetProfilePicture @useremail;

	exec GetUserNameAndEmail @useremail, @password;

	exec GetUserClasses @useremail, @password;

	exec GetUserStickersAndReviews @useremail, @password;

	exec GetUserOrganizations @useremail, @password;
end;

/**************************************************************************
* Filter reviews based on a >= star ranking
**************************************************************************/
go
create proc FilterUserReviewsByGreaterThanStarRank
(	@starfilter float,
	@displayname varchar(61) = null
) as
begin
	select DisplayFName + ' ' + DisplayLName as 'User', StarRanking, Description
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StickerID = Review.StickerID
	where StarRanking >= @starfilter and 
		@displayname = case when @displayname is not null
						then 'User'
					end
end;

/**************************************************************************
* Filter reviews based on a specific star ranking
**************************************************************************/
go
create proc FilterUserReviewsByEqualStarRank
(	@starfilter float,
	@displayname varchar(61) = null
) as	
begin
	select DisplayFName + ' ' + DisplayLName as 'User', StarRanking, Description
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StickerID = Review.StickerID
	where StarRanking = @starfilter and
		@displayname = case when @displayname is not null
						then 'User'
					end
end;

/**************************************************************************
* User forgot login information
**************************************************************************/
go
create proc RetrieveLogin
(	@useremail varchar(50)	) as
begin
	select UserPassword
	from UserProfile
	where EmailAddress = @useremail;
end;

/**************************************************************************
* Pull all active Stickers (user specification is optional)
**************************************************************************/
go
create proc GetAllActiveStickers
(	@displayname varchar(61) = null	) as
begin
	select DisplayFName + ' ' + DisplayLName as Student,
		CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		TermOffered, ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
	where datediff(minute, getdate(), Timeout) > 0 and
		DisplayFName + ' ' + DisplayLName = case when @displayname is not null
												then @displayname
											end
end;

/**************************************************************************
* Pull active Stickers available for a certain class a user may be associated
* with (user specification is optional)
**************************************************************************/
go
create proc PullActiveClassSpecificStickers
(	@Name varchar(50),
	@Code varchar(5),
	@Number smallint,
	@Term tinyint,
	@displayname varchar(61) = null
) as
begin
	select DisplayFName + ' ' + DisplayLName as Student,
		CourseCode /*+ ' ' + convert(varchar, */,CourseNumber/*) + ' - ' + */,CourseName,-- as Course,
		TermOffered, ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
	where CourseName = @Name and CourseCode = @Code and CourseNumber = @Number
		and TermOffered = @Term and
		DisplayFName + ' ' + DisplayLName = case when @displayname is not null
												then @displayname
											end
end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (star ranking, mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickersWithStarRankOrMentorOrganization
(	@displayname varchar(61) = null,
	@starrank float = null,
	@organization nvarchar(50) = null
) as
begin
	select DisplayFName + ' ' + DisplayLName as Student,
		CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		TermOffered, ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
		join OmToUser				on UserProfile.UserID = OmToUser.UserID
		join OfficialMentor			on OmToUser.MentorID = OfficialMentor.MentorID
	where DisplayFName + ' ' + DisplayLName = case when @displayname is not null
													then @displayname
												end
		and MinimumStarRanking >= case when @starrank is not null
										then @starrank
									end
		and OrganizationName = case when @organization is not null
									then @organization
								end
end;

/**************************************************************************
* Pull all resolved Stickers (user specification is optional)
**************************************************************************/
go
create proc GetAllResolvedStickers
(	@displayname varchar(61) = null	) as
begin
	select DisplayFName + ' ' + DisplayLName as Student,
		CourseCode + ' ' + convert(varchar, CourseNumber) + ' - ' + CourseName as Course,
		TermOffered, ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
	where datediff(minute, getdate(), Timeout) < 0 and
		DisplayFName + ' ' + DisplayLName = case when @displayname is not null
												then @displayname
											end
end;

/**************************************************************************
* Get all users with a specific overall avg star ranking
**************************************************************************/
go
create proc GetUsersWithOverallStarRank
(	@starrank float	) as
begin
	declare @totalrank float;
	set @totalrank = (select StarRanking
			 from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
				join Review					on Sticker.StudentID = Review.ReviewerID)
									+
			(select StarRanking
			 from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
				join Review					on Sticker.StudentID = Review.ReviewerID);

	select DisplayFName + ' ' + DisplayLName as 'User', EmailAddress,
		avg(@totalrank) as AvgStarRank
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentID = Review.ReviewerID
	group by DisplayFName + ' ' + DisplayLName, EmailAddress
	having avg(@totalrank) >= 3--@starrank
end;

/**************************************************************************
* View a user's avg student rating
**************************************************************************/
go
create proc GetUserAvgStudentStarRank
(	@displayname varchar(61)	) as
begin
	select avg(StarRanking) as AvgStarRank
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentID = Review.ReviewerID
	where DisplayFName + ' ' + DisplayLName = @displayname
end;

/**************************************************************************
* View a user's avg tutor rating
**************************************************************************/
go
create proc GetUserAvgTutorStarRank
(	@displayname varchar(61)	) as
begin
	select avg(StarRanking) as AvgStarRank
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentID = Review.ReviewerID
	where DisplayFName + ' ' + DisplayLName = @displayname
end;

/**************************************************************************
* Pull chat messages and files between two users
**************************************************************************/
go
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