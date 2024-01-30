using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCountyVMs
{
    public partial class BasicCountyListVM : BasePagedListVM<BasicCounty_View, BasicCountySearcher>
    {

        protected override IEnumerable<IGridColumn<BasicCounty_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicCounty_View>>{
                this.MakeGridHeader(x => x.Code),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicCounty_View> GetSearchQuery()
        {
            var query = DC.Set<BasicCounty>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .Select(x => new BasicCounty_View
                {
				    ID = x.ID,
                    Code = x.Code,
                    Name = x.Name,
                    Name_view = x.City.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicCounty_View : BasicCounty{
        [Display(Name = "名称")]
        public String Name_view { get; set; }

    }
}
