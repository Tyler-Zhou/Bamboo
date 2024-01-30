using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCustomerVMs
{
    public partial class BasicCustomerListVM : BasePagedListVM<BasicCustomer_View, BasicCustomerSearcher>
    {

        protected override IEnumerable<IGridColumn<BasicCustomer_View>> InitGridHeader()
        {
            return new List<GridColumn<BasicCustomer_View>>{
                this.MakeGridHeader(x => x.Code),
                this.MakeGridHeader(x => x.Name),
                this.MakeGridHeader(x => x.Name_view),
                this.MakeGridHeader(x => x.Name_view2),
                this.MakeGridHeader(x => x.Name_view3),
                this.MakeGridHeader(x => x.CellPhone),
                this.MakeGridHeader(x => x.HomePhone),
                this.MakeGridHeader(x => x.Name_view4),
                this.MakeGridHeader(x => x.Address),
                this.MakeGridHeader(x => x.Region),
                this.MakeGridHeaderAction(width: 200)
            };
        }

        public override IOrderedQueryable<BasicCustomer_View> GetSearchQuery()
        {
            var query = DC.Set<BasicCustomer>()
                .CheckContain(Searcher.Code, x=>x.Code)
                .CheckContain(Searcher.Name, x=>x.Name)
                .CheckEqual(Searcher.ProvinceID, x=>x.ProvinceID)
                .CheckEqual(Searcher.CityID, x=>x.CityID)
                .CheckEqual(Searcher.CountyID, x=>x.CountyID)
                .CheckContain(Searcher.CellPhone, x=>x.CellPhone)
                .CheckContain(Searcher.HomePhone, x=>x.HomePhone)
                .CheckEqual(Searcher.SalesID, x=>x.SalesID)
                .CheckContain(Searcher.Address, x=>x.Address)
                .CheckContain(Searcher.Region, x=>x.Region)
                .Select(x => new BasicCustomer_View
                {
				    ID = x.ID,
                    Code = x.Code,
                    Name = x.Name,
                    Name_view = x.Province.Name,
                    Name_view2 = x.City.Name,
                    Name_view3 = x.County.Name,
                    CellPhone = x.CellPhone,
                    HomePhone = x.HomePhone,
                    Name_view4 = x.Sales.Name,
                    Address = x.Address,
                    Region = x.Region,
                })
                .OrderBy(x => x.ID);
            return query;
        }

    }

    public class BasicCustomer_View : BasicCustomer{
        [Display(Name = "名称")]
        public String Name_view { get; set; }
        [Display(Name = "名称")]
        public String Name_view2 { get; set; }
        [Display(Name = "名称")]
        public String Name_view3 { get; set; }
        [Display(Name = "_Admin.Name")]
        public String Name_view4 { get; set; }

    }
}
