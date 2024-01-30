using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.DG.DGOrderDetailVMs
{
    public partial class DGOrderDetailTemplateVM : BaseTemplateVM
    {
        [Display(Name = "发货单")]
        public ExcelPropety OrderInfo_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.OrderInfoID);
        [Display(Name = "发货人")]
        public ExcelPropety DeliveryMan_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.DeliveryMan);
        [Display(Name = "发货电话")]
        public ExcelPropety DeliveryPhone_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.DeliveryPhone);
        [Display(Name = "发货地址")]
        public ExcelPropety DeliveryAddress_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.DeliveryAddress);
        [Display(Name = "收货人")]
        public ExcelPropety ReceivingMan_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.ReceivingMan);
        [Display(Name = "收货电话")]
        public ExcelPropety ReceivingPhone_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.ReceivingPhone);
        [Display(Name = "收货地址")]
        public ExcelPropety ReceivingAddress_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.ReceivingAddress);
        [Display(Name = "订单编号")]
        public ExcelPropety OrderNO_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.OrderNO);
        [Display(Name = "门数量")]
        public ExcelPropety DoorQuantity_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.DoorQuantity);
        [Display(Name = "套数量")]
        public ExcelPropety SleeveQuantity_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.SleeveQuantity);
        [Display(Name = "线条数量")]
        public ExcelPropety LinesQuantity_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.LinesQuantity);
        [Display(Name = "其他数量")]
        public ExcelPropety OtherQuantity_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.OtherQuantity);
        [Display(Name = "备注")]
        public ExcelPropety Remark_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.Remark);
        [Display(Name = "交付状态")]
        public ExcelPropety DeliveryStatus_Excel = ExcelPropety.CreateProperty<DGOrderDetail>(x => x.DeliveryStatus);

	    protected override void InitVM()
        {
            OrderInfo_Excel.DataType = ColumnDataType.ComboBox;
            OrderInfo_Excel.ListItems = DC.Set<DGOrderInfo>().GetSelectListItems(Wtm, y => y.Name);
        }

    }

    public class DGOrderDetailImportVM : BaseImportVM<DGOrderDetailTemplateVM, DGOrderDetail>
    {

    }

}
