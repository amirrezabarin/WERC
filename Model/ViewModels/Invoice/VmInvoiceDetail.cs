using System;
using System.ComponentModel;

namespace Model.ViewModels.Invoice
{
    public class VmInvoiceDetail
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public int TeamId { get; set; }

        [DisplayName("Team List")]
        public string TeamName { get; set; }
        public int PaymentRuleId { get; set; }
        public bool PayStatus { get; set; }

        [DisplayName("Type of Registration")]
        public string TypeOfRegistration { get; set; }
        public bool IsFirstTeam { get; set; }

        [DisplayName("First Team or Extra Team")]
        public string FirstTeamOrExtraTeam { get; set; }

        [DisplayName("Origin Cost For Every Team Base on Type of Registration")]
        public decimal TeamUnitCost { get; set; }

        [DisplayName("Number of Extra Participants")]
        public int ExtraParticipantCount { get; set; }

        [DisplayName("Unit Cost For An Extra Participant")]
        public decimal ExtraParticipantUnitCost { get; set; }

        [DisplayName("Amount For Extra Participant")]
        public decimal ExtraParticipantAmount { get; set; }

        [DisplayName("Discount For Every Extra Team")]
        public decimal ExtraTeamDiscount { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsChecked { get; set; }
        public decimal? ConventionalFee { get; set; }

        [DisplayName("Total Service Fee")]
        public decimal TotalConventionalFee { get; set; }

        [DisplayName("Task")]
        public string Task { get; set; }
    }
}
