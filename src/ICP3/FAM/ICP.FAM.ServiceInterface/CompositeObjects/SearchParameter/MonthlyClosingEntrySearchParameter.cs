using System;

namespace ICP.FAM.ServiceInterface
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MonthlyClosingEntrySearchParameter
    {
        /// <summary>
        /// 操作口岸
        /// </summary>
        public Guid[] CompanyIDs { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public string ApplicantName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? From { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? To { get; set; }
        /// <summary>
        /// 利润(计算开始时间)
        /// </summary>
        public DateTime? ProfitFrom { get; set; }
        /// <summary>
        /// 利润(计算结束时间)
        /// </summary>
        public DateTime? ProfitTo { get; set; }
        /// <summary>
        /// 是否投保
        /// </summary>
        public bool? IsInsured { get; set; }
        /// <summary>
        /// 总记录行数
        /// </summary>
        public int TotalRecords { get; set; }
    }
}
