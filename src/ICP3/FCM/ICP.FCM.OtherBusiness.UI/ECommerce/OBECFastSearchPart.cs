using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.UI.Common;
using System;
using System.Collections.Generic;


namespace ICP.FCM.OtherBusiness.UI.ECommerce
{
    /// <summary>
    /// 其他业务查询面板
    /// </summary>
    public class OBECFastSearchPart : OBFastSearchPart
    {
        /// <summary>
        /// 点击更多，显示查询面板
        /// </summary>
        protected override void OnClickMore()
        {
            Workitem.Commands[OBECCommandConstants.Command_ShowSearch].Execute();
        }
        public override object GetData()
        {
            List<OtherBusinessList> list =
                OtherBusinessService.GetOtherBusinessListForFaster(CompanyIDs
                                                    , new[] { 5,6,7}
                                                    , base.PartNoSearchType
                                                    , txtNo.Text.Trim()
                                                    , base.PartCustomerSearchType
                                                    , stxtCustomer.Text.Trim()
                                                    , base.PartPortSearchType
                                                    , stxtPort.Text.Trim()
                                                    , base.PartDateSearchType
                                                    , Guid.Empty
                                                    , null
                                                    , base.From
                                                    , base.To
                                                    , true
                                                    , 100);
            return list;
        }
    }
      
}
