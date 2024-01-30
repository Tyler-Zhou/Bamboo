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
    public partial class BasicLogisticsPositionTemplateVM : BaseTemplateVM
    {
        [Display(Name = "物流")]
        public ExcelPropety Logistics_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.LogisticsID);
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.Name);
        [Display(Name = "联系人")]
        public ExcelPropety Contact_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.Contact);
        [Display(Name = "电话1")]
        public ExcelPropety Phone1_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.Phone1);
        [Display(Name = "电话2")]
        public ExcelPropety Phone2_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.Phone2);
        [Display(Name = "电话3")]
        public ExcelPropety Phone3_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.Phone3);
        [Display(Name = "省")]
        public ExcelPropety Province_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.ProvinceID);
        [Display(Name = "市")]
        public ExcelPropety City_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.CityID);
        [Display(Name = "县")]
        public ExcelPropety County_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.CountyID);
        [Display(Name = "地址")]
        public ExcelPropety Address_Excel = ExcelPropety.CreateProperty<BasicLogisticsPosition>(x => x.Address);

	    protected override void InitVM()
        {
            Logistics_Excel.DataType = ColumnDataType.ComboBox;
            Logistics_Excel.ListItems = DC.Set<BasicLogistics>().GetSelectListItems(Wtm, y => y.Name);
            Province_Excel.DataType = ColumnDataType.ComboBox;
            Province_Excel.ListItems = DC.Set<BasicProvince>().GetSelectListItems(Wtm, y => y.Name);
            City_Excel.DataType = ColumnDataType.ComboBox;
            City_Excel.ListItems = DC.Set<BasicCity>().GetSelectListItems(Wtm, y => y.Name);
            County_Excel.DataType = ColumnDataType.ComboBox;
            County_Excel.ListItems = DC.Set<BasicCounty>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class BasicLogisticsPositionImportVM : BaseImportVM<BasicLogisticsPositionTemplateVM, BasicLogisticsPosition>
    {

    }

}
