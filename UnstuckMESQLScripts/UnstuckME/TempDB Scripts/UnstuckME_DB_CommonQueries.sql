/**************************************************************************
* UnstuckME Common Queries
**************************************************************************/
use UnstuckME_DB
go

/**************************************************************************
* Drop existing stored procedures
**************************************************************************/
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
if object_id('AdminPullReportsForOptionalUser') is not null
	drop procedure AdminPullReportsForOptionalUser;
if object_id('ViewAllUsers') is not null
	drop procedure ViewAllUsers;
if object_id('ViewClasses') is not null
	drop procedure ViewClasses;
if object_id('GetAllOrganizations') is not null
	drop procedure GetAllOrganizations;
if object_id('GetUserID') is not null
	drop procedure GetUserID;
if object_id('GetUserInfo') is not null
	drop procedure GetUserInfo;
if object_id('GetProfilePicture') is not null
	drop procedure GetProfilePicture;
if object_id('GetDisplayNameAndEmail') is not null
	drop procedure GetDisplayNameAndEmail;
if object_id('GetUserClasses') is not null
	drop procedure GetUserClasses;
--if object_id('GetUserStickersAndReviews') is not null
--	drop procedure GetUserStickersAndReviews;
if object_id('GetUserOrganizations') is not null
	drop procedure GetUserOrganizations;
--if object_id('OpenProfilePage') is not null
	--drop procedure OpenProfilePage;
if object_id('FilterUserReviewsByStarRank') is not null
	drop procedure FilterUserReviewsByStarRank;
--if object_id('FilterUserReviewsByEqualStarRank') is not null
--	drop procedure FilterUserReviewsByEqualStarRank;
--if object_id('GetAllActiveStickers') is not null
--	drop procedure GetAllActiveStickers;
if object_id('PullActiveClassSpecificStickers') is not null
	drop procedure PullActiveClassSpecificStickers;
if object_id('GetActiveStickers') is not null
	drop procedure GetActiveStickers;
if object_id('GetAllResolvedStickers') is not null
	drop procedure GetAllResolvedStickers;
if object_id('GetUsersWithOverallStarRank') is not null
	drop procedure GetUsersWithOverallStarRank;
if object_id('GetUserAvgStudentStarRank') is not null
	drop procedure GetUserAvgStudentStarRank;
if object_id('GetUserAvgTutorStarRank') is not null
	drop procedure GetUserAvgTutorStarRank;
if object_id('GetAllChatIDsAUserIsPartOF') is not null
	drop procedure GetAllChatIDsAUserIsPartOF;
if object_id('PullChatMessagesBetweenUsers') is not null
	drop procedure PullChatMessagesBetweenUsers;
if object_id('GetUserPasswordAndSalt') is not null
	drop procedure GetUserPasswordAndSalt;
if object_id('GetUserFriends') is not null
	drop procedure GetUserFriends;
if object_id('GetUserStudentReviews') is not null
	drop procedure GetUserStudentReviews;
if object_id('GetUserTutorReviews') is not null
	drop procedure GetUserTutorReviews;
if object_id('GetCourseCodes') is not null
	drop procedure GetCourseCodes;
if object_id('GetCourseIDByCodeAndNumber') is not null
	drop procedure GetCourseIDByCodeAndNumber;
if object_id('GetCourseNameByCodeAndNumber') is not null
	drop procedure GetCourseNameByCodeAndNumber;
if object_id('GetCourseNumberByCourseCode') is not null
	drop procedure GetCourseNumberByCourseCode;
if object_id('GetReportsByUser') is not null
	drop procedure GetReportsByUser;
if object_id('GetUserSubmittedStickers') is not null
	drop procedure GetUserSubmittedStickers;
if object_id('GetUserTutoredStickers') is not null
	drop procedure GetUserTutoredStickers;

/**************************************************************************
* Gets all Stickers for Admin
**************************************************************************/
go
create proc GetAllStickers as
begin
	select UserID, DisplayFName, DisplayLName, EmailAddress, CourseCode, CourseNumber,
			CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Classes.ClassID = Sticker.ClassID;
end;

/**************************************************************************
* Gets all Student Reviews for Admin
**************************************************************************/
go
create proc GetAllStudentReviews as
begin
	select Sticker.StickerID, DisplayFName, DisplayLName, StarRanking, Description
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Review.ReviewerID = Sticker.TutorID
										and Sticker.StickerID = Review.StickerID;
end;

/**************************************************************************
* Gets all Tutor Reviews for Admin
**************************************************************************/
go
create proc GetAllTutorReviews as
begin
	select Sticker.StickerID, DisplayFName, DisplayLName, StarRanking, Description
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.TutorID
		join Review					on Review.ReviewerID = Sticker.StudentID
										and Sticker.StickerID = Review.StickerID;
