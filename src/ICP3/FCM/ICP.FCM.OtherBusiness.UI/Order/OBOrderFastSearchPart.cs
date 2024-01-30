﻿#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/14 星期三 16:03:20
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using ICP.FCM.OtherBusiness.ServiceInterface;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.FCM.OtherBusiness.UI.Common;
using System;
using System.Collections.Generic;

namespace ICP.FCM.OtherBusiness.UI.Order
{
    /// <summary>
    /// 其他业务-工具栏(订单)
    /// </summary>
    public class OBOrderFastSearchPart : OBFastSearchPart
    {
        /// <summary>
        /// 添加业务类型
        /// </summary>
        public override AddType OTType
        {
            get { return AddType.OtherBusinessOrder; }
        }

        /// <summary>
        /// 点击更多，显示查询面板
        /// </summary>
        protected override void OnClickMore()
        {
            Workitem.Commands[OBOCommandConstants.Command_ShowSearch].Execute();
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public override object GetData()
        {
            List<OtherBusinessList> list =
                OtherBusinessService.GetOtherBusinessListForFaster(CompanyIDs
                                                    , new[] { 1, 2, 3, 4, 5, 6 }
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