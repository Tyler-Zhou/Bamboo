using System;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    #region
    [Serializable]
    public class AssociationInfo
    {
        public Guid BankTransactionID { get; set; }
        public decimal TransactionAmount { get; set; }
        public string AssociationWriteOffNO { get; set; }
        public DateTime? AssociationWriteOffDate { get; set; }
        public string WriteOffNO { get; set; }
        public decimal WriteOffAmount { get; set; }
        public DateTime WriteOffDate { get; set; }
        public DateTime ChargingClosingDate { get; set; }
    }
    #endregion

    #region 支付对象
    /// <summary>
    /// 支付对象
    /// </summary>
    [Serializable]
    public class APIPaymentInfo
    {

        /// <summary>
        /// 操作口岸ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 权限管理模式
        /// </summary>
        public string PermissionMode { get; set; }
        /// <summary>
        /// 业务参考号
        /// </summary>
        public string BusinessNO { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public Guid BankAccountID { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccountNO { get; set; }
        /// <summary>
        /// 银行户名
        /// </summary>
        public string BankAccountName { get; set; }
        /// <summary>
        /// 银行类型
        /// </summary>
        public BANKCODE RelativeBankCode { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethod { get; set; }
        /// <summary>
        /// 对方银行账号
        /// </summary>
        public string RelativeAccountNO { get; set; }
        /// <summary>
        /// 对方银行账户名
        /// </summary>
        public string RelativeAccountName { get; set; }
        /// <summary>
        /// 对方银行支行名称
        /// </summary>
        public string RelativeBranchName { get; set; }
        /// <summary>
        /// 对方开户行
        /// </summary>
        public string RelativeBankName { get; set; }
        /// <summary>
        /// 对方电子联行号
        /// </summary>
        public string RelativeBankNumber { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 交易备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string UseDescription { get; set; }
    }
    #endregion
}
