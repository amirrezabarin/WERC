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
    
    public partial class ViewTeamGradeDetail
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int GradeDetailId { get; set; }
        public int TaskId { get; set; }
        public string TeamName { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> State { get; set; }
        public string ImageUrl { get; set; }
        public string LabResultUrl { get; set; }
        public string WrittenReportUrl { get; set; }
        public int GradeId { get; set; }
        public string EvaluationItem { get; set; }
        public double MaxPoint { get; set; }
        public string Grade { get; set; }
        public Nullable<double> Point { get; set; }
        public Nullable<int> TeamNumber { get; set; }
        public string Description { get; set; }
        public string JudgeUserId { get; set; }
        public double Coefficient { get; set; }
        public string Signature { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
