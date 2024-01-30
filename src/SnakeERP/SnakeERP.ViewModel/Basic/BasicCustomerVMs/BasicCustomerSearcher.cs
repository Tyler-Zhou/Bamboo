using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCustomerVMs
{
    public partial class BasicCustomerSearcher : BaseSearcher
    {
        [Display(Name = "编码")]
        public String Code { get; set; }
        [Display(Name = "名称")]
        public String Name { get; set; }
        [Display(Name = "省")]
        public Guid? ProvinceID { get; set; }
        [Display(Name = "市")]
        public Guid? CityID { get; set; }
        [Display(Name = "县")]
        public Guid? CountyID { get; set; }
        [Display(Name = "手机")]
        public String CellPhone { get; set; }
        [Display(Name = "座机")]
        public String HomePhone { get; set; }
        [Display(Name = "销售员")]
        public Guid? SalesID { get; set; }
        [Display(Name = "地址")]
        public String Address { get; set; }
        [Display(Name = "区域")]
        public String Region { get; set; }

        protected override void InitVM()
        {
        }

    }
}
