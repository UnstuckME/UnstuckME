//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace UnstuckMEServerGUI
{
    public partial class Report
    {
        public int ReportID { get; set; }
        public string ReportDescription { get; set; }
        public Nullable<int> FlaggerID { get; set; }
        public int ReviewID { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
        public virtual Review Review { get; set; }
    }
}
