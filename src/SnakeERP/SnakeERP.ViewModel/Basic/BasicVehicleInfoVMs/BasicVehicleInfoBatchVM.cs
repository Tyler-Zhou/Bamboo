using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicVehicleInfoVMs
{
    public partial class BasicVehicleInfoBatchVM : BaseBatchVM<BasicVehicleInfo, BasicVehicleInfo_BatchEdit>
    {
        public BasicVehicleInfoBatchVM()
        {
            ListVM = new BasicVehicleInfoListVM();
            LinkedVM = new BasicVehicleInfo_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasicVehicleInfo_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
