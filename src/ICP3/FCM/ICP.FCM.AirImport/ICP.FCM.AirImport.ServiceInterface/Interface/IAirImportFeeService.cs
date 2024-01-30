using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System.ServiceModel;

namespace ICP.FCM.AirImport.ServiceInterface
{
    /// <summary>
    /// 空运进口费用服务类
    /// </summary>
    [ServiceInfomation("空运进口费用服务")]
    [ServiceContract]
    public interface IAirImportFeeService
    {
        /// <summary>
        /// 获取订单费用列表
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>返回订单费用列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<AirImportFeeList> GetAIOrderFeeList(Guid orderID,Guid companyID);

        /// <summary>
        /// 保存费用信息
        /// </summary>
        /// <param name="feeSaveRequest">订单ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveAIOrderFeeList(FeeSaveRequest feeSaveRequest);

        /// <summary>
        /// 删除订单费用
        /// </summary>
        /// <param name="ids">ID 列表</param>
        /// <param name="removeByID">删除人 </param>
        /// <param name="updateDates">更新时间-做数据版本用</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveAIOrderFeeList(Guid[] ids, Guid companyID,Guid removeByID,DateTime?[] updateDates);
    }
}
