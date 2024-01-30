using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicProvinceVMs
{
    public partial class BasicProvinceBatchVM : BaseBatchVM<BasicProvince, BasicProvince_BatchEdit>
    {
        public BasicProvinceBatchVM()
        {
            ListVM = new BasicProvinceListVM();
            LinkedVM = new BasicProvince_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasicProvince_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
