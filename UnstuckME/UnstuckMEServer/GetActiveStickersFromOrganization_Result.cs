//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace UnstuckMEServer
{
    public partial class GetActiveStickersFromOrganization_Result
    {
        public string DisplayFName { get; set; }
        public string DisplayLName { get; set; }
        public int StickerID { get; set; }
        public int ClassID { get; set; }
        public Nullable<int> ChatID { get; set; }
        public int StudentID { get; set; }
        public Nullable<int> TutorID { get; set; }
        public string CourseCode { get; set; }
        public short CourseNumber { get; set; }
        public string CourseName { get; set; }
        public string ProblemDescription { get; set; }
        public Nullable<double> MinimumStarRanking { get; set; }
        public DateTime SubmitTime { get; set; }
        public DateTime Timeout { get; set; }
    }
}
