using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCityVMs
{
    public partial class BasicCityBatchVM : BaseBatchVM<BasicCity, BasicCity_BatchEdit>
    {
        public BasicCityBatchVM()
        {
            ListVM = new BasicCityListVM();
            LinkedVM = new BasicCity_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasicCity_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
