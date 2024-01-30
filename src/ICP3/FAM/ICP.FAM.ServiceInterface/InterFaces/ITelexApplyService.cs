using System;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using ICP.Framework.CommonLibrary.Attributes;
using System.ServiceModel;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 总电放单管理的服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface ITelexApplyService
    {
        /// <summary>
        /// 获取总电放单列表
        /// </summary>
        /// <param name="companyIds">公司ID集合</param>
        /// <param name="customerName">客户</param>
        /// <param name="consigneeName">收货人</param>
        /// <param name="applicantName">申请人</param>
        /// <param name="from">起始申请时间</param>
        /// <param name="to">截止申请时间</param>
        /// <param name="totalRecords">记录数</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        List<TelexApplyList> GetTelexApplyList(
            Guid[] companyIds,   
            string customerName,
            string consigneeName, 
            string applicantName,
            DateTime? from, 
            DateTime? to, 
            int totalRecords);

        /// <summary>
        /// 获取总总电放的详细信息
        /// </summary>
        /// <param name="requestId">主键ID</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        TelexApplyInfo GetTelexApply(Guid requestId);

        /// <summary>
        /// 设置总总电放的有效性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <param name="saveById"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        SingleResult ChangeTelexRequestValidity(Guid id, bool isCancel, Guid saveById, DateTime? updateDate);

        /// <summary>
        /// 保存总总电放信息
        /// </summary>
        /// <param name="saveRequest">总电放</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        SingleResult SaveTelexRequest(TelexRequestSaveRequest saveRequest);
    }
}
