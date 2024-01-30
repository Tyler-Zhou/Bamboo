using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.FCM.OtherBusiness.ServiceInterface.DataObjects;
namespace ICP.FCM.OtherBusiness.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 用于保存业务管理之基本信息
    /// </summary>
    [Serializable]
    public class OtherBusinessSaveRequest:SaveRequest
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid? id;
        /// <summary>
        /// MBL
        /// </summary>
        public string MBLNO;
        /// <summary>
        /// HBL
        /// </summary>
        public string HBLNO;
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerID;
        /// <summary>
        /// 业务类型
        /// </summary>
        public OtOperationType OTOperationType;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID;
        /// <summary>
        /// 发货人ID
        /// </summary>
        public Guid? ShipperID;
        /// <summary>
        /// 收货人ID
        /// </summary>
        public Guid? ConsigneeID;
        /// <summary>
        /// 通知人
        /// </summary>
        public Guid? NotifyPartyID;
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid? CarrierID;
        /// <summary>
        /// 代理人ID
        /// </summary>
        public Guid? AgentOfCarrierID;
        /// <summary>
        /// POLID
        /// </summary>
        public Guid? POLID;
        /// <summary>
        /// POLNAME
        /// </summary>
        public string POLName;
        /// <summary>
        /// POLID
        /// </summary>
        public Guid? PODID;
        /// <summary>
        /// POLNAME
        /// </summary>
        public string PODName;
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime? ETD;
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime? ETA;
        /// <summary>
        /// FETA
        /// </summary>
        public DateTime? FETA;
        /// <summary>
        /// 目的地ID
        /// </summary>
        public Guid? FinalDestinationID;
        /// <summary>
        /// 目的地
        /// </summary>
        public string FinalDestinationName;
        /// <summary>
        /// 客户付款方式ID
        /// </summary>
        public Guid? PaymentTypeID;
        /// <summary>
        /// 大船船名航次ID
        /// </summary>
        public Guid? VoyageID;
        /// <summary>
        /// 品名
        /// </summary>
        public string Commodity;
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity;
        /// <summary>
        /// 数量单位ID
        /// </summary>
        public Guid? QuantityUnitID;
        /// <summary>
        /// 货物重量
        /// </summary>
        public decimal Weight;
        /// <summary>
        /// 货物重量单位ID
        /// </summary>
        public Guid? WeightUnitID;
        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement;
        /// <summary>
        /// 货物体积ID
        /// </summary>
        public Guid? MeasurementUnitID;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;
        /// <summary>
        /// 业务所属部门ID
        /// </summary>
        public Guid? SalesDepartmentID;
        /// <summary>
        /// 业务员ID
        /// </summary>
        public Guid? SalesID;
        /// <summary>
        /// 海外部客服ID
        /// </summary>
        public Guid? OverseasFilerID;

        /// <summary>
        /// 订舱人
        /// </summary>
        public Guid? OperatorID;
        /// <summary>
        /// 订舱时间
        /// </summary>
        public DateTime? OperationDate;
        /// <summary>
        /// 是否拖车
        /// </summary>
        public bool IsTruck;
        /// <summary>
        /// 是否报关
        /// </summary>
        public bool IsCustoms;
        /// <summary>
        /// 是否商检
        /// </summary>
        public bool IsCommodityInspection;
        /// <summary>
        /// 是否动植检
        /// </summary>
        public bool IsQuarantineInspection;
        /// <summary>
        /// 是否仓储
        /// </summary>
        public bool IsWarehouse;
        /// <summary>
        /// 报关行ID
        /// </summary>
        public Guid? CustomsBrokerID;
        /// <summary>
        /// 仓储ID
        /// </summary>
        public Guid? WarehouseID;
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO;
        /// <summary>
        /// 关联的业务ID
        /// </summary>
        public Guid? OperationID;
        /// <summary>
        /// 关联的业务号
        /// </summary>
        public string OperationNo;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate;
        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid? SaveByID;
        /// <summary>
        /// 是否英文
        /// </summary>
        public bool IsEnglish;

    }
}
