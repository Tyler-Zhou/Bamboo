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
    /// 业务保存类
    /// </summary>
    [Serializable]
    [KnownType(typeof(DryCargo))]
    [KnownType(typeof(ReeferCargo))]
    [KnownType(typeof(DangerousCargo))]
    [KnownType(typeof(AwkwardCargo))]
    public class BusinessSaveRequest : ICP.Framework.CommonLibrary.Common.SaveRequest 
    {
        #region ID
        [Required(CMessage = "ID",EMessage="ID")]
        public Guid ID;
        #endregion

        #region 业务号
        /// <summary>
        /// 编号
        /// </summary>
        [Required(CMessage = "编号",EMessage="No")]
        public string No;

        #endregion

        #region 状态
        /// <summary>
        /// 状态
        /// </summary>
        [Required(CMessage = "状态",EMessage="State")]
        public OIOrderState State;

        #endregion

        #region 操作公司
        /// <summary>
        /// 操作公司ID
        /// </summary>
        [Required(CMessage = "操作公司",EMessage="Company")]
        public Guid CompanyID;



        #endregion

        #region 业务类型
        /// <summary>
        /// 业务类型
        /// </summary>
        [Required(CMessage = "业务类型",EMessage="OperationType")]
        public FCMOperationType OperationType;

        #endregion

        #region 客户
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID;

        /// <summary>
        /// 客户描述
        /// </summary>
        public CustomerDescription CustomerDescription;

        #endregion

        #region 客户参考单号
        /// <summary>
        /// 客户参考单号
        /// </summary>
        public string CustomerNo;

        #endregion

        #region 贸易条款
        /// <summary>
        /// 贸易条款ID
        /// </summary>
        [Required(CMessage = "贸易条款",EMessage="TradeTerm")]
        public Guid TradeTermID;
        #endregion

        #region 揽货方式
        /// <summary>
        /// 揽货类型ID
        /// </summary>
        public Guid? SalesTypeID;

        #endregion

        #region 揽货人
        /// <summary>
        /// 揽货人ID
        /// </summary>
        public Guid? SalesID;

        #endregion

        #region 揽货人部门
        /// <summary>
        /// 揽货人部门ID
        /// </summary>
        [GuidRequired(CMessage = "业务所属部门",EMessage="SalesDepartment")]
        public Guid SalesDepartmentID;
        #endregion

        #region 委托方式
        /// <summary>
        /// 委托方式ID
        /// </summary>
        public FCMBookingMode BookingMode;
        #endregion

        #region 委托日期
        /// <summary>
        /// 委托日期
        /// </summary>
        public DateTime BookingDate;
        #endregion

        #region 付款方式
        /// <summary>
        /// 付款方式ID
        /// </summary>
        public Guid? PaymentTermID;
        #endregion

        #region 运输条款
        /// <summary>
        /// 运输条款ID
        /// </summary>
        public Guid? TransportClauseID;
        #endregion

        #region 文件
        /// <summary>
        /// 文件
        /// </summary>
        public Guid? FilerId;
        #endregion

        #region 客服
        /// <summary>
        /// 客服ID
        /// </summary>
        public Guid? CustomerServiceID;
        #endregion

        #region 港前客服
        /// <summary>
        /// 港前客服ID
        /// </summary>
        public Guid? POLFilerID;
        /// <summary>
        /// 港前客服
        /// </summary>
        public string POLFilerName;

        #endregion

        #region 海外部客服
        /// <summary>
        /// 海外部客服ID
        /// </summary>
        public Guid? OverSeasFilerId;

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

        #region 代理号
        /// <summary>
        /// 代理参考号
        /// </summary>
        public string AgentNo;

        #endregion

        #region 发货人
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? ShipperID;
          /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescription ShipperDescription;
        #endregion
  
        #region 收货人
        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid? ConsigneeID;
        /// <summary>
        /// 收货人描述
        /// </summary>
        public CustomerDescription ConsigneeDescription;
        #endregion

        #region 通知人
        /// <summary>
        ///通知人ID
        /// </summary>
        public Guid? NotifyPartyID;

        /// <summary>
        /// 通知人描述
        /// </summary>
        public CustomerDescription NotifyPartyDescription;

        #endregion

        #region 收货地
        /// <summary>
        /// 收货地
        /// </summary>
        public Guid? PlaceOfReceiptID;


        #endregion

        #region 装货港
        /// <summary>
        /// 装货港ID
        /// </summary>
        public Guid? POLID;

        #endregion

        #region 卸货港
        /// <summary>
        /// 卸货港ID
        /// </summary>
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
        public Guid? FinalDestinationID;
        #endregion

        #region ETA、ETD
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime? ETD;

        /// <summary>
        /// ETA
        /// </summary>
        public DateTime? ETA;

        #endregion

        #region DETA
        /// <summary>
        /// D.ETA
        /// </summary>
        public DateTime? DETA;

        #endregion

        #region FETA
        /// <summary>
        ///  F.ETA 
        /// </summary>
        public DateTime? FETA;
        #endregion

        #region 品名
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity;

        #endregion

        #region 数量、重量、体积
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity;
        /// <summary>
        /// 数量单位ID
        /// </summary>
        public Guid QuantityUnitID;

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight;
        /// <summary>
        /// 重量单位ID
        /// </summary>
        public Guid WeightUnitID;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement;
        /// <summary>
        /// 体积单位ID
        /// </summary>
        public Guid MeasurementUnitID;
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

        #region 仓储
        /// <summary>
        /// 是否仓库
        /// </summary>
        public bool IsWareHouse;
        #endregion

        #region 拖车
        /// <summary>
        /// 是否拖车
        /// </summary>
        public bool IsTruck;

        #endregion

        #region 报关
        /// <summary>
        /// 是否报关
        /// </summary>
        public bool IsCustoms;
        #endregion

        #region 商检
        /// <summary>
        /// 是否需要商检
        /// </summary>
        public bool IsCommodityInspection;

        #endregion

        #region 质检
        /// <summary>
        /// 是否需要质检
        /// </summary>
        public bool IsQuarantineInspection;
        #endregion

        #region 电放
        /// <summary>
        /// 是否电放
        /// </summary>
        public bool IsTelex;
        #endregion

        #region 放货通知书
        /// <summary>
        /// 是否需要放货通知书
        /// </summary>
        public bool IsReleaseNotify;
        #endregion

        #region 转运
        /// <summary>
        /// 是否转运
        /// </summary>
        public bool IsTransport;

        #endregion

        #region 仓库信息
        /// <summary>
        /// 仓库ID
        /// </summary>
        public Guid? WareHouseID;
        /// <summary>
        /// 仓库描述
        /// </summary>
        public CustomerDescription WareHouseDescription;
        #endregion

        #region 报关信息
        /// <summary>
        /// 报关行ID
        /// </summary>
        public Guid? CustomsID;
        /// <summary>
        /// 报关行描述
        /// </summary>
        public CustomerDescription CustomsDescription;
        #endregion

        #region 清关
        /// <summary>
        /// 是否清关
        /// </summary>
        public bool IsClearance;

        #endregion

        #region 清关日期
        /// <summary>
        /// 清关时间
        /// </summary>
        public DateTime? ClearanceDate;

        #endregion

        #region 备注
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;

        #endregion

        #region 主提单号
        /// <summary>
        /// MBLNo
        /// </summary>
        public string MBLNo;

        #endregion

        #region 分提单号
        /// <summary>
        /// HBLNo
        /// </summary>
        public string HBLNo;
        #endregion

        #region 船公司
        /// <summary>
        /// 船公司
        /// </summary>
        public Guid? CarrierID;
        /// <summary>
        /// 船公司描述
        /// </summary>
        public CustomerDescription CarrierDescription;
        #endregion

        #region 承运人
        Guid? _agentofcarrierid;
        /// <summary>
        /// 承运人ID
        /// </summary>
        public Guid? AgentOfCarrierID;
        /// <summary>
        /// 承运人描述
        /// </summary>
        public CustomerDescription AgentOfCarrierDescription;
        #endregion

        #region 船名、航次
        /// <summary>
        /// 船名ID
        /// </summary>
        public Guid VesselID;
        /// <summary>
        /// 航次
        /// </summary>
        public string Voyage;

        #endregion

        #region 提货地
        /// <summary>
        /// 提货地
        /// </summary>
        public Guid? FinalWareHouseID;
        /// <summary>
        /// 提货地描述
        /// </summary>
        public CustomerDescription FinalWareHouseDescription;
        #endregion

        #region 还柜地
        /// <summary>
        /// 还柜地ID
        /// </summary>
        public Guid? ReturnLocationID;
        /// <summary>
        /// 还柜地描述
        /// </summary>
        public CustomerDescription ReturnLocationDescription;
        #endregion

        #region 转关
        /// <summary>
        /// 转关号
        /// </summary>
        public string ITNO;
        /// <summary>
        /// 转关时间
        /// </summary>
        public DateTime? ITDate;
        /// <summary>
        /// 转关地点
        /// </summary>
        public string ITPalce;
        #endregion

        #region 放货类型

        /// <summary>
        /// 放货类型
        /// </summary>
        public FCMReleaseType ReleaseType;

        #endregion

        #region 放货时间

        /// <summary>
        /// 放货时间
        /// </summary>
        public DateTime? ReleaseDate;

        #endregion

        #region MBL运输方式
        /// <summary>
        /// MBL运输方式ID
        /// </summary>
        public Guid? MBLTransportClauseID;
        #endregion

        #region 保存日期
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? Updatedate;

        #endregion 

        #region 保存人
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid saveByID;
        #endregion

        #region MBLID
        /// <summary>
        /// MBLID
        /// </summary>
        public Guid MBLID;
        #endregion

        #region 是否英文环境 
        /// <summary>
        /// 是否英文环境 
        /// </summary>
        public bool IsEnglish;
        #endregion

    }
}
