/**************************************************************************
* UnstuckME Common Queries
**************************************************************************/
use UnstuckME_DB
go

/**************************************************************************
* Drop existing stored procedures
**************************************************************************/
if object_id('GetUsersThatCanTutorASticker') is not null
	drop procedure GetUsersThatCanTutorASticker;
if object_id('GetSchoolName') is not null
	drop procedure GetSchoolName;
if object_id('InitialStickerPull') is not null
	drop procedure InitialStickerPull;
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
if object_id('GetUserOrganizations') is not null
	drop procedure GetUserOrganizations;
if object_id('GetAllUsersInAnOrganization') is not null
	drop procedure GetAllUsersInAnOrganization;
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
if object_id('CheckIfAChatBetweenTwoUsersExists') is not null
	drop procedure CheckIfAChatBetweenTwoUsersExists;
if object_id('GetUserPasswordAndSalt') is not null
	drop procedure GetUserPasswordAndSalt;
if object_id('GetUserFriends') is not null
	drop procedure GetUserFriends;
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
if object_id('GetAllResolvedOrTimedOutStickers') is not null
	drop procedure GetAllResolvedOrTimedOutStickers;
if object_id('GetActiveStickers') is not null
	drop procedure GetActiveStickers;
if object_id('GetResolvedStickers') is not null
	drop procedure GetResolvedStickers;
if object_id('GetTimedOutStickers') is not null
	drop procedure GetTimedOutStickers;
if object_id('GetUserStudentReviews') is not null
	drop procedure GetUserStudentReviews;
if object_id('GetUserTutorReviews') is not null
	drop procedure GetUserTutorReviews;
if object_id('GetUserSubmittedStickers') is not null
	drop procedure GetUserSubmittedStickers;
if object_id('GetUserTutoredStickers') is not null
	drop procedure GetUserTutoredStickers;
if object_id('GetNumMsgsInAChat') is not null 
  drop procedure GetNumMsgsInAChat;
if object_id('GetStickerInfo') is not null
	drop procedure GetStickerInfo;
if object_id('Ryans_GetChatMessage') is not null
	drop procedure Ryans_GetChatMessage;

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
GO

/**************************************************************************
* Gets all stickers a user can tutor for (Has all checks)
**************************************************************************/

CREATE PROC [dbo].[InitialStickerPull]
(
	@InUserID		INT
)
AS
	BEGIN
		SELECT * From 
		(SELECT DISTINCT Sticker.StickerID, ProblemDescription, c.AverageStudentRank as StudentRanking, Classes.CourseCode, Classes.CourseName, Classes.CourseNumber, Sticker.ClassID, ChatID, StudentID, TutorID, MinimumStarRanking, SubmitTime, Timeout 
		FROM Sticker 
		JOIN UserProfile as c ON Sticker.StudentID = c.UserID               /*Gets Student Profile*/
		JOIN Classes ON Sticker.ClassID = Classes.ClassID
		JOIN UserToClass ON Classes.ClassID = UserToClass.ClassID
		JOIN UserProfile as a ON UserToClass.UserID = @InUserID
		JOIN OmToUser ON OmToUser.UserID = @InUserID
		FULL JOIN StickerToMentor ON Sticker.StickerID = StickerToMentor.StickerID				
		WHERE	(Sticker.TutorID IS NULL) 
				AND a.UserID = @InUserID
				AND Sticker.StudentID != @InUserID
				AND a.AverageTutorRank >= Sticker.MinimumStarRanking
				AND Sticker.Timeout > GETDATE()
				AND StickerToMentor.MentorID = OmToUser.MentorID) AS Result1
		UNION
		SELECT DISTINCT Sticker.StickerID, ProblemDescription, c.AverageStudentRank as StudentRanking, Classes.CourseCode, Classes.CourseName, Classes.CourseNumber, Sticker.ClassID, ChatID, StudentID, TutorID, MinimumStarRanking, SubmitTime, Timeout
		FROM Sticker
		JOIN UserProfile as c ON Sticker.StudentID = c.UserID               /*Gets Student Profile*/
		JOIN Classes ON Sticker.ClassID = Classes.ClassID
		JOIN UserToClass ON Classes.ClassID = UserToClass.ClassID
		JOIN UserProfile as a ON UserToClass.UserID = @InUserID
		FULL JOIN StickerToMentor ON StickerToMentor.StickerID = Sticker.StickerID
		WHERE (StickerToMentor.StickerID is NULL)
				AND (Sticker.TutorID IS NULL) 
				AND a.UserID = @InUserID
				AND Sticker.StudentID != @InUserID
				AND a.AverageTutorRank >= Sticker.MinimumStarRanking
				AND Sticker.Timeout > GETDATE()
	END
GO

