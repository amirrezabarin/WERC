using System.Collections.Generic;
using Model.Base;
using Model.ViewModels.Grade.Report;

namespace Model.ViewModels.Judge
{
    public class VmFinalGradeReportManagement : BaseViewModel
    {
        public IEnumerable<VmTaskBaseGrade> GradeReportList { get; set; }
        public IEnumerable<VmTaskBaseGrade> OtherTeamsGradeReportList { get; set; }
        public string ViewLayout { get; set; }
    }
}
