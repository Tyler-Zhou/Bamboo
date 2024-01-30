using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 交易流水(查询参数)
    /// </summary>
    [Serializable]
    public class BankTransactionSearchParameter
    {
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 银行编码
        /// </summary>
        public BANKCODE BankCode { get; set; }
        /// <summary>
        /// 银行账号ID
        /// </summary>
        public Guid BankAccountID { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccountNO { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string BusinessNO { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string FlowWaterNO { get; set; }
        /// <summary>
        /// 对方银行账号名
        /// </summary>
        public string RelativeAccountName { get; set; }
        /// <summary>
        /// 借贷标志
        /// </summary>
        public string DebitCreditFlag { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrentName { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 最小金额
        /// </summary>
        public decimal MinimumAmount { get; set; }
        /// <summary>
        /// 最大金额
        /// </summary>
        public decimal MaximumAmount { get; set; }
        /// <summary>
        /// 总记录行数
        /// </summary>
        public int TotalRecords { get; set; }
        /// <summary>
        /// 查询人
        /// </summary>
        public Guid Queryer { get; set; }
    }
}
