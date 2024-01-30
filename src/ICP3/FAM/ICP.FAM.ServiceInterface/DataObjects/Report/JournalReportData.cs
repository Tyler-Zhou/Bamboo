using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.DataObjects.Report
{  
    /// <summary>
    /// 日记帐报表数据对象
    /// </summary>
    [Serializable]
    public class JournalReportData
    {
        /// <summary>
        /// JournalBaseReportData
        /// </summary>
        public JournalBaseReportData JournalBaseReportData { get; set; }

        /// <summary>
        /// 应收/应付明细列表
        /// </summary>
        public List<JournalDetailReportData> DetailList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<TotalFee> TotalFeeList { get; set; }
    }

    /// <summary>
    /// JournalBaseReportData
    /// </summary>
    [Serializable]
    public class JournalBaseReportData
    {
        /// <summary>
        /// 单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string PostDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }    
    }

    /// <summary>
    /// 应收/应付明细
    /// </summary>
    [Serializable]
    public class JournalDetailReportData
    {
        /// <summary>
        /// 会计科目描述
        /// </summary>
        public string GLDescription { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
       
        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal DRAmount{ get; set; }
             
        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal CRAmount{ get; set; }
             
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }       
    }

    /// <summary>
    /// TotalFee
    /// </summary>
    [Serializable]
    public class TotalFee
    {
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 总应收金额
        /// </summary>
        public string TotalDRAmount { get; set; }

        /// <summary>
        /// 总应付金额
        /// </summary>
        public string TotalCRAmount { get; set; }
    }
}