/**************************************************************************
* Pull All Users That Can Tutor a Sticker (Tests If Tutoring Filter Is Applied)
**************************************************************************/
CREATE PROC GetUsersThatCanTutorASticker
(
	@inStickerID INT
)
AS
	BEGIN /*IF TUTORING FILTER IS APPLIED*/
		IF EXISTS (SELECT * FROM StickerToMentor JOIN Sticker ON Sticker.StickerID = StickerToMentor.StickerID WHERE Sticker.StickerID = @inStickerID)
		BEGIN
			SELECT DISTINCT UserProfile.UserID FROM Sticker
			JOIN StickerToMentor AS B ON B.StickerID = Sticker.StickerID
			JOIN Classes ON Sticker.ClassID = Classes.ClassID
			JOIN UserToClass ON Classes.ClassID = UserToClass.ClassID
			JOIN UserProfile ON UserToClass.UserID = UserProfile.UserID
			JOIN OmToUser ON UserProfile.UserID = OmToUser.UserID
			JOIN StickerToMentor AS a ON B.MentorID = OmToUser.MentorID
			WHERE Sticker.StickerID = @inStickerID AND UserProfile.AverageTutorRank >= Sticker.MinimumStarRanking	AND UserProfile.UserID != Sticker.StudentID
		END
		ELSE /*NO TUTORING FILTER*/
		BEGIN
			SELECT DISTINCT UserProfile.UserID FROM Sticker
			JOIN Classes ON Classes.ClassID = Sticker.ClassID
			JOIN UserToClass ON Classes.ClassID = UserToClass.ClassID
			JOIN UserProfile ON UserToClass.UserID = UserProfile.UserID
			WHERE Sticker.StickerID = @inStickerID AND UserProfile.AverageTutorRank >= Sticker.MinimumStarRanking AND UserProfile.UserID != Sticker.StudentID
		END
	END
GO

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
* Pull active Stickers available with a certain star ranking and/or a
* specific mentor organization (mentor specification, and user
* specification is optional)
**************************************************************************/
go
create proc GetActiveStickers
(	@loggedin_user int,
	@organization int = null,
	@starrank float = 0,
	@userid int = null,
	@classid int = null
) as
begin
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
		full join StickerToMentor	on Sticker.StickerID = StickerToMentor.StickerID
		right join (select * from OmToUser where UserID = @loggedin_user) as UsersinOrgs
			on UsersinOrgs.MentorID = StickerToMentor.MentorID
		join (select * from UserToClass where UserID = @loggedin_user) as UserClasses
			on Sticker.ClassID = UserClasses.ClassID
	where Sticker.TutorID is null and Sticker.StudentID <> @loggedin_user
		and UserProfile.UserID = case when @userid is not null
										then @userid
									else UserProfile.UserID
							   end
		and MinimumStarRanking <= case when @starrank <> 0
											then @starrank
									   else MinimumStarRanking
								  end
		and Classes.ClassID = case when @classid is not null
										then @classid
								   else Classes.ClassID
							  end
		and StickerToMentor.MentorID = case when @organization is not null
												then @organization
											else StickerToMentor.MentorID
									   end
		and DATEDIFF(second, GETDATE(), [Timeout]) > 0
union
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
		full join StickerToMentor	on Sticker.StickerID = StickerToMentor.StickerID
		join (select * from UserToClass where UserID = @loggedin_user) as UserClasses
			on Sticker.ClassID = UserClasses.ClassID
	where Sticker.TutorID is null and Sticker.StudentID <> @loggedin_user
		and UserProfile.UserID = case when @userid is not null
										then @userid
									else UserProfile.UserID
							   end
		and MinimumStarRanking <= case when @starrank <> 0
											then @starrank
									   else MinimumStarRanking
								  end
		and Classes.ClassID = case when @classid is not null
										then @classid
								   else Classes.ClassID
							  end
		and DATEDIFF(second, GETDATE(), [Timeout]) > 0
		and StickerToMentor.MentorID is null
order by [Timeout] asc, SubmitTime asc;
end;

/**************************************************************************
* Pull resolved Stickers: filterable by user, which stickers in table,
* submit time, and class
**************************************************************************/
go
create proc GetResolvedStickers
(	@starrank float = 0,
	@organization int = null,
	@userid int = null,
	@classid int = null
) as
begin
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
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
		and MinimumStarRanking <= case when @starrank <> 0
											then @starrank
									   else MinimumStarRanking
								  end
		and Classes.ClassID = case when @classid is not null
									then @classid
								   else Classes.ClassID
							  end
		and StickerToMentor.MentorID = case when @organization is not null
												then @organization
											else StickerToMentor.MentorID
									   end
