using ICP.FCM.Common.ServiceInterface;
using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP提单信息
    /// <summary>
    /// CSP提单信息
    /// </summary>
    [Serializable]
    public class ShipmentItemForCSPAPI
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int customerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shipmentId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string shipmentName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CSP_SHIPMENTTYPE shipmentType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string billOfLadingNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int shipperCustomerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int consigneeCustomerId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string containerType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int incoterms { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int quoteEnquiryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? truckOriginAddressId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? truckDestinationAddressId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalQuantity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalQuantityUnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal totalWeight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalWeightUnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal totalVolume { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int totalVolumeUnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string specialInstructions { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string containerNos { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cargoReadyDate { get; set; }
        /// <summary>
        /// 预计拖车到达起始港时间
        /// </summary>
        public string estTruckDeliveryOrignDate { get; set; }
        /// <summary>
        /// 实际拖车到达起始港时间
        /// </summary>
        public string actualTruckDeliveryOrignDate { get; set; }
        /// <summary>
        /// 预估装车时间（离港后）
        /// </summary>
        public string estPickUpTruckDestinationDate { get; set; }
        /// <summary>
        /// 实际装车时间（离港后）
        /// </summary>
        public string actualPickUpTruckDestinationDate { get; set; }
        /// <summary>
        /// 预计最终到达时间
        /// </summary>
        public string estTruckDeliveryDate { get; set; }
        /// <summary>
        /// 实际最终到达时间
        /// </summary>
        public string actualTruckDeliveryDate { get; set; }
        
    } 
    #endregion

    #region CSP提单信息-单个
    /// <summary>
    /// CSP提单信息-单个
    /// </summary>
    [Serializable]
    public class ShipmentItemForCSPAPIItem : PlatformResponseContent<ShipmentItemForCSPAPI>
    {

    }
    #endregion

    #region CSP提单信息-列表
    /// <summary>
    /// CSP提单信息-列表
    /// </summary>
    [Serializable]
    public class ShipmentItemForCSPAPIList : PlatformResponseBaseList<List<ShipmentItemForCSPAPI>>
    {

    }
    #endregion
}
