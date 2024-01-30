using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCountyVMs
{
    public partial class BasicCountySearcher : BaseSearcher
    {
        [Display(Name = "编码")]
        public String Code { get; set; }
        [Display(Name = "名称")]
        public String Name { get; set; }

        protected override void InitVM()
        {
        }

    }
}
