using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.DG.DGOrderInfoVMs
{
    public partial class DGOrderInfoBatchVM : BaseBatchVM<DGOrderInfo, DGOrderInfo_BatchEdit>
    {
        public DGOrderInfoBatchVM()
        {
            ListVM = new DGOrderInfoListVM();
            LinkedVM = new DGOrderInfo_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DGOrderInfo_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
