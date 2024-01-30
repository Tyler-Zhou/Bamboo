using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.DG.DGOrderDetailVMs
{
    public partial class DGOrderDetailSearcher : BaseSearcher
    {
        [Display(Name = "收货人")]
        public String ReceivingMan { get; set; }
        [Display(Name = "订单编号")]
        public String OrderNO { get; set; }
        [Display(Name = "交付状态")]
        public EnumDeliveryStatus? DeliveryStatus { get; set; }

        protected override void InitVM()
        {
        }

    }
}
