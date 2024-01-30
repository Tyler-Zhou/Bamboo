using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.DG.DGOrderInfoVMs
{
    public partial class DGOrderInfoTemplateVM : BaseTemplateVM
    {
        [Display(Name = "名称")]
        public ExcelPropety Name_Excel = ExcelPropety.CreateProperty<DGOrderInfo>(x => x.Name);
        [Display(Name = "发货时间")]
        public ExcelPropety ShipDateTime_Excel = ExcelPropety.CreateProperty<DGOrderInfo>(x => x.ShipDateTime);
        [Display(Name = "发货公司")]
        public ExcelPropety DeliveryCompany_Excel = ExcelPropety.CreateProperty<DGOrderInfo>(x => x.DeliveryCompany);
        [Display(Name = "发货公司电话")]
        public ExcelPropety DeliveryCompanyPhone_Excel = ExcelPropety.CreateProperty<DGOrderInfo>(x => x.DeliveryCompanyPhone);
        [Display(Name = "发货人")]
        public ExcelPropety DeliveryMan_Excel = ExcelPropety.CreateProperty<DGOrderInfo>(x => x.DeliveryMan);
        [Display(Name = "车牌")]
        public ExcelPropety LicensePlate_Excel = ExcelPropety.CreateProperty<DGOrderInfo>(x => x.LicensePlate);

	    protected override void InitVM()
        {
        }

    }

    public class DGOrderInfoImportVM : BaseImportVM<DGOrderInfoTemplateVM, DGOrderInfo>
    {

    }

}
