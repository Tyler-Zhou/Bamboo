//-----------------------------------------------------------------------
// <copyright file="BLReportData.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects
{
    using System;

    /// <summary>
    /// 订舱确认报表数据对象
    /// </summary>
    [Serializable]
    public class BookingConfirmationReportData
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }

        /// <summary>
        /// 公司电话
        /// </summary>
        public string CompanyTel { get; set; }

        /// <summary>
        /// 公司传真
        /// </summary>
        public string CompanyFax { get; set; }

        /// <summary>
        /// 发货人详细信息(包括名称，地址，电话，传真)
        /// </summary>
        public string ShipperDescription { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }

        /// <summary>
        /// 通知人
        /// </summary>
        public string NotifyParty { get; set; }

        /// <summary>
        /// 代理
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// 打印人
        /// </summary>
        public string PrintdBy { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime PrintTime { get; set; }

        /// <summary>
        /// 船名航次
        /// </summary>
        public string VesselVoyage { get; set; }

        /// <summary>
        /// 程运人
        /// </summary>
        public string Carrier { get; set; }

        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONo { get; set; }

        /// <summary>
        /// 收货地
        /// </summary>
        public string PlaceOfReceipt { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        public string PortOfLoading { get; set; }

        /// <summary>
        /// 离港日
        /// </summary>
        public DateTime? ETD { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        public string PortOfDischarge { get; set; }

        /// <summary>
        /// 到港日
        /// </summary>
        public DateTime? ETA { get; set; }

        /// <summary>
        /// 交货地
        /// </summary>
        public string PlaceOfDelivery { get; set; }

        /// <summary>
        /// 最终目的地
        /// </summary>
        public string FinalDestination { get; set; }

        /// <summary>
        /// 麦头
        /// </summary>
        public string Marks { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quanity { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string QuanityUnit { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        public string WeightUnit { get; set; }

        /// <summary>
        /// 体积
        /// </summary>
        public decimal Measurement { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        public string MeasurementUnit { get; set; }

        /// <summary>
        /// 截关日
        /// </summary>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClause { get; set; }

        /// <summary>
        /// 提空桂地点
        /// </summary>
        public string PickupLocation { get; set; }

        /// <summary>
        /// 箱尺寸
        /// </summary>
        public string ContainerSize { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
