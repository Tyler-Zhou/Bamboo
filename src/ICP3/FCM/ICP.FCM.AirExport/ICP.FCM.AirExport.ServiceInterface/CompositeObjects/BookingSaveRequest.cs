using System;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Runtime.Serialization;

namespace ICP.FCM.AirExport.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 用于保存订舱单的对象
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public class BookingSaveRequest : SaveRequest
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid? id { get; set; }
        /// <summary>
        /// 客户参考号
        /// </summary>
        public string customerRefNo { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public Guid customerID { get; set; }
        /// <summary>
        /// 贸易条款
        /// </summary>
        public Guid tradeTermID { get; set; }

        /// <summary>
        /// 口岸公司
        /// </summary>
        public Guid companyID { get; set; }
        /// <summary>
        /// 订舱
        /// </summary>
        public Guid? bookingerID { get; set; }
        /// <summary>
        /// 文件
        /// </summary>
        public Guid? filerID { get; set; }
        /// <summary>
        /// 海外部客服
        /// </summary>
        public Guid? overSeasFilerId { get; set; }
        /// <summary>
        /// 业务部门
        /// </summary>
        public Guid salesDepartmentID { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        public Guid? salesID { get; set; }
        /// <summary>
        /// 揽货方式
        /// </summary>
        public Guid salesTypeID { get; set; }
        /// <summary>
        /// 委托方式
        /// </summary>
        public FCMBookingMode bookingMode { get; set; }
        /// <summary>
        /// 委托时间
        /// </summary>
        public DateTime bookingDate { get; set; }
        /// <summary>
        /// 订舱客户
        /// </summary>
        public Guid? bookingCustomerID { get; set; }
        /// <summary>
        /// 订舱客户描述
        /// </summary>
        public CustomerDescription bookingCustomerDescription { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? shipperID { get; set; }
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription shipperDescription { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public Guid? consigneeID { get; set; }
        /// <summary>
        /// 收货人描述
        /// </summary>
        public CustomerDescription consigneeDescription { get; set; }
        /// <summary>
        /// 收货地
        /// </summary>
        public Guid? placeOfReceiptID { get; set; }
        /// <summary>
        /// 装货港
        /// </summary>
        public Guid polID { get; set; }
        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid podID { get; set; }

        public DateTime? ETD { get; set; }

        public DateTime? ETA { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid placeOfDeliveryID { get; set; }
        /// <summary>
        /// 代理
        /// </summary>
        public Guid? agentID { get; set; }
        /// <summary>
        /// 代理描述
        /// </summary>
        public CustomerDescription agentDescription { get; set; }
        /// <summary>
        /// 船东
        /// </summary>
        public Guid carrierID { get; set; }
        /// <summary>
        /// 承运人
        /// </summary>
        public Guid? agentOfCarrierID { get; set; }
        /// <summary>
        /// 是否有合约
        /// </summary>
        public bool isContract { get; set; }
        /// <summary>
        /// 合约
        /// </summary>
        public Guid? freightRateID { get; set; }
        /// <summary>
        /// 订舱
        /// </summary>
        public Guid? oceanShippingOrderID { get; set; }
        /// <summary>
        /// 订舱号
        /// </summary>
        public string oceanShippingOrderNo { get; set; }
        /// <summary>
        /// 订舱确认时间
        /// </summary>
        public DateTime? soDate { get; set; }
        /// <summary>
        /// 估计交货时间
        /// </summary>
        public DateTime? estimatedDeliveryDate { get; set; }
        /// <summary>
        /// 实际交货时间
        /// </summary>
        public DateTime? actualDeliveryDate { get; set; }
        /// <summary>
        /// 期望出运时间
        /// </summary>
        public DateTime? expectedShipDate { get; set; }
        /// <summary>
        /// 期望到达时间
        /// </summary>
        public DateTime? expectedArriveDate { get; set; }

        public DateTime? dOCClosingDate { get; set; }
        /// <summary>
        /// 关单时间
        /// </summary>
        public DateTime? closingDate { get; set; }
        /// <summary>
        /// 客户付款方式
        /// </summary>
        public Guid? paymentTermID { get; set; }
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid transportClauseID { get; set; }
        /// <summary>
        /// 航线
        /// </summary>
        public Guid? shippingLineID { get; set; }
        /// <summary>
        /// 驳船船名航次
        /// </summary>
        public Guid? preVoyageID { get; set; }
        /// <summary>
        /// 大船船名航次
        /// </summary>
        public Guid? voyageID { get; set; }
        /// <summary>
        /// 货物品名
        /// </summary>
        public string commodity { get; set; }
        /// <summary>
        /// 货物数量
        /// </summary>
        public int quantity { get; set; }
        /// <summary>
        /// 货物数量单位
        /// </summary>
        public Guid? quantityUnitID { get; set; }
        /// <summary>
        /// 货物重量
        /// </summary>
        public decimal weight { get; set; }
        /// <summary>
        /// 货物重量单位
        /// </summary>
        public Guid? weightUnitID { get; set; }
        /// <summary>
        /// 货物体积
        /// </summary>
        public decimal measurement { get; set; }
        /// <summary>
        /// 货物体积单位
        /// </summary>
        public Guid? measurementUnitID { get; set; }
        /// <summary>
        /// 货物描述
        /// </summary>
        public CargoDescription cargoDescription { get; set; }
        /// <summary>
        /// 箱描述
        /// </summary>
        public ContainerDescription containerDescription { get; set; }
        /// <summary>
        /// MBL付款方式
        /// </summary>
        public Guid? mblPaymentTermID { get; set; }
        /// <summary>
        /// HBL付款方式
        /// </summary>
        public Guid? hblPaymentTermID { get; set; }
        /// <summary>
        /// 是否需要派车
        /// </summary>
        public bool isTruck { get; set; }
        /// <summary>
        /// 是否需要报关
        /// </summary>
        public bool isCustoms { get; set; }
        /// <summary>
        /// 是否需要商检
        /// </summary>
        public bool isCommodityInspection { get; set; }
        /// <summary>
        /// 是否需要质检
        /// </summary>
        public bool isQuarantineInspection { get; set; }
        /// <summary>
        /// 是否需要仓储
        /// </summary>
        public bool isWarehouse { get; set; }
        /// <summary>
        /// 是否只出MBL
        /// </summary>
        public bool isOnlyMBL { get; set; }
        /// <summary>
        /// MBL放单类型
        /// </summary>
        public FCMReleaseType? mblReleaseType { get; set; }
        /// <summary>
        /// HBL放单类型
        /// </summary>
        public FCMReleaseType? hblReleaseType { get; set; }
        /// <summary>
        /// MBL要求
        /// </summary>
        public string mblRequirements { get; set; }
        /// <summary>
        /// HBL要求
        /// </summary>
        public string hblRequirements { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 最终目的地
        /// </summary>
        public Guid? finalDestinationID { get; set; }
        /// <summary>
        /// 还柜地点
        /// </summary>
        public Guid? returnLocationID { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        public Guid? warehouseID { get; set; }
        /// <summary>
        /// 截仓日
        /// </summary>
        public DateTime? closingWarehousedate { get; set; }
        
        /// <summary>
        /// shippingOrder表更新时间
        /// </summary>
        public DateTime? airShippingOrderUpdateDate { get; set; }
        /// <summary>
        /// shippingOrderID
        /// </summary>
        public Guid? airWayBillID { get; set; }
        
        /// <summary>
        /// 航空公司ID
        /// </summary>
        public Guid? AirlineID { get; set; }
        /// <summary>
        /// 航班ID
        /// </summary>
        public Guid? FlightID { get; set; }
        /// <summary>
        /// 是否同步到CSP
        /// </summary>
        public bool IsSyncCSP { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID { get; set; }
        /// <summary>
        /// 更新时间-做数据版本用
        /// </summary>
        public DateTime? oceanOrderUpdateDate { get; set; }
    }
}
