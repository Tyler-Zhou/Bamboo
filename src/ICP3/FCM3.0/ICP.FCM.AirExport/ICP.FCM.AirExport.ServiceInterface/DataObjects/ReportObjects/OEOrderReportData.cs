﻿//-----------------------------------------------------------------------
// <copyright file="ContainerPackingReportData.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirExport.ServiceInterface.DataObjects.ReportObjects
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 订单报表数据对象
    /// </summary>
    [Serializable]
    public class OEOrderReportData
    {
        /// <summary>
        /// 构造函数
        public OEOrderReportData()
        {
            this.Fees = new List<OEOrderFeeReportData>();
        }

        /// <summary>
        /// 唯一键
        public Guid ID { get; set; }


        /// <summary>
        /// 订单号
        public string OrderNo { get; set; }


        /// <summary>
        /// PO NO可以拼装
        /// </summary>
        public string PONo { get; set; }

        /// <summary>
        /// 订舱号
        public string SONo { get; set; }



        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName { get; set; }


        /// <summary>
        /// 客户详细信息
        /// </summary>
        public string CustomerDescription { get; set; }

        /// <summary>
        /// 船公司
        public string CarrierName { get; set; }

        /// <summary>
        /// 承运人
        public string AgentOfCarrierName { get; set; }

        /// <summary>
        /// 发货人名称
        public string ShipperName { get; set; }

        /// <summary>
        /// 发货人描述
        public string ShipperDescription { get; set; }

        /// <summary>
        /// 代理
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// 代理描述
        /// </summary>
        public string AgentDescription { get; set; }

        /// <summary>
        /// 订舱公司
        /// </summary>
        public string BookingCustomerName { get; set; }

        /// <summary>
        /// 收货人名称
        public string ConsigneeName { get; set; }

        /// <summary>
        /// 收货人描述信息
        public string ConsigneeDescription { get; set; }
        /// <summary>
        /// 装货港
        public string POLName { get; set; }
        /// <summary>
        /// 卸货港
        public string PODName { get; set; }

        /// <summary>
        /// 目的地
        public string PlaceOfDelivery { get; set; }

        /// <summary>
        /// 船名航次
        /// </summary>
        public string VesselVoyageName { get; set; }

        /// <summary>
        /// 预计离港日
        public DateTime? ETD { get; set; }

        /// <summary>
        /// 截关日
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// 运输条款
        /// </summary>
        public string TransportClause { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// 报关
        /// </summary>
        public bool IsCustoms { get; set; }

        /// <summary>
        /// 仓储
        /// </summary>
        public bool IsWarehouse { get; set; }

        /// <summary>
        /// 派车
        /// </summary>
        public bool IsTruck { get; set; }

        /// <summary>
        /// 质检
        /// </summary>
        public bool IsQuarantineInspection { get; set; }

        /// <summary>
        /// 商检
        /// </summary>
        public bool IsCommodityInspection { get; set; }

        /// <summary>
        /// 出HBL
        /// </summary>
        public bool IsOutHBL { get; set; }

        /// <summary>
        /// 出MBL
        /// </summary>
        public bool IsOutMBL { get; set; }

        /// <summary>
        /// 贸易条款
        /// </summary>
        public string TradeTerm { get; set; }

        /// <summary>
        /// 放货类型
        /// </summary>
        public string ReleaseType { get; set; }


        /// <summary>
        /// MBL 付款方式
        /// </summary>
        public string MBLPaymentType { get; set; }

        /// <summary>
        /// MBL 要求
        /// </summary>
        public string MBLRequest { get; set; }

        /// <summary>
        /// MBL 要求
        /// </summary>
        public string HBLPaymentType { get; set; }

        /// <summary>
        /// HBL要求
        /// </summary>
        public string HBLRequest { get; set; }

        /// <summary>
        /// 业务员
        public string SalesName { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// 口岸公司
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 货物名称
        /// </summary>
        public string GoodsMarks { get; set; }

        /// <summary>
        /// 货物数量和单位
        public string GoodsQty { get; set; }

        /// <summary>
        /// 货物重量和单位
        public string GoodsWeight { get; set; }

        /// <summary>
        /// 货物体积和单位
        public string GoodsMeasurement { get; set; }

        /// <summary>
        /// 箱需求
        public string ContainerRequest { get; set; }

        /// <summary>
        /// 订单费用
        /// </summary>
        public List<OEOrderFeeReportData> Fees { get; set; }
    }

    /// <summary>
    /// 报表订单费用
    /// </summary>
    [Serializable]
    public class OEOrderFeeReportData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public string Way { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string FeeName { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}