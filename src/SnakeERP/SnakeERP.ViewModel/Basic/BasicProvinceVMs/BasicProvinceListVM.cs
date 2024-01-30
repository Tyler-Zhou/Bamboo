using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicProvinceVMs
{
    public partial class BasicProvinceListVM : BasePagedListVM<BasicProvince_View, BasicProvinceSearcher>
    {

        protected override IEnumerable<IGridColumn<BasicProvince_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicProvince_View>>{
                this.MakeGridHeader(x => x.Code),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicProvince_View> GetSearchQuery()
        {
            var query = DC.Set<BasicProvince>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .Select(x => new BasicProvince_View
                {
				    ID = x.ID,
                    Code = x.Code,
                    Name = x.Name,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicProvince_View : BasicProvince{

    }
}