union
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
		join Review					on Review.StickerID = Sticker.StickerID
	where datediff(minute, getdate(), [Timeout]) < 0
		and (select count(*) from Review where Review.StickerID = Sticker.StickerID) = 2
		and UserID = case when @userid is not null
							then @userid
						  else UserID
					 end
		and MinimumStarRanking <= case when @starrank <> 0
											then @starrank
									   else MinimumStarRanking
								  end
		and Classes.ClassID = case when @classid is not null
									then @classid
								   else Classes.ClassID
							  end
order by [Timeout] asc, SubmitTime asc;
end;

/**************************************************************************
* Pull timed out Stickers: filterable by user, which stickers in table,
* submit time, and class
**************************************************************************/
go
create proc GetTimedOutStickers
(	@starrank float = 0,
	@organization int = null,
	@userid int = null,
	@classid int = null
) as
begin
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
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
		and MinimumStarRanking <= case when @starrank <> 0
											then @starrank
									   else MinimumStarRanking
								  end
		and Classes.ClassID = case when @classid is not null
									then @classid
								   else Classes.ClassID
							  end
		and StickerToMentor.MentorID = case when @organization is not null
												then @organization
											else StickerToMentor.MentorID
									   end
union
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
		join Review					on Review.StickerID = Sticker.StickerID
	where datediff(minute, getdate(), [Timeout]) < 0
		and (select count(*) from Review where Review.StickerID = Sticker.StickerID) <> 2
		and UserID = case when @userid is not null
							then @userid
						  else UserID
					 end
		and MinimumStarRanking <= case when @starrank <> 0
											then @starrank
									   else MinimumStarRanking
								  end
		and Classes.ClassID = case when @classid is not null
									then @classid
								   else Classes.ClassID
							  end
order by [Timeout] asc, SubmitTime asc;
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
(	@chatid int,
	@startrow smallint = 0,
	@endrow smallint = 75
) as
begin
	select * from (
		select ROW_NUMBER() over (order by SentTime desc, MessageID desc) as [Row],
			MessageID, DisplayFName, DisplayLName, MessageData, FilePath, SentBy, SentTime
		from UserProfile join UserToChat	on UserProfile.UserID = UserToChat.UserID
			join Chat						on UserToChat.ChatID = Chat.ChatID
			join [Messages]					on Chat.ChatID = [Messages].ChatID
		where UserToChat.ChatID = @chatid and UserProfile.UserID = [Messages].SentBy
	) as [ChatMessages]
	where [Row] between @startrow and @endrow
	order by [Row] desc
end;

/**************************************************************************
* Pull chat messages and files between users
**************************************************************************/
go
create proc Ryans_GetChatMessage
(	@chatid int,
	@startingID int = 0,
	@nummessages int = 20
) as
begin
	select Top (@nummessages) MessageID, MessageData, SentBy, SentTime
	from Messages
	where ChatID = @chatID and MessageID <= @startingID
	order by MessageID desc
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
	select FriendUserID, DisplayFName, DisplayLName, EmailAddress
	from UserProfile join Friends	on UserProfile.UserID = Friends.FriendUserID
	where Friends.UserID = @userid;
end;

/*************************************************************************
* Gets the reviews a student has submitted: filterable by star rank, 
*************************************************************************/
go
create proc GetUserStudentReviews
(	@userid int,
	@starrank float = 0
) as
begin
	select distinct ReviewID, Review.StickerID, ReviewerID, StarRanking, Description
	from Review join Sticker	on Review.StickerID = Sticker.StickerID
	where Sticker.StudentID = @userID
		and StarRanking <= case when @starrank <> 0
									then @starrank
							   else StarRanking
						  end
	order by StarRanking asc;
end;

/*************************************************************************
* Gets the reviews a tutor has submitted
*************************************************************************/
go
create proc GetUserTutorReviews
(	@userid int,
	@starrank float = 0
) as
begin
	select distinct ReviewID, Review.StickerID, ReviewerID, StarRanking, Description
	from Review join Sticker	on Review.StickerID = Sticker.StickerID
	where Sticker.TutorID = @userID and Review.ReviewerID = @userid
		and StarRanking <= case when @starrank <> 0
									then @starrank
							   else StarRanking
						  end
	order by StarRanking asc;
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
create proc GetUserSubmittedStickers
(	@userID int,
	@organization int = null,
	@starrank float = 0,
	@classID int = null
) as
begin
	select distinct DisplayFName, DisplayLName, StickerID, Sticker.ClassID, ChatID, StudentID, TutorID, 
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker		on UserProfile.UserID = Sticker.StudentID
		join Classes					on Classes.ClassID = Sticker.ClassID
	where Sticker.StudentID = @userID
		and MinimumStarRanking <= case when @starrank <> 0
										then @starrank
									  else MinimumStarRanking
								 end
		and Sticker.ClassID = case when @classID is not null
								then @classID
						   else Sticker.ClassID
					  end
