using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Attributes;
using ICP.ReportCenter.ServiceInterface.DataObjects;
using System.ServiceModel;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;

namespace ICP.ReportCenter.ServiceInterface
{
    [ServiceInfomation("报表中心服务")]
    [ServiceContract]
    public interface IReportCenterService
    {
        [FunctionInfomation("根据当前用户返回报表服务器的地址")]
        [OperationContract]
        ReportServerInfo GetReportServerUrl();

        /// <summary>
        /// 返回业务型
        /// </summary>
        /// <param name="getContainerTypeOnly">是否只获取有箱的业务类型</param>
        /// <returns>ReportOperationType</returns>
        [FunctionInfomation("返回业务型")]
        [OperationContract]
        List<ReportOperationType> GetReportOperationType(bool getContainerTypeOnly);


        /// <summary>
        /// 返回分组方式包括不支技的业务类型
        /// </summary>
        /// <returns>ReportOperationType</returns>
        [FunctionInfomation("返回分组方式包括不支技的业务类型")]
        [OperationContract]
        List<ReportGroupType> GetReportGroupType();


        [FunctionInfomation("根据当前用户返回所属公司下所有的部门")]
        [OperationContract]
        List<OrganizationList> GetOrganizationListForReport(Guid userID, bool isEnglish);


        [FunctionInfomation("根据当前用户返回所属公司下所有的部门(CRM报表专用)")]
        [OperationContract]
        List<OrganizationList> GetOrganizationListForCRMReport(Guid userID, bool isEnglish);

        [FunctionInfomation("验证应收应付盈余表和凭证明细表")]
        [OperationContract]
        void CheckLedgersForBill(DateTime @fromDate, DateTime @toDate, string @companyIDS);

        /// <summary>
        /// 科目余额明细表
        /// </summary>
        /// <param name="glID">科目ID</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        [FunctionInfomation("获取科目余额明细表")]
        [OperationContract]
        List<GLDetailData> GetGLDetailBalanceList(Guid glID, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// 外币金额式科目余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="fromGLCode">开始科目</param>
        /// <param name="toGLCode">结束科目</param>
        /// <param name="glCodeType">科目类型(1资产;2负债;3权益;4成本;5损益)</param>
        /// <param name="fromGLLevel">开始级别</param>
        /// <param name="toGLLevel">结束级别</param>
        /// <param name="showEndLevel">只显示未级科目</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="showCumulation">本期无发生额，累计有发生显示</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<FCGLBalanceData> GetFCGLBalanceDataList(Guid[] companyIDs,
                                                int? fromGLCode,
                                                int? toGLCode,
                                                GLCodeType glCodeType,
                                                int fromGLLevel,
                                                int toGLLevel,
                                                bool showEndLevel,
                                                DateTime fromDate,
                                                DateTime toDate,
                                                bool showCumulation);

        /// <summary>
        /// 外币式科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="glID">科目ID</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<FCGLDetailData> GetFCGLDetailBalanceList(Guid[] companyIDs,
                                                      Guid glID,
                                                      DateTime fromDate,
                                                      DateTime toDate);

