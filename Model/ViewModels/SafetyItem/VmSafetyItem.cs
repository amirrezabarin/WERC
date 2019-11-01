using System;
using System.Web;

namespace Model.ViewModels.SafetyItem
{
    public class VmSafetyItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Instruction { get; set; }
        public int Priority { get; set; }

    }
}
