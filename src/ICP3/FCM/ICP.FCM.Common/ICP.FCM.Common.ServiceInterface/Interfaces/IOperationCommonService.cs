using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 业务公共信息接口
    /// </summary>
    [ServiceInfomation("业务公共信息接口")]
    [ServiceContract]
    public interface IOperationCommonService
    {
        /// <summary>
        /// 获取业务公共信息
        /// </summary>
        /// <param name="operationID">业务ID</param>
        /// <param name="operationType">表单类型</param>
        /// <returns>返回业务公共信息</returns>
        [FunctionInfomation]
        [OperationContract]
        OperationCommonInfo GetOperationCommonInfo(Guid operationID, OperationType operationType);

        /// <summary>
        /// 获取业务公共信息列表
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="accountDate">计费日期</param>
        /// <param name="operationNo">业务号</param>
        /// <param name="operationType">业务类型</param>
        /// <returns>返回业务公共信息列表</returns>
        [FunctionInfomation]
        [OperationContract]
        List<OperationCommonInfo> GetOperationCommonInfoList(DateTime? beginTime, DateTime? endTime
            , DateTime accountDate, string operationNo, OperationType operationType);

        /// <summary>
        /// 获取公司配置客户信息
        /// </summary>
        /// <param name="companyID">口岸ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ConfigureCustomerInfo> GetConfigureCustomers(Guid companyID);
    }
}
 