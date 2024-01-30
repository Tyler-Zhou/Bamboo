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
    public partial class BasicLogisticsTemplateVM : BaseTemplateVM
    {
        [Display(Name = "编码")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BasicLogistics>(x => x.Code);
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BasicLogistics>(x => x.Name);
        [Display(Name = "别名")]
        public ExcelPropety Alias_Excel = ExcelPropety.CreateProperty<BasicLogistics>(x => x.Alias);
        [Display(Name = "手机")]
        public ExcelPropety CellPhone_Excel = ExcelPropety.CreateProperty<BasicLogistics>(x => x.CellPhone);
        [Display(Name = "地址")]
        public ExcelPropety Address_Excel = ExcelPropety.CreateProperty<BasicLogistics>(x => x.Address);
        [Display(Name = "车牌")]
        public ExcelPropety LicensePlate_Excel = ExcelPropety.CreateProperty<BasicLogistics>(x => x.LicensePlate);

	    protected override void InitVM()
        {
        }

    }

    public class BasicLogisticsImportVM : BaseImportVM<BasicLogisticsTemplateVM, BasicLogistics>
    {

    }

}
