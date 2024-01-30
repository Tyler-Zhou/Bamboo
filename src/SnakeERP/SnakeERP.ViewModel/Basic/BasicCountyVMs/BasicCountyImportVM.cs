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
    public partial class BasicCountyTemplateVM : BaseTemplateVM
    {
        [Display(Name = "编码")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BasicCounty>(x => x.Code);
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BasicCounty>(x => x.Name);
        [Display(Name = "市")]
        public ExcelPropety City_Excel = ExcelPropety.CreateProperty<BasicCounty>(x => x.CityID);

	    protected override void InitVM()
        {
            City_Excel.DataType = ColumnDataType.ComboBox;
            City_Excel.ListItems = DC.Set<BasicCity>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class BasicCountyImportVM : BaseImportVM<BasicCountyTemplateVM, BasicCounty>
    {

    }

}
