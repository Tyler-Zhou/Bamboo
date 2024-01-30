using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicProvinceVMs
{
    public partial class BasicProvinceTemplateVM : BaseTemplateVM
    {
        [Display(Name = "编码")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BasicProvince>(x => x.Code);
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BasicProvince>(x => x.Name);

	    protected override void InitVM()
        {
        }

    }

    public class BasicProvinceImportVM : BaseImportVM<BasicProvinceTemplateVM, BasicProvince>
    {

    }

}
