//-----------------------------------------------------------------------
// <copyright file="ShippingOrderReportData.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects
{

    /// <summary>
    /// 装货单报表数据对象 
    /// </summary>
    [System.Serializable]
    public class ShippingOrderReportData
    {
        /// <summary>
        /// 提单号
        /// </summary>
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
        /// 唛头
        /// </summary>
        public string Marks { get; set; }

        /// <summary>
        /// 货物描述
        /// </summary>
        public string GoodsDescription { get; set; }


        /// <summary>
        /// 箱型
        /// </summary>
        public string ContainerType { get; set; }


        /// <summary>
        /// 数量 
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string QtyUnit { get; set; }


        /// <summary>
        /// 毛重量
        /// </summary>
        public decimal GrossWeight { get; set; }

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
        /// 毛重描述
        /// </summary>
        public string GrossWeightDescription { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
