using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 银行流水/销账关联数据
    /// </summary>
    [Serializable]
    public class BankTransaction2ChecksSearchParameter
    {
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid BankTransactionID { get; set; }
    }

    /// <summary>
    /// 银行流水报表查询参数
    /// </summary>
    [Serializable]
    public class BankTransactionReportSearchParameter
    {
        /// <summary>
        /// 银行流水ID集合
        /// </summary>
        public Guid[] BankTransactionIDs { get; set; }
    }
}
