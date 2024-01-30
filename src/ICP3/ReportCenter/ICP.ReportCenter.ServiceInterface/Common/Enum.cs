using ICP.Framework.CommonLibrary.Attributes;
using System;

namespace ICP.ReportCenter.ServiceInterface
{
    #region CRM客户类型
    /// <summary>
    /// CRM客户类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum SalesCustomerType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [MemberDescription("全部", "All")]
        All = 1,

        /// <summary>
        /// 同行
        /// </summary>
        [MemberDescription("同行", "NVOCC")]
        NVOCC = 2,

        /// <summary>
        /// 直客
        /// </summary>
        [MemberDescription("直客", "Direct Client")]
        DirectClient = 3,

        /// <summary>
        /// 商务
        /// </summary>
        [MemberDescription("商务", "Business")]
        Business = 4,

        /// <summary>
        /// 其它
        /// </summary>
        [MemberDescription("其它", "Other")]
        Other = 5
    } 
    #endregion

    #region 费用类型
    /// <summary>
    /// 费用类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum ExpenseType
    {
        /// <summary>
        /// 管理费用
        /// </summary>
        [MemberDescription("管理费用分析表", "AdministrativeExpenseAnalysisSheet")]
        AdministrativeExpenseAnalysisSheet = 1,

        /// <summary>
        /// 财务费用
        /// </summary>
        [MemberDescription("财务费用分析表", "FinancialExpenseAnalysisSheet ")]
        FinancialExpenseAnalysisSheet = 2,
    } 
    #endregion

    #region 费用发生类型
    /// <summary>
    /// 费用发生类型
    /// </summary>
    [Flags]
    [Serializable]
    public enum ExpenseHappenType
    {
        /// <summary>
        /// 本月发生数
        /// </summary>
        [MemberDescription("本月发生数", "Month Happen")]
        Month = 1,

        /// <summary>
        /// 本年累计数
        /// </summary>
        [MemberDescription("本年累计数", "Year Total")]
        Year = 2,
    } 
    #endregion

    #region 日期查询类型(出口箱列表)
    /// <summary>
    /// 日期查询类型(出口箱列表)
    /// </summary>
    [Flags]
    [Serializable]
    public enum ECLDateSearchType
    {
        /// <summary>
        /// ETD
        /// </summary>
        [MemberDescription("ETD", "ETD")]
        ETD = 1,

        /// <summary>
        /// ETA
        /// </summary>
        [MemberDescription("ETA", "ETA")]
        ETA = 2,
    } 
    #endregion
}
