/*************************************
* UnstuckME Common Queries
*************************************/
use UnstuckME_DB
go

/*************************************
* Drop existing stored procedures
*************************************/
if object_id('GetServerInfo') is not null
	drop GetServerInfo;
if object_id('GetAdminInfo') is not null
	drop GetAdminInfo;
if object_id('OpenProfilePage') is not null
	drop OpenProfilePage;
if object_id('AdminPullReports') is not null
	drop AdminPullReports;
if object_id('AdminOnlyPullAllReports') is not null
	drop AdminOnlyPullAllReports;
if object_id('FilterUserReviewsByGreaterStarRank') is not null
	drop FilterUserReviewsByGreaterStarRank;
if object_id('FilterUserReviewsByEqualStarRank') is not null
	drop FilterUserReviewsByEqualStarRank;
if object_id('RetrieveLogin') is not null
	drop RetrieveLogin;
if object_id('PullClassSpecificStickers') is not null
	drop PullClassSpecificStickers;
if object_id('PullUserSpecificStickers') is not null
	drop PullUserSpecificStickers;
if object_id('GetUserAvgRating') is not null
	drop GetUserAvgRating;
if object_id('PullRankingSpecificStickers') is not null
	drop PullRankingSpecificStickers;
if object_id('PullChatMessagesBetweenUsers') is not null
	drop PullChatMessagesBetweenUsers;

/*************************************
* Gets server information
*************************************/
go
create proc GetServerInfo as
begin
	select ServerName, ServerIP, ServerDomain, SchoolName, EmailCredentials
	from Server;
end;

/*************************************
* Gets Administrator information
*************************************/
go
create proc GetAdminInfo as
begin
	select AdminUsername, AdminPassword
	from Server;
end;

/*************************************
* User opens profile page
*************************************/
go
create proc OpenProfilePage
(	@useremail varchar(50),
	@password binary(64)
) as
begin
	select UserProfile.DisplayFName, UserProfile.DisplayLName, UserProfile.EmailAddress,
		Classes.CourseName, Classes.CourseCode, Classes.CourseNumber, 
		(select ClassID, SubmitTime, ProblemDescription
			 from Sticker
			 where TutorReviewID is not null) as "Resolved Stickers",
		(select ClassID, SubmitTime, ProblemDescription
			 from Sticker
			 where TutorReviewID is null) as "Active Stickers",
		Review.StarRanking, Review.Description as ReviewDescription, OrganizationName
	from UserProfile join UserToClass	on UserProfile.UserID = UserToClass.UserID
		join Classes					on UserToClass.ClassID = Classes.ClassID
		join Sticker					on Sticker.StudentID = UserProfile.UserID
		join Review						on Review.ReviewID = Sticker.TutorReviewID
		join OmToUser					on UserProfile.UserID = OmToUser.UserID
		join OfficialMentor				on OfficialMentor.MentorID = OmToUser.MentorID
	where EmailAddress = @useremail and UserPassword = @password
end

/*************************************
* Pull Reports for a specific user (Admins only)
*************************************/
go
create proc AdminPullReports
(	@user varchar(61)	) as
begin
	select DisplayFName, DisplayLName
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentReviewID = Review.ReviewID
		join Report					on Review.ReportID = Report.ReportID
	where DisplayFName + DisplayLName = @user;
end;

/*************************************
* Pull reports for all users (Admins only)
*************************************/
go
create proc AdminOnlyPullAllReports as
begin
	select DisplayFName, DisplayLName, FlaggerID
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentReviewID = Review.ReviewID
		join Report					on Review.ReportID = Report.ReportID
	where UserProfile.UserID = Report.FlaggerID;
end;

/*************************************
* Filter reviews based on a >= star ranking
*************************************/
go
create proc FilterUserReviewsByGreaterStarRank
(	@starfilter float,
	@user int
) as
begin
	select StarRanking, Description
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Review.ReviewID = Sticker.StudentReviewID
	where StarRanking >= @starfilter and UserProfile.UserID = @user;
end;

/*************************************
* Filter reviews based on a specific star ranking
*************************************/
go
create proc FilterUserReviewsByEqualStarRank
(	@starfilter float,
	@user int
) as	
begin
	select StarRanking, Description
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Review.ReviewID = Sticker.StudentReviewID
	where StarRanking = @starfilter;
end;

/*************************************
* User forgot login information
*************************************/
go
create proc RetrieveLogin
(	@useremail varchar(50)	) as
begin
	select UserPassword
	from UserProfile
	where EmailAddress = @useremail;
end;

/*************************************
* Pull Stickers available for a certain class
*************************************/
go
create proc PullClassSpecificStickers
(	@Name varchar(50),
	@Code varchar(5),
	@Number smallint,
	@Term tinyint
) as
begin
	select DisplayFName + DisplayLName as "Poster", CourseCode + CourseNumber + ': ' + CourseName as Course,
		TermOffered, ProblemDescription, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
	where CourseName = @Name
		and CourseCode = @Code
		and CourseNumber = @Number
		and TermOffered = @Term;
end;

/*************************************
* Pull Stickers available from a specific user
*************************************/
go
create proc PullUserSpecificStickers
(	@Name varchar(61)	) as
begin
	select DisplayFName + DisplayLName as "Poster", CourseCode + CourseNumber + ': ' + CourseName as Course,
		TermOffered, ProblemDescription, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
	where DisplayFName + DisplayLName = @Name;
end;

/*************************************
* View a user's avg rating
*************************************/
go
create proc GetUserAvgRating
(	@user int	) as
begin
	select avg(StarRanking) as AverageRating
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StudentReviewID = Review.ReviewID
	where UserProfile.UserID = @user;
end;

/*************************************
* Pull Stickers available to users with a specific ranking
*************************************/
go
create proc PullRankingSpecificStickers
(	@user int,
	@avgrank float = null
) as
begin
	exec GetUserAvgRating @user;

	select DisplayFName + DisplayLName as "Poster", CourseCode + CourseNumber + ': ' + CourseName as Course,
		TermOffered, ProblemDescription, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
	where MinimumStarRanking >= @avgrank;
end;

/*************************************
* Pull chat messages between two users
*************************************/
go
create proc PullChatMessagesBetweenUsers
(	@user int,
	@tutor int
) as
begin
	select MessageData, FileData
	from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
		join Chat						on UserToChat.ChatID = Chat.ChatID
		join Files						on Chat.FileID = Files.FileID
		join Messages					on Chat.MessageID = Messages.MessageID
	where UserProfile.UserID = @user or UserProfile.UserID = @tutor;
end;