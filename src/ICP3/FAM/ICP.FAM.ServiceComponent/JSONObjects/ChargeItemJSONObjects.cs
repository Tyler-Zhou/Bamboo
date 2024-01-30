using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceComponent.JSONObjects
{
    #region CSP账单费用
    /// <summary>
    /// CSP账单费用
    /// </summary>
    [Serializable]
    public class ChargeItemForCSPAPI
    {
        /// <summary>
        /// 费用ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 费用ID
        /// </summary>
        public Guid FeeID { get; set; }
        /// <summary>
        /// 费用方向
        /// </summary>
        public FeeWay chargeType { get; set; }
        /// <summary>
        /// 账单ID
        /// </summary>
        public int billId { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int customerId { get; set; }
        /// <summary>
        /// 费用代码ID
        /// </summary>
        public int chargingCodeId { get; set; }
        /// <summary>
        /// 币种ID
        /// </summary>
        public int currencyId { get; set; }
        /// <summary>
        /// 计费单位ID
        /// </summary>
        public int unitId { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal unitPrice { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal quantity { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }

    } 
    #endregion

    #region CSP账单费用-单个
    /// <summary>
    /// CSP账单费用-单个
    /// </summary>
    [Serializable]
    public class ChargeItemForCSPAPIItem : PlatformResponseContent<ChargeItemForCSPAPI>
    {
    }
    #endregion

    #region CSP账单费用-列表
    /// <summary>
    /// CSP账单费用-列表
    /// </summary>
    [Serializable]
    public class ChargeItemForCSPAPIList : PlatformResponseBaseList<List<ChargeItemForCSPAPI>>
    {
    }
    #endregion
}
