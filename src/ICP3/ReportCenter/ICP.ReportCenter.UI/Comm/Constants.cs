
using System;

namespace ICP.ReportCenter.UI
{

    #region 报表通用常量
    public class ReportCommonConstants
    {
        public const string DividedSymbol = ",";
    }
    #endregion

    #region 功能项常量
    /// <summary>
    /// FCM模块常量
    /// </summary>
    public class BusinessFunctionConstants
    {
        /// <summary>
        /// 箱量利润统计表 
        /// </summary>
        public const string RP_DWPortfolioAndProfitTotal = "RP_DWPortfolioAndProfitTotal";
        /// <summary>
        /// 箱量利润详细表 
        /// </summary>
        public const string RP_DWPortfolioAndProfitDetail = "RP_DWPortfolioAndProfitDetail";
        /// <summary>
        /// 进口箱量统计表 
        /// </summary>
        public const string ImportContainerList = "ImportContainerList";
        /// <summary>
        /// 出口箱信息列表
        /// </summary>
        public const string RP_ExportContainerList = "RP_EXPORTCONTAINERLIST";
        /// <summary>
        /// 商务箱量统计表 
        /// </summary>
        public const string RP_CommercePortfolioAndProfitForGeneral_Total = "RP_CommercePortfolioAndProfitForGeneral_Total";
        /// <summary>
        /// 商务箱量详细表 
        /// </summary>
        public const string RP_CommercePortfolioAndProfitForGeneral_Detail = "RP_CommercePortfolioAndProfitForGeneral_Detail";
        /// <summary>
        /// 利润组成分析 
        /// </summary>
        public const string RP_DWProfitForCompose = "RP_DWProfitForCompose";
        /// <summary>
        /// 单箱利润分析 
        /// </summary>
        public const string RP_DWPorfitForT = "RP_DWPorfitForT";
        /// <summary>
        /// 利润趋势分析 
        /// </summary>
        public const string RP_ALLGetDirectionForProfit = "RP_ALLGetDirectionForProfit";
        /// <summary>
        /// 箱量趋势分析 
        /// </summary>
        public const string RP_ALLGetDirectionForTEU = "RP_ALLGetDirectionForTEU";
        /// <summary>
        /// 利润同期对比分析 
        /// </summary>
        public const string RP_ALLGetSameTermCompareForProfit = "RP_ALLGetSameTermCompareForProfit";
        /// <summary>
        /// 箱量同期对比分析 
        /// </summary>
        public const string RP_ALLGetSameTermCompareForT = "RP_ALLGetSameTermCompareForT";
        /// <summary>
        /// 成本组成分析 
        /// </summary>
        public const string RP_ALLCostFor_Total = "RP_ALLCostFor_Total";
        /// <summary>
        /// 成本趋势分析 
        /// </summary>
        public const string RP_ALLCostForDirection = "RP_ALLCostForDirection";
        /// <summary>
        /// 成本同期对较 
        /// </summary>
        public const string RP_ALLCostForSameTermCompare = "RP_ALLCostForSameTermCompare";
        /// <summary>
        /// 经营状况分析表 
        /// </summary>
        public const string RP_AnalysisOfOperatingConditions = "RP_AnalysisOfOperatingConditions";
        /// <summary>
        /// 成本分析-个人帐单 
        /// </summary>
        public const string RP_CostDetail = "RP_CostDetail";
        /// <summary>
        /// 业务员利润提成表 
        /// </summary>
        public const string RP_ALLPortfolioAndProfitForSalesCustomer = "RP_ALLPortfolioAndProfitForSalesCustomer";

        /// <summary>
        /// 根据航线统计箱量
        /// </summary>
        public const string RP_ContainerVolumeForShippingLine_Total = "RP_ContainerVolumeForShippingLine_Total";
        /// <summary>
        /// 目的港返利表
        /// </summary>
        public const string RP_DESTINATIONRETURNPROFIT = "RP_DestinationReturnProfit";
        /// <summary>
        /// 配比汇总
        /// </summary>
        public const string RP_PROFITRATIOS_SUMMARY = "RP_ProfitRatios_Summary";
        /// <summary>
        /// 本地服务详细表
        /// </summary>
        public const string RP_LOCALSERVICE_DETAIL = "RP_LocalService_Detail";
      
    }

    /// <summary>
    /// FIN_OE 模块常量
    /// </summary>
    public class FIN_OEFunctionConstants
    {
        /// <summary>
        /// 业务管理成本 
        /// </summary>
        public const string RP_Commision = "RP_Commision";
        /// <summary>
        /// 应收帐款表 
        /// </summary>
        public const string RP_DcNoteDR = "RP_DcNoteDR";

