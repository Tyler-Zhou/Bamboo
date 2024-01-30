using ICP.FAM.ServiceInterface.DataObjects;
using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 银行水单查询类
    /// </summary>
    [Serializable]
    public class BankReceiptSearchParameter
    {
        /// <summary>
        /// 水单号码
        /// </summary>
        public string ReceiptNO { get; set; }
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        public Guid[] CompanyIDs { get; set; }
        /// <summary>
        /// 状态列表
        /// </summary>
        public BankReceiptStatus Status { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool? IsValid { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? FromDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ToDate { get; set; }
    }
}
