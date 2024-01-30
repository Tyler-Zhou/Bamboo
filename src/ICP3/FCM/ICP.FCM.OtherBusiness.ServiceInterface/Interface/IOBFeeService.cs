using ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.OtherBusiness.ServiceInterface
{
    /// <summary>
    /// 其他业务服务
    /// </summary>
    [ServiceInfomation("其他业务费用服务")]
    [ServiceContract]
    public interface IOBFeeService
    {
        #region 获取其他业务订单费用列表

        /// <summary>
        /// 获取其他业务订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns>返回订单费用列表</returns>
        [OperationContract]
        List<OBFeeList> GetOBOrderFeeList(Guid orderID,Guid companyID); 
        #endregion

        #region 保存费用信息
        /// <summary>
        /// 保存费用信息
        /// </summary>
        /// <param name="feeInfo">费用信息</param>
        /// <returns>返回ManyResult</returns>
        [OperationContract]
        ManyResult SaveOBOrderFeeList(FeeSaveRequest feeInfo); 
        #endregion

        #region 删除订单费用
        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="companyID">订单口岸ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [OperationContract]
        void RemoveOBOrderFeeList(Guid[] ids,Guid companyID, Guid removeByID, DateTime?[] updateDates); 
        #endregion
    }
}
