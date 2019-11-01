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
    
    public partial class InvoiceDetail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int TeamId { get; set; }
        public int PaymentRuleId { get; set; }
        public bool IsFirstTeam { get; set; }
        public decimal TeamUnitCost { get; set; }
        public int ExtraParticipantCount { get; set; }
        public decimal ExtraParticipantUnitCost { get; set; }
        public decimal ExtraParticipantAmount { get; set; }
        public decimal ExtraTeamDiscount { get; set; }
        public decimal Amount { get; set; }
        public Nullable<decimal> ConventionalFee { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual PaymentRule PaymentRule { get; set; }
    }
}