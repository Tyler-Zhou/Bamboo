using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleBrandVMs
{
    public partial class BasicVehicleBrandBatchVM : BaseBatchVM<BasicVehicleBrand, BasicVehicleBrand_BatchEdit>
    {
        public BasicVehicleBrandBatchVM()
        {
            ListVM = new BasicVehicleBrandListVM();
            LinkedVM = new BasicVehicleBrand_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasicVehicleBrand_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