        /// <summary>
        /// 应付帐款表 
        /// </summary>
        public const string RP_DcNoteCR = "RP_DcNoteCR";
        /// <summary>
        /// 帐龄表 
        /// </summary>
        public const string RP_DcNoteForAge = "RP_DcNoteForAge";
        /// <summary>
        /// 帐龄统计表 
        /// </summary>
        public const string RP_DcNoteForAgeSum = "RP_DcNoteForAgeSum";
        /// <summary>
        /// 代理对帐表 
        /// </summary>
        public const string RP_DcNoteForAgent = "RP_DcNoteForAgent";
        /// <summary>
        /// 应收应付盈余表 
        /// </summary>
        public const string RP_DebitNoteForProfit = "RP_DebitNoteForProfit";

        /// <summary>
        /// 凭证明细列表 
        /// </summary>
        public const string RP_ALLGetVoucherInfo = "RP_ALLGetVoucherInfo";
       
    }


    /// <summary>
    /// FIN_OIFunctionConstants 模块常量
    /// </summary>
    public class FIN_OIFunctionConstants
    {
        /// <summary>
        /// 帐龄表 
        /// </summary>
        public const string RP_AgingReport = "RP_AgingReport";
        /// <summary>
        /// 代理对帐表 
        /// </summary>
        public const string RP_AgentStatement = "RP_AgentStatement";
        /// <summary>
        /// 对帐表 
        /// </summary>
        public const string RP_LocalStatement = "RP_LocalStatement";
        /// <summary>
        /// 收支报表 
        /// </summary>
        public const string RP_CheckDeposit = "RP_CheckDeposit";

        /// <summary>
        /// 收支报表CA 
        /// </summary>
        public const string RP_CheckDepositCA = "RP_CheckDepositCA";

        /// <summary>
        /// 预收预付
        /// </summary>
        public const string RP_PrepaidInAdvance = "RP_PrepaidInAdvance";
    }


    /// <summary>
    /// FIN_OE 模块常量
    /// </summary>
    public class FIN_FunctionConstants
    {
        /// <summary>
        /// 科目余额表 
        /// </summary>
        public const string RP_GLSummary = "RP_GLSummary";
        /// <summary>
        /// 试算平衡表 
        /// </summary>
        public const string RP_TrialBalance = "RP_TrialBalance";

        /// <summary>
        /// 资产负债表 
        /// </summary>
        public const string RP_BalanceSheetReport = "RP_BalanceSheetReport";
        /// <summary>
        /// 损益表 
        /// </summary>
        public const string RP_InComeStatementReport = "RP_InComeStatementReport";
        /// <summary>
        /// 银行对帐表 
        /// </summary>
        public const string RP_BankstandingReport = "RP_BankstandingReport";
        /// <summary>
        /// 日记帐报表 
        /// </summary>
        public const string RP_Journal_Report = "RP_Journal_Report";
       

    }

    /// <summary>
    /// 用友报表 模块常量
    /// </summary>
    public class UF_FunctionConstants
    {
        public const string RP_GLBalanceData = "RP_GLBalanceData";
        public const string RP_GLDetailData = "RP_GLDetailData";
        public const string RP_CustomerGLBalance = "RP_CustomerGLBalance";
        public const string RP_CustomerGLDetail = "RP_CustomerGLDetail";
        public const string RP_PersonalGLBalance = "RP_PersonalGLBalance";
        public const string RP_PersonalGLDetail = "RP_PersonalGLDetail";
        public const string RP_Customer3ColumnGLBalance = "RP_Customer3ColumnGLBalance";
        public const string RP_Personal3ColumnGLBalance = "RP_Personal3ColumnGLBalance";
        public const string RP_UFTest = "RP_UFTest";
        public const string RP_BalanceSheet = "RP_BalanceSheet";
        public const string RP_BalanceSheetDetail = "RP_BalanceSheetDetail";
        public const string RP_BalanceSheetReportForLA = "RP_BalanceSheetReportForLA";
        public const string RP_BalanceSheetReportForAll = "RP_BalanceSheetReportForAll";
        public const string RP_ExpenseAnalysisSheetDetail = "RP_ExpenseAnalysisSheetDetail";
        public const string RP_ExpenseAnalysisSheet = "RP_ExpenseAnalysisSheet";

        public const string RP_ProfitDetail = "RP_PROFITDETAIL";
        public const string RP_ProfitTotal = "RP_PROFITTOTAL";
        public const string RP_ProfitTotalAll = "RP_PROFITTOTALAll";
        public const string RP_ProfitAllocationDetail = "RP_PROFITALLOCATIONDETAIL";
        public const string RP_ProfitAllocationTotal = "RP_PROFITALLOCATIONTOTAL";
        
    }
    /// <summary>
    /// 动作项的常量
    /// </summary>
    public class ActionConstants
    {
        /// <summary>
        /// 是否可以看到应付帐款的往来单位 
        /// </summary>
        public const string DWJobInformation_ViewShipper = "DWJobInformation_ViewShipper";
        /// <summary>
        /// 是否可以看到汇兑表
        /// </summary>
        public const string Report_Total = "REPORT_TOTAL";
    }


