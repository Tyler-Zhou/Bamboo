using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCountyVMs
{
    public partial class BasicCountyBatchVM : BaseBatchVM<BasicCounty, BasicCounty_BatchEdit>
    {
        public BasicCountyBatchVM()
        {
            ListVM = new BasicCountyListVM();
            LinkedVM = new BasicCounty_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasicCounty_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
