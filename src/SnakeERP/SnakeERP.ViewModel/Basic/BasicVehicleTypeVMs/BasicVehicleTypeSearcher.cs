using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleTypeVMs
{
    public partial class BasicVehicleTypeSearcher : BaseSearcher
    {
        [Display(Name = "名称")]
        public String Name { get; set; }

        protected override void InitVM()
        {
        }

    }
}
