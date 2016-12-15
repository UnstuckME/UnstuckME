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

    public class UserClasses
    {
        public string CourseCode { set; get; }
        public string CourseName { set; get; }
        public short CourseNumber { set; get; }
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
}
