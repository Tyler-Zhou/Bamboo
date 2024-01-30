#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/6/3 星期日 22:22:20
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion



namespace ICP.FCM.OceanExport.ServiceInterface
{
    using Framework.CommonLibrary.Common;
    using ICP.Common.ServiceInterface.DataObjects;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.FCM.OceanExport.ServiceInterface.DataObjects;
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// 用于保存提单的对象
    /// </summary>
    [Serializable]
    public class BLSaveRequest : SaveRequest
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid oceanBookingID;

        /// <summary>
        /// 主提单ID
        /// </summary>
        public Guid MBLID;

        /// <summary>
        /// 分提单ID
        /// </summary>
        public Guid?[] BLIDs;
        /// <summary>
        /// 分提单号
        /// </summary>
        public string[] BLNos;
        /// <summary>
        /// 唛头
        /// </summary>
        public string[] Markss;
        /// <summary>
        /// 品名描述
        /// </summary>
        public string[] GoodsDescriptions;
        /// <summary>
        /// 包装单位ID
        /// </summary>
        public Guid?[] QuantityUnitIDs;
        /// <summary>
        /// 毛重单位ID
        /// </summary>
        public Guid?[] WeightUnitIDs;
        /// <summary>
        /// 体积单位ID
        /// </summary>
        public Guid?[] MeasurementUnitIDs;
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid saveByID;
        /// <summary>
        /// 更新时间-做数据版本用
        /// </summary>
        public DateTime?[] containerUpdateDates;
    }

    #region 保存MBL的参数
    /// <summary>
    /// 保存MBL的参数
    /// </summary>
    [KnownType(typeof(CustomerDescription))]
    [Serializable]
    public class SaveMBLInfoParameter
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 中海进箱日期
        /// </summary>
        public DateTime? CSCLGateIn { get; set; }
        /// <summary>
        /// 订舱
        /// </summary>
        public Guid OceanBookingID { get; set; }
        /// <summary>
        /// MBL号
        /// </summary>
        public string MBLNo { get; set; }
        /// <summary>
        /// 提单份数
        /// </summary>
        public short? NumberOfOriginal { get; set; }
        /// <summary>
        /// 航次显示类型
        /// </summary>
        public VoyageShowType VoyageShowType { get; set; }
        /// <summary>
        /// 对单人
        /// </summary>
        public Guid? CheckerID { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        public Guid? ShipperID { get; set; }
        /// <summary>
        /// 发货人描述
        /// </summary>
        public CustomerDescriptionForNew ShipperDescription { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public Guid? ConsigneeID { get; set; }
        /// <summary>
        /// 收货人描述
        /// </summary>
        public CustomerDescriptionForNew ConsigneeDescription { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public Guid? NotifyPartyID { get; set; }
        /// <summary>
        /// 通知人描述
        /// </summary>
        public CustomerDescriptionForNew NotifyPartyDescription { get; set; }

        /// <summary>
        /// 通知人2
        /// </summary>
        public string NotifyParty2 { get; set; }

        /// <summary>
        /// 代理
        /// </summary>
        public Guid? AgentID { get; set; }
        /// <summary>
        /// 代理描述
        /// </summary>
        public CustomerDescription AgentDescription { get; set; }
        /// <summary>
        /// 收货地
        /// </summary>
        public Guid? PlaceOfReceiptID { get; set; }
        /// <summary>
        /// 收货地名
        /// </summary>
        public string PlaceOfReceiptName { get; set; }
        /// <summary>
        /// 头程船名航次
        /// </summary>
        public Guid? PreVoyageID { get; set; }
        /// <summary>
        /// 船名航次
        /// </summary>
        public Guid? VoyageID { get; set; }
        /// <summary>
        /// 装货港
        /// </summary>
        public Guid POLID { get; set; }
        /// <summary>
        /// 装货港名
        /// </summary>
        public string POLName { get; set; }
        /// <summary>
        /// 卸货港
        /// </summary>
        public Guid PODID { get; set; }
        /// <summary>
        /// 卸货港名
        /// </summary>
        public string PODName { get; set; }
        /// <summary>
        /// 卸货港代码(宁波手动输入使用)
        /// </summary>
        public string NBPODCode { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public Guid? PlaceOfDeliveryID { get; set; }
        /// <summary>
        /// 交货地名
        /// </summary>
        public string PlaceOfDeliveryName { get; set; }
        /// <summary>
        /// 最终目的地
        /// </summary>
        public Guid? FinalDestinationID { get; set; }
        /// <summary>
        /// 最终目的地名
        /// </summary>
        public string FinalDestinationName { get; set; }
        /// <summary>
        /// 运输条款
        /// </summary>
        public Guid TransportClauseID { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public Guid? PaymentTermID { get; set; }
        /// <summary>
        /// 费用描述
        /// </summary>
        public string FreightDescription { get; set; }
        /// <summary>
        /// 不显示在提单备注
        /// </summary>
        public string NSITBLNotes { get; set; }
        /// <summary>
        /// 电放方式
        /// </summary>
        public FCMReleaseType ReleaseType { get; set; }
        /// <summary>
        /// 电放日期
        /// </summary>
        public DateTime? ReleaseDate { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 数量单位
        /// </summary>
        public Guid QuantityUnitID { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 重量单位
        /// </summary>
        public Guid WeightUnitID { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement { get; set; }
        /// <summary>
        /// 体积单位
        /// </summary>
        public Guid MeasurementUnitID { get; set; }
        /// <summary>
        /// 货物标示
        /// </summary>
        public string Marks { get; set; }
        /// <summary>
        /// 货物描述
        /// </summary>
        public string GoodsDescription { get; set; }
        /// <summary>
        /// 是否木质包装
        /// </summary>
        public bool IsWoodPacking { get; set; }
        /// <summary>
        /// 箱数或件数合计
        /// </summary>
        public string CTNQtyInfo { get; set; }
        /// <summary>
        /// 签发地
        /// </summary>
        public Guid IssuePlaceID { get; set; }
        /// <summary>
        /// 签发人
        /// </summary>
        public Guid IssueByID { get; set; }
        /// <summary>
        /// 签发日期
        /// </summary>
        public DateTime? IssueDate { get; set; }
        /// <summary>
        /// 木质包装的字符串
        /// </summary>
        public string WoodPacking { get; set; }
        /// <summary>
        /// 签单类型
        /// </summary>
        public IssueType IssueType { get; set; }
        /// <summary>
        /// 代理文本(用于打印)
        /// </summary>
        public string AgentText { get; set; }
        /// <summary>
        /// 合约ID
        /// </summary>
        public Guid? FreightRateID { get; set; }
        /// <summary>
        /// 头程估计离港时间(Estimated time of departure)
        /// </summary>
        public DateTime? PreETD;
        /// <summary>
        /// 估计离港时间(Estimated time of departure)
        /// </summary>
        public DateTime? ETD;
        /// <summary>
        /// 预计到达时间(Estimated Time of Arrival)
        /// </summary>
        public DateTime? ETA;
        /// <summary>
        /// 集装箱进堆场时间
        /// </summary>
        public DateTime? GateInDate;
        /// <summary>
        /// 截单时间
        /// </summary>
        public DateTime? ClosingDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CYClosingDate;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DOCClosingDate;
        /// <summary>
        /// 真实收货人
        /// </summary>
        public string ReallyConsignee
        {
            get;
            set;
        }
        /// <summary>
        /// 真实发货人
        /// </summary>
        public string ReallyShipper
        {
            get;
            set;
        }
        /// <summary>
        /// 真实通知人
        /// </summary>
        public string ReallyNotifyParty
        {
            get;
            set;
        }
        /// <summary>
        /// ShippingOrder是否第三地付款
        /// </summary>
        public bool IsThirdPlacePayOrder
        {
            get;
            set;
        }
        /// <summary>
        /// ShippingOrder第三付款地ID
        /// </summary>
        public Guid? CollectbyAgentOrderID
        {
            get;
            set;
        }
        /// <summary>
        /// 订舱人
        /// </summary> 
        public Guid? BookingPartyID
        {
            get;
            set;
        }
        /// <summary>
        /// 是否承运人代发AMS
        /// </summary>
        public bool IsCarrierSendAMS
        {
            get;
            set;
        }
        /// <summary>
        /// 是否含有金额
        /// </summary>
        public bool HasFee
        {
            get;
            set;
        }
        /// <summary>
        /// HSCODE
        /// </summary>
        public string HSCODE
        {
            get;
            set;
        }
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity
        {
            get;
            set;
        }
        /// <summary>
        /// 箱描述
        /// </summary>
        public string Container
        {
            get;
            set;
        }
        /// <summary>
        /// 货物描述
        /// </summary>
        public SpclCargoDescription CargoDescription
        {
            get;
            set;
        }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveByID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
    #endregion
}