union
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker		on UserProfile.UserID = Sticker.StudentID
		join Classes					on Classes.ClassID = Sticker.ClassID
		join StickerToMentor			on StickerToMentor.StickerID = Sticker.StickerID
	where Sticker.StudentID = @userID
		and MinimumStarRanking <= case when @starrank <> 0
										then @starrank
									  else MinimumStarRanking
								 end
		and Sticker.ClassID = case when @classID is not null
								then @classID
						   else Sticker.ClassID
					  end
		and StickerToMentor.MentorID = case when @organization is not null
												then @organization
											else StickerToMentor.MentorID
									   end
order by [Timeout] asc, SubmitTime asc;
end;

/*************************************************************************
* Gets the Stickers tutored by a user, active and resolved
*************************************************************************/
go
create proc GetUserTutoredStickers
(	@userID int,
	@organization int = null,
	@starrank float = 0,
	@classID int = null
) as
begin
	select distinct DisplayFName, DisplayLName, StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker		on UserProfile.UserID = Sticker.StudentID
		join Classes					on Classes.ClassID = Sticker.ClassID
	where Sticker.TutorID = @userID
		and MinimumStarRanking <= case when @starrank <> 0
										then @starrank
									  else MinimumStarRanking
								 end
		and Sticker.ClassID = case when @classID is not null
								then @classID
						   else Sticker.ClassID
					  end
union
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, ChatID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker		on UserProfile.UserID = Sticker.StudentID
		join Classes					on Classes.ClassID = Sticker.ClassID
		join StickerToMentor			on StickerToMentor.StickerID = Sticker.StickerID
	where Sticker.TutorID = @userID
		and MinimumStarRanking <= case when @starrank <> 0
										then @starrank
									  else MinimumStarRanking
								 end
		and Sticker.ClassID = case when @classID is not null
								then @classID
						   else Sticker.ClassID
					  end
		and StickerToMentor.MentorID = case when @organization is not null
												then @organization
											else StickerToMentor.MentorID
									   end
order by [Timeout] asc, SubmitTime asc;
end;

/************************************************************************* 
* Gets the number of messages a chat has 
*************************************************************************/ 
go 
create proc GetNumMsgsInAChat 
(  @chatID int 
) as 
begin 
  SELECT COUNT(*) AS NumberOfMsgInChat FROM [Messages] 
  WHERE ChatID = @chatID; 
end;


/*************************************************************************
* Gets the Stickers associated with a user that are either resolved or timed out
*************************************************************************/
go
create proc GetAllResolvedOrTimedOutStickers
(	@userID int,
	@organization int = null,
	@starrank float = 0,
	@classID int = null
) as
begin
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
		join Review					on Review.StickerID = Sticker.StickerID
	where Sticker.StudentID = @userID
		and MinimumStarRanking <= case when @starrank <> 0
											then @starrank
									   else MinimumStarRanking
								  end
		and Classes.ClassID = case when @classid is not null
									  then @classid
								   else Classes.ClassID
							  end
		and datediff(minute, getdate(), [Timeout]) < 0
		and (select count(*) from Review where Review.StickerID = Sticker.StickerID) < 2
union
	select distinct DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
		join Classes				on Sticker.ClassID = Classes.ClassID
		join Review					on Review.StickerID = Sticker.StickerID
		join StickerToMentor		on Sticker.StickerID = StickerToMentor.StickerID
	where Sticker.StudentID = @userID
		and MinimumStarRanking <= case when @starrank <> 0
											then @starrank
									   else MinimumStarRanking
								  end
		and Classes.ClassID = case when @classid is not null
									  then @classid
								   else Classes.ClassID
							  end
		and StickerToMentor.MentorID = case when @organization is not null
												then @organization
											else StickerToMentor.MentorID
									   end
		and datediff(minute, getdate(), [Timeout]) < 0
		and (select count(*) from Review where Review.StickerID = Sticker.StickerID) < 2
order by [Timeout] asc, SubmitTime asc;
end;

/*************************************************************************
* Gets the Stickers associated with a user that are either resolved or timed out
*************************************************************************/
go
create proc GetStickerInfo
(	@stickerID int	) as
begin
	select DisplayFName, DisplayLName, Sticker.StickerID, Sticker.ClassID, StudentID, TutorID,
		CourseCode, CourseNumber, CourseName, ProblemDescription, MinimumStarRanking, SubmitTime, [Timeout]
	from UserProfile join Sticker	on UserProfile.UserId = Sticker.StudentID
		join Classes				on Classes.ClassID = Sticker.ClassID
		full join StickerToMentor	on StickerToMentor.StickerID = Sticker.StickerID
	where Sticker.StickerID = 129
end;