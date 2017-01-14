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
        public string Privileges { get; set; }
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
        public DateTime SubmitTime { get; set; }
        public DateTime Timeout { get; set; }
    }
}
