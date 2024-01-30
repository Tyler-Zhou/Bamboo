using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicLogisticsVMs
{
    public partial class BasicLogisticsSearcher : BaseSearcher
    {
        [Display(Name = "编码")]
        public String Code { get; set; }
        [Display(Name = "名称")]
        public String Name { get; set; }
        [Display(Name = "别名")]
        public String Alias { get; set; }
        [Display(Name = "手机")]
        public String CellPhone { get; set; }

        protected override void InitVM()
        {
        }

    }
}
