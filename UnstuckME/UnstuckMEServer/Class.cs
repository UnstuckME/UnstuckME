//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace UnstuckMEServer
{
    public partial class Class
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            this.Stickers = new HashSet<Sticker>();
            this.UserProfiles = new HashSet<UserProfile>();
        }
    
        public int ClassID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public short CourseNumber { get; set; }
    
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sticker> Stickers { get; set; }
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
