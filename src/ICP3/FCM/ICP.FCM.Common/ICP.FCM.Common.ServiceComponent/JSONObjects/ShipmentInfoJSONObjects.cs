using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP运单信息
    /// <summary>
    /// CSP运单信息
    /// </summary>
    [Serializable]
    public class ShipmentInfoForCSPAPI : BaseOrderInfoForCSPAPI
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string shipmentNo { get; set; } 
        /// <summary>
        /// 名称
        /// </summary>
        public string shipmentName { get; set; } 
        /// <summary>
        /// 订舱号
        /// </summary>
        public string soNo { get; set; } 
        /// <summary>
        /// 运输条款
        /// </summary>
        public int transportClauses { get; set; }
        /// <summary>
        /// 船名+航次
        /// </summary>
        public string vessel { get; set; }
        /// <summary>
        /// 船东
        /// </summary>
        public int? carrierCustomerId { get; set; }
        /// <summary>
        /// 拖车
        /// </summary>
        public int? truckCustomerId { get; set; }
        /// <summary>
        /// 截文件日
        /// </summary>
        public string siCutOffDate { get; set; }
        /// <summary>
        /// 截VGM日
        /// </summary>
        public string vgmCutOffDate { get; set; }
        /// <summary>
        /// 截柜日
        /// </summary>
        public string cyCutOffTime { get; set; }
        /// <summary>
        /// 预估离开出发港日期
        /// </summary>
        public string estDepatureOrginPortDate { get; set; } 
        /// <summary>
        /// 实际离开出发港日期
        /// </summary>
        public string actualDepatureOrginPortDate { get; set; } 
        /// <summary>
        /// 预估到达目的港日期
        /// </summary>
        public string estArrivalDestinationPortDate { get; set; } 
        /// <summary>
        /// 实际到达目的港日期
        /// </summary>
        public string actualArrivalDestinationPortDate { get; set; } 
        /// <summary>
        /// 客服ID
        /// </summary>
        public int customerServiceUserId { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public int businessUserId { get; set; } 
        /// <summary>
        /// 订舱用户Id
        /// </summary>
        public int bookingUserId { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public List<BookingInfoForCSPAPI> shipmentBookings { get; set; }
    } 
    #endregion

    #region CSP运单信息-订舱号
    /// <summary>
    /// CSP运单信息-单个
    /// </summary>
    [Serializable]
    public class ShipmentForCSPItem : PlatformResponseContent<ShipmentInfoForCSPAPI>
    {
        
    }
    #endregion

    #region CSP运单信息-列表
    /// <summary>
    /// CSP运单信息-列表
    /// </summary>
    [Serializable]
    public class ShipmentForCSPList : PlatformResponseBaseList<List<ShipmentInfoForCSPAPI>>
    {

    }
    #endregion
}
