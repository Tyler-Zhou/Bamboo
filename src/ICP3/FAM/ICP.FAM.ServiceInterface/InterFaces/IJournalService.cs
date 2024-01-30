using System;
using System.Collections.Generic;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.Framework.CommonLibrary.Common;
using ICP.FAM.ServiceInterface.DataObjects.SaveRequests;
using System.ServiceModel;

namespace ICP.FAM.ServiceInterface
{



    /// <summary>
    /// 日记帐服务类
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IJournalService
    {
        /// <summary>
        /// 获得日记帐列表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <param name="minAmount">最小金额</param>
        /// <param name="maxAmount">最大金额</param>
        /// <param name="isValid">有效性</param>
        /// <param name="dataPageInfo">dataPageInfo</param>
        /// <param name="isEnglish">是否英文版本</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        PageList GetJournalList(
                          string NO,
                          Guid[] companyIDs,
                          DateTime? startDate,
                          DateTime? endDate,
                          Decimal? minAmount,
                          Decimal? maxAmount,
                          bool? isValid,
                          DataPageInfo dataPageInfo,
                          bool isEnglish);


        /// <summary>
        /// 作废日记帐
        /// </summary>
        /// <param name="id">日记帐ID</param>
        /// <param name="isCancel">是否作废(True为作废,False为激活)</param>
        /// <param name="cancelByID">操作人ID</param>
        /// <param name="updateDate">最后更新时间</param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult CancelJournal(
                         Guid id,
                         bool isCancel,
                         Guid cancelByID,
                         DateTime? updateDate,
                          bool isEnglish);

        /// <summary>
        /// 获得日记帐详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        JournalInfo GetJournalInfo(Guid id, bool isEnglish);

        /// <summary>
        /// 保存日记帐信息
        /// </summary>
        /// <param name="saveRequest">saveRequest</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        SingleResult SaveJournal(JournalSaveRequest saveRequest);

        /// <summary>
        /// 获得日记帐明细列表
        /// </summary>
        /// <param name="journalID"></param>
        /// <param name="isEnglish">isEnglish</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<JournalDetail> GetJournalDetailList(Guid journalID,
                          bool isEnglish);

        /// <summary>
        /// 删除日记帐明细
        /// </summary>
        /// <param name="id">明细ID</param>
        /// <param name="removeByID">删除人ID</param>
        /// <param name="isEnglish">isEnglish</param>
        [FunctionInfomation]
        [OperationContract]
        void RemoveJournalDetailList(Guid? id, Guid removeByID,
                          bool isEnglish);

        /// <summary>
        /// 保存日记帐明细
        /// </summary>
        /// <param name="saveRequest">saveRequest</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveJournalDetail(JournalDetailSaveRequest saveRequest);

        /// <summary>
        /// 以事务保存
        /// </summary>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        Dictionary<Guid, SaveResponse> SaveJournalWithTrans(
                       JournalSaveRequest journalSaveRequest,
                       List<JournalDetailSaveRequest> detailSaveRequestList);


        /// <summary>
        /// 保存管理费用月预算信息
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="glIds"></param>
        /// <param name="amounts"></param>
        /// <param name="remarks"></param>
        /// <param name="updateDate"></param>
        /// <param name="companyID"></param>
        /// <param name="year"></param>
        /// <param name="saveBy"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        ManyResult SaveFeeMonthBudgets(Guid[] ids,
                                        Guid[] glIds,
                                        decimal[] amounts,
                                        string[] remarks,
                                        DateTime?[] updateDates,
                                        Guid companyID,
                                        int year,
                                        FeeMonthBudgetType type,
                                        Guid saveBy);
        /// <summary>
        /// 获得管理费用月预算列表
        /// </summary>
        /// <param name="companyID"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<FeeYearMonthBudgetList> GetFeeMonthBudgetList(
                                        Guid companyID, 
                                        int year,
                                        FeeMonthBudgetType type);


        /// <summary>
        /// 解锁凭证
        /// </summary>
        /// <param name="checkIds"></param>
        /// <param name="chargeByID"></param>
        #region 解锁凭证
        [FunctionInfomation]
        [OperationContract]
        void UntieLockLedgerForJournal(Guid checkIds, Guid chargeByID);
        #endregion


    }
}
