using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Runtime.Serialization;

namespace ICP.FCM.OceanExport.ServiceInterface.CompositeObjects
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
        public Guid? id;
        /// <summary>
        /// 客户参考号
        /// </summary>
        public string customerRefNo;
        /// <summary>
        /// 客户
        /// </summary>
        public Guid customerID;
        /// <summary>
        /// 贸易条款
        /// </summary>
        public Guid tradeTermID;
        /// <summary>
        /// 订舱类型
        /// </summary>
        public FCMOperationType oeOperationType;
        /// <summary>
        /// 口岸公司
        /// </summary>
        public Guid companyID;
        /// <summary>
        /// 订舱
        /// </summary>
        public Guid? bookingerID;
        /// <summary>
        /// 文件
        /// </summary>
        public Guid? filerID;
        /// <summary>
        /// 海外部客服
        /// </summary>
        public Guid? overSeasFilerId;
        /// <summary>
        /// 业务部门
        /// </summary>
        public Guid salesDepartmentID;
        /// <summary>
        /// 业务员
        /// </summary>
        public Guid? salesID;
        /// <summary>
        /// 揽货方式
        /// </summary>
        public Guid salesTypeID;
        /// <summary>
        /// 委托方式
        /// </summary>
        public FCMBookingMode bookingMode;
        /// <summary>
        /// 委托时间
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
        /// 代理
        /// </summary>
        public Guid? agentID;
        /// <summary>
        /// 代理描述
        /// </summary>
        public CustomerDescription agentDescription;
        /// <summary>
        /// 船东
        /// </summary>
        public Guid? carrierID;
        /// <summary>
        /// 承运人
        /// </summary>
        public Guid agentOfCarrierID;
        /// <summary>
        /// 是否有合约
        /// </summary>
        public bool isContract;
        /// <summary>
        /// 合约
        /// </summary>
        public Guid? freightRateID;
        /// <summary>
        /// 订舱
        /// </summary>
        public Guid? oceanShippingOrderID;
        /// <summary>
        /// 订舱号
        /// </summary>
        public string oceanShippingOrderNo;
        /// <summary>
        /// 订舱确认时间
        /// </summary>
        public DateTime? soDate;
        /// <summary>
        /// 估计交货时间
        /// </summary>
        public DateTime? estimatedDeliveryDate;
        /// <summary>
        /// 实际交货时间
        /// </summary>
        public DateTime? actualDeliveryDate;
        /// <summary>
        /// 期望出运时间
        /// </summary>
        public DateTime? expectedShipDate;
        /// <summary>
        /// 期望到达时间
        /// </summary>
        public DateTime? expectedArriveDate;
        /// <summary>
        /// 关单时间
        /// </summary>
        public DateTime? closingDate;
        /// <summary>
        /// 客户付款方式
        /// </summary>
        public Guid? paymentTermID;
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid transportClauseID;
        /// <summary>
        /// 航线
        /// </summary>
        public Guid? shippingLineID;
        /// <summary>
        /// 驳船船名航次
        /// </summary>
        public Guid? preVoyageID;
        /// <summary>
        /// 大船船名航次
        /// </summary>
        public Guid? voyageID;
        /// <summary>
        /// 货物品名
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
        /// 箱描述
        /// </summary>
        public ContainerDescription containerDescription;
        /// <summary>
        /// MBL付款方式
        /// </summary>
        public Guid? mblPaymentTermID;
        /// <summary>
        /// HBL付款方式
        /// </summary>
        public Guid? hblPaymentTermID;
        /// <summary>
        /// 是否需要派车
        /// </summary>
        public bool isTruck;
        /// <summary>
        /// 是否需要报关
        /// </summary>
        public bool isCustoms;
        /// <summary>
        /// 是否需要商检
        /// </summary>
        public bool isCommodityInspection;
        /// <summary>
        /// 是否需要质检
        /// </summary>
        public bool isQuarantineInspection;
        /// <summary>
        /// 是否需要仓储
        /// </summary>
        public bool isWarehouse;
        /// <summary>
        /// 是否只出MBL
        /// </summary>
        public bool isOnlyMBL;
        /// <summary>
        /// MBL放单类型
        /// </summary>
        public FCMReleaseType? mblReleaseType;
        /// <summary>
        /// HBL放单类型
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
        /// 最终目的地
        /// </summary>
        public Guid? finalDestinationID;
        /// <summary>
        /// 还柜地点
        /// </summary>
        public Guid? returnLocationID;
        /// <summary>
        /// 仓库
        /// </summary>
        public Guid? warehouseID;
        /// <summary>
        /// 截仓日
        /// </summary>
        public DateTime? closingWarehousedate;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
        /// <summary>
        /// shippingOrder表更新时间
        /// </summary>
        public DateTime? oceanShippingOrderUpdateDate;
        /// <summary>
        /// 更新时间-做数据版本用
        /// </summary>
        public DateTime? oceanOrderUpdateDate;

        /// <summary>
        /// 是否需要生成账单
        /// </summary>
        public bool IsCreateBill;
        public DateTime? PreETD;
        public DateTime? ETD;
        public DateTime? ETA;
        public DateTime? ClosingDate;
        public DateTime? CYClosingDate;
        public DateTime? DOCClosingDate;

    }
}