end;

/**************************************************************************
* Pull Reports for a specific user (Admins only)
**************************************************************************/
go
create proc AdminPullReportsForOptionalUser
(	@userid int = null	) as
begin
	select DisplayFName, DisplayLName, UserID, Review.ReviewID, StarRanking as ReviewStarRanking, Description as ReviewDescription, ReportDescription
	from UserProfile join Review	on UserProfile.UserID = Review.ReviewerID
		join Report					on Review.ReviewID = Report.ReviewID
		join Sticker				on Sticker.StudentID = Report.FlaggerID
	where Report.FlaggerID is not null and UserID = case when @userid is not null
															then @userid
														 else UserID
													end;
end;

/**************************************************************************
* Admin wants to view all users
**************************************************************************/
go
create proc ViewAllUsers as
begin
	select DisplayFName, DisplayLName, EmailAddress, AverageStudentRank,
			AverageTutorRank, TotalTutorReviews, TotalStudentReviews, Privileges
	from UserProfile;
end;

/**************************************************************************
* View classes
**************************************************************************/
go
create proc ViewClasses
(	@ClassID int = null	) as
begin
	select *
	from Classes
	where ClassID = case when @ClassID is not null
							then @ClassID
						 else ClassID
					end;
end;

/**************************************************************************
* View all organizations
**************************************************************************/
go
create proc GetAllOrganizations as
begin
	select *
	from OfficialMentor;
end;

/**************************************************************************
* Get UserID based on Email address
**************************************************************************/
go
create proc GetUserID
(	@useremail varchar(50)	) as
begin
	select UserID
	from UserProfile
	where EmailAddress = @useremail;
end;

/**************************************************************************
* Get User Info based on UserID
**************************************************************************/
go
create proc GetUserInfo
(	@userid int	= null,
	@email varchar(50) = null
) as
begin
	select *
	from UserProfile
	where UserID = case when @userid is not null
							then @userid
						else UserID
				   end
		or EmailAddress = case when @email is not null
									then @email
							   else EmailAddress
						  end;
end;

/**************************************************************************
* Get Profile Picture
**************************************************************************/
go
create proc GetProfilePicture
(	@userid int ) as
begin
	select Photo
	from Picture join UserProfile	on UserProfile.UserID = Picture.UserID
	where UserProfile.UserID = @userid;
end;

/**************************************************************************
* Get username, email address
**************************************************************************/
go
create proc GetDisplayNameAndEmail
(	@userid int	) as
begin
	select DisplayFName, DisplayLName, EmailAddress
	from UserProfile
	where UserProfile.UserID = @userid;
end;

/**************************************************************************
* Get user's classes
**************************************************************************/
go
create proc GetUserClasses
(	@userid int	) as
begin
	select Classes.ClassID, Classes.CourseName, Classes.CourseCode, Classes.CourseNumber
	from UserProfile join UserToClass	on UserProfile.UserID = UserToClass.UserID
		join Classes					on UserToClass.ClassID = Classes.ClassID
	where UserProfile.UserID = @userid;
end;

/**************************************************************************
* Get Stickers with reviews submitted by user
**************************************************************************/
--go
--create proc GetUserStickersAndReviews
--(	@userid int	) as 
--begin
--	select Sticker.SubmitTime, Sticker.Timeout, Sticker.ProblemDescription,
--			Sticker.MinimumStarRanking, Review.StarRanking, Review.Description
--	from UserProfile join Sticker		on Sticker.StudentID = UserProfile.UserID
--		join Review						on Review.StickerID = Sticker.StickerID
--	where UserProfile.UserID = @userid;
--end;

/**************************************************************************
* Get Organizations user is apart of
**************************************************************************/
go
create proc GetUserOrganizations
(	@userid int	) as
begin
	select OrganizationName
	from UserProfile join OmToUser		on UserProfile.UserID = OmToUser.UserID
		join OfficialMentor				on OfficialMentor.MentorID = OmToUser.MentorID
	where UserProfile.UserID = @userid;
end;

/**************************************************************************
* User opens profile page
**************************************************************************/
--go
--create proc OpenProfilePage
--(	@userid int	) as
--begin
--	select Photo, DisplayFName + ' ' + DisplayLName as [User], CourseName, CourseCode, CourseNumber,
--			SubmitTime, Timeout, ProblemDescription, MinimumStarRanking, StarRanking, Description,
--			OrganizationName
--	from Picture join UserProfile	on UserProfile.UserID = Picture.UserID
--			join UserToClass		on UserProfile.UserID = UserToClass.UserID
--			join Classes			on UserToClass.ClassID = Classes.ClassID
--			join Sticker			on Sticker.StudentID = UserProfile.UserID
--			join Review				on Review.StickerID = Sticker.StickerID
--			join OmToUser			on UserProfile.UserID = OmToUser.UserID
--			join OfficialMentor		on OfficialMentor.MentorID = OmToUser.MentorID
--end;

