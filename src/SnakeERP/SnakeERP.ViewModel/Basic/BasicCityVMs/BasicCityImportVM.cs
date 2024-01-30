using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCityVMs
{
    public partial class BasicCityTemplateVM : BaseTemplateVM
    {
        [Display(Name = "编码")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BasicCity>(x => x.Code);
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BasicCity>(x => x.Name);
        [Display(Name = "省")]
        public ExcelPropety Province_Excel = ExcelPropety.CreateProperty<BasicCity>(x => x.ProvinceID);

	    protected override void InitVM()
        {
            Province_Excel.DataType = ColumnDataType.ComboBox;
            Province_Excel.ListItems = DC.Set<BasicProvince>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class BasicCityImportVM : BaseImportVM<BasicCityTemplateVM, BasicCity>
    {

    }

}
