using Model.Base;
using System.Collections.Generic;

namespace Model.ViewModels.Team
{
    public class VmTeamMemberManagement : BaseViewModel
    {

        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string WrittenReportUrl { get; set; }

    }
}
