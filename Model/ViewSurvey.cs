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
    
    public partial class ViewSurvey
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int QuestionPriority { get; set; }
        public int QuestionType { get; set; }
        public int AnswerId { get; set; }
        public int QuestionAnswerId { get; set; }
        public int AnswerType { get; set; }
        public decimal Weight { get; set; }
        public int AnswerPriority { get; set; }
        public string Answer { get; set; }
        public bool TitleVisible { get; set; }
        public string Comment { get; set; }
        public string QuestionComment { get; set; }
        public bool ShowComment { get; set; }
    }
}