using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleBrandVMs
{
    public partial class BasicVehicleBrandListVM : BasePagedListVM<BasicVehicleBrand_View, BasicVehicleBrandSearcher>
    {

        protected override IEnumerable<IGridColumn<BasicVehicleBrand_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicVehicleBrand_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicVehicleBrand_View> GetSearchQuery()
        {
            var query = DC.Set<BasicVehicleBrand>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .Select(x => new BasicVehicleBrand_View
                {
				    ID = x.ID,
                    Name = x.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicVehicleBrand_View : BasicVehicleBrand{

    }
}
