using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.DG.DGOrderInfoVMs
{
    public partial class DGOrderInfoListVM : BasePagedListVM<DGOrderInfo_View, DGOrderInfoSearcher>
    {

        protected override IEnumerable<IGridColumn<DGOrderInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<DGOrderInfo_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.ShipDateTime),
                this.MakeGridHeader(x => x.DeliveryCompany),
                this.MakeGridHeader(x => x.DeliveryCompanyPhone),
                this.MakeGridHeader(x => x.DeliveryMan),
                this.MakeGridHeader(x => x.LicensePlate),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<DGOrderInfo_View> GetSearchQuery()
        {
            var query = DC.Set<DGOrderInfo>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckBetween(Searcher.ShipDateTime?.GetStartTime(), Searcher.ShipDateTime?.GetEndTime(), x => x.ShipDateTime, includeMax: false)
                .CheckContain(Searcher.DeliveryMan, x=>x.DeliveryMan)
                .CheckContain(Searcher.LicensePlate, x=>x.LicensePlate)
                .Select(x => new DGOrderInfo_View
                {
				    ID = x.ID,
                    Name = x.Name,
                    ShipDateTime = x.ShipDateTime,
                    DeliveryCompany = x.DeliveryCompany,
                    DeliveryCompanyPhone = x.DeliveryCompanyPhone,
                    DeliveryMan = x.DeliveryMan,
                    LicensePlate = x.LicensePlate,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class DGOrderInfo_View : DGOrderInfo{

    }
}
