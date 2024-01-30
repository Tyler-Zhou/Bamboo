using System;
using System.Collections.Generic;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FCM.OceanImport.ServiceInterface;
using System.ServiceModel;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 海运进口费用服务类
    /// </summary>
    [ServiceInfomation("海运进口费用服务")]
    [ServiceContract]
    public interface IOceanImportFeeService
    {
        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        [OperationContract]
        List<OceanImportFeeList> GetOIOrderFeeList(Guid orderID);

        /// <summary>
        /// 保存费用信息
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="ids">ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="chargingCodeIDs">费用代码集合</param>
        /// <param name="currencyIDs">币种列表</param>
        /// <param name="quantities">数量列表</param>
        /// <param name="unitPrices">单价列表</param>
        /// <param name="ways">方向列表(DR-0-应收 ,CR-1-应付</param>
        /// <param name="amounts">金额列表</param>
        /// <param name="remarks">备注列表</param>
        /// <param name="saveByID">保存人</param>
        /// <param name="updateDates">更新时间</param>
        /// <returns></returns>
        [OperationContract]
        ManyResult SaveOIOrderFeeList(FeeSaveRequest feeSaveRequest);

        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [OperationContract]
        void RemoveOIOrderFeeList(
            Guid[] ids,
            Guid removeByID,
            DateTime?[] updateDates);
    }
}
