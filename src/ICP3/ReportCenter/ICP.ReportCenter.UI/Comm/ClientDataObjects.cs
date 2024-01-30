using System.Collections.Generic;
using Microsoft.Reporting.WinForms;
using ICP.Framework.CommonLibrary.Attributes;
using System;

namespace ICP.ReportCenter.UI
{
    public class ReportData
    {
        public List<ReportParameter> Parameters { get; set; }
        public string ReportName { get; set; }
        public string ServiceReportPath { get; set; }

        public List<ReportDataSource> DataSource { get; set; }
        public List<ReportDataSource> SubDataSource { get; set; }
        public bool IsLocalReport { get; set; }

        /// <summary>
        /// 发邮件使用客户ID
        /// </summary>
        public Guid? CustomerID { get; set; }
    }

    /// <summary>
    /// 是否亏损 0 All ,Profit 1 ,Loss 2
    /// </summary>
    public enum ReportDeficitType
    {
        [MemberDescription("全部", "All")]
        All = 0,
        [MemberDescription("盈利", "Profit")]
        Profit = 1,
        [MemberDescription("亏损", "Loss")]
        Loss = 2
    }
    /// <summary>
    /// 余额方向0 All ,Debit 1,Credit 2
    /// </summary>
    public enum BalanceDirection
    {
        [MemberDescription("全部", "All")]
        All = 0,
        [MemberDescription("借方", "Debit")]
        Debit = 1,
        [MemberDescription("贷方", "Credit")]
        Credit = 2
    }


    /// <summary>
    /// 统计类别(0 年,1月
    /// </summary>
    public enum ReportStatisticsType
    {
        [MemberDescription("年", "Year")]
        Year = 0,
        [MemberDescription("月", "Month")]
        Month = 1
    }

    /// <summary>
    /// 揽货方式 6 All
    /// </summary>
    public enum ReportSalesType
    {
        [MemberDescription("同行货", "NVOCC")]
        NVOCC = 0,
        [MemberDescription("自揽货", "SALES")]
        SALES = 1,
        [MemberDescription("指定货", "AGENT")]
        AGENT = 2,

        [MemberDescription("公司货", "COMPANY")]
        COMPANY = 3,
        [MemberDescription("外地自揽货", "FOREIGN SALES")]
        FOREIGN_SALES = 4,
        [MemberDescription("海外指定货", "OVERSEA AGENT")]
        OVERSEA_AGENT = 5,
        [MemberDescription("全部", "ALL")]
        ALL = 6,
    }

    /// <summary>
    /// 费用类型标志 0-All ;1－Commission
    /// </summary>
    public enum ReportFeeType
    {
        [MemberDescription("全部", "All")]
        All = 0,
        [MemberDescription("Commission", "Commission")]
        Commission = 1
    }

    /// <summary>
    /// 折合币种类型 0-Origin ;1－USD
    /// </summary>
    public enum ReportCurrencyType
    {
        [MemberDescription("原始币别", "Origin")]
        Origin = 0,
        [MemberDescription("折合为美金", "USD")]
        USD = 1
    }

    /// <summary>
    /// 费用类型标志 0－客户;1－代理;2－全部
    /// </summary>
    public enum ReportViewType
    {
        [MemberDescription("客户", "Customer")]
        Customer = 0,
        [MemberDescription("代理", "Agent")]
        Agent = 1,
        [MemberDescription("全部", "All")]
        All = 2,
        [MemberDescription("非内部往来", "Out")]
        Out = 3
    }

    /// <summary>
    /// 费用类型标志 0－客户;1－代理;2－全部
    /// </summary>
    public enum ReportGroupby
    {
        [MemberDescription("业务发生地", "Place")]
        Place = 0,
        [MemberDescription("客户类型", "CusType")]
        CusType = 1,
    }
    /// <summary>
    /// ReportComboboxType2 0 All ,Yes 1 ,No 2
    /// </summary>
    public enum ReportYesNoType
    {
        [MemberDescription("全部", "All")]
        All = 0,
        [MemberDescription("是", "Yes")]
        Yes = 1,
        [MemberDescription("否", "No")]
        No = 2
    }

    /// <summary>
    /// ReportComboboxType 0－是;1－否;2－全部
    /// </summary>
    public enum ReportYesNoType2
    {

        [MemberDescription("是", "Yes")]
        Yes = 0,
        [MemberDescription("否", "No")]
        No = 1,
        [MemberDescription("全部", "All")]
        All = 2
    }

    /// <summary>
    /// ReportComboboxType 0－否;1－是;2－全部
    /// </summary>
    public enum ReportYesNoType3
    {
        [MemberDescription("否", "No")]
        No = 0,
        [MemberDescription("是", "Yes")]
        Yes = 1,
        [MemberDescription("全部", "All")]
        All = 2
    }

    /// <summary>
    /// ReportComboboxType 0－否;1－是;
    /// </summary>
    public enum ReportYesNoType4
    {
        [MemberDescription("否", "No")]
        No = 0,
        [MemberDescription("是", "Yes")]
        Yes = 1
    }

    /// <summary>
    /// ReportUserState 0－无效;1－有效;2－全部
    /// </summary>
    public enum ReportUserState
    {
        [MemberDescription("无效", "Invalidated")]
        Invalidated = 0,
        [MemberDescription("有效", "Valid")]
        Valid = 1,
        [MemberDescription("全部", "All")]
        All = 2
    }

    /// <summary>
    /// ReportDateType 0－创建日期;1－ETD;
    /// </summary>
    public enum ReportDateType
    {
        [MemberDescription("创建日期", "CreateDate")]
        CreateDate = 0,
        [MemberDescription("ETD", "ETD")]
        ETD = 1
    }

    /// <summary>
    /// CheckDepositDateType 0－输入日期;1－到帐日期 ;2 -支票日期 ;
    /// </summary>
    public enum CheckDepositDateType
    {
        [MemberDescription("输入日期", "CreateDate")]
        CreateDate = 0,
        [MemberDescription("到帐日期", "DuDate")]
        DuDate = 1,
        [MemberDescription("支票日期", "CheckDate")]
        CheckDate = 2
    }

    /// <summary>
    /// CheckDepositGroupType 0－往来单位;1－银行 ;2 -日期&银行 ;
    /// </summary>
    public enum CheckDepositGroupType
    {
        [MemberDescription("往来单位", "BillTo")]
        BillTo = 0,
        [MemberDescription("银行", "Bank")]
        Bank = 1,
        [MemberDescription("日期&银行", "Date&Bank")]
        DateAndBank = 2
    }

    /// <summary>
    /// CheckDepositSortByType 0－支票号;1－日期 ;
    /// </summary>
    public enum CheckDepositSortByType
    {
        [MemberDescription("支票号", "Check No")]
        CheckNo = 0,
        [MemberDescription("日期", "Date")]
        Date = 1
    }



}
