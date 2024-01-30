using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.DG.DGOrderDetailVMs
{
    public partial class DGOrderDetailBatchVM : BaseBatchVM<DGOrderDetail, DGOrderDetail_BatchEdit>
    {
        public DGOrderDetailBatchVM()
        {
            ListVM = new DGOrderDetailListVM();
            LinkedVM = new DGOrderDetail_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class DGOrderDetail_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
