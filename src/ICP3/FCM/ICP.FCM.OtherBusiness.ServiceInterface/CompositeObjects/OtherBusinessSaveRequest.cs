using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 用于保存业务管理之基本信息
    /// </summary>
    [Serializable]
    public class OtherBusinessSaveRequest : SaveRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? id { get; set; }
        /// <summary>
        /// MBL
        /// </summary>
        public string MBLNO { get; set; }
        /// <summary>
        /// HBL
        /// </summary>
        public string HBLNO { get; set; }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string ExpressNo { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public OtOperationType OTOperationType { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 发货人ID
        /// </summary>
        public Guid? ShipperID { get; set; }
        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid? ConsigneeID { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public Guid? NotifyPartyID { get; set; }
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID { get; set; }
        /// <summary>
        /// 代理人ID
        /// </summary>
        public Guid? AgentOfCarrierID { get; set; }
        /// <summary>
        /// POLID
        /// </summary>
        public Guid? POLID { get; set; }
        /// <summary>
        /// POLNAME
        /// </summary>
        public string POLName { get; set; }
        /// <summary>
        /// POLID
        /// </summary>
        public Guid? PODID { get; set; }
        /// <summary>
        /// POLNAME
        /// </summary>
        public string PODName { get; set; }
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime? ETD { get; set; }
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime? ETA { get; set; }
        /// <summary>
        /// FETA
        /// </summary>
        public DateTime? FETA { get; set; }
        /// <summary>
        /// 目的地ID
        /// </summary>
        public Guid? FinalDestinationID { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public string FinalDestinationName { get; set; }
        /// <summary>
        /// 客户付款方式ID
        /// </summary>
        public Guid? PaymentTypeID { get; set; }
        /// <summary>
        /// 大船船名航次ID
        /// </summary>
        public Guid? VoyageID { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// 数量单位ID
        /// </summary>
        public Guid? QuantityUnitID { get; set; }
        /// <summary>
        /// 货物重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 货物重量单位ID
        /// </summary>
        public Guid? WeightUnitID { get; set; }
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement { get; set; }
        /// <summary>
        /// 货物体积ID
        /// </summary>
        public Guid? MeasurementUnitID { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 业务所属部门ID
        /// </summary>
        public Guid? SalesDepartmentID { get; set; }
        /// <summary>
        /// 业务员ID
        /// </summary>
        public Guid? SalesID { get; set; }
        /// <summary>
        /// 海外部客服ID
        /// </summary>
        public Guid? OverseasFilerID { get; set; }

        /// <summary>
        /// 订舱人
        /// </summary>
        public Guid? OperatorID { get; set; }
        /// <summary>
        /// 订舱时间
        /// </summary>
        public DateTime? OperationDate { get; set; }
        /// <summary>
        /// 是否拖车
        /// </summary>
        public bool IsTruck { get; set; }
        /// <summary>
        /// 是否报关
        /// </summary>
        public bool IsCustoms { get; set; }
        /// <summary>
        /// 是否商检
        /// </summary>
        public bool IsCommodityInspection { get; set; }
        /// <summary>
        /// 是否动植检
        /// </summary>
        public bool IsQuarantineInspection { get; set; }
        /// <summary>
        /// 是否仓储
        /// </summary>
        public bool IsWarehouse { get; set; }
        /// <summary>
        /// 报关行ID
        /// </summary>
        public Guid? CustomsBrokerID { get; set; }
        /// <summary>
        /// 仓储ID
        /// </summary>
        public Guid? WarehouseID { get; set; }
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO { get; set; }
        /// <summary>
        /// 关联的业务ID
        /// </summary>
        public Guid? OperationID { get; set; }
        /// <summary>
        /// 关联的业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 币种 ID
        /// </summary>
        public Guid? CurrencyID { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal CostAmount { get; set; }
        /// <summary>
        /// 计费吨
        /// </summary>
        public decimal RevenueTon { get; set; }

        /// <summary>
        /// 是否同步到CSP
        /// </summary>
        public bool IsSyncCSP { get; set; }
       
        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid? SaveByID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
