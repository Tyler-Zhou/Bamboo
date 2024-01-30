using System;
using System.Collections.Generic;

namespace ICP.FCM.OceanImport.ServiceInterface.DataObjects
{
    /// <summary>
    /// 业务提单报表数据对象
    /// </summary>
    [Serializable]
    public class OIBusinessReportData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 业务类型（整箱，拼箱，散货
        /// </summary>
        public string OperationType { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClause { get; set; }
        /// <summary>
        /// 付款条约
        /// </summary>
        public string PaymentTerm { get; set; }
        /// <summary>
        /// 贸易条款
        /// </summary>
        public string TradeTerm { get; set; }
        /// <summary>
        /// 揽货人
        /// </summary>
        public string Sales { get; set; }
        /// <summary>
        /// 揽货人部门
        /// </summary>
        public string SalesDep { get; set; }
        /// <summary>
        /// 揽货方式
        /// </summary>
        public string SalesType { get; set; }
        /// <summary>
        /// 订舱类型
        /// </summary>
        public string BookingMode { get; set; }
        /// <summary>
        /// 订舱日
        /// </summary>
        public string BookingDate { get; set; }
        /// <summary>
        /// 海外客服
        /// </summary>
        public string CustomerService { get; set; }
        ///// <summary>
        ///// 订舱人
        ///// </summary>
        //public string Bookinger { get; set; }
        /// <summary>
        /// 文件
        /// </summary>
        public string Filer { get; set; }
        /// <summary>
        /// 收货地
        /// </summary>
        public string PlaceOfReceipt { get; set; }
        /// <summary>
        /// 装货港
        /// </summary>
        public string POL { get; set; }
        /// <summary>
        /// 卸货港
        /// </summary>
        public string POD { get; set; }
        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDelivery { get; set; }
        /// <summary>
        /// 最终目的地
        /// </summary>
        public string FinalDestination { get; set; }
        /// <summary>
        /// 承运人
        /// </summary>
        public string AgentOfCarrier { get; set; }
        /// <summary>
        /// 船公司
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// 驳船
        /// </summary>
        public string PreVoyage { get; set; }
        /// <summary>
        /// 大船
        /// </summary>
        public string Voyage { get; set; }
        /// <summary>
        /// ETD
        /// </summary>
        public string ETD { get; set; }
        /// <summary>
        /// ETA
        /// </summary>
        public string ETA { get; set; }

        /// <summary>
        /// DETA
        /// </summary>
        public string DETA { get; set; }

        /// <summary>
        /// FETA
        /// </summary>
        public string FETA { get; set; }

        /// <summary>
        /// 航线
        /// </summary>   
        public string ShippingLine { get; set; }

        /// <summary>
        /// MBL No
        /// </summary>
        public string MBLNO { get; set; }

        /// <summary>
        /// MBL放单方式
        /// </summary>
        public string MBLReleaseType { get; set; }

        /// <summary>
        /// 提单列表
        /// </summary>
        public List<BLListReportData> blListReportDatas { get; set; }

    }

    /// <summary>
    /// 业务提单报表数据对象
    /// </summary>
    [Serializable]
    public class BLListReportData
    {
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNO { get; set; }
        /// <summary>
        /// 主提单号
        /// </summary>
        public string MBLNO { get; set; }

        /// <summary>
        /// 放货方式
        /// </summary>
        public string BLReleaseType { get; set; }

        /// <summary>
        /// 发货人
        /// </summary>
        public string Shipper { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// 通知人
        /// </summary>
        public string NotifyParty { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
    }
}
