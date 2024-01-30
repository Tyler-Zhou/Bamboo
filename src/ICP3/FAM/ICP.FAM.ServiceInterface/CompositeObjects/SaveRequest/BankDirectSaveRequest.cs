using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SinglePaymentSaveRequest
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidCode { get; set; }
        /// <summary>
        /// 操作口岸ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 银行编码
        /// </summary>
        public BANKCODE BankCode { get; set; }
        /// <summary>
        /// 权限模式
        /// </summary>
        public string PermissionMode { get; set; }
        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string AccountNO { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BusinessID { get; set; }
        /// <summary>
        /// 业务参考号
        /// </summary>
        public string BusinessNO { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string RelativeAccountNO { get; set; }
        /// <summary>
        /// 银行账户名
        /// </summary>
        public string RelativeAccountName { get; set; }
        /// <summary>
        /// 支行名称
        /// </summary>
        public string RelativeBranchName { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string RelativeBankName { get; set; }
        /// <summary>
        /// 对方银行行号
        /// </summary>
        public string RelativeBankNumber { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethod { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 交易备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string UseDescription { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveByID { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string SaveBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// 批量支付保存对象
    /// </summary>
    [Serializable]
    public class BatchPaymentSaveRequest
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
        /// 币种名称
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid[] BusinessIDs { get; set; }
        /// <summary>
        /// 业务参考号
        /// </summary>
        public string[] BusinessNOs { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string[] RelativeAccountNOs { get; set; }
        /// <summary>
        /// 银行账户名
        /// </summary>
        public string[] RelativeAccountNames { get; set; }
        /// <summary>
        /// 支行名称
        /// </summary>
        public string[] RelativeBankNames { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal[] Amounts { get; set; }
        /// <summary>
        /// 交易备注
        /// </summary>
        public string[] Remarks { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveByID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }

    /// <summary>
    /// 关联销账
    /// </summary>
    [Serializable]
    public class AssociationSaveRequest
    {
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid BankTransactionID { get; set; }
        /// <summary>
        /// 操作口岸
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 销账单号
        /// </summary>
        public string WriteOffNO { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveByID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
