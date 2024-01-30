using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleInfoVMs
{
    public partial class BasicVehicleInfoTemplateVM : BaseTemplateVM
    {
        [Display(Name = "号码")]
        public ExcelPropety Number_Excel = ExcelPropety.CreateProperty<BasicVehicleInfo>(x => x.Number);
        [Display(Name = "车辆类型")]
        public ExcelPropety VehicleType_Excel = ExcelPropety.CreateProperty<BasicVehicleInfo>(x => x.VehicleTypeID);
        [Display(Name = "车辆品牌")]
        public ExcelPropety VehicleBrand_Excel = ExcelPropety.CreateProperty<BasicVehicleInfo>(x => x.VehicleBrandID);
        [Display(Name = "是否新能源")]
        public ExcelPropety NewEnergy_Excel = ExcelPropety.CreateProperty<BasicVehicleInfo>(x => x.NewEnergy);
        [Display(Name = "状态")]
        public ExcelPropety Status_Excel = ExcelPropety.CreateProperty<BasicVehicleInfo>(x => x.Status);

	    protected override void InitVM()
        {
            VehicleType_Excel.DataType = ColumnDataType.ComboBox;
            VehicleType_Excel.ListItems = DC.Set<BasicVehicleType>().GetSelectListItems(Wtm, y => y.Name);
            VehicleBrand_Excel.DataType = ColumnDataType.ComboBox;
            VehicleBrand_Excel.ListItems = DC.Set<BasicVehicleBrand>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class BasicVehicleInfoImportVM : BaseImportVM<BasicVehicleInfoTemplateVM, BasicVehicleInfo>
    {

    }

}
