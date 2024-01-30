using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.FAM.ServiceInterface.DataObjects;
using System.ServiceModel;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 会计报表的服务
    /// </summary>
    [ServiceInfomation(ServiceType.Business)]
    [ServiceContract]
    public interface IAccountingReportService
    {
        #region 获取会计科目
        /// <summary>
        /// GetGLDetail
        /// </summary>
        /// <param name="glCode">glCode</param>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companys</param>
        /// <param name="billTypes">billTypes</param>
        /// <returns>LedgerData</returns>
       [FunctionInfomation]  [OperationContract]
        List<LedgerData> GetGLDetail(string glCode, DateTime fromDate, DateTime toDate, Guid[] companyIDs, ReportBillType[] billTypes);


        #endregion

        #region GetGLSummary

        /// <summary>
        /// GetGLSummary
        /// </summary>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companys</param>
        /// <param name="billTypes">billTypes</param>
        /// <returns>LedgerData</returns>
       [FunctionInfomation]  [OperationContract]
        List<GLData> GetGLSummary(DateTime fromDate, DateTime toDate, Guid[] companyIDs, ReportBillType[] billTypes);

        #endregion


        #region  GetCheckDatas
        /// <summary>
        /// GetCheckDatas
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>PaymentCheckData</returns>
       [FunctionInfomation]  [OperationContract]
        CheckData GetCheckData(Guid id);

        #endregion


        #region  GetReportBillData
        /// <summary>
        /// GetReportBillData
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ReportBillData</returns>
       [FunctionInfomation]  [OperationContract]
        ReportBillData GetReportBillData(Guid id);

        #endregion


        #region  GetTrialBalance
        /// <summary>
        /// GetGLSummary
        /// </summary>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companys</param>
        /// <param name="billTypes">billTypes</param>
        /// <returns>GetTrialBalance</returns>
       [FunctionInfomation]  [OperationContract]
        List<GLData> GetTrialBalance(DateTime toDate, Guid[] companyIDs, ReportBillType[] billTypes);


        #endregion

        #region GetBalanceSheet

        /// <summary>
        /// GetBalanceSheet
        /// </summary>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companys</param>
        /// <returns>GLData</returns>
       [FunctionInfomation]  [OperationContract]
       GLDataList GetBalanceSheet(DateTime fromDate, DateTime toDate, Guid[] companyIDs);

        #endregion

        #region GetIncome

        /// <summary>
        /// GetIncome
        /// </summary>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companys</param>
        /// <returns>GLData</returns>
       [FunctionInfomation]  [OperationContract]
       GLDataAndTotalInfo GetIncome(DateTime fromDate, DateTime toDate, Guid[] companyIDs, bool isIncomeOrder);

        #endregion

        #region GetBankOutStandingDataTotal
        /// <summary>
        /// GetBankOutStandingDataTotal
        /// </summary>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="hasBankDate">hasBankDate</param>
        /// <returns>LedgerData</returns>
       [FunctionInfomation]  [OperationContract]
        List<BankOutStandingData> GetBankOutStandingDataTotal(DateTime toDate, Guid[] companyIDs, bool hasBankDate);

        #endregion

        #region GetBankOutStandingDataTotal
        /// <summary>
        /// GetBankOutStandingDataTotal
        /// </summary>
        /// <param name="BankAccountId">BankAccountId</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="hasBankDate">hasBankDate</param>
        /// <returns>LedgerData</returns>
       [FunctionInfomation]  [OperationContract]
        List<BankOutStandingDetailData> GetBankOutStandingDataDetail(Guid BankAccountId, DateTime toDate, Guid[] companyIDs, bool hasBankDate);

        #endregion


        #region GetJournalReportData
        /// <summary>
        /// 获取日记帐列表
        /// </summary>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="minAmount">minAmount</param>
        /// <param name="maxAmount">maxAmount</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <returns>JournalList</returns>
       [FunctionInfomation]  [OperationContract]
        List<ICP.FAM.ServiceInterface.DataObjects.JournalDetailReportData> GetJournalReportData(
            DateTime? fromDate, DateTime? toDate
            , decimal? minAmount, decimal? maxAmount
            , Guid[] companyIDs);

        #endregion

        #region 获取凭证数据

        /// <summary>
        /// 获取凭证数据
        /// </summary>
        /// <param name="fromDate">fromDate</param>
        /// <param name="toDate">toDate</param>
        /// <param name="companys">companys</param>
        /// <param name="vType">vType</param>
        /// <returns>LedgerData</returns>
       [FunctionInfomation]  [OperationContract]
        List<VoucherLedgerData> GetLedgerData(DateTime fromDate, DateTime toDate, string companys, short vType);

        #endregion

        #region AgingReport

        /// <summary>
        /// GetAgingReportForTotal
        /// </summary>
        /// <param name="endingDate">endingDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="billTypes">billTypes</param>
        /// <param name="operationTypes">operationTypes</param>
        /// <param name="customerId">customerId</param>
        /// <param name="SearchType">SearchType</param>
        /// <param name="onlyOverPaid">onlyOverPaid</param>
        /// <returns>AgingReportData</returns>
       [FunctionInfomation]  [OperationContract]
        List<AgingReportData> GetAgingReportForTotal(
                                                    DateTime endingDate
                                                    , Guid[] companyIDs
                                                    , BillType[] billTypes
                                                    , OperationType[] operationTypes
                                                    , Guid? customerId
                                                    , short SearchType
                                                    , bool onlyOverPaid
                                                    , TermType termType
                                                    ,AgingDateState agingDateState
                                                    );
        /// <summary>
        /// GetAgingReportForDetail
        /// </summary>
        /// <param name="endingDate">endingDate</param>
        /// <param name="companyIDs">companyIDs</param>
        /// <param name="billTypes">billTypes</param>
        /// <param name="operationTypes">operationTypes</param>
        /// <param name="customerId">customerId</param>
        /// <param name="currency">currency</param>
        /// <param name="SearchType">SearchType</param>
        /// <param name="onlyOverPaid">onlyOverPaid</param>
        /// <returns>AgingReportDetailData</returns>
       [FunctionInfomation]  [OperationContract]
        List<AgingReportDetailData> GetAgingReportForDetail(DateTime endingDate
            , Guid[] companyIDs
            , BillType[] billTypes
            , OperationType[] operationTypes
            , Guid? customerId
            , string currency
            , short SearchType
            , bool onlyOverPaid
            ,AgingDateState agingDateState
            , TermType termType);


        /// <summary>
        /// 获得CA数据
        /// </summary>
        /// <param name="begingDate"></param>
        /// <param name="endDate"></param>
        /// <param name="companyIDs"></param>
        /// <param name="billTypes"></param>
        /// <param name="operationTypes"></param>
        /// <param name="customerId"></param>
        /// <param name="currency"></param>
        /// <param name="onlyOverPaid"></param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        List<AgingReportDetailData> GetAgingReportForCA(
            DateTime begingDate,
            DateTime endDate
            , Guid[] companyIDs
            , BillType[] billTypes
            , OperationType[] operationTypes
            , Guid? customerId
            , string currency
            , AgingDateState agingDateState
            , TermType termType
            , bool onlyOverPaid);



        #region GetAgingReportForFeeDetail TODO
        /// <summary>
        /// GetAgingReportForFeeDetail
        /// </summary>
        /// <param name="BillId">BillId</param>
        /// <returns>AgingReportFeeData</returns>
       [FunctionInfomation]  [OperationContract]
        List<AgingReportFeeData> GetAgingReportForFeeDetail(Guid BillId);

        #endregion



        #endregion

        #region 收支报表

        #region 获取收支
        /// <summary>
        /// 获取收支
        /// </summary>
        /// <param name="checkType">checkType</param>
        /// <param name="dateType">0:CreateDate,1:BankDate;2:CheckDate</param>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        /// <param name="groupBy">0:BillTo;  1:BankName	2:Date</param>
        /// <param name="companyId">companyId</param>
        /// <returns></returns>
       [FunctionInfomation]  [OperationContract]
        List<RepCheckData> GetCheckListReportData(CheckType checkType, short dateType, DateTime? from, DateTime? to, short groupBy, Guid[] companyId);
        #endregion

        #region 获取收支详细
        /// <summary>
        /// 获取收支详细
        /// </summary>
        /// <param name="checkID">checkID</param>
        /// <param name="groupBy">0:BillTo;  1:BankName	2:Date</param>
        /// <returns>RepCheckDetailData</returns>
       [FunctionInfomation]  [OperationContract]
       List<RepCheckDetailData> GetCheckDetailReportData(
                                   CheckType checkType
                                 , short dateType
                                 , DateTime? from
                                 , DateTime? to
                                 , short groupBy
                                 , Guid guoupByID
                                 , Guid[] companyId);

        #endregion

        #region 获取收支CA
        /// <summary>
        /// 获取收支CA
        /// </summary>
        /// <param name="checkType">checkType</param>
        /// <param name="dateType">0:CreateDate,1:BankDate;2:CheckDate</param>
        /// <param name="from">from</param>
        /// <param name="to">to</param>
        /// <param name="sortBy">0:BillTo;  1:BankName	2:Date</param>
        /// <param name="companyId">companyId</param>
        /// <returns>RepCACheckDepositData</returns>
       [FunctionInfomation]  [OperationContract]
        List<RepCACheckDepositData> GetCheckListReportDataCA(CheckType checkType, short dateType, DateTime? from, DateTime? to, short sortBy, Guid[] companyId);

        /// <summary>
        /// 获取预收预付列表
        /// </summary>
        /// <param name="from">开始时间</param>
        /// <param name="to">结束时间</param>
        /// <param name="companyIds">公司ID</param>
        /// <param name="CustomerID">客户ID</param>
        /// <param name="GLID">科目ID</param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       List<PrepaidInAdvanceData> GetPrepaidInAdvanceData(
            DateTime? from, DateTime? to
          , Guid[] companyIds, Guid? CustomerID
          , Guid? GLID);

       /// <summary>
       /// 获取预收预付余额清单
       /// </summary>
       /// <param name="from">开始时间</param>
       /// <param name="to">结束时间</param>
       /// <param name="companyIds">公司ID</param>
       /// <param name="CustomerID">客户ID</param>
       /// <param name="GLID">科目ID</param>
       /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
       List<GLCheckBalanceData> GetGLCheckBalanceData(
            DateTime? from, DateTime? to
          , Guid[] companyIds, Guid? CustomerID
          , Guid? GLID);
        #endregion

        #endregion

        #region 进口代理对帐 


       /// <summary>
       /// 获取代理对帐单统计表
       /// </summary>
       /// <param name="userCompanyID">用户公司ID</param>
       /// <param name="customerID">客户ID</param>
       /// <param name="agentCompanyIDs">代理公司ID集合</param>
       /// <param name="operactioType">业务类型</param>
       /// <param name="DateType">日期类型(0:BillDate,1:ETD)</param>
       /// <param name="currencyID">折合币种</param>
       /// <param name="fromDate">开始日期</param>
       /// <param name="toDate">结束日期</param>
       /// <param name="orderByName">排序字段</param>
       /// <param name="billType">帐单类型(0:All全部,1:Open未完全付 2:Paid已付)</param>
       /// <param name="isShowPaidStatus">是否显示付款状态</param>
       /// <param name="isAttached">是否显示明细表</param>
       /// <returns></returns>
       [FunctionInfomation("获取代理对帐单")]
       [OperationContract]
       AgentStatementReportDateTotal GetAgentStatementReportDate(
                                         Guid userCompanyID,
                                         Guid? customerID,
                                         Guid[] agentCompanyIDs,
                                         string operactioType ,
                                         Int16 dateType,
                                         Guid? currencyID,
                                         DateTime? fromDate,
                                         DateTime? toDate,
                                         AgentStatementSortByEnum orderByName,
                                         Int16 billType, 
                                         bool isShowPaidStatus,
                                         bool isAttached);


       /// <summary>
       /// 获取代理对帐单详细
       /// </summary>
       /// <param name="billNo"></param>
       /// <returns><REPDebitDetailDataTable/returns>
       [FunctionInfomation("获取代理对帐单详细")]
       [OperationContract]
       List<AgentStatementReportDetailDate> GetAgentStatementReportDetailDate(Guid billID);

        #endregion

        #region 本地对帐单
        /// <summary>
        /// GetLocalStatementReportData
        /// </summary>
        /// <param name="billToId">billToId</param>
        /// <param name="createFromDate">createFromDate</param>
        /// <param name="createToDate">createToDate</param>
        /// <param name="orderBy">orderBy</param>
        /// <param name="billType">billType</param>
        /// <param name="billState">billState</param>
        /// <param name="companyIds">userCompanyId</param>
        /// <param name="ETAFrom">ETAFrom</param>
        /// <param name="ETATo">ETATo</param>
        /// <param name="ETDFrom">ETDFrom</param>
        /// <param name="ETDTo">ETDTo</param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
        List<LocalStatementReportData> GetLocalStatementReportData(
            Guid? billToId
            , DateTime? createFromDate, DateTime? createToDate
            , LocalStatementOrderByEnum orderBy
            , CheckType billType, StatementBillStateEnum billState
            , Guid[] companyIds
            , DateTime? ETAFrom, DateTime? ETATo
            , DateTime? ETDFrom, DateTime? ETDTo);

        /// <summary>
        /// GetLocalStatementReportDetailData
        /// </summary>
        /// <param name="billToId">billToId</param>
        /// <param name="createFromDate">createFromDate</param>
        /// <param name="createToDate">createToDate</param>
        /// <param name="orderBy">orderBy</param>
        /// <param name="billType">billType</param>
        /// <param name="billState">billState</param>
        /// <param name="companyIds">userCompanyId</param>
        /// <param name="ETAFrom">ETAFrom</param>
        /// <param name="ETATo">ETATo</param>
        /// <param name="ETDFrom">ETDFrom</param>
        /// <param name="ETDTo">ETDTo</param>
        /// <returns></returns>
       [FunctionInfomation]
       [OperationContract]
        List<LocalStatementReportDetailData> GetLocalStatementReportDetailData(
            Guid? billToId
            , DateTime? createFromDate, DateTime? createToDate
            , LocalStatementOrderByEnum orderBy
            , CheckType billType, StatementBillStateEnum billState
            , Guid[] companyIds
            , DateTime? ETAFrom, DateTime? ETATo
            , DateTime? ETDFrom, DateTime? ETDTo);

        #endregion


    }
}
