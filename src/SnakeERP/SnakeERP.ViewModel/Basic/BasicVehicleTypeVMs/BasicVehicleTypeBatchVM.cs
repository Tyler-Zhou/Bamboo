using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleTypeVMs
{
    public partial class BasicVehicleTypeBatchVM : BaseBatchVM<BasicVehicleType, BasicVehicleType_BatchEdit>
    {
        public BasicVehicleTypeBatchVM()
        {
            ListVM = new BasicVehicleTypeListVM();
            LinkedVM = new BasicVehicleType_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasicVehicleType_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
