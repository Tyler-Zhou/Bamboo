using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleInfoVMs
{
    public partial class BasicVehicleInfoListVM : BasePagedListVM<BasicVehicleInfo_View, BasicVehicleInfoSearcher>
    {

        protected override IEnumerable<IGridColumn<BasicVehicleInfo_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicVehicleInfo_View>>{
                this.MakeGridHeader(x => x.Number),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.NewEnergy),
                this.MakeGridHeader(x => x.Status),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicVehicleInfo_View> GetSearchQuery()
        {
            var query = DC.Set<BasicVehicleInfo>()
                .CheckContain(Searcher.Number, x=>x.Number)
                .CheckEqual(Searcher.NewEnergy, x=>x.NewEnergy)
                .CheckEqual(Searcher.Status, x=>x.Status)
                .Select(x => new BasicVehicleInfo_View
                {
				    ID = x.ID,
                    Number = x.Number,
                    Name_view = x.VehicleType.Name,
                    Name_view2 = x.VehicleBrand.Name,
                    NewEnergy = x.NewEnergy,
                    Status = x.Status,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicVehicleInfo_View : BasicVehicleInfo{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "名称")]
        public String Name_view2 { get; set; }

    }
}
