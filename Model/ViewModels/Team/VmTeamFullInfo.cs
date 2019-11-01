
using Model.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Model.ViewModels.Team
{
    public class VmTeamFullInfo
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Advisor { get; set; }
        public string Leader { get; set; }
        public string TaskName { get; set; }
        public string Judges { get; set; }
        public string Identifier { get; set; }
        public bool? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string UserDefiner { get; set; }
        public DateTime? LastSignIn { get; set; }
        public int? UniversityId { get; set; }
        public string University { get; set; }
        public string StreetLine1 { get; set; }
        public string StreetLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ShortBio { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string ResumeUrl { get; set; }
        public string MemberUserId { get; set; }
        public System.DateTime Date { get; set; }
        public int? TeamState { get; set; }
        public string TeamStateDescription { get; set; }
        public bool? PayStatus{ get; set; }
        public string PayStatusDescription { get; set; }
        public bool SafetyStatus { get; set; }
        public string TeamImageUrl { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool? Survey { get; set; }
        public bool? RegistrationStatus { get; set; }
        public int? SizeId { get; set; }
        public string T_Shirt_Size { get; set; }
        public string EmgyPersonFirstName { get; set; }
        public string EmgyPersonLastName { get; set; }
        public string EmgyPersonPhoneNumber { get; set; }
        public string EmgyPersonRelationship { get; set; }
        public string PhoneNumber { get; set; }
        public string OnActionSuccess { get; set; }
        public string OnActionFailed { get; set; }
        public bool ReadOnlyForm { get; set; }
        public string LabResultUrl { get; set; }
        public string Shipping { get; set; }
        public string WrittenReportUrl { get; set; }
        public int? JacketSizeId { get; set; }
        public string JacketSize { get; set; }
        public int? DietTypeId { get; set; }
        public string DietType { get; set; }
        public string UniversityPictureUrl { get; set; }
        public bool Deactivate { get; set; }
        public bool? SubmitStatus { get; set; }
        public bool? Approved { get; set; }
        public bool? Status { get; set; }
        public string WrittenReportUrlForMember { get; set; }
        public DateTime? WrittenReportDate { get; set; }
    }
}