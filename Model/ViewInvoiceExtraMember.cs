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
    
    public partial class ViewInvoiceExtraMember
    {
        public Nullable<int> InvoiceId { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> DateOfIssue { get; set; }
        public Nullable<long> InvoiceNumber { get; set; }
        public Nullable<decimal> InvoiceTotal { get; set; }
        public Nullable<int> Id { get; set; }
        public Nullable<int> PaymentRuleId { get; set; }
        public Nullable<bool> IsFirstTeam { get; set; }
        public Nullable<decimal> TeamUnitCost { get; set; }
        public Nullable<int> ExtraParticipantCount { get; set; }
        public Nullable<decimal> ExtraParticipantUnitCost { get; set; }
        public Nullable<decimal> ExtraParticipantAmount { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> Subtotal { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<decimal> AmountDue { get; set; }
        public string TypeOfRegistration { get; set; }
        public Nullable<decimal> FirstTeamFee { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }
        public string TeamName { get; set; }
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public Nullable<int> UniversityId { get; set; }
        public string University { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool PayStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Sex { get; set; }
        public string UserId { get; set; }
        public int TeamId { get; set; }
        public Nullable<bool> Finished { get; set; }
        public Nullable<decimal> ExtraTeamDiscount { get; set; }
        public Nullable<decimal> ConventionalFee { get; set; }
        public Nullable<decimal> DetailConventionalFee { get; set; }
        public string Task { get; set; }
    }
}
