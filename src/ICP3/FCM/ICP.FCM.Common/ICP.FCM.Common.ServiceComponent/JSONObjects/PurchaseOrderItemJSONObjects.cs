using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP采购单明细项信息
    /// <summary>
    /// CSP采购单明细项信息
    /// </summary>
    [Serializable]
    public class PurchaseOrderItemForCSPAPI
    {
        /// <summary>
        /// 
        /// </summary>
        public int shipmentItemContainerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int orderId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int itemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string orderNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string productName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sku { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mpn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int cartons { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal units { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal unitCost { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal weight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal volume { get; set; }

    }
    #endregion

    #region CSP采购单明细项信息-单个
    /// <summary>
    /// CSP采购单明细项信息-单个
    /// </summary>
    [Serializable]
    public class PurchaseOrderItemForCSPAPIItem : PlatformResponseContent<PurchaseOrderItemForCSPAPI>
    {

    }
    #endregion

    #region CSP采购单明细项信息-列表
    /// <summary>
    /// CSP采购单明细项信息-列表
    /// </summary>
    [Serializable]
    public class PurchaseOrderItemForCSPAPIList : PlatformResponseBaseList<List<PurchaseOrderItemForCSPAPI>>
    {

    }
    #endregion
}
