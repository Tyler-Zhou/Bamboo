using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicLogisticsPositionVMs
{
    public partial class BasicLogisticsPositionSearcher : BaseSearcher
    {
        [Display(Name = "名称")]
        public String Name { get; set; }
        [Display(Name = "电话1")]
        public String Phone1 { get; set; }
        [Display(Name = "电话2")]
        public String Phone2 { get; set; }
        [Display(Name = "电话3")]
        public String Phone3 { get; set; }
        [Display(Name = "地址")]
        public String Address { get; set; }

        protected override void InitVM()
        {
        }

    }
}
