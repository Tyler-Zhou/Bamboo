#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/4/2 星期一 18:32:43
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;

namespace ICP.FCM.OceanExport.ServiceInterface.DataObjects.ReportObjects
{
    /// <summary>
    /// 订舱确认书(宁波)
    /// </summary>
    [Serializable]
    public class BookingConfirmation4NBReportData
    {
        /// <summary>
        /// 报表标题
        /// </summary>
        public string ReportTitle { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string Vessel { get; set; }

        /// <summary>
        /// 航次
        /// </summary>
        public string Voyage { get; set; }
        /// <summary>
        /// 开航日期
        /// </summary>
        public string SailingDate { get; set; }
        /// <summary>
        /// 联系人信息
        /// </summary>
        public string ContactInfo { get; set; }
        
        
        /// <summary>
        /// 订舱确认书(宁波)-BL信息
        /// </summary>
        public List<BLInfo4NBReportData> BLInfoList { get; set; }
        /// <summary>
        /// 订舱确认书(宁波)-箱汇总信息
        /// </summary>
        public List<ContainerInfo4NBReportData> ContainerInfoList { get; set; }
    }
    /// <summary>
    /// 订舱确认书-BL信息
    /// </summary>
    [Serializable]
    public class BLInfo4NBReportData
    {
        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo { get; set; }
        /// <summary>
        /// 截单日期
        /// </summary>
        public string Cut_OffDate { get; set; }
        /// <summary>
        /// 交货地点
        /// </summary>
        public string PlaceofDelivery { get; set; }
        /// <summary>
        /// 船代
        /// </summary>
        public string ShippingAgent { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        public string Measurement { get; set; }
        /// <summary>
        /// 船公司
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PaymentTerm { get; set; }
        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClause { get; set; }
        /// <summary>
        /// 外编
        /// </summary>
        public string ExternalNumber { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 操作电话
        /// </summary>
        public string OperatorTel { get; set; }
        /// <summary>
        /// 操作邮箱
        /// </summary>
        public string OperatorEMail { get; set; }
        
    }

    /// <summary>
    /// 订舱确认书(宁波)-箱汇总信息
    /// </summary>
    [Serializable]
    public class ContainerInfo4NBReportData
    {

        /// <summary>
        /// 箱型
        /// </summary>
        public string ContainerType { get; set; }
        /// <summary>
        /// 箱数
        /// </summary>
        public string ContainerQuantity { get; set; }
        /// <summary>
        /// 提单类型
        /// </summary>
        public string BLType { get; set; }
        /// <summary>
        /// 件数
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// 毛重
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// 尺码
        /// </summary>
        public string Measurement { get; set; }
    }
}
