using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP运单事件
    /// <summary>
    /// CSP运单事件
    /// </summary>
    [Serializable]
    public class ShipmentEventInfoForCSPAPI
    {
        /// <summary>
        /// 事件代码
        /// </summary>
        public string eventCode { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 发生节点
        /// </summary>
        public int happenNode { get; set; }
        /// <summary>
        /// 事件类型
        /// </summary>
        public CSP_SHIPMENT_EVENT_TYPE type { get; set; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public string happenTime { get; set; }
        /// <summary>
        /// 详细
        /// </summary>
        public string details { get; set; }
        /// <summary>
        /// 是针对总shipment事件还是shipmentItem事件
        /// </summary>
        public CSP_EVENTFORMTYPE businessEventType { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public int businessId { get; set; }
        /// <summary>
        /// 是否异常
        /// </summary>
        public bool isException { get; set; }
        /// <summary>
        /// 唯一键
        /// </summary>
        public int id { get; set; }
    } 
    #endregion

    #region CSP运单事件-单个
    /// <summary>
    /// CSP运单事件-单个
    /// </summary>
    [Serializable]
    public class ShipmentEventForCSPAPIItem : PlatformResponseContent<ShipmentEventInfoForCSPAPI>
    {
    } 
    #endregion

    #region CSP运单事件-列表
    /// <summary>
    /// CSP运单事件-列表
    /// </summary>
    [Serializable]
    public class ShipmentEventForCSPList : PlatformResponseBaseList<List<ShipmentEventInfoForCSPAPI>>
    {
    }
    #endregion
}
