//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnstuckMEServer
{
    public partial class GetUserInfo_Result
    {
        public int UserID { get; set; }
        public string DisplayFName { get; set; }
        public string DisplayLName { get; set; }
        public string EmailAddress { get; set; }
        public string UserPassword { get; set; }
        public double AverageStudentRank { get; set; }
        public double AverageTutorRank { get; set; }
        public int TotalTutorReviews { get; set; }
        public int TotalStudentReviews { get; set; }
        public int Privileges { get; set; }
        public string Salt { get; set; }
    }
}