/**************************************************************************
* Filter reviews based on a >= star ranking
**************************************************************************/
go
create proc FilterUserReviewsByStarRank
(	@starfilter float,
	@userid int = null
) as
begin
	select DisplayFName, DisplayLName, StarRanking, Description
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Sticker.StickerID = Review.StickerID
	where StarRanking >= @starfilter and 
		UserID = case when @userid is not null
						then @userid
					  else UserID
				 end;
end;

/**************************************************************************
* Filter reviews based on a specific star ranking
**************************************************************************/
--go
--create proc FilterUserReviewsByEqualStarRank
--(	@starfilter float,
--	@userid int = null
--) as	
--begin
--	select DisplayFName, DisplayLName, StarRanking, Description
--	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
--		join Review					on Sticker.StickerID = Review.StickerID
--	where StarRanking = @starfilter and
--		UserID = case when @userid is not null
--						then @userid
--					  else UserID
--				 end;
--end;

/**************************************************************************
* Pull all active Stickers (user specification is optional)
**************************************************************************/
--go
--create proc GetAllActiveStickers
--(	@userid int = null	) as
--begin
--	select DisplayFName, DisplayLName, CourseCode, CourseNumber, CourseName,
--		ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
--	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
--		join Classes				on Sticker.ClassID = Classes.ClassID
--	where datediff(minute, getdate(), Timeout) > 0 and
--		UserID = case when @userid is not null
--						then @userid
--					  else UserID
--				 end;
--end;

/**************************************************************************
* Pull active Stickers available for a certain class a user may be associated
* with (user specification is optional)
**************************************************************************/
go
create proc PullActiveClassSpecificStickers
(	@Name varchar(50),
	@Code varchar(5),
	@Number smallint,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout]) as [Row], DisplayFName, DisplayLName,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking,
			SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
		where CourseName = @Name and CourseCode = @Code
			and CourseNumber = @Number
			and UserID = case when @userid is not null
								then @userid
							  else UserID
						 end
	) as Stickers
	where [Row] in (@startrow, @endrow);
end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickers
(	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float,
	@userid int = null,
	@organization nvarchar(50) = null,
	@Name varchar(50) = null,
	@Code varchar(5) = null,
	@Number smallint = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout]) as [Row], DisplayFName, DisplayLName,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking,
			SubmitTime, [Timeout], OrganizationName
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join OmToUser				on UserProfile.UserID = OmToUser.UserID
			join OfficialMentor			on OmToUser.MentorID = OfficialMentor.MentorID
		where UserProfile.UserID = case when @userid is not null
											then @userid
										else UserProfile.UserID
								   end
			and MinimumStarRanking >= case when @starrank is not null
												then @starrank
										   else MinimumStarRanking
									  end
			and OrganizationName = case when @organization is not null
											then @organization
										else OrganizationName
								   end
			and CourseName = case when @Name is not null
									then @Name
								  else CourseName
							 end
			and CourseCode = case when @Code is not null
									then @Code
								  else CourseCode
							 end
			and CourseNumber = case when @Number is not null
										then @Number
									else CourseNumber
							   end
	) as Stickers
	where [Row] in (@startrow, @endrow);
end;

/**************************************************************************
* Pull all resolved Stickers (user specification is optional)
**************************************************************************/
go
create proc GetAllResolvedStickers
(	@userid int = null	) as
begin
	select distinct DisplayFName, DisplayLName, CourseCode, CourseNumber, CourseName,
		ProblemDescription, MinimumStarRanking, SubmitTime, Timeout
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
		join Review					on Review.StickerID = Sticker.StickerID
										and (select count(*) from Review where Review.StickerID = Sticker.StickerID) = 2
	where datediff(minute, getdate(), Timeout) < 0 and
		UserID = case when @userid is not null
						then @userid
					  else UserID
				 end;
end;

/**************************************************************************
* Get all users with a specific overall avg star ranking
**************************************************************************/
go
create proc GetUsersWithOverallStarRank
(	@starrank float	) as
begin
	select DisplayFName, DisplayLName, EmailAddress, avg(StarRanking) as AvgStarRank
	from UserProfile join Review	on UserProfile.UserID = Review.ReviewerID
	group by DisplayFName, DisplayLName, EmailAddress
	having avg(StarRanking) >= @starrank;
