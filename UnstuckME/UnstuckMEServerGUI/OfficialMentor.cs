//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnstuckMEServerGUI
{
    using System;
    using System.Collections.Generic;
    
    public partial class OfficialMentor
    {
        public OfficialMentor()
        {
            this.UserProfiles = new HashSet<UserProfile>();
            this.Stickers = new HashSet<Sticker>();
        }
    
        public int MentorID { get; set; }
        public string OrganizationName { get; set; }
    
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<Sticker> Stickers { get; set; }
    }
}