//-----------------------------------------------------------------------
// <copyright file="ContainerPackingReportData.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects
{
    using System;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    ///装箱单    /// </summary>
    [Serializable]
    public class ContainerPackingReportData
    {
        /// <summary>
        /// 发货人
        /// </summary>
        public string ShipperName { get; set; }

        /// <summary>
        /// 发货人描述
        /// </summary>
        public string ShipperDescription { get; set; }

        /// <summary>
        /// 收货人
        /// </summary>
        public string ConsigneeName { get; set; }

        /// <summary>
        /// 收货人描述
        /// </summary>
        public string ConsigneeDescription { get; set; }

        /// <summary>
        /// 通知人
        /// </summary>
        public string NotifyPartyName { get; set; }

        /// <summary>
        /// 通知人描述信息
        /// </summary>
        public string NotifyPartyDescription { get; set; }

        /// <summary>
        /// 唛头
        /// </summary>
        public string Marks { get; set; }

        /// <summary>
        /// 提单号        /// </summary>
        public string BLNo { get; set; }

        /// <summary>
        /// 船名
        /// </summary>
        public string Vessel { get; set; }

        /// <summary>
        /// 航次
        /// </summary>
        public string Voyage { get; set; }


        /// <summary>
        /// 收货地        /// </summary>
        public string PlaceOfReceipt { get; set; }


        /// <summary>
        /// 装货港        /// </summary>
        public string POL { get; set; }

        /// <summary>
        /// 卸货港        /// </summary>
        public string POD { get; set; }

        /// <summary>
        /// 交货地        /// </summary>
        public string PlaceOfDelivery { get; set; }

        /// <summary>
        /// 最终目的地
        /// </summary>
        public string FinalDestination { get; set; }

        /// <summary>
        /// 箱信息描述(包括箱号，数量，箱型，尺寸)CONTAINER NO:CAIU4022062 / 40GP / / 1 CTNS/ 0.000 KGS/ 0.000 CBM
        /// </summary>
        public string ContainerInfo { get; set; }

        /// <summary>
        /// 箱数描述// TOTAL:TWO CONTAINER(2*40GP)
        /// </summary>
        public string ContainerQtyDescription { get; set; }

        /// <summary>
        /// 货物描述
        /// </summary>
        public string GoodsDescription { get; set; }


        /// <summary>
        /// 数量 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string QuantityUnit { get; set; }


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
        /// 毛重量        /// </summary>
        public decimal GrossWeight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
