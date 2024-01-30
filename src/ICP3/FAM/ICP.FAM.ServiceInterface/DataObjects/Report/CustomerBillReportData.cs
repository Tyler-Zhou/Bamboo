using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.DataObjects
{

    #region 费用清单报表对象

    /// <summary>
    /// 费用清单的报表对象
    /// </summary>
    [Serializable]
    public class FeeListReportData
    {
        /// <summary>
        /// 订舱号
        /// </summary>
        public string SONo { get; set; }
        /// <summary>
        /// OperationNo
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// BLNo
        /// </summary>
        public string BLNo { get; set; }
        /// <summary>
        /// VesselVoyage
        /// </summary>
        public string VesselVoyage { get; set; }

        /// <summary>
        /// 截单日
        /// </summary>
        public DateTime? PreClosingDate { get; set; }

        /// <summary>
        /// 截单日
        /// </summary>
        public DateTime? ClosingDate { get; set; }

        /// <summary>
        /// PreETD
        /// </summary>
        public DateTime? PreETD { get; set; }
        /// <summary>
        /// ETD
        /// </summary>
        public DateTime? ETD { get; set; }
        /// <summary>
        /// ETA
        /// </summary>
        public DateTime? ETA { get; set; }
        /// <summary>
        /// ContainerType(1 * 20GP,2 * 40GP)
        /// </summary>
        public string ContainerType { get; set; }
        /// <summary>
        /// ContainerNo(用;分开)
        /// </summary>
        public string ContainerNo { get; set; }
        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// POL
        /// </summary>
        public string POL { get; set; }
        /// <summary>
        /// POD
        /// </summary>
        public string POD { get; set; }
        /// <summary>
        /// Destination
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// FETA
        /// </summary>
        public DateTime? FETA { get; set; }
        /// <summary>
        /// 计费
        /// </summary>
        public string Assessor { get; set; }
        /// <summary>
        /// Sales
        /// </summary>
        public string Sales { get; set; }
        /// <summary>
        /// Filer
        /// </summary>
        public string Filer { get; set; }

        /// <summary>
        /// 在表1中返回
        /// </summary>
        public List<FeeListReportFee> FeeListReportFees { get; set; }
    }

    /// <summary>
    /// 费用清单的报表费用列表对象
    /// </summary>
    [Serializable]
    public class FeeListReportFee
    {
        /// <summary>
        /// ChargeCode
        /// </summary>
        public string ChargeCode { get; set; }
        /// <summary>
        /// FeeWay
        /// </summary>
        public FeeWay Way { get; set; }
        /// <summary>
        /// WayDescription(客户端处理)
        /// </summary>
        public string WayDescription { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// AmountOfUSD(折合成USD
        /// </summary>
        public decimal AmountOfUSD { get; set; }
        /// <summary>
        /// BillToName
        /// </summary>
        public string BillToName { get; set; }
    }

    #endregion

    /// <summary>
    /// 报表信息说明性信息
    /// </summary>
    [Serializable]
    public class CommonBillReportData
    {

        /// <summary>
        /// 船名航次
        /// </summary>
        public string VesselVoyage { get; set; }

        /// <summary>
        /// 海出特有的,如果是海出而且要求显示驳船,就把此值赋予VesselVoyage,报表中只接收VesselVoyage字段
        /// </summary>
        public string PreVesselVoyage { get; set; }      

        /// <summary>
        /// 离港日
        /// </summary>
        public string ETD { get; set; }

        /// <summary>
        /// 到港日
        /// </summary>
        public string ETA { get; set; }

        /// <summary>
        /// 到港日(进口需要显示
        /// </summary>
        public string FETA { get; set; }

        /// <summary>
        /// 装货港
        /// </summary>
        public string POL { get; set; }

        /// <summary>
        /// 卸货港
        /// </summary>
        public string POD { get; set; }

        /// <summary>
        /// 目的港
        /// </summary>
        public string Destination { get; set; }


        /// <summary>
        /// 箱量和箱型
        /// </summary>
        public string ContainerType { get; set; }

        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo { get; set; }

        /// <summary>
        /// 2.0中"Order NO:"+OrderNO +" " + "Sales:"+Sales.Code （Sales的逻辑 如果业务的AgentId是配置中的客户就显示。)
        /// </summary>
        public string SalesInfo { get; set; }

    }

    /// <summary>
    /// 打印帐单业务数据信息
    /// </summary>
    [Serializable]
    public class LocalBillReportData
    {
        /// <summary>
        /// MBLNo
        /// </summary>
        public string MBLNo { get; set; }
        /// <summary>
        /// HBLNo
        /// </summary>
        public string HBLNo { get; set; }
        /// <summary>
        /// 下载过来的出口业务号
        /// </summary>
        public string AgentRefNo { get; set; }
        /// <summary>
        /// Shipper
        /// </summary>
        public string Shipper { get; set; }
        /// <summary>
        /// Consignee
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// Notify
        /// </summary>
        public string Notify { get; set; }
        /// <summary>
        /// Carrier
        /// </summary>
        public string Carrier { get; set; }
        /// <summary>
        /// ContainerNo(格式: FLUU001/20GP,FLUU002/40GP
        /// </summary>
        public string ContainerNo { get; set; }
        /// <summary>
        /// VesselVoyage
        /// </summary>
        public string VesselVoyage { get; set; }
        /// <summary>
        /// ETD
        /// </summary>
        public string ETD { get; set; }
        /// <summary>
        /// ETA
        /// </summary>
        public string ETA { get; set; }
        /// <summary>
        /// FETA
        /// </summary>
        public string FETA { get; set; }
        /// <summary>
        /// POL
        /// </summary>
        public string POL { get; set; }
        /// <summary>
        /// POD
        /// </summary>
        public string POD { get; set; }
        /// <summary>
        /// Destination
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// Commodity
        /// </summary>
        public string Commodity { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        public string Weight { get; set; }
        /// <summary>
        /// Measurement
        /// </summary>
        public string Measurement { get; set; }

        /// <summary>
        /// 客户参考号
        /// </summary>
        public string CustomerRefNo { get; set; }

        /// <summary>
        /// 业务的备注
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// PaymentMode
        /// </summary>
        public string PaymentMode { get; set; }

        /// <summary>
        /// PaymentMode
        /// </summary>
        public ICP.Framework.CommonLibrary.Common.CustomerDescription truckerDescription { get; set; }
    }
}
