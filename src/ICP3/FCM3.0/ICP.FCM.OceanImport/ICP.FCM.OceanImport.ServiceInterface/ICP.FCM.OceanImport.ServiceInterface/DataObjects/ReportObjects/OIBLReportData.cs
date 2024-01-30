//-----------------------------------------------------------------------
// <copyright file="OIBLReportData.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.OceanImport.ServiceInterface
{
    using System;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using ICP.FCM.Common.ServiceInterface.DataObjects;
    using ICP.Framework.CommonLibrary.Common;

    /// <summary>
    /// 提单报表数据对象
    /// </summary>
    [Serializable]
    public class OIBLReportData
    {
        /// <summary>
        /// 报表抬头
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// 报表标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 打印的LOGO图标名
        /// </summary>
        public string CompanyLogo { get; set; }

        /// <summary>
        /// 公司传真
        /// </summary>
        public string CompanyFax { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string CompanyTel { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 公司名
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 打印份数
        /// </summary>
        public int NumberOfOriginal { get; set; }

        /// <summary>
        /// 打印份数
        /// </summary>
        public string NumberOfOriginalPrint { get; set; }

        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo { get; set; }

        /// <summary>
        /// 订舱号
        /// </summary>
        public string ShipperOrderNo { get; set; }

        /// <summary>
        /// 主提单号
        /// </summary>
        public string MBLNo { get; set; }

        ///// <summary>
        ///// 提单类型(0:HBL,1:MBL)--进口业务只打HBL提单，所以去掉这个属性
        ///// </summary>
        //public BLType BLType { get; set; }

        /// <summary>
        /// 发货人的详细信息
        /// </summary>
        public string Shipper { get; set; }

        /// <summary>
        /// 收货人的详细信息
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 通知人的详细信息
        /// </summary>
        public string NotifyParty { get; set; }

        /// <summary>
        /// 代理的的详细信息
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// 代理的的详细信息
        /// </summary>
        public string AgentText { get; set; }

        /// <summary>
        /// 承运人
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// 头程运输
        /// </summary>
        public string PreCarriage { get; set; }

        /// <summary>
        /// 应收/应付,根据付款方式唯一确定的
        /// </summary>
        public string FreightPayable { get; set; }

        /// <summary>
        /// 船名航次(打印中只有一个船名航次)
        /// </summary>
        public string VesselVoyage { get; set; }


        /// <summary>
        /// 船名航次(打印无效)
        /// </summary>
        public string PreVesselVoyage { get; set; }

        /// <summary>
        /// 收货地
        /// </summary>
        public string PlaceOfReceipt { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        public string PortOfLoading { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        public string PortOfDischarge { get; set; }

        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDelivery { get; set; }

        /// <summary>
        /// 唛头和标志
        /// </summary>
        public string Marks { get; set; }

        /// <summary>
        /// 数量+数量单位
        /// </summary>
        public string Quantity { get; set; }

        /// <summary>
        /// 货物描述
        /// </summary>
        public string DescriptionOfGoods { get; set; }

        /// <summary>
        /// 箱描述
        /// </summary>
        public string DescriptionOfContainer { get; set; }

        /// <summary>
        /// 毛重+重量单位
        /// </summary>
        public string GrossWeight { get; set; }

        /// <summary>
        /// 体积+体积单位
        /// </summary>
        public string Measurement { get; set; }

        /// <summary>
        /// 集装箱或件数合计
        /// </summary>
        public string CtnQtyInfo { get; set; }

        /// <summary>
        /// 离港日
        /// </summary>
        public DateTime? ETD { get; set; }

        /// <summary>
        /// 费用描述
        /// </summary>
        public string FreightAndCharges { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public string PaymentTerm { get; set; }

        /// <summary>
        /// 签发地
        /// </summary>
        public string PlaceOfIssue { get; set; }

        /// <summary>
        /// 签发人
        /// </summary>
        public string IssueBy { get; set; }

        /// <summary>
        /// 签发时间
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// 报表样式(根据该样式，在报表中呈现不同的标志文字,如：TELEX RELEASE)
        /// </summary>
        public string ReportStyle { get; set; }

        /// <summary>
        /// AMS NO
        /// </summary>
        public string AMSNO { get; set; }

        /// <summary>
        /// ISF NO
        /// </summary>
        public string ISFNO { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public string RefNO { get; set; }

        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClause { get; set; }
    }
}
