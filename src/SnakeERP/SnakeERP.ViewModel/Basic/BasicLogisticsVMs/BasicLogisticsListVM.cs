using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicLogisticsVMs
{
    public partial class BasicLogisticsListVM : BasePagedListVM<BasicLogistics_View, BasicLogisticsSearcher>
    {

        protected override IEnumerable<IGridColumn<BasicLogistics_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicLogistics_View>>{
                this.MakeGridHeader(x => x.Code),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Alias),
                this.MakeGridHeader(x => x.CellPhone),
                this.MakeGridHeader(x => x.Address),
                this.MakeGridHeader(x => x.LicensePlate),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicLogistics_View> GetSearchQuery()
        {
            var query = DC.Set<BasicLogistics>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.Alias, x=>x.Alias)
                .CheckContain(Searcher.CellPhone, x=>x.CellPhone)
                .Select(x => new BasicLogistics_View
                {
				    ID = x.ID,
                    Code = x.Code,
                    Name = x.Name,
                    Alias = x.Alias,
                    CellPhone = x.CellPhone,
                    Address = x.Address,
                    LicensePlate = x.LicensePlate,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicLogistics_View : BasicLogistics{

    }
}