        /// <summary>
        /// 外币式客户余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="propertys">余额方向(1借方,2贷方)</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerGLBalance> GetCustomerFCGLBalanceList(Guid[] companyIDs,
                                                   Guid[] customerIDs,
                                                   Guid[] glIDs,
                                                   DateTime fromDate,
                                                   DateTime toDate,
                                                   GLCodeProperty[] propertys);

        /// <summary>
        /// 外币式客户科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="customerIDs">客户ID集合</param>
        /// <param name="glIDs">科目ID集合</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CustomerGLDetail> GetCustomerFCGLDetailList(Guid[] companyIDs,
                                                 Guid[] customerIDs,
                                                 Guid[] glIDs,
                                                 DateTime fromDate,
                                                 DateTime toDate);

        /// <summary>
        /// 外币式个人科目余额表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门ID集合</param>
        /// <param name="personalIDs">个人ID集合</param>
        /// <param name="glIds">科目ID集合</param>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="property">统计方向(0全部;1借方;2贷方)</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<PersonalGLBalance> GetPersonalFCGLBalanceList(Guid[] companyIDs,
                                                   Guid[] departmentIDs,
                                                   Guid[] personalIDs,
                                                   Guid[] glIds,
                                                   DateTime fromDate,
                                                   DateTime toDate,
                                                   GLCodeProperty property);

        /// <summary>
        /// 外币式个人科目明细表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>
        /// <param name="departmentIDs">部门ID集合</param>
        /// <param name="personalIDs">个人ID集合</param>
        /// <param name="glIds">科目ID集合</param>
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param>
        /// <param name="noAccounting">包含未记账凭证</param>
        /// <param name="orderByDebit">借方在前,贷方在后</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<PersonalGLDetail> GetPersonalFCGLDetailList(Guid[] companyIDs,
                                                 Guid[] departmentIDs,
                                                 Guid[] personalIDs,
                                                 Guid[] glIds,
                                                 DateTime fromDate,
                                                 DateTime toDate,
                                                 bool orderByDebit);

        /// <summary>
        /// 资产负债表（口岸）
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param>        
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>     
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<BalanceSheet> GetCompanyBalanceSheetDetail(Guid[] companyIDs, DateTime fromDate, DateTime toDate);


        /// <summary>
        /// 资产负债表（汇总表）
        /// </summary>       
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CompanyBalanceSheet> GetCompanyBalanceSheet(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// 资产负债表（集团汇总表）
        /// </summary>       
        /// <param name="fromDate">开始日期</param>
        /// <param name="toDate">结束日期</param> 
        /// <param name="companyIDs">公司集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CompanyBalanceSheetAll> GetCompanyBalanceSheetAll(DateTime fromDate, DateTime toDate, List<Guid> companyIDs);

        /// <summary>
        /// 费用分析表
        /// </summary>
        /// <param name="companyIDs">公司ID集合</param> 
        /// <param name="expenseType">费用类型(1.管理费用,2.财务费用)</param>         
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ExpenseAnalysisSheet> GetExpenseAnalysisSheetDetail(Guid[] companyIDs,
                                                        ExpenseType expenseType,
                                                        DateTime fromDate,
                                                        DateTime toDate,
                                                        bool isCheckGL);

        /// <summary>
        /// 费用分析表汇总表
        /// </summary>
        /// <param name="expenseType">费用类型(1.管理费用,2.财务费用)</param>            
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CompanyExpenseAnalysisSheet> GetExpenseAnalysisSheet(ExpenseType expenseType, ExpenseHappenType happenType, DateTime fromDate, DateTime toDate,bool isCheckGL);

        /// <summary>
        /// 根据航线统计箱量
        /// </summary>
        /// <param name="fromDate">开始时间</param>
        /// <param name="toDate">结束时间</param>
        /// <param name="customerids">客户名称集合</param>
        /// <param name="shipperlines">航线集合</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ReportContainerVolumeForShipperLine> GetContainerVolumeForShipperLine(DateTime fromDate, DateTime toDate, Guid[] customerids, Guid[] shipperlines,Guid[] salesids,Guid[] companyids);

        /// <summary>
        /// 备份凭证
        /// </summary>
        /// <param name="yearMonth"></param>
        /// <param name="companyIds"></param>
        /// <param name="SaveById"></param>
        [FunctionInfomation]
        [OperationContract]
        void BackupLedger(string yearMonth, Guid[] companyIds, Guid SaveById);

        /// <summary>
        /// 利润表
        /// </summary>
        /// <param name="companyIDs"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ProfitDetailReport> GetProfitDetailList(Guid[] companyIDs, DateTime fromDate, DateTime toDate);
       
        /// <summary>
        /// 利润汇总表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ProfitTotalReport> GetProfitTotalList(DateTime fromDate, DateTime toDate);


        /// <summary>
        /// 利润分配
        /// </summary>
        /// <param name="companyIDs"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<ProfitAllocationDetailReport> GetProfitAllocationDetailList(Guid[] companyIDs, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// 利润分配汇总表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        List<CompanyBalanceSheet> GetProfitAllocationTotalList(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// 进口箱列表
        /// </summary>
        /// <param name="CompanyIDs">公司列表</param>
        /// <param name="FreightLocationIDs">提柜地列表</param>
        /// <param name="BeginTime">开始时间</param>
        /// <param name="EndTime">结束时间</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OIContainerReportData GetOIContaierList(string CompanyIDs, string FreightLocationIDs, DateTime BeginTime, DateTime EndTime);
        /// <summary>
        /// 进口箱量统计表
        /// </summary>
        /// <param name="queryParameter">查询条件</param>
        /// <returns></returns>
        [FunctionInfomation]
        [OperationContract]
        OIContainerReportData GetOIContainerVolumeTotal(QueryCriteria4OIContainerVolumeTotal queryParameter);
    }
}
