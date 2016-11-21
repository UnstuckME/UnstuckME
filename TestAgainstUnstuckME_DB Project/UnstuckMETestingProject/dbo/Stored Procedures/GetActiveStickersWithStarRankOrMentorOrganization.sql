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
