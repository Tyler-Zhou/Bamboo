using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleInfoVMs
{
    public partial class BasicVehicleInfoSearcher : BaseSearcher
    {
        [Display(Name = "号码")]
        public String Number { get; set; }
        [Display(Name = "是否新能源")]
        public Boolean? NewEnergy { get; set; }
        [Display(Name = "状态")]
        public EnumVehicleStatus? Status { get; set; }

        protected override void InitVM()
        {
        }

    }
}
