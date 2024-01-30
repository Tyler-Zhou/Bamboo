using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 银行流水报表数据
    /// </summary>
    public class BankTransactionReportData
    {
        /// <summary>
        /// 交易金额
        /// </summary>
        public string TransactionAmount { get; set; }
        /// <summary>
        /// 交易币种名称
        /// </summary>
        public string TransactionCurrency { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string UseDescription { get; set; }
        /// <summary>
        /// 交易状态
        /// </summary>
        public string TransactionStatus { get; set; }
        /// <summary>
        /// 经办日
        /// </summary>
        public string OperationDate { get; set; }
        /// <summary>
        /// 付方账号
        /// </summary>
        public string PaymentAccountNO { get; set; }
        /// <summary>
        /// 付方币种
        /// </summary>
        public string PaymentCurrency { get; set; }
        /// <summary>
        /// 付方账户名称
        /// </summary>
        public string PaymentAccountName { get; set; }
        /// <summary>
        /// 付方开户行
        /// </summary>
        public string PaymentBranchName { get; set; }
        /// <summary>
        /// 收方账号
        /// </summary>
        public string ReceiveAccountNO { get; set; }
        /// <summary>
        /// 收方币种
        /// </summary>
        public string ReceiveCurrency { get; set; }
        /// <summary>
        /// 收方账户名称
        /// </summary>
        public string ReceiveAccountName { get; set; }
        /// <summary>
        /// 收方支行
        /// </summary>
        public string ReceiveBranchName { get; set; }

    }
}