end;

/**************************************************************************
* View a user's avg student rating
**************************************************************************/
go
create proc GetUserAvgStudentStarRank
(	@userid int	) as
begin
	select avg(StarRanking) as AvgStarRank
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Review					on Review.ReviewerID = Sticker.TutorID
										and Sticker.StickerID = Review.StickerID
	where UserID = @userid;
end;

/**************************************************************************
* View a user's avg tutor rating
**************************************************************************/
go
create proc GetUserAvgTutorStarRank
(	@userid int	) as
begin
	select avg(StarRanking) as AvgStarRank
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.TutorID
		join Review					on Review.ReviewerID = Sticker.StudentID
										and Sticker.StickerID = Review.StickerID
	where UserID = @userid;
end;

/**************************************************************************
* Pull all chats a user is part of
**************************************************************************/
go
create proc GetAllChatIDsAUserIsPartOF
(	@userid int	) as
begin
	select DisplayFName, DisplayLName, ChatID
	from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
	where (select UserID from UserProfile where UserID = @userid) = UserToChat.UserID;
end;

/**************************************************************************
* Pull chat messages and files between two users
**************************************************************************/
go
create proc PullChatMessagesBetweenUsers
(	@userid int,
	@tutorid int
) as
begin
	select DisplayFName, DisplayLName, MessageData, SentTime
	from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
		join Chat						on UserToChat.ChatID = Chat.ChatID
		join [Messages]					on Chat.ChatID = [Messages].ChatID
	where (UserProfile.UserID = @userid and SentBy = UserProfile.UserID)
							or
		  (UserProfile.UserID = @tutorid and SentBy = UserProfile.UserID);
end;

/**************************************************************************
* Get User Password And Salt By EmailAddress
**************************************************************************/
go
create proc GetUserPasswordAndSalt
(	@EmailAddress varchar(50)	) as
begin
	select UserPassword, Salt
	from UserProfile
	where EmailAddress = @EmailAddress;
end;

/*************************************************************************
* Gets the friends list of a specific user
*************************************************************************/
go
create proc GetUserFriends
(	@userid int	) as
begin
	select DisplayFName, DisplayLName, EmailAddress
	from UserProfile join Friends	on UserProfile.UserID = Friends.FriendUserID
	where Friends.UserID = @userid;
end;

/*************************************************************************
* Gets the reviews a student has submitted
*************************************************************************/
go
create proc GetUserStudentReviews
(	@userid int	) as
begin
	select *
	from Review join Sticker	on Review.StickerID = Sticker.StickerID
	where Sticker.StudentID = @userID;
end;

/*************************************************************************
* Gets the reviews a tutor has submitted
*************************************************************************/
go
create proc GetUserTutorReviews
(	@userid int	) as
begin
	select *
	from Review join Sticker	on Review.StickerID = Sticker.StickerID
	where Sticker.StudentID = @userID;
end;

/*************************************************************************
* Gets all Course Codes
*************************************************************************/
go
create proc GetCourseCodes as
begin
	select CourseCode
	from Classes;
end;

/*************************************************************************
* Gets the ClassID given the code and number
*************************************************************************/
go
create proc GetCourseIDByCodeAndNumber
(	@Code varchar(5),
	@Number smallint
) as
begin
	select ClassID
	from Classes
	where CourseCode = @Code and CourseNumber = @Number;
end;

/*************************************************************************
* Gets the CourseName given the code and number
*************************************************************************/
go
create proc GetCourseNameByCodeAndNumber
(	@code varchar(5),
	@Number smallint
) as
begin
	select CourseName
	from Classes
	where CourseCode = @code and CourseNumber = @Number;
end;

/*************************************************************************
* Gets CourseNumbers given the CourseCode
*************************************************************************/
go
create proc GetCourseNumberByCourseCode
(	@Code varchar(5)	) as
begin
	select CourseNumber
	from Classes
	where CourseCode = @Code;
end;

/*************************************************************************
* Gets the Reports submitted by a user
*************************************************************************/
go
create proc GetReportsByUser
(	@userid int	) as
begin
	select *
	from Report
	where FlaggerID = @userid;
end;

/*************************************************************************
* Gets Stickers submitted by a user
*************************************************************************/
go
create proc GetUserSubmittedStickers
(	@UserID int	) as
begin
	select *
	from Sticker
	where Sticker.StudentID = @UserID;
end

/*************************************************************************
* Gets the Stickers tutored by a user
*************************************************************************/
go
create proc GetUserTutoredStickers
(	@UserID int	) as
begin
	select *
	from Sticker
	where Sticker.TutorID = @UserID;
end;