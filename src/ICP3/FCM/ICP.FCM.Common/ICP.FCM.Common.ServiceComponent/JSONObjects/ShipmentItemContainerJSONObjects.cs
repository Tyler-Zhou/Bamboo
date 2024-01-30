using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP运单提单箱信息
    /// <summary>
    /// CSP运单提单箱信息
    /// </summary>
    [Serializable]
    public class ShipmentItemContainerForCSPAPI
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 提单箱ID
        /// </summary>
        public Guid BLContainerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shipmentItemId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shipmentContainerId { get; set; }
        /// <summary>
        /// 业务箱ID
        /// </summary>
        public Guid BusinessContainerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int quantityUnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal weight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int weightUnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal volume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int volumeUnitId { get; set; }
    }
    #endregion

    #region CSP运单提单箱信息-单个
    /// <summary>
    /// CSP运单提单箱信息-单个
    /// </summary>
    [Serializable]
    public class ShipmentItemContainerForCSPAPIItem : PlatformResponseContent<ShipmentItemContainerForCSPAPI>
    {

    }
    #endregion

    #region CSP运单提单箱信息-列表
    /// <summary>
    /// CSP运单提单箱信息-列表
    /// </summary>
    [Serializable]
    public class ShipmentItemContainerForCSPAPIList : PlatformResponseBaseList<List<ShipmentItemContainerForCSPAPI>>
    {

    }
    #endregion
}
