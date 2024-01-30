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
    /// 月结协议管理
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IMonthlyClosingAgreementService
    {
        /// <summary>
        /// 根据查询面板输入的条件，获取月结协议列表
        /// </summary>
        /// <param name="companyIds">公司ID集合</param>
        /// <param name="customerName">客户名称</param>
        /// <param name="applicantName">申请人</param>
        /// <param name="from">起始申请时间</param>
        /// <param name="to">截止申请时间</param>
        /// <param name="totalRecords">记录数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetMonthlyClosingEntryLists_Old")]
        List<MonthlyClosingEntryList> GetMonthlyClosingEntryLists(
            Guid[] companyIds,
            string customerName,
            string applicantName,
            DateTime? from,
            DateTime? to,
            DateTime? Profitfrom,
            DateTime? Profitto,
            int totalRecords);

        /// <summary>
        /// 根据查询面板输入的条件，获取月结协议列表
        /// </summary>
        /// <param name="param">查询参数</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract(Name = "GetMonthlyClosingEntryLists")]
        List<MonthlyClosingEntryList> GetMonthlyClosingEntryLists(MonthlyClosingEntrySearchParameter param);

        /// <summary>
        /// 获取月结协议的详细信息
        /// </summary>
        /// <param name="entryId">主键ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        MonthlyClosingAgreement GetMonthlyClosingEntryInfo(Guid entryId);

        /// <summary>
        /// 通过客户ID和公司ID得到客户的月结信息
        /// </summary>
        /// <param name="customerID">客户ID</param>
        /// <param name="companyID">公司ID</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        MonthlyClosingCustomer GetMonthlyClosingCustomer(Guid customerID, Guid companyID);

        /// <summary>
        /// 设置月结协议的有效性
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCancel"></param>
        /// <param name="saveById"></param>
        /// <param name="updateDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult ChangeMonthlyClosingEntryValidity(Guid id, bool isCancel, Guid saveById, DateTime? updateDate);

        /// <summary>
        /// 保存月结协议
        /// </summary>
        /// <param name="saveRequest">月结协议</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveMonthlyClosingEntryInfo(MonthlyClosingEntrySaveRequest saveRequest);

        /// <summary>
        /// 根据客户ID查询是否已存在月结协议
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        bool IsExistMonthlyClosingEntry(Guid customerid);

        /// <summary>
        /// 保存客户账单偏好设置
        /// </summary>
        /// <param name="CustomerPreference"></param>
        [FunctionInfomation]
        [OperationContract]
        void SaveCustomerPreferencesInfo(CustomerPreferencesSaveRequest CustomerPreference);

        /// <summary>
        /// 获取客户账单偏好设置
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CustomerId"></param>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerPreferencesInfo> GetCustomerPreferencesInfo(Guid? id, Guid? CustomerId, Guid? CompanyId);
    }
}
