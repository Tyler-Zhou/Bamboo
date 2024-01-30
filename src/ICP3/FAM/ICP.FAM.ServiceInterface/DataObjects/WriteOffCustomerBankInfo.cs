using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 客户银行信息
    /// </summary>
    [Serializable]
    public class CustomerBankInfo
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 账号名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 账号账号
        /// </summary>
        public string AccountNO { get; set; }
        /// <summary>
        /// 支行名称
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// 银行行号
        /// </summary>
        public string BankNumber { get; set; }
        /// <summary>
        /// 开户银行
        /// </summary>
        public string BankName { get; set; }

    }
}
