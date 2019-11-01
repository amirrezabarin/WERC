//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            this.TeamGradeDetails = new HashSet<TeamGradeDetail>();
            this.TeamMembers = new HashSet<TeamMember>();
            this.TeamSafetyItems = new HashSet<TeamSafetyItem>();
        }
    
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> State { get; set; }
        public string ImageUrl { get; set; }
        public string LabResultUrl { get; set; }
        public string WrittenReportUrl { get; set; }
        public Nullable<int> TeamNumber { get; set; }
        public bool PayStatus { get; set; }
        public bool Deactivate { get; set; }
        public Nullable<bool> SubmitStatus { get; set; }
        public Nullable<System.DateTime> WrittenReportDate { get; set; }
        public bool SuppressScoring { get; set; }
    
        public virtual Task Task { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamGradeDetail> TeamGradeDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamMember> TeamMembers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamSafetyItem> TeamSafetyItems { get; set; }
    }
}