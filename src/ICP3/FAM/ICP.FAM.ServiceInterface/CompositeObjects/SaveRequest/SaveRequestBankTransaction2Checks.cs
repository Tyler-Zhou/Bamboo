using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 保存对象
    /// </summary>
    [Serializable]
    public class SaveRequestBankTransaction2Checks
    {
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid BankTransactionID { get; set; }
        /// <summary>
        /// 销账ID
        /// </summary>
        public Guid[] ChecksIDs { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
