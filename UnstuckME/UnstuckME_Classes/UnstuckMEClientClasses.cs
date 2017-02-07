using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnstuckME_Classes
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public int Privileges { get; set; }
        public float AvgTutorRank { get; set; }
        public float AvgStudentRank { get; set; }
        public byte [] UserProfilePictureBytes { get; set; }
    }

    public class UserClass
    {
        public string CourseCode { set; get; }
        public string CourseName { set; get; }
        public short CourseNumber { set; get; }
        public int ClassID { set; get; }
    }
    public class UnstuckMEMessage
    {
        DateTime Time { get; set; }
        string Message { get; set; }
        int UserID { get; set; }
    }
    public class UserMessages
    {
        List<int> Users { get; set; }
        List<UnstuckMEMessage> Messages { get; set; }
    }

    public class UnstuckMEReview
    {
        public int ReviewID { get; set; }
        public int StickerID { get; set; }
        public int ReviewerID { get; set; }
        public float StarRanking { get; set; }
        public string Description { get; set; }
    }

    public class UnstuckMESticker
    {
        public int StickerID { get; set; }
        public string ProblemDescription { get; set; }
        public int ClassID { get; set; }
        public int StudentID { get; set; }
        public int TutorID { get; set; }
        public float MinimumStarRanking { get; set; }
		public List<string> TutoringOrganizations { get; set; }
		public DateTime SubmitTime { get; set; }
        public DateTime Timeout { get; set; }
    }

	public class CourseCodes
    {
        public string Code { get; set; }
    }

    public class UnstuckMESchool
    {
        public int SchoolID { get; set; }
        public string SchoolName { get; set; }
        public string SchoolDomain { get; set; }
        public string SchoolEmailCredentials { get; set; }
        public string ServerIPAdress { get; set; }
        public string ServerName { get; set; }
    }
	public class Organization
	{
		public int MentorID { get; set; }
		public string OrganizationName { get; set; }
	}
}
