//-----------------------------------------------------------------------
// <copyright file="BLReportData.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using ICP.FCM.Common.ServiceInterface.DataObjects;

namespace ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects
{
    using System;
    using System.Collections.Generic;


    public class BLReport
    {
        public BLReportData_MBL BLReportData_MBL { get; set; }
        public BLReportData_HBL BLReportData_HBL { get; set; }
    }
    /// <summary>
    /// 提单报表数据对象
    /// </summary>
    [Serializable]
    public class BLReportData
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


        /// <summary>
        /// 提单类型(0:HBL,1:MBL)
        /// </summary>
        public AWBType AWBType { get; set; }

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
        /// 应付运费
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
        /// 项描述
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


    /// <summary>
    /// 业务提单报表数据对象
    /// </summary>
    [Serializable]
    public class OEBusinessReportData
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
        /// 空外客服
        /// </summary>
        public string OverSeasFiler { get; set; }
        /// <summary>
        /// 订舱人
        /// </summary>
        public string Bookinger { get; set; }
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
        /// 航线
        /// </summary>
        public string ShippingLine { get; set; }
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
    /// <summary>
    /// MBL报表数据对象
    /// </summary>
    [Serializable]
    public class BLReportData_MBL
    {
        public string MAWBNO { get; set; }
        public string Shipper { get; set; }
        public string ShipperAccountNo { get; set; }
        public string Consignee { get; set; }
        public string ConsigneeAccountNo { get; set; }
        public string AgentIATACode { get; set; }
        public string DepartureName { get; set; }
        public string TranshipmentPort1 { get; set; }
        public string Airline { get; set; }
        public string TranshipmentPort1By { get; set; }
        public string TranshipmentPort2 { get; set; }
        public string TranshipmentPort2By { get; set; }
        public string TranshipmentPort3 { get; set; }
        public string TranshipmentPort3By { get; set; }
        public string DestinationName { get; set; }
        public string FlightDate { get; set; }
        public string IssueBy { get; set; }
        public string NumberOfOriginal { get; set; }
        public string AccountInformation { get; set; }
        public string Currency { get; set; }
        public string CHGSCode { get; set; }
        public string PaymentTerm { get; set; }
        public string OtherPaymentTerm { get; set; }
        public string DeclaredValueForCarriage { get; set; }
        public string DeclaredValueForCustoms { get; set; }
        public string AmountofInsurance { get; set; }
        public string NoofPiecesRCP { get; set; }
        public string GrossWeight { get; set; }
        public string Kglb { get; set; }
        public string RateClass { get; set; }
        public string CommodityItemNo { get; set; }
        public string ChargeableWeight { get; set; }
        public string RateCharge { get; set; }
        public string Total { get; set; }
        public string GoodsDescription { get; set; }
        public string PrepaidWeightCharge { get; set; }
        public string CollectWeightCharge { get; set; }
        public string PrepaidValuationCharge { get; set; }
        public string CollectValuationCharge { get; set; }
        public string PrepaidTax { get; set; }
        public string CollectTax { get; set; }
        public string PrepaidTotalotherAgentCharges { get; set; }
        public string CollectTotalotherAgentCharges { get; set; }
        public string PrepaidTotalotherCarrierCharges { get; set; }
        public string CollectTotalotherCarrierCharges { get; set; }
        public string TotalPrepaid { get; set; }
        public string TotalCollect { get; set; }
        public string OtherChargers { get; set; }
        public string CurrencyConversionRate { get; set; }
        public string DestinationCurrencyAmount { get; set; }
        public string ChargesAtDestination { get; set; }
        public string HandlingInformation { get; set; }
        public string IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public string Agent { get; set; }
    }
    /// <summary>
    /// HBL报表数据对象
    /// </summary>
    [Serializable]
    public class BLReportData_HBL
    {
        public string HAWBNO { get; set; }
        public string MAWBNO { get; set; }
        public string Shipper { get; set; }
        public string ShipperAccountNo { get; set; }
        public string Consignee { get; set; }
        public string ConsigneeAccountNo { get; set; }
        public string AgentIATACode { get; set; }
        public string DepartureName { get; set; }
        public string TranshipmentPort1 { get; set; }
        public string Airline { get; set; }
        public string TranshipmentPort1By { get; set; }
        public string TranshipmentPort2 { get; set; }
        public string TranshipmentPort2By { get; set; }
        public string TranshipmentPort3 { get; set; }
        public string TranshipmentPort3By { get; set; }
        public string DestinationName { get; set; }
        public string FlightDate { get; set; }
        public string IssueBy { get; set; }
        public string NumberOfOriginal { get; set; }
        public string AccountInformation { get; set; }
        public string Currency { get; set; }
        public string CHGSCode { get; set; }
        public string PaymentTerm { get; set; }
        public string OtherPaymentTerm { get; set; }
        public string DeclaredValueForCarriage { get; set; }
        public string DeclaredValueForCustoms { get; set; }
        public string AmountofInsurance { get; set; }
        public string NoofPiecesRCP { get; set; }
        public string GrossWeight { get; set; }
        public string Kglb { get; set; }
        public string RateClass { get; set; }
        public string CommodityItemNo { get; set; }
        public string ChargeableWeight { get; set; }
        public string RateCharge { get; set; }
        public string Total { get; set; }
        public string GoodsDescription { get; set; }
        public string PrepaidWeightCharge { get; set; }
        public string CollectWeightCharge { get; set; }
        public string PrepaidValuationCharge { get; set; }
        public string CollectValuationCharge { get; set; }
        public string PrepaidTax { get; set; }
        public string CollectTax { get; set; }
        public string PrepaidTotalotherAgentCharges { get; set; }
        public string CollectTotalotherAgentCharges { get; set; }
        public string PrepaidTotalotherCarrierCharges { get; set; }
        public string CollectTotalotherCarrierCharges { get; set; }
        public string TotalPrepaid { get; set; }
        public string TotalCollect { get; set; }
        public string OtherChargers { get; set; }
        public string CurrencyConversionRate { get; set; }
        public string DestinationCurrencyAmount { get; set; }
        public string ChargesAtDestination { get; set; }
        public string HandlingInformation { get; set; }
        public string IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public string Agent { get; set; }
        public string Marks { get; set; }
        public string FlightNo { get; set; }

        //报表设置参数（非数据库获取）
        public string ReportStyle { get; set; }
        public string Title { get; set; }//报表类型为正本时,=Notify，else   =string.empty
        public string Notify { get; set; }
        public string LogoName{ get; set; }
        /// <summary>
        /// 提单类型(0:HBL,1:MBL)
        /// </summary>
        public AWBType AWBType { get; set; }
    }
}
