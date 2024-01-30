using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP运单箱信息
    /// <summary>
    /// CSP运单箱信息
    /// </summary>
    [Serializable]
    public class ShipmentContainerForCSPAPI
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 业务箱ID
        /// </summary>
        public Guid ShipmentContainerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shipmentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string containerNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sealNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int containerTypeId { get; set; }
    } 
    #endregion

    #region CSP运单箱信息-单个
    /// <summary>
    /// CSP运单箱信息-单个
    /// </summary>
    [Serializable]
    public class ShipmentContainerForCSPAPIItem : PlatformResponseContent<ShipmentContainerForCSPAPI>
    {

    }
    #endregion

    #region CSP运单箱信息-列表
    /// <summary>
    /// CSP运单箱信息-列表
    /// </summary>
    [Serializable]
    public class ShipmentContainerForCSPAPIList : PlatformResponseBaseList<List<ShipmentContainerForCSPAPI>>
    {

    }
    #endregion
}
