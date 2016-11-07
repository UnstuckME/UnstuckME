/*************************************
* Gets server information
*************************************/

select ServerName, ServerIP, ServerDomain, SchoolName, EmailCredentials
from Server;

/*************************************
* Gets Administrator information
*************************************/

select AdminUsername, AdminPassword
from Server;

/*************************************
* Updates email credentials, school name on server
*************************************/

declare @email_credential nvarchar(15),
		@school_name nvarchar(25);

if @email_credential like '@%.%'
begin
	update Server
	set EmailCredentials = @email_credential,
		SchoolName = @school_name
end
else
begin
	print 'Email credentials must contain a "@" and "." in order to be valid'
end;

/*************************************
* Update Admin Username and password on server
* This will be done later because passwords will have encryption
*************************************/

/*************************************
* User opens profile page
*************************************/

select UserProfile.DisplayFName, UserProfile.DisplayLName, UserProfile.EmailAddress, Classes.CourseName,
		Classes.CourseCode, Classes.CourseNumber, Sticker.ClassID, Sticker.SubmitTime,
		Sticker.ProblemDescription as StickerDescription, Review.StarRanking, Review.Description as ReviewDescription
from UserProfile join UserToClass	on UserProfile.UserID = UserToClass.UserID
	join Classes					on UserToClass.ClassID = Classes.ClassID
	join Sticker					on Sticker.StudentID = UserProfile.UserID
	join Review						on Review.ReviewID = Sticker.TutorReviewID

/*************************************
* Filter a sticker based on a >= star ranking
*************************************/

declare @starfilter tinyint

select StarRanking, Description
from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
	join Review					on Review.ReviewID = Sticker.TutorReviewID--?
where StarRanking >= @starfilter

/*************************************
* Filter a sticker based on a specific star ranking
*************************************/

select StarRanking, Description
from UserProfile join Sticker	on UserProfile.UserID = Sticker.StudentID
	join Review					on Review.ReviewID = Sticker.TutorReviewID--?
where StarRanking = @starfilter