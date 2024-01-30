using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicLogisticsPositionVMs
{
    public partial class BasicLogisticsPositionListVM : BasePagedListVM<BasicLogisticsPosition_View, BasicLogisticsPositionSearcher>
    {

        protected override IEnumerable<IGridColumn<BasicLogisticsPosition_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicLogisticsPosition_View>>{
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Contact),
                this.MakeGridHeader(x => x.Phone1),
                this.MakeGridHeader(x => x.Phone2),
                this.MakeGridHeader(x => x.Phone3),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.Name_view3),
                this.MakeGridHeader(x => x.Name_view4),
                this.MakeGridHeader(x => x.Address),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicLogisticsPosition_View> GetSearchQuery()
        {
            var query = DC.Set<BasicLogisticsPosition>()
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckContain(Searcher.Phone1, x=>x.Phone1)
                .CheckContain(Searcher.Phone2, x=>x.Phone2)
                .CheckContain(Searcher.Phone3, x=>x.Phone3)
                .CheckContain(Searcher.Address, x=>x.Address)
                .Select(x => new BasicLogisticsPosition_View
                {
				    ID = x.ID,
                    Name_view = x.Logistics.Name,
                    Name = x.Name,
                    Contact = x.Contact,
                    Phone1 = x.Phone1,
                    Phone2 = x.Phone2,
                    Phone3 = x.Phone3,
                    Name_view2 = x.Province.Name,
                    Name_view3 = x.City.Name,
                    Name_view4 = x.County.Name,
                    Address = x.Address,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicLogisticsPosition_View : BasicLogisticsPosition{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "名称")]
        public String Name_view2 { get; set; }
        [Display(Name = "名称")]
        public String Name_view3 { get; set; }
        [Display(Name = "名称")]
        public String Name_view4 { get; set; }

    }
}
