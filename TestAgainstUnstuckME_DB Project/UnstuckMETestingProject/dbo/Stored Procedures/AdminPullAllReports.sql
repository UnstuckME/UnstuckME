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
