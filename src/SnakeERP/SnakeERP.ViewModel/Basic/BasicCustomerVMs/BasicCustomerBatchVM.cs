using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicCustomerVMs
{
    public partial class BasicCustomerBatchVM : BaseBatchVM<BasicCustomer, BasicCustomer_BatchEdit>
    {
        public BasicCustomerBatchVM()
        {
            ListVM = new BasicCustomerListVM();
            LinkedVM = new BasicCustomer_BatchEdit();
        }

    }

	/// <summary>
    /// Class to define batch edit fields
    /// </summary>
    public class BasicCustomer_BatchEdit : BaseVM
    {

        protected override void InitVM()
        {
        }

    }

}
