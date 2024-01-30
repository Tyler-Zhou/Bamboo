using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;
using System.Runtime.Serialization;

namespace ICP.FCM.OceanExport.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 传输保存数据的订单实体
    /// </summary>
    [Serializable]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public class OrderSaveRequest : SaveRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? id;
        /// <summary>
        /// 业务号
        /// </summary>
        public string refNo;
        /// <summary>
        /// 口岸公司
        /// </summary>
        public Guid companyID;
        /// <summary>
        /// 订舱类型
        /// </summary>
        public FCMOperationType oeOperationType;
        /// <summary>
        /// 客户
        /// </summary>
        public Guid customerID;
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid tradeTermID;
        /// <summary>
        /// 揽货方式
        /// </summary>
        public Guid salesTypeID;
        /// <summary>
        /// 业务员
        /// </summary>
        public Guid? salesID;
        /// <summary>
        /// 业务部门
        /// </summary>
        public Guid? salesDepartmentID;
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid transportClauseID;
        /// <summary>
        /// id
        /// </summary>
        public Guid? paymentTermID;
        /// <summary>
        /// 海外部客服
        /// </summary>
        public Guid? overSeasFilerId;
        /// <summary>
        /// 操作(客服)
        /// </summary>
        public Guid? bookingerID;

        /// <summary>
        /// 订舱
        /// </summary>
        public Guid? bookingByID;

        /// <summary>
        /// 委托方式/订舱方式
        /// </summary>
        public FCMBookingMode bookingMode;
        /// <summary>
        /// 委托日期
        /// </summary>
        public DateTime bookingDate;
        /// <summary>
        /// 订舱客户
        /// </summary>
        public Guid bookingCustomerID;
        /// <summary>
        /// 订舱客户描述
        /// </summary>
        public CustomerDescription bookingCustomerDescription;
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? shipperID;
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription shipperDescription;
        /// <summary>
        /// 收货人
        /// </summary>
        public Guid? consigneeID;
        /// <summary>
        /// 收货人描述
        /// </summary>
        public CustomerDescription consigneeDescription;
        /// <summary>
        /// 收货地
        /// </summary>
        public Guid? placeOfReceiptID;
        /// <summary>
        /// 装货港
        /// </summary>
        public Guid polID;
        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid podID;
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid placeOfDeliveryID;
        /// <summary>
        /// 最终目的地
        /// </summary>
        public Guid? finalDestinationID;
        /// <summary>
        /// 品名
        /// </summary>
        public string commodity;
        /// <summary>
        /// 货物数量
        /// </summary>
        public int quantity;
        /// <summary>
        /// 货物数量单位
        /// </summary>
        public Guid? quantityUnitID;
        /// <summary>
        /// 货物重量
        /// </summary>
        public decimal weight;
        /// <summary>
        /// 货物重量单位
        /// </summary>
        public Guid? weightUnitID;
        /// <summary>
        /// 货物体积
        /// </summary>
        public decimal measurement;
        /// <summary>
        /// 货物体积单位
        /// </summary>
        public Guid? measurementUnitID;
        /// <summary>
        /// 货物描述
        /// </summary>
        public CargoDescription cargoDescription;
        /// <summary>
        /// 6箱描述
        /// </summary>
        public ContainerDescription containerDescription;
        /// <summary>
        /// 承运人
        /// </summary>
        public Guid? carrierID;
        /// <summary>
        /// 截关时间
        /// </summary>
        public DateTime? closingDate;
        /// <summary>
        /// 估计交货
        /// </summary>
        public DateTime? estimatedDeliveryDate;
        /// <summary>
        /// 期望出运时间
        /// </summary>
        public DateTime? expectedShipDate;
        /// <summary>
        /// 期望到达时间
        /// </summary>
        public DateTime? expectedArriveDate;
        /// <summary>
        /// 是否派车
        /// </summary>
        public bool isTruck;
        /// <summary>
        /// 是否报关
        /// </summary>
        public bool isCustoms;
        /// <summary>
        /// 是否商检
        /// </summary>
        public bool isCommodityInspection;
        /// <summary>
        /// 是否质检
        /// </summary>
        public bool isQuarantineInspection;
        /// <summary>
        /// 是否仓储
        /// </summary>
        public bool isWarehouse;
        /// <summary>
        /// 是否只出MBL
        /// </summary>
        public bool isOnlyMBL;
        /// <summary>
        /// MBL放单类型
        /// </summary>
        public Guid? mblpaymentTermID;
        /// <summary>
        /// HBL放单类型
        /// </summary>
        public Guid? hblpaymentTermID;
        /// <summary>
        /// MBL放单方式
        /// </summary>
        public FCMReleaseType? mblReleaseType;
        /// <summary>
        /// HBL放单方式
        /// </summary>
        public FCMReleaseType? hblReleaseType;
        /// <summary>
        /// MBL要求
        /// </summary>
        public string mblRequirements;
        /// <summary>
        /// HBL要求
        /// </summary>
        public string hblRequirements;
        /// <summary>
        /// 备注
        /// </summary>
        public string remark;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? updateDate;

        /// <summary>
        /// 询价ID
        /// </summary>
        public Guid InquirePriceId { get; set; }

        /// <summary>
        /// 商务确认人ID
        /// </summary>
        public Guid ConfirmedById { get; set; }


        /// <summary>
        /// 运费已包含
        /// </summary>
        public string FreightIncluded { get; set; }
        /// <summary>
        /// 报价ID
        /// </summary>
        public Guid? QuotedPriceID { get; set; }
    }
}
