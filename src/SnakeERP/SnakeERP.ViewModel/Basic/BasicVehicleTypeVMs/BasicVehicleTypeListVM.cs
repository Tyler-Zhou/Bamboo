using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleTypeVMs
{
    public partial class BasicVehicleTypeListVM : BasePagedListVM<BasicVehicleType_View, BasicVehicleTypeSearcher>
    {

        protected override IEnumerable<IGridColumn<BasicVehicleType_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicVehicleType_View>>{
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicVehicleType_View> GetSearchQuery()
        {
            var query = DC.Set<BasicVehicleType>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .Select(x => new BasicVehicleType_View
                {
				    ID = x.ID,
                    Name = x.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicVehicleType_View : BasicVehicleType{

    }
}
