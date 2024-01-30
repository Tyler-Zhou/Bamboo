using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.Framework.CommonLibrary.Common; 
using ICP.FCM.Common.ServiceInterface.DataObjects;
using System.Runtime.Serialization;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// 保存订单的时候传递的实体
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public class OrderSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest
    {
        #region ID(唯一键)
        /// <summary>
        /// 唯一键
        /// </summary>
        public Guid ID;

        #endregion

        #region 业务号
        /// <summary>
        /// 业务号
        /// </summary>
        public string RefNo;
        #endregion

        #region 状态

        /// <summary>
        /// 状态
        /// </summary>
        public OIOrderState State;

        #endregion
     
        #region 业务类型

        /// <summary>
        /// 业务类型
        /// </summary>
        public FCMOperationType OIOperationType;

        #endregion

        #region 客户
        /// <summary>
        /// 客户ID
        /// </summary>
        [GuidRequired(CMessage = "客户",EMessage="Customer")]
        public Guid CustomerID;

        /// <summary>
        /// 客户描述
        /// </summary>
        public CustomerDescription CustomerDescription;

        #endregion

        #region 贸易条款

        /// <summary>
        /// 贸易条款ID
        /// </summary>
        [GuidRequired(CMessage = "贸易条款",EMessage="TradeTerm")]
        public Guid TradeTermID;


        #endregion

        #region 揽货类型
        /// <summary>
        /// 揽货类型ID
        /// </summary>
        [GuidRequired(CMessage = "揽货类型",EMessage="SalesType")]
        public Guid SalesTypeID;

        #endregion

        #region 揽货人

        /// <summary>
        /// 业务员ID
        /// </summary>
        [GuidRequired(CMessage = "业务员",EMessage="Sales")]
        public Guid? SalesID;
        #endregion

        #region 揽货部门
        /// <summary>
        /// 揽货部门ID
        /// </summary>
        [GuidRequired(CMessage = "揽货部门",EMessage="SalesDepartMent")]
        public Guid? SalesDepartmentID;


        #endregion

        #region 运输条款
        /// <summary>
        /// 运输条款ID
        /// </summary>
        [GuidRequired(CMessage = "运输条款",EMessage="TransportClause")]
        public Guid TransportClauseID;

        #endregion

        #region 付款方式
        /// <summary>
        /// 付款方式ID
        /// </summary>
        public Guid? PaymentTermID;

        #endregion

        #region 委托方式

        /// <summary>
        /// 委托方式/订舱方式（0电话、1邮件、2电子订舱）
        /// </summary>
        [Required(CMessage = "委托方式",EMessage="BookingMode")]
        public FCMBookingMode BookingMode;
        #endregion

        #region 委托日期
        /// <summary>
        /// 委托日期
        /// </summary>
        [Required(CMessage = "委托日期",EMessage="BookingDate")]
        public DateTime BookingDate;

        #endregion

        #region 创建日

        /// <summary>
        /// 创建日
        /// </summary>
        public DateTime CreateDate;

        #endregion

        #region 到交货地日
        /// <summary>
        /// 到交货地日
        /// </summary>
        public DateTime? ArriveDate;

        #endregion

        #region 放货日
         /// <summary>
        /// 放货日
        /// </summary>
        public DateTime? DeliveryOfGoodsDate;

        #endregion

        #region 离港日
        /// <summary>
        /// 离港日
        /// </summary>
        public DateTime? ETD;
        #endregion

        #region 到港日
        /// <summary>
        /// 到港日
        /// </summary>
        public DateTime? ETA;

        #endregion

        #region 海外部客服

        /// <summary>
        /// 海外部客服ID
        /// </summary>
        public Guid? OverSeasFilerId;

        #endregion

        #region 客服

        /// <summary>
        /// 客服ID
        /// </summary>
        public Guid? FilerID;
        #endregion

        #region 是否有效
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid;

        #endregion

        #region 更新时间-做数据版本控制用
        /// <summary>
        /// 更新时间-做数据版本控制用
        /// </summary>
        public DateTime? UpdateDate;
        #endregion

        #region 建立人
        /// <summary>
        /// 建立人ID
        /// </summary>
        public Guid CreateByID;

        #endregion

        #region 操作口岸

        /// <summary>
        /// 操作口岸ID
        /// </summary>
        [GuidRequired(CMessage = "操作口岸",EMessage="Company")]
        public Guid CompanyID;

        #endregion

        #region 代理

        /// <summary>
        /// 代理ID
        /// </summary>
        public Guid? AgentID;
        /// <summary>
        /// 代理详细信息
        /// </summary>
        public CustomerDescription AgentDescription;

        #endregion

        #region 船东
        /// <summary>
        /// 船公司ID
        /// </summary>
        public Guid? CarrierID;

        #endregion

        #region 发货人

        /// <summary>
        /// 发货人ID
        /// </summary>
        [GuidRequired(CMessage = "发货人",EMessage="Shipper")]
        public Guid? ShipperID;


        CustomerDescription _shipperdescription;
        /// <summary>
        /// 发货人详细信息
        /// </summary>
        public CustomerDescription ShipperDescription;

        #endregion

        #region 收货人

        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid? ConsigneeID;

        /// <summary>
        /// 收货人详细信息
        /// </summary>
        public CustomerDescription ConsigneeDescription;

        #endregion

        #region 收货地

        /// <summary>
        /// 收货地ID
        /// </summary>
        public Guid? PlaceOfReceiptID;

        #endregion

        #region 装货港
        /// <summary>
        /// 装货港ID
        /// </summary>
        [GuidRequired(CMessage = "装货港",EMessage="POL")]
        public Guid? POLID;

        #endregion

        #region 卸货港

        /// <summary>
        /// 卸货港ID
        /// </summary>
        [GuidRequired(CMessage = "卸货港",EMessage="POD")]
        public Guid? PODID;

        #endregion

        #region 交货地
        /// <summary>
        /// 交货地ID
        /// </summary>
        public Guid? PlaceOfDeliveryID;

        #endregion

        #region 最终目的地
        /// <summary>
        /// 最终目的地ID
        /// </summary>
        [GuidRequired(CMessage = "最终目的",EMessage="FinalDestination")]
        public Guid? FinalDestinationId;


        #endregion

        #region 期望出运日

        /// <summary>
        /// 期望出运日
        /// </summary>
        public DateTime? ExpectedShipDate;

        #endregion

        #region 期望到达

        /// <summary>
        /// 期望到达时间
        /// </summary>
        public DateTime? ExpectedArriveDate;

        #endregion

        #region 放货类型

        /// <summary>
        /// House B/L放货类型（0正本，1电放）
        /// </summary>
        public FCMReleaseType? HBLReleaseType;

        #endregion

        #region 是否仓储

        /// <summary>
        /// 是否仓储
        /// </summary>
        public bool IsWarehouse;

        #endregion

        #region 是否拖车

        /// <summary>
        /// 是否拖车
        /// </summary>
        public bool IsTruck;

        #endregion

        #region 是否报关

        /// <summary>
        /// 是否报关
        /// </summary>
        public bool IsCustoms;

        #endregion

        #region 是否商检

        /// <summary>
        /// 是否商检
        /// </summary>
        public bool IsCommodityInspection;

        #endregion

        #region 质检

        /// <summary>
        /// 质检(是否动植检)
        /// </summary>
        public bool IsQuarantineInspection;

        #endregion

        #region 品名
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity;
        #endregion

        #region 数量

        /// <summary>
        /// 数量
        /// </summary>
        [Required(CMessage = "包装数量",EMessage="Quantity")]
        public int Quantity;

        #endregion

        #region 数量单位
        /// <summary>
        /// 数量单位ID
        /// </summary>
        public Guid? QuantityUnitID;
        #endregion

        #region 重量

    
        /// <summary>
        /// 重量
        /// </summary>
        [Required(CMessage = "重量",EMessage="Weight")]
        public decimal Weight;

        #endregion

        #region 重量单位
        /// <summary>
        /// 重量单位ID
        /// </summary>
        public Guid? WeightUnitID;

        #endregion

        #region 体积

        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement;
        #endregion

        #region 体积单位

        /// <summary>
        /// 体积单位ID
        /// </summary>
        public Guid? MeasurementUnitID;

        #endregion

        #region 货物类型

        /// <summary>
        /// 货物类型
        /// </summary>
        public CargoType? CargoType;

        #endregion

        #region 货物描述
        /// <summary>
        /// 货物描述
        /// </summary>
        public CargoDescription CargoDescription;

        #endregion

        #region 箱信息

        /// <summary>
        /// 箱信息(集装箱描述)
        /// </summary>
        public ContainerDescription ContainerDescription;

        #endregion

        #region 备注
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;
        #endregion

        #region 保存人ID

        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid SaveByID;

        #endregion

        #region 是否英文环境
        /// <summary>
        /// 是否英文环境
        /// </summary>
        public bool IsEnglish;
        #endregion

    }
}
