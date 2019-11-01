using Model.Base;
using Model.ViewModels.Task;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Model.ViewModels.Person
{
    public partial class VmPerson : BaseViewModel
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int? SizeId { get; set; }
        public int? UniversityId { get; set; }
        public int? DietTypeId { get; set; }
        public int? JacketSizeId { get; set; }
        public string UserId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string ResumeUrl { get; set; }
        public string UniversityUrl { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Display(Name = "Secondary Email")]
        public string SecondaryEmail { get; set; }

        [Display(Name = "T-Shirt Size")]
        public string T_Shirt_Size { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Street Line 1")]
        public string StreetLine1 { get; set; }

        [Display(Name = "Street Line 2")]
        public string StreetLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string EmgyPersonFirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string EmgyPersonLastName { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        public string EmgyPersonPhoneNumber { get; set; }
        [Required]
        [Display(Name = "Relationship")]
        public string EmgyPersonRelationship { get; set; }
        [Required]
        [Display(Name = "Short Bio")]
        public string ShortBio { get; set; }

        [Display(Name = "Welcome Dinner on Sunday April 7th, 2019 at 5:00 PM")]
        public bool WelcomeDinner { get; set; }

        [Display(Name = "Lunch on Monday April 8th, 2019")]
        public bool LunchOnMonday { get; set; }

        [Display(Name = "Lunch on Tuesday")]
        public bool LunchOnTuesday { get; set; }

        [Display(Name = "Reception Network  on Tuesday")]
        public bool ReceptionNetworkOnTuesday { get; set; }

        [Display(Name = "Award Banquet")]
        public bool AwardBanquet { get; set; }

        [Display(Name = "None of the above")]
        public bool NoneOfTheAbove { get; set; }

        public HttpPostedFileBase UploadedProfilePicture { get; set; }
        public HttpPostedFileBase UploadedResume { get; set; }

        public string FulName { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string University { get; set; }
        public string OnActionSuccess { get; set; }
        public string OnActionFailed { get; set; }
        public bool ReadOnlyForm { get; set; }
        public bool HideEmergency { get; set; }
        public object LabResultUrl { get; set; }
        public bool? Sex { get; set; }
        public string JacketSize { get; set; }
        public string DietType { get; set; }
        public string Allergies { get; set; }
        public string UniversityPictureUrl { get; set; }
        public bool? Agreement { get; set; }
        public bool? EmailConfirmed { get; set; }
        public int[] TaskIds { get; set; }
        public string ClientTaskIds { get; set; }
        public IEnumerable<VmTask> TaskList { get; set; }

    }
}
