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
    public partial class BasicCustomerTemplateVM : BaseTemplateVM
    {
        [Display(Name = "编码")]
        public ExcelPropety Code_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.Code);
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.Name);
        [Display(Name = "省")]
        public ExcelPropety Province_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.ProvinceID);
        [Display(Name = "市")]
        public ExcelPropety City_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.CityID);
        [Display(Name = "县")]
        public ExcelPropety County_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.CountyID);
        [Display(Name = "手机")]
        public ExcelPropety CellPhone_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.CellPhone);
        [Display(Name = "座机")]
        public ExcelPropety HomePhone_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.HomePhone);
        [Display(Name = "销售员")]
        public ExcelPropety Sales_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.SalesID);
        [Display(Name = "地址")]
        public ExcelPropety Address_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.Address);
        [Display(Name = "区域")]
        public ExcelPropety Region_Excel = ExcelPropety.CreateProperty<BasicCustomer>(x => x.Region);

	    protected override void InitVM()
        {
            Province_Excel.DataType = ColumnDataType.ComboBox;
            Province_Excel.ListItems = DC.Set<BasicProvince>().GetSelectListItems(Wtm, y => y.Name);
            City_Excel.DataType = ColumnDataType.ComboBox;
            City_Excel.ListItems = DC.Set<BasicCity>().GetSelectListItems(Wtm, y => y.Name);
            County_Excel.DataType = ColumnDataType.ComboBox;
            County_Excel.ListItems = DC.Set<BasicCounty>().GetSelectListItems(Wtm, y => y.Name);
            Sales_Excel.DataType = ColumnDataType.ComboBox;
            Sales_Excel.ListItems = DC.Set<FrameworkUser>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class BasicCustomerImportVM : BaseImportVM<BasicCustomerTemplateVM, BasicCustomer>
    {

    }

}
