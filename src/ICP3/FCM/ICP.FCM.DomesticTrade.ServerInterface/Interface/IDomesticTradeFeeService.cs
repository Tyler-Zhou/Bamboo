using System;
using System.Collections.Generic;
using ICP.FCM.DomesticTrade.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.DomesticTrade.ServiceInterface.CompositeObjects;
using System.ServiceModel;

namespace ICP.FCM.DomesticTrade.ServiceInterface
{
    /// <summary>
    /// 内贸业务费用服务
    /// </summary>
    [ServiceInfomation("内贸业务费用服务")]
    [ServiceContract]
    public interface IDomesticTradeFeeService
    {
        #region 获取订单费用列表
        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<DTBookingFeeList> GetDTOrderFeeList(Guid orderID, Guid companyID); 
        #endregion

        #region 保存订单费用
        /// <summary>
        /// 保存订单费用
        /// </summary>
        /// <param name="feeInfo">详见数据对象</param>
        /// <returns>返回ManyResult</returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveDTOrderFeeList(FeeSaveRequest feeInfo); 
        #endregion

        #region 删除订单费用
        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveDTOrderFeeList(
            Guid[] ids, Guid companyID,
            Guid removeByID,
            DateTime?[] updateDates); 
        #endregion
    }
}
