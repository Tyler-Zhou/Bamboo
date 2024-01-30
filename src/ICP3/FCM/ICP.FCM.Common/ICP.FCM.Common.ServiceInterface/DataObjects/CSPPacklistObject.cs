using System;
using System.Collections.Generic;
using ICP.Framework.CommonLibrary.Platform;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// CSP Pack List
    /// </summary>
    [Serializable]
    public class CSPPacklist
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderNumber { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string productName { get; set; }

        /// <summary>
        /// SKU
        /// </summary>
        public string sku { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string mpn { get; set; }

        /// <summary>
        /// 箱
        /// </summary>
        public int cartons { get; set; }

        /// <summary>
        /// 件数
        /// </summary>
        public decimal units { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal weight { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        public decimal volume { get; set; }
    }

    /// <summary>
    /// Order Item List
    /// </summary>
    public class CSPOrderItem : PlatformResponseContent<List<CSPPacklist>>
    {

    }

}
