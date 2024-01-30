using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using WalkingTec.Mvvm.Core;
using WalkingTec.Mvvm.Core.Extensions;
using SnakeERP.Model;


namespace SnakeERP.ViewModel.Basic.BasicLogisticsPositionVMs
{
    public partial class BasicLogisticsPositionVM : BaseCRUDVM<BasicLogisticsPosition>
    {

        public BasicLogisticsPositionVM()
        {
            SetInclude(x => x.Logistics);
            SetInclude(x => x.Province);
            SetInclude(x => x.City);
            SetInclude(x => x.County);
        }

        protected override void InitVM()
        {
        }

        public override void DoAdd()
        {           
            base.DoAdd();
        }

        public override void DoEdit(bool updateAllFields = false)
        {
            base.DoEdit(updateAllFields);
        }

        public override void DoDelete()
        {
            base.DoDelete();
        }
    }
}
