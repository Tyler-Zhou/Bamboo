using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleBrandVMs
{
    public partial class BasicVehicleBrandTemplateVM : BaseTemplateVM
    {
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<BasicVehicleBrand>(x => x.Name);

	    protected override void InitVM()
        {
        }

    }

    public class BasicVehicleBrandImportVM : BaseImportVM<BasicVehicleBrandTemplateVM, BasicVehicleBrand>
    {

    }

}
