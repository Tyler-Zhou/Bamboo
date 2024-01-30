using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceComponent.JSONObjects
{
    #region CSP账单支付记录
    /// <summary>
    /// CSP账单支付记录
    /// </summary>
    [Serializable]
    public class PaymentRecordForCSPAPI
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CheckItemID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int customerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int chargeItemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int currencyId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal payAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int checkerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bankDate { get; set; }

    } 
    #endregion

    #region CSP账单支付记录-单个
    /// <summary>
    /// CSP账单支付记录-单个
    /// </summary>
    [Serializable]
    public class PaymentRecordForCSPAPIItem : PlatformResponseContent<PaymentRecordForCSPAPI>
    {
    }
    #endregion
}
