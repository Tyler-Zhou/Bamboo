using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace ICP.FCM.AirImport.ServiceInterface
{
    /// <summary>
    /// ProfitReportData详细对象
    /// </summary>
    [Serializable]
    public class ProfitReportData
    {
        public string PrintDate { get; set; }

        /// <summary>
        /// HouseBLNo
        /// </summary>
        public string HouseBLNo { get; set; }

        /// <summary>
        /// MasterBLNo
        /// </summary>
        public string MasterBLNo { get; set; }

        /// <summary>
        /// ETD
        /// </summary>
        public string ETD { get; set; }

        /// <summary>
        /// LoadPortName
        /// </summary>
        public string LoadPortName { get; set; }

        /// <summary>
        /// ETA
        /// </summary>
        public string ETA { get; set; }

        /// <summary>
        /// DiscPortName
        /// </summary>
        public string DiscPortName { get; set; }

        /// <summary>
        /// VoyageName
        /// </summary>
        public string FlightNo { get; set; }

        /// <summary>
        /// ReferenceNo
        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// AgentName
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<ProfitReportFeeData> Fees { get; set; }

        public decimal? TotalRevenue { get; set; }

        public decimal? TotalCost { get; set; }

        public decimal? TotalAgent { get; set; }

        public decimal? Profit { get; set; }
    }

    /// <summary>
    /// HBLFEEReportData详细对象
    /// </summary>
    [Serializable]
    public class ProfitReportFeeData
    {
        /// <summary>
        /// HBLID
        /// </summary>
        public Guid ConsignId { get; set; }

        /// <summary>
        /// InvNo
        /// </summary>
        public string InvNo { get; set; }

        /// <summary>
        /// PostDate
        /// </summary>
        public string PostDate { get; set; }

        /// <summary>
        /// ChargeItemDescription
        /// </summary>
        public string ChargeItemDescription { get; set; }

        public Guid ChargeItemID { get; set; }

        /// <summary>
        /// pmt
        /// </summary>
        public string pmt { get; set; }

        /// <summary>
        /// Revenue
        /// </summary>
        public decimal? Revenue { get; set; }

        /// <summary>
        /// Cost
        /// </summary>
        public decimal? Cost { get; set; }

        /// <summary>
        /// agent
        /// </summary>
        public decimal? agent { get; set; }

        /// <summary>
        /// company
        /// </summary>
        public string company { get; set; }

        /// <summary>
        /// currency
        /// </summary>
        public string currency { get; set; }      
    }
}
