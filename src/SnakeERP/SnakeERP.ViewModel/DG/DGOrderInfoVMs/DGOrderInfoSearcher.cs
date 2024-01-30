using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.DG.DGOrderInfoVMs
{
    public partial class DGOrderInfoSearcher : BaseSearcher
    {
        [Display(Name = "名称")]
        public String Name { get; set; }
        [Display(Name = "发货时间")]
        public DateRange ShipDateTime { get; set; }
        [Display(Name = "发货人")]
        public String DeliveryMan { get; set; }
        [Display(Name = "车牌")]
        public String LicensePlate { get; set; }

        protected override void InitVM()
        {
        }

    }
}
