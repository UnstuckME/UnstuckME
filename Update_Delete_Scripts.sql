--UnstuckME Update/Delete Scripts
USE UnstuckME_DB;

--********Delete********
--Server
DELETE FROM [Server]
WHERE ServerID = 2

--User Profile
DELETE FROM UserProfile
WHERE UserID = 1;

--Photo
DELETE Picture
WHERE UserID = 1

--File
DELETE Files
WHERE FileID = 1;
--Message
DELETE [Messages]
WHERE MessageID = 1;

--OfficialMentor
DELETE OfficialMentor
WHERE MentorID = 1;

--Class
DELETE Classes
WHERE ClassID = 1;

--Sticker
DELETE Sticker
WHERE StickerID = 1;

--Review
DELETE Review
WHERE ReviewID = 1;

--Review Description
UPDATE Review
SET [Description] = ''
WHERE ReviewID = 1;

--Report
DELETE Report
WHERE ReportID = 1;

--********Update********
--Server Name
UPDATE [Server]
SET ServerName = 'Test Name'
WHERE ServerID = 1;

--ServerIP
UPDATE	dbo.[Server]
SET ServerIP = '192.168.1.1'
WHERE ServerID = 1;

--ServerDomain
UPDATE	[Server]
SET ServerDomain = 'Test Domain'
WHERE ServerID = 1;

--SchoolName
UPDATE	[Server]
SET SchoolName = 'OregonTech'
WHERE ServerID = 1;

--AdminPassword
UPDATE [Server]
SET AdminPassword = 'Password'
WHERE ServerID = 1;

--AdminUsername
UPDATE [Server]
SET AdminUsername = 'admin'
WHERE ServerID = 1;

--EmailCredentials
UPDATE [Server]
SET ServerName = '.edu'
WHERE ServerID = 1;

--Photo
/********************************NEED MORE INFO ON HOW WE ARE STORING PHOTOS*******************************/

--OrganizationName
UPDATE OfficialMentor
SET OrganizationName = 'Test Organization Name'
WHERE MentorID = 1;

--DisplayFName
UPDATE UserProfile
SET DisplayFName = 'Test FName'
WHERE UserID = 1;

--DisplayLName
UPDATE UserProfile
SET DisplayLName = 'Test LName'
WHERE UserID = 1;

--EmailAddress
UPDATE UserProfile
SET EmailAddress = 'Test@oit.edu'
WHERE UserID = 1;

--UserPassword
UPDATE UserProfile
SET UserPassword = CONVERT(VARBINARY(64), 'password')
WHERE UserID = 1;

--Priveleges
UPDATE UserProfile
SET Privileges = CONVERT(BINARY(4), 1)
WHERE UserID = 1;

--ProblemDescription
UPDATE Sticker
SET ProblemDescription = 'This is a problem description'
WHERE StickerID = 1;

--Timeout
UPDATE Sticker
SET [Timeout] = GETDATE()
WHERE StickerID = 1;

--CourseName
UPDATE Classes
SET CourseName = 'Test Course Name'
WHERE ClassID = 1;

--CourseCode
UPDATE Classes
SET CourseCode = 'TST'
WHERE ClassID = 1;

--CourseNumber
UPDATE Classes
SET CourseNumber = '123'
WHERE ClassID = 1;

--TermsOffered
UPDATE Classes
SET TermOffered = 1
WHERE ClassID = 1;

--StarRanking
UPDATE Review
SET StarRanking = 5
WHERE ReviewID = 1;

--Description
UPDATE Review
SET [Description] = 'This is a review description'
WHERE ReviewID = 1;