    /// <summary>
    /// FCMFunctionConstants 模块常量
    /// </summary>
    public class FCMFunctionConstants
    {
        /// <summary>
        /// 操作工作量统计 
        /// </summary>
        public const string RP_WorkLoadForOperator = "RP_WorkLoadForOperator";
        /// <summary>
        /// 操作指定货报表 
        /// </summary>
        public const string RP_AgentForOperator = "RP_AgentForOperator";
        /// <summary>
        /// 出口业务信息报表 
        /// </summary>
        public const string RP_OEBussinesInfo = "RP_OEBussinesInfo";
        /// <summary>
        /// 业务数据核对表 
        /// </summary>
        public const string RP_JobInfoForCargoTracking = "RP_JobInfoForCargoTracking";
    }

     /// <summary>
    /// CRMFunctionConstants 模块常量
    /// </summary>
    public class CRMFunctionConstants
    {
        /// <summary>
        /// 业务信息表 
        /// </summary>
        public const string RP_DWJobInformation = "RP_DWJobInformation";

        /// <summary>
        /// 业务活动统计 
        /// </summary>
        public const string RP_DevCRMSalesActivity = "RP_DevCRMSalesActivity";
        /// <summary>
        /// 经理分析统计 
        /// </summary>
        public const string RP_DevCRMCustomerAnalysis = "RP_DevCRMCustomerAnalysis";
        /// <summary>
        /// 合作状态统计 
        /// </summary>
        public const string RP_DevCRMCustomerState = "RP_DevCRMCustomerState";
        /// <summary>
        /// 业务阶段统计 
        /// </summary>
        public const string RP_DevCRMCustomerPhase = "RP_DevCRMCustomerPhase";
    }


    #endregion

    #region UI常量

   /// <summary>
   /// 命令常量
   /// </summary>
    public class CommandConstants
    {
        /// <summary>
        /// 显示或隐藏查询面板
        /// </summary>
        public const string Command_ShowSearch = "Command_ShowSearch";
    }

    /// <summary>
   /// ReportPathConstants
   /// </summary>
    public class ReportPathConstants
    {
        /// <summary>
        /// 显示或隐藏查询面板
        /// </summary>
        [Obsolete("Plan New Path:CRMReport")]
        public const string ReportCenter = @"/ReportCenter/";
        /// <summary>
        /// 会计报表
        /// </summary>
        [Obsolete("Plan New Path:AccountingReport")]
        public const string FAMReport = @"/FAMReport/";
        /// <summary>
        /// 业务管理报表
        /// </summary>
        public const string BusinessReports = @"/BusinessReports/";
        /// <summary>
        /// CRM报表
        /// </summary>
        public const string CRMReport = @"/BusinessReports/";
        /// <summary>
        /// 会计报表
        /// </summary>
        public const string AccountingReport = @"/AccountingReport/";
        /// <summary>
        /// 会计报表(进口报表)
        /// </summary>
        public const string AccountingReportByImport = @"/AccountingReport.Import/";
        /// <summary>
        /// 操作报表
        /// </summary>
        public const string OperationReport = @"/OperationReport/";
        
    }

    
    /// Workspace常量
    /// </summary>
    public class ReportWorkSpaceConstants
    {
        /// <summary>
        /// SearchWorkspace
        /// </summary>
        public const string SearchWorkspace = "SearchWorkspace";

        /// <summary>
        /// ReportWorkspace
        /// </summary>
        public const string ReportWorkspace = "ReportWorkspace";
    }

    public class SearchFieldConstants
    {
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName" };

        public static readonly string[] ResultValueChargeCode = new string[] { "ChargingCodeID", "Code", "EName", "CName" };

        public static readonly string[] VesselResultValue = new string[] { "ID", "No", "VesselName" };

        public const string VesselVoyage = @"Vessel/Voyage";
        public const string Vessel = @"Vessel";
        public const string Voyage = @"Voyage";

        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";

        public static readonly string[] ChargeCodeResultValue = new string[] { "ID", "ChargingCodeName" };   
    }

    #endregion

    #region  报表参数常量

    ///<summary>
    /// 报表参数常量
    /// </summary>
    public class ReportConfigConstants
    {

        /// <summary>
        /// 报表中心的配置代码
        /// </summary>
        public const string ReportCenterConfig = "ReportCenterConfig";
        

        /// <summary>
        /// 进口对帐单附件类型 0:国内1:国外
        /// </summary>
        public const string LocalStatementAttachType = "LocalStatementAttachType";
    }

    #endregion
}
