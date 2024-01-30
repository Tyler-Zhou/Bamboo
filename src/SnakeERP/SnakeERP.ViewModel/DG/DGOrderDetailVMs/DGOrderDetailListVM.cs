using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.DG.DGOrderDetailVMs
{
    public partial class DGOrderDetailListVM : BasePagedListVM<DGOrderDetail_View, DGOrderDetailSearcher>
    {

        protected override IEnumerable<IGridColumn<DGOrderDetail_View>> InitGridHeader()
        {
            return new List<GridColumn<DGOrderDetail_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.DeliveryMan),
                this.MakeGridHeader(x => x.DeliveryPhone),
                this.MakeGridHeader(x => x.DeliveryAddress),
                this.MakeGridHeader(x => x.ReceivingMan),
                this.MakeGridHeader(x => x.ReceivingPhone),
                this.MakeGridHeader(x => x.ReceivingAddress),
                this.MakeGridHeader(x => x.OrderNO),
                this.MakeGridHeader(x => x.DoorQuantity),
                this.MakeGridHeader(x => x.SleeveQuantity),
                this.MakeGridHeader(x => x.LinesQuantity),
                this.MakeGridHeader(x => x.OtherQuantity),
                this.MakeGridHeader(x => x.Remark),
                this.MakeGridHeader(x => x.DeliveryStatus),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DGOrderDetail_View> GetSearchQuery()
        {
            var query = DC.Set<DGOrderDetail>()
                .CheckContain(Searcher.ReceivingMan, x=>x.ReceivingMan)
                .CheckContain(Searcher.OrderNO, x=>x.OrderNO)
                .CheckEqual(Searcher.DeliveryStatus, x=>x.DeliveryStatus)
                .Select(x => new DGOrderDetail_View
                {
				    ID = x.ID,
                    Name_view = x.OrderInfo.Name,
                    DeliveryMan = x.DeliveryMan,
                    DeliveryPhone = x.DeliveryPhone,
                    DeliveryAddress = x.DeliveryAddress,
                    ReceivingMan = x.ReceivingMan,
                    ReceivingPhone = x.ReceivingPhone,
                    ReceivingAddress = x.ReceivingAddress,
                    OrderNO = x.OrderNO,
                    DoorQuantity = x.DoorQuantity,
                    SleeveQuantity = x.SleeveQuantity,
                    LinesQuantity = x.LinesQuantity,
                    OtherQuantity = x.OtherQuantity,
                    Remark = x.Remark,
                    DeliveryStatus = x.DeliveryStatus,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DGOrderDetail_View : DGOrderDetail{
        [Display(Name = "名称")]
        public String Name_view { get; set; }

    }
}
