using ICP.FCM.Common.ServiceInterface;
using System;
using System.Collections.Generic;

namespace ICP.FCM.Common.ServiceComponent.JSONObjects
{
    #region CSP订单基本信息
    /// <summary>
    /// CSP订单基本信息
    /// </summary>
    [Serializable]
    public class BaseOrderInfoForCSPAPI
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 运输方式
        /// </summary>
        public CSP_FREIGHTMETHODTYPE freightMethodType { get; set; }
        /// <summary>
        /// 运输类型
        /// </summary>
        public CSP_SHIPMENTTYPE shipmentType { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public CSP_BOOKING_STATUS status { get; set; }
        /// <summary>
        /// 贸易类型(单选取字典)
        /// </summary>
        public CSP_TRADETYPE tradeType { get; set; }
        /// <summary>
        /// 贸易条款(单选取字典)
        /// </summary>
        public int incotermsId { get; set; }
        /// <summary>
        /// 贸易条款
        /// </summary>
        public string incotermsString { get; set; }
        /// <summary>
        /// FBA运输方式
        /// </summary>
        public int? fbaFreightMethodId { get; set; }
        /// <summary>
        /// FBA运输方式
        /// </summary>
        public string fbaFreightMethodString { get; set; }
        /// <summary>
        /// 运输类型（门到门港 到 港等）
        /// </summary>
        public string freightTypeString { get; set; }
        /// <summary>
        /// 出发地是否需要拖车
        /// </summary>
        public bool originIsRequireTruck { get; set; }
        /// <summary>
        /// 是否报关
        /// </summary>
        public bool isDeclaration { get; set; }
        /// <summary>
        /// 是否保险
        /// </summary>
        public bool isInsurance { get; set; }
        /// <summary>
        /// 始发装载时间
        /// </summary>
        public DateTime? cargoReadyDate { get; set; }
        /// <summary>
        /// 货物详情
        /// </summary>
        public string cargoDetails { get; set; }
        /// <summary>
        /// 产品描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 箱型规格保存json字符串，如 [ {name:20GP,value:1},{name:40GP,value2} ]
        /// </summary>
        public string containerType { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int customerId { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string customerName { get; set; }
        /// <summary>
        /// 发货方公司客户Id
        /// </summary>
        public int shipperCustomerId { get; set; }
        /// <summary>
        /// 发货方名称
        /// </summary>
        public string shipperCustomerName { get; set; }
        /// <summary>
        /// 发货方地址
        /// </summary>
        public string shipperAddress { get; set; }
        /// <summary>
        /// 收获方公司客户Id
        /// </summary>
        public int? consigneeCustomerId { get; set; }
        /// <summary>
        /// 收货人名称
        /// </summary>
        public string consigneeCustomerName { get; set; }
        /// <summary>
        /// 收货人地址
        /// </summary>
        public string consigneeAddress { get; set; }
        /// <summary>
        /// 起运港ID
        /// </summary>
        public int? originPortId { get; set; }
        /// <summary>
        /// 起运港
        /// </summary>
        public string originPort { get; set; }
        /// <summary>
        /// 起运港地址
        /// </summary>
        public int? originAddressId { get; set; }
        /// <summary>
        /// 起运港地址
        /// </summary>
        public string originAddress { get; set; }
        /// <summary>
        /// 目的口岸Id
        /// </summary>
        public int? destinationPortId { get; set; }
        /// <summary>
        /// 目的口岸
        /// </summary>
        public string destinationPort { get; set; }
        /// <summary>
        /// 目的港地址
        /// </summary>
        public int? destinationAddressId { get; set; }
        /// <summary>
        /// 目的港地址
        /// </summary>
        public string destinationAddress { get; set; }
        /// <summary>
        /// 预计交货时间
        /// </summary>
        public DateTime? deliveryDate { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 件数单位
        /// </summary>
        public int? quantityUnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string quantityUnitString { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>
        public decimal weight { get; set; }
        /// <summary>
        /// 毛重单位
        /// </summary>
        public int? weightUnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string weightUnitString { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal volume { get; set; }
        /// <summary>
        /// 体积单位
        /// </summary>
        public int? volumeUnitId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string volumeUnitString { get; set; }
        /// <summary>
        /// 由业务员绑定询价Id
        /// </summary>
        public int? quoteEnquiryId { get; set; }
        /// <summary>
        /// 业务服务人员列表
        /// </summary>
        public List<ShipmentContactForCSPAPI> serviceUsers { get; set; }
        /// <summary>
        /// 是否移除
        /// </summary>
        public bool IsRemove { get; set; }
    }
    #endregion
}
