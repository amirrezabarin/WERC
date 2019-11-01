
using Model.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Model.ViewModels.Task
{
    public class VmTask : BaseViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string[] Grades { get; set; }
        public int[] GradeIds { get; set; }
        public string ClientGradeIds { get; set; }
        public string[] Tests { get; set; }
        public int[] TestIds { get; set; }
        public string ClientTestIds { get; set; }
        public string ImageUrl { get; set; }
        public HttpPostedFileBase UploadedDocument { get; set; }
        public string OnActionSuccess { get; set; }
        public string OnActionFailed { get; set; }
        public bool ReadOnlyForm { get; set; }
        [Required]
        public string Description { get; set; }
    }
}