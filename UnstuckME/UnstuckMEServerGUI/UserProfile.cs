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
    
    public partial class UserProfile
    {
        public UserProfile()
        {
            this.Files = new HashSet<File>();
            this.Friends = new HashSet<Friend>();
            this.Messages = new HashSet<Message>();
            this.Reports = new HashSet<Report>();
            this.Reviews = new HashSet<Review>();
            this.Stickers = new HashSet<Sticker>();
            this.Stickers1 = new HashSet<Sticker>();
            this.OfficialMentors = new HashSet<OfficialMentor>();
            this.Chats = new HashSet<Chat>();
            this.Classes = new HashSet<Class>();
        }
    
        public int UserID { get; set; }
        public string DisplayFName { get; set; }
        public string DisplayLName { get; set; }
        public string EmailAddress { get; set; }
        public string UserPassword { get; set; }
        public string Privileges { get; set; }
        public string Salt { get; set; }
    
        public virtual ICollection<File> Files { get; set; }
        public virtual ICollection<Friend> Friends { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Sticker> Stickers { get; set; }
        public virtual ICollection<Sticker> Stickers1 { get; set; }
        public virtual ICollection<OfficialMentor> OfficialMentors { get; set; }
        public virtual ICollection<Chat> Chats { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}
