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
    
    public partial class ViewTaskTeam
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TaskImageUrl { get; set; }
        public string TeamImageUrl { get; set; }
        public Nullable<int> TeamState { get; set; }
        public System.DateTime Date { get; set; }
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public string University { get; set; }
        public Nullable<bool> Survey { get; set; }
        public Nullable<bool> RegistrationStatus { get; set; }
        public string LabResultUrl { get; set; }
        public string MemberName { get; set; }
        public string WrittenReportUrl { get; set; }
        public Nullable<int> TeamNumber { get; set; }
        public string UniversityPictureUrl { get; set; }
        public bool PayStatus { get; set; }
        public string TaskDescription { get; set; }
        public bool Deactivate { get; set; }
        public Nullable<System.DateTime> WrittenReportDate { get; set; }
        public bool SuppressScoring { get; set; }
    }
}