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
if object_id('GetUsersInAClass') is not null
	drop procedure GetUsersInAClass;
--if object_id('GetUserStickersAndReviews') is not null
--	drop procedure GetUserStickersAndReviews;
if object_id('GetUserOrganizations') is not null
	drop procedure GetUserOrganizations;
if object_id('GetAllUsersInAnOrganization') is not null
	drop procedure GetAllUsersInAnOrganization;
--if object_id('OpenProfilePage') is not null
	--drop procedure OpenProfilePage;
--if object_id('FilterUserReviewsByStarRank') is not null
--	drop procedure FilterUserReviewsByStarRank;
--if object_id('FilterUserReviewsByEqualStarRank') is not null
--	drop procedure FilterUserReviewsByEqualStarRank;
--if object_id('GetAllActiveStickers') is not null
--	drop procedure GetAllActiveStickers;
--if object_id('PullActiveClassSpecificStickers') is not null
--	drop procedure PullActiveClassSpecificStickers;
if object_id('GetActiveStickers_ClassASC') is not null
	drop procedure GetActiveStickers_ClassASC;
if object_id('GetActiveStickers_ClassDESC') is not null
	drop procedure GetActiveStickers_ClassDESC;

if object_id('GetActiveStickersWithOrganization_OrgClassASC') is not null
	drop procedure GetActiveStickersWithOrganization_OrgClassASC;
if object_id('GetActiveStickersWithOrganization_OrgDESC') is not null
	drop procedure GetActiveStickersWithOrganization_OrgDESC;
if object_id('GetActiveStickersWithOrganization_ClassDESC') is not null
	drop procedure GetActiveStickersWithOrganization_ClassDESC;
if object_id('GetActiveStickersWithOrganization_OrgClassDESC') is not null
	drop procedure GetActiveStickersWithOrganization_OrgClassDESC;

if object_id('GetResolvedStickers_ClassASC') is not null
	drop procedure GetResolvedStickers_ClassASC;
if object_id('GetResolvedStickers_ClassDESC') is not null
	drop procedure GetResolvedStickers_ClassDESC;

if object_id('GetTimedOutStickers_ClassASC') is not null
	drop procedure GetTimedOutStickers_ClassASC;
if object_id('GetTimedOutStickers_ClassDESC') is not null
	drop procedure GetTimedOutStickers_ClassDESC;

if object_id('GetUsersOverallStarRank') is not null
	drop procedure GetUsersOverallStarRank;
if object_id('GetUserAvgStudentStarRank') is not null
	drop procedure GetUserAvgStudentStarRank;
if object_id('GetUserAvgTutorStarRank') is not null
	drop procedure GetUserAvgTutorStarRank;
if object_id('GetAllChatsAUserIsPartOF') is not null
	drop procedure GetAllChatsAUserIsPartOF;
if object_id('GetAllMembersOfAChat') is not null
	drop procedure GetAllMembersOfAChat;
if object_id('GetChatMessages') is not null
	drop procedure GetChatMessages;
if object_id('GetUserPasswordAndSalt') is not null
	drop procedure GetUserPasswordAndSalt;
if object_id('GetUserFriends') is not null
	drop procedure GetUserFriends;

if object_id('GetUserStudentReviews_RankASC') is not null
	drop procedure GetUserStudentReviews_RankASC;
if object_id('GetUserStudentReviews_RankDESC') is not null
	drop procedure GetUserStudentReviews_RankDESC;

if object_id('GetUserTutorReviews_RankASC') is not null
	drop procedure GetUserTutorReviews_RankASC;
if object_id('GetUserTutorReviews_RankDESC') is not null
	drop procedure GetUserTutorReviews_RankDESC;

if object_id('GetCourseCodes') is not null
	drop procedure GetCourseCodes;
if object_id('GetCourseIDByCodeAndNumber') is not null
	drop procedure GetCourseIDByCodeAndNumber;
if object_id('GetCourseNameByCodeAndNumber') is not null
	drop procedure GetCourseNameByCodeAndNumber;
if object_id('GetCourseNumberByCourseCode') is not null
	drop procedure GetCourseNumberByCourseCode;

if object_id('GetReportsSubmittedByUser') is not null
	drop procedure GetReportsSubmittedByUser;

if object_id('GetUserSubmittedStickers_ClassASC') is not null
	drop procedure GetUserSubmittedStickers_ClassASC;
if object_id('GetUserSubmittedStickers_ClassDESC') is not null
	drop procedure GetUserSubmittedStickers_ClassDESC;

if object_id('GetUserTutoredStickers_ClassASC') is not null
	drop procedure GetUserTutoredStickers_ClassASC;
if object_id('GetUserTutoredStickers_ClassDESC') is not null
	drop procedure GetUserTutoredStickers_ClassDESC;

/**************************************************************************
* Gets all Stickers for Admin
**************************************************************************/
go
create proc GetAllStickers as
begin
	select StickerID, UserID, DisplayFName, DisplayLName, EmailAddress, Sticker.ClassID,
			ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
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
		and EmailAddress = case when @email is not null
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
* Get all users in a class
**************************************************************************/
go
create proc GetUsersInAClass
(	@classid int	) as
begin
	select UserProfile.UserID, DisplayFName, DisplayLName, EmailAddress, Classes.ClassID,
			Classes.CourseName, Classes.CourseCode, Classes.CourseNumber
	from UserProfile join UserToClass	on UserProfile.UserID = UserToClass.UserID
		join Classes					on UserToClass.ClassID = Classes.ClassID
	where Classes.ClassID = @classid;
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
* Get all users in an organization
**************************************************************************/
go
create proc GetAllUsersInAnOrganization
(	@orgid int	) as
begin
	select UserProfile.UserID, DisplayFName, DisplayLName, EmailAddress, AverageStudentRank,
			AverageTutorRank, OrganizationName
	from UserProfile join OmToUser		on UserProfile.UserID = OmToUser.UserID
		join OfficialMentor				on OfficialMentor.MentorID = OmToUser.MentorID
	where OmToUser.MentorID = @orgid;
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
--go
--create proc FilterUserSubmittedReviewsByStarRank
--(	@starfilter float,
--	@userid int = null
--) as
--begin
--	select DisplayFName, DisplayLName, StarRanking, Description
--	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
--		join Review					on Sticker.StickerID = Review.StickerID
--	where StarRanking >= @starfilter and 
--		UserID = case when @userid is not null
--						then @userid
--					  else UserID
--				 end;
--end;

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
--go
--create proc PullActiveClassSpecificStickers
--(	@Name varchar(50),
--	@Code varchar(5),
--	@Number smallint,
--	@startrow smallint = 0,
--	@endrow smallint = 50,
--	@userid int = null
--) as
--begin
--	select * from (
--		select ROW_NUMBER() over (order by [Timeout]) as [Row], DisplayFName, DisplayLName,
--			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking,
--			SubmitTime, [Timeout]
--		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
--			join Classes				on Sticker.ClassID = Classes.ClassID
--		where CourseName = @Name and CourseCode = @Code
--			and CourseNumber = @Number
--			and UserID = case when @userid is not null
--								then @userid
--							  else UserID
--						 end
--	) as Stickers
--	where [Row] in (@startrow, @endrow);
--end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickers_ClassASC
(	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classid asc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
		where UserProfile.UserID = case when @userid is not null
											then @userid
										else UserProfile.UserID
								   end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
											then @classid
									   else Classes.ClassID
								  end
			and DATEDIFF(second, GETDATE(), [Timeout]) > 0
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickers_ClassDESC
(	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classid desc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
		where UserProfile.UserID = case when @userid is not null
											then @userid
										else UserProfile.UserID
								   end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
											then @classid
									   else Classes.ClassID
								  end
			and DATEDIFF(second, GETDATE(), [Timeout]) > 0
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickersWithOrganization_OrgClassASC
(	@organization int,
	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @organization asc, @classid asc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
		where UserProfile.UserID = case when @userid is not null
											then @userid
										else UserProfile.UserID
								   end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
										then @classid
									   else Classes.ClassID
							   end
			and MentorID in (0, @organization)
			and DATEDIFF(second, GETDATE(), [Timeout]) > 0
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickersWithOrganization_OrgDESC
(	@organization int,
	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @organization desc, @classid asc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
		where UserProfile.UserID = case when @userid is not null
											then @userid
										else UserProfile.UserID
								   end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
										then @classid
									   else Classes.ClassID
							   end
			and MentorID in (0, @organization)
			and DATEDIFF(second, GETDATE(), [Timeout]) > 0
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickersWithOrganization_ClassDESC
(	@organization int,
	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @organization asc, @classid desc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
		where UserProfile.UserID = case when @userid is not null
											then @userid
										else UserProfile.UserID
								   end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
										then @classid
									   else Classes.ClassID
							   end
			and MentorID in (0, @organization)
			and DATEDIFF(second, GETDATE(), [Timeout]) > 0
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickersWithOrganization_OrgClassDESC
(	@organization int,
	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @organization desc, @classid desc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
		where UserProfile.UserID = case when @userid is not null
											then @userid
										else UserProfile.UserID
								   end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
										then @classid
									   else Classes.ClassID
							   end
			and MentorID in (0, @organization)
			and DATEDIFF(second, GETDATE(), [Timeout]) > 0
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull resolved Stickers: filterable by user, which stickers in table,
* submit time, and class
**************************************************************************/
go
create proc GetResolvedStickers_ClassASC
(	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select distinct ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classid asc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join Review					on Review.StickerID = Sticker.StickerID
			join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
		where datediff(minute, getdate(), [Timeout]) < 0
			and (select count(*) from Review where Review.StickerID = Sticker.StickerID) = 2
			and UserID = case when @userid is not null
								then @userid
							  else UserID
						 end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
										then @classid
									   else Classes.ClassID
								  end
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull resolved Stickers: filterable by user, which stickers in table,
* submit time, and class
**************************************************************************/
go
create proc GetResolvedStickers_ClassDESC
(	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select distinct ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classid desc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join Review					on Review.StickerID = Sticker.StickerID
			join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
		where datediff(minute, getdate(), [Timeout]) < 0
			and (select count(*) from Review where Review.StickerID = Sticker.StickerID) = 2
			and UserID = case when @userid is not null
								then @userid
							  else UserID
						 end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
										then @classid
									   else Classes.ClassID
								  end
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull timed out Stickers: filterable by user, which stickers in table,
* submit time, and class
**************************************************************************/
go
create proc GetTimedOutStickers_ClassASC
(	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select distinct ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classid asc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join Review					on Review.StickerID = Sticker.StickerID
			join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
		where datediff(minute, getdate(), [Timeout]) < 0
			and (select count(*) from Review where Review.StickerID = Sticker.StickerID) <> 2
			and UserID = case when @userid is not null
								then @userid
							  else UserID
						 end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
										then @classid
									   else Classes.ClassID
								  end
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Pull timed out Stickers: filterable by user, which stickers in table,
* submit time, and class
**************************************************************************/
go
create proc GetTimedOutStickers_ClassDESC
(	@starrank float = 0,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@userid int = null,
	@classid int = null
) as
begin
	select * from (
		select distinct ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classid desc) as [Row],
			DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
			CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
		from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
			join Classes				on Sticker.ClassID = Classes.ClassID
			join Review					on Review.StickerID = Sticker.StickerID
			join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
		where datediff(minute, getdate(), [Timeout]) < 0
			and (select count(*) from Review where Review.StickerID = Sticker.StickerID) <> 2
			and UserID = case when @userid is not null
								then @userid
							  else UserID
						 end
			and MinimumStarRanking >= case when @starrank <> 0
												then @starrank
										   else MinimumStarRanking
									  end
			and Classes.ClassID = case when @classid is not null
										then @classid
									   else Classes.ClassID
								  end
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/**************************************************************************
* Get all users with a specific overall avg star ranking
**************************************************************************/
go
create proc GetUsersOverallStarRank
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
create proc GetAllChatsAUserIsPartOF
(	@userid int	) as
begin
	select ChatID
	from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
	where @userid = UserToChat.UserID;
end;

/**************************************************************************
* Pull all users in a chat
**************************************************************************/
go
create proc GetAllMembersOfAChat
(	@chatid int	) as
begin
	select UserProfile.UserID, DisplayFName, DisplayLName, EmailAddress, AverageStudentRank,
			AverageTutorRank, TotalStudentReviews, TotalTutorReviews
	from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
	where @chatid = UserToChat.ChatID;
end;

/**************************************************************************
* Pull chat messages and files between users
**************************************************************************/
go
create proc GetChatMessages
(	@chatid int	) as
begin
	select MessageID, DisplayFName, DisplayLName, MessageData, FilePath, SentBy, SentTime
	from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
		join Chat						on UserToChat.ChatID = Chat.ChatID
		join [Messages]					on Chat.ChatID = [Messages].ChatID
	where UserToChat.ChatID = @chatid and UserProfile.UserID = [Messages].SentBy
	order by SentTime desc;
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
* Gets the reviews a student has submitted: filterable by star rank, 
*************************************************************************/
go
create proc GetUserStudentReviews_RankASC
(	@userid int,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float = 0
) as
begin
	select * from (
		select ROW_NUMBER() over (order by StarRanking asc) as [Row], ReviewID,
			Review.StickerID, ReviewerID, StarRanking, Description--, ProblemDescription,
			--ClassID, StudentID, TutorID, MinimumStarRanking, SubmitTime, [Timeout]
		from Review join Sticker	on Review.StickerID = Sticker.StickerID
		where Sticker.StudentID = @userID
			and StarRanking = case when @starrank <> 0
										then @starrank
								   else StarRanking
							  end
	) as Reviews
	where [Row] between @startrow and @endrow;
end;
/*************************************************************************
* Gets the reviews a student has submitted: filterable by star rank, 
*************************************************************************/
go
create proc GetUserStudentReviews_RankDESC
(	@userid int,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float = 0
) as
begin
	select * from (
		select ROW_NUMBER() over (order by StarRanking desc) as [Row], ReviewID,
			Review.StickerID, ReviewerID, StarRanking, Description--, ProblemDescription,
			--ClassID, StudentID, TutorID, MinimumStarRanking, SubmitTime, [Timeout]
		from Review join Sticker	on Review.StickerID = Sticker.StickerID
		where Sticker.StudentID = @userID
			and StarRanking = case when @starrank <> 0
										then @starrank
								   else StarRanking
							  end
	) as Reviews
	where [Row] between @startrow and @endrow;
end;

/*************************************************************************
* Gets the reviews a tutor has submitted
*************************************************************************/
go
create proc GetUserTutorReviews_RankASC
(	@userid int,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float = 0
) as
begin
	select * from (
		select ROW_NUMBER() over (order by StarRanking asc) as [Row], ReviewID,
			Review.StickerID, ReviewerID, StarRanking, Description--, ProblemDescription,
			--ClassID, StudentID, TutorID, MinimumStarRanking, SubmitTime, [Timeout]
		from Review join Sticker	on Review.StickerID = Sticker.StickerID
		where Sticker.TutorID = @userID and Review.ReviewerID = @userid
			and StarRanking = case when @starrank <> 0
										then @starrank
								   else StarRanking
							  end
	) as Reviews
	where [Row] between @startrow and @endrow;
end;
/*************************************************************************
* Gets the reviews a tutor has submitted
*************************************************************************/
go
create proc GetUserTutorReviews_RankDESC
(	@userid int,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float = 0
) as
begin
	select * from (
		select ROW_NUMBER() over (order by StarRanking desc) as [Row], ReviewID,
			Review.StickerID, ReviewerID, StarRanking, Description--, ProblemDescription,
			--ClassID, StudentID, TutorID, MinimumStarRanking, SubmitTime, [Timeout]
		from Review join Sticker	on Review.StickerID = Sticker.StickerID
		where Sticker.TutorID = @userID and Review.ReviewerID = @userid
			and StarRanking = case when @starrank <> 0
										then @starrank
								   else StarRanking
							  end
	) as Reviews
	where [Row] between @startrow and @endrow;
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
create proc GetReportsSubmittedByUser
(	@userid int	) as
begin
	select *
	from Report
	where FlaggerID = @userid;
end;

/*************************************************************************
* Gets Stickers submitted by a user, active and resolved
*************************************************************************/
go
create proc GetUserSubmittedStickers_ClassASC
(	@userID int,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float = 0,
	@classID int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classID asc) as [Row], *
		from Sticker
		where Sticker.StudentID = @userID
			and MinimumStarRanking = case when @starrank <> 0
											then @starrank
										  else MinimumStarRanking
									 end
			and ClassID = case when @classID is not null
									then @classID
							   else ClassID
						  end
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/*************************************************************************
* Gets Stickers submitted by a user, active and resolved
*************************************************************************/
go
create proc GetUserSubmittedStickers_ClassDESC
(	@userID int,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float = 0,
	@classID int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classID desc) as [Row], *
		from Sticker
		where Sticker.StudentID = @userID
			and MinimumStarRanking = case when @starrank <> 0
											then @starrank
										  else MinimumStarRanking
									 end
			and ClassID = case when @classID is not null
									then @classID
							   else ClassID
						  end
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/*************************************************************************
* Gets the Stickers tutored by a user, active and resolved
*************************************************************************/
go
create proc GetUserTutoredStickers_ClassASC
(	@userID int,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float = 0,
	@classID int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classID asc) as [Row], *
		from Sticker
		where Sticker.TutorID = @userID
			and MinimumStarRanking = case when @starrank <> 0
											then @starrank
										  else MinimumStarRanking
									 end
			and ClassID = case when @classID is not null
									then @classID
							   else ClassID
						  end
	) as Stickers
	where [Row] between @startrow and @endrow;
end;

/*************************************************************************
* Gets the Stickers tutored by a user, active and resolved
*************************************************************************/
go
create proc GetUserTutoredStickers_ClassDESC
(	@userID int,
	@startrow smallint = 0,
	@endrow smallint = 50,
	@starrank float = 0,
	@classID int = null
) as
begin
	select * from (
		select ROW_NUMBER() over (order by [Timeout] asc, SubmitTime asc, @classID desc) as [Row], *
		from Sticker
		where Sticker.TutorID = @userID
			and MinimumStarRanking = case when @starrank <> 0
											then @starrank
										  else MinimumStarRanking
									 end
			and ClassID = case when @classID is not null
									then @classID
							   else ClassID
						  end
	) as Stickers
	where [Row] between @startrow and @endrow;
end;