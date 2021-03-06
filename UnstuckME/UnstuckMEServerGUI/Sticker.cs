//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace UnstuckMEServerGUI
{
    public partial class Sticker
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sticker()
        {
            this.Reviews = new HashSet<Review>();
            this.OfficialMentors = new HashSet<OfficialMentor>();
        }
    
        public int StickerID { get; set; }
        public string ProblemDescription { get; set; }
        public int ClassID { get; set; }
        public Nullable<int> ChatID { get; set; }
        public int StudentID { get; set; }
        public Nullable<int> TutorID { get; set; }
        public Nullable<double> MinimumStarRanking { get; set; }
        public DateTime SubmitTime { get; set; }
        public DateTime Timeout { get; set; }
    
        public virtual Chat Chat { get; set; }
        public virtual Class Class { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual UserProfile UserProfile { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfficialMentor> OfficialMentors { get; set; }
    }
}
