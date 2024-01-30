using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.DataObjects.Report
{  
    /// <summary>
    /// 代理对账单报表数据对象
    /// </summary>
    [Serializable]
    public class AgentBillCheckingReportData
    {
        /// <summary>
        /// AgentBillCheckingBaseReportData
        /// </summary>
        public AgentBillCheckingBaseReportData BaseReportData { get; set; }

        /// <summary>
        /// 对账单明细列表
        /// </summary>
        public List<AgentBillCheckDetailReportData> DetailList { get; set; }
    }

    /// <summary>
    /// AgentBillCheckingBaseReportData
    /// </summary>
    [Serializable]
    public class AgentBillCheckingBaseReportData
    {
        /// <summary>
        /// 报表打印时间
        /// </summary>
        public DateTime? PrintDate { get; set; }

         /// <summary>
        /// 发起单位名称
        /// </summary>
        public string LaunchCompanyName { get; set; }

        /// <summary>
        /// 发起人名称
        /// </summary>
        public string LaunchUserName { get; set; }

        /// <summary>
        /// 核对公司名称
        /// </summary>
        public string CheckCompanyName { get; set; }

        /// <summary>
        /// 核对人名称
        /// </summary>
        public string CheckUserName { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string OperationTypes { get; set; }

        /// <summary>
        /// ETD截止日
        /// </summary>
        public DateTime? EndingETD { get; set; }

        public string TotalLaunchDebit { get; set; }

        public string TotalLaunchCredit { get; set; }

        public string TotalLaunchBalance { get; set; }

        public string TotalGap { get; set; }

        public string TotalCheckBalance { get; set; }

        public string TotalCheckDebit { get; set; }

        public string TotalCheckCredit { get; set; }
    }

    /// <summary>
    /// 对账单明细
    /// </summary>
    [Serializable]
    public class AgentBillCheckDetailReportData
    {
        /// <summary>
        /// ETD(Estimated Time of Departure)
        /// </summary>
        public DateTime? ETD { get; set; }

        /// <summary>
        /// 提单号（指的是HBL或MBL）
        /// </summary>
        /// <remarks>
        /// 来源于帐单的代理参考号
        /// </remarks>
        public string BLNO { get; set; }

        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// 发起代理的帐单号(可包含多个帐单号)
        /// </summary>
        /// <example>
        /// 例：OESZGS11070522,OESZGS11070336
        /// </example>
        public string LaunchBillNOs { get; set; }
       
        /// <summary>
        /// 发起代理的应收金额
        /// </summary>
        public string LaunchDebit { get; set; }

        /// <summary>
        /// 发起代理的应付金额
        /// </summary>
        public string LaunchCredit { get; set; }

        /// <summary>
        /// 发起代理的余额
        /// </summary>
        public string LaunchBalance { get; set; }

        /// <summary>
        /// 两边代理余额相加的差额
        /// </summary>
        public string Gap { get; set; }

        /// <summary>
        /// 核对代理的帐单号(可包含多个帐单号)
        /// </summary>
        /// <example>
        /// 例：OESZGS11070522,OESZGS11070336
        /// </example>
        public string CheckBillNOs { get; set; }   

        /// <summary>
        /// 核对代理的应收金额
        /// </summary>
        public string CheckDebit { get; set; }

        /// <summary>
        /// 核对代理的应付金额
        /// </summary>
        public string CheckCredit { get; set; }

        /// <summary>
        /// 核对代理的余额
        /// </summary>
        public string CheckBalance { get; set; }
    }    
}
