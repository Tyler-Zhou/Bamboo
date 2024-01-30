//-----------------------------------------------------------------------
// <copyright file="ProfitReportData.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.DomesticTrade.ServiceInterface.DataObjects.ReportObjects
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 利润报表数据对象
    /// </summary>
    [System.Serializable]
    public class ProfitReportData
    {
        /// <summary>
        /// 构造函数        /// </summary>
        public ProfitReportData()
        {
            this.Fees = new List<ProfitFeeReportData>();
        }

        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime PrintTime { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 业务号        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// 主单号        /// </summary>
        public string MBLNo { get; set; }

        /// <summary>
        /// 分单号        /// </summary>
        public string HBLNo { get; set; }

        /// <summary>
        /// 代理
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// 船名航次
        /// </summary>
        public string VesselVoyage { get; set; }

        /// <summary>
        /// 装货港        /// </summary>
        public string POL { get; set; }

        /// <summary>
        /// 离港日        /// </summary>
        public DateTime? ETD { get; set; }

        /// <summary>
        /// 卸货港        /// </summary>
        public string POD { get; set; }

        /// <summary>
        /// 到港日        /// </summary>
        public DateTime? ETA { get; set; }

        /// <summary>
        /// 费用列表
        /// </summary>
        public List<ProfitFeeReportData> Fees { get; set; }
    }


    /// <summary>
    /// 利润报表的费用数据对象    /// </summary>
    [Serializable]
    public class ProfitFeeReportData
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid BookingID { get; set; }

        /// <summary>
        /// 帐单号        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime PostDate { get; set; }

        /// <summary>
        /// 费用描述
        /// </summary>
        public string ChargingCodeDescription { get; set; }

        /// <summary>
        /// 费用代码ID
        /// </summary>
        public Guid ChargingCodeID { get; set; }

        /// <summary>
        /// 帐单是否都完全核销(Y:是,N:否）
        /// </summary>
        public string Paid { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal Revenue { get; set; }

        /// <summary>
        /// 应付金额
        /// </summary>
        public decimal Cost { get; set; }

        /// <summary>
        /// 代收，代付金额(应付用负数表示，应收用正数表示)
        /// </summary>
        public decimal DRCR { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
    }
}
