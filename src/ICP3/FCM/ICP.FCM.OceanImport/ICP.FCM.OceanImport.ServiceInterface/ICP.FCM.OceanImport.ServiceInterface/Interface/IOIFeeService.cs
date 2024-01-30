using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 海运进口费用服务类
    /// </summary>
    [ServiceInfomation("海运进口费用服务")]
    [ServiceContract]
    public interface IOIFeeService
    {
        #region 获取订单费用列表
        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        [OperationContract(Name = "GetOIOrderFeeList1")]
        List<OceanImportFeeList> GetOIOrderFeeList(Guid orderID);

        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <param name="companyID">订单口岸ID</param>
        /// <returns>返回订单费用列表</returns>
        [OperationContract(Name = "GetOIOrderFeeList2")]
        List<OceanImportFeeList> GetOIOrderFeeList(Guid orderID,Guid companyID); 
        #endregion

        #region 保存费用信息
        /// <summary>
        /// 保存费用信息
        /// </summary>
        /// <param name="feeSaveRequest">用于保存费用的对象</param>
        /// <returns></returns>
        [OperationContract]
        ManyResult SaveOIOrderFeeList(FeeSaveRequest feeSaveRequest);

        #endregion

        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [OperationContract(Name = "RemoveOIOrderFeeList1")]
        void RemoveOIOrderFeeList(Guid[] ids,Guid removeByID,DateTime?[] updateDates);

        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">费用ID列表</param>
        /// <param name="companyID">订单口岸ID列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [OperationContract(Name = "RemoveOIOrderFeeList2")]
        void RemoveOIOrderFeeList(Guid[] ids,Guid companyID, Guid removeByID, DateTime?[] updateDates);
    }
}
