using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCityVMs
{
    public partial class BasicCityListVM : BasePagedListVM<BasicCity_View, BasicCitySearcher>
    {

        protected override IEnumerable<IGridColumn<BasicCity_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicCity_View>>{
                this.MakeGridHeader(x => x.Code),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicCity_View> GetSearchQuery()
        {
            var query = DC.Set<BasicCity>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .Select(x => new BasicCity_View
                {
				    ID = x.ID,
                    Code = x.Code,
                    Name = x.Name,
                    Name_view = x.Province.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicCity_View : BasicCity{
        [Display(Name = "名称")]
        public String Name_view { get; set; }

    }
}
