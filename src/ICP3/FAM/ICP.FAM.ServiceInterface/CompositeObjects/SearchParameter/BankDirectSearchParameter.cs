using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 支付查询参数
    /// </summary>
    [Serializable]
    public class PaymentSearchParameter
    {
        /// <summary>
        /// 销账ID
        /// </summary>
        public Guid[] CheckAmountIDs { get; set; }
    }

    /// <summary>
    /// 批量支付查询参数
    /// </summary>
    [Serializable]
    public class BatchPaymentSearchParameter
    {
        /// <summary>
        /// 销账ID
        /// </summary>
        public Guid[] WriteOffIDs { get; set; }
    }

    /// <summary>
    /// 直连银行查询参数
    /// </summary>

    [Serializable]
    public class DirectBankSearchParameter
    {
        /// <summary>
        /// 操作口岸
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 仅直连银行(否：银行名下某一银行账号支持的所有账号)
        /// </summary>
        public bool OnlyDirectBank { get; set; }
    }
}
