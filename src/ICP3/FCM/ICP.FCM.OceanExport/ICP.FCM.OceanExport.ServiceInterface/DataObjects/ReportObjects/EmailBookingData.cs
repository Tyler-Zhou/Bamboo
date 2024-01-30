using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects
{
    /// <summary>
    /// 邮件订舱数据
    /// </summary>
    [Serializable]
    public class EmailBookingReport
    {
        public string Shipper { get; set; }
        public string Consignee { get; set; }
        public string Notify { get; set; }
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONO { get; set; }
        /// <summary>
        /// 合约号
        /// </summary>
        public string ContractNO { get; set; }
        public string VesselVoyage { get; set; }
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
        public string Delivery { get; set; }
        /// <summary>
        /// 最终目的地
        /// </summary>
        public string FinalDestination { get; set; }
        /// <summary>
        /// 付款地中国
        /// </summary>
        public bool PaymentSettledAtCN { get; set; }
        /// <summary>
        /// 付款地香港
        /// </summary>
        public bool PaymentSettledAtHK { get; set; }
        public bool CYCY { get; set; }
        public bool CYDR { get; set; }
        public bool DRDR { get; set; }
        public bool DRCY { get; set; }
        public bool CYFO { get; set; }
        public bool CYLO { get; set; }
        /// <summary>
        /// 正本提单1
        /// </summary>
        public bool OriginalBL { get; set; }
        /// <summary>
        /// 海运提单3
        /// </summary>
        public bool SeaWayBill { get; set; }
        /// <summary>
        /// 电放2
        /// </summary>
        public bool TelexRelease { get; set; }
        /// <summary>
        /// 唛头
        /// </summary>
        public string ShippingMarks { get; set; }
        /// <summary>
        /// 货物总数,<=0时为空
        /// </summary>
        public string NumberOfGoods { get; set; }
        /// <summary>
        /// 包装类型
        /// </summary>
        public string PackageType { get; set; }
        /// <summary>
        /// 货物描述
        /// </summary>
        public string DescriptionOfGoods { get; set; }
        /// <summary>
        /// 毛重,<=0时为空
        /// </summary>
        public string GrossWeight { get; set; }
        /// <summary>
        /// 体积,<=0时为空
        /// </summary>
        public string Measurements { get; set; }
        public string GP20 { get; set; }
        public string GP40 { get; set; }
        public string HQ40 { get; set; }
        public string HQ45 { get; set; }
        /// <summary>
        /// 其它箱型
        /// </summary>
        public string Anothers { get; set; }
        /// <summary>
        /// 预付
        /// </summary>
        public bool FreightPrepaid { get; set; }
        /// <summary>
        /// 到付
        /// </summary>
        public bool FreightCollect { get; set; }
    }
    /// <summary>
    /// 邮件订舱/补料配置文件
    /// </summary>
    [Serializable]
    public class EmailBookingSIConfig
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 邮件订舱/补料唯一代码
        /// </summary>
        public EmailBookingSICode EmailBookingSICode { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 船东ID
        /// </summary>
        public Guid CarrierID { get; set; }
        /// <summary>
        /// EDIMode 订舱/补料
        /// </summary>
        public EDIMode EDIMode { get; set; }
        /// <summary>
        /// 报表名称
        /// </summary>
        public string ReportName { get; set; }
        /// <summary>
        /// 存储过程名称
        /// </summary>
        public string StoredProcedure { get; set; }
    }
    /// <summary>
    /// 其它字段
    /// </summary>
    [Serializable]
    public class OtherEmailBookingField
    {
        /// <summary>
        /// 付款地是否中国
        /// </summary>
        public bool PaymentSettledAtCN { get; set; }
    }
}
