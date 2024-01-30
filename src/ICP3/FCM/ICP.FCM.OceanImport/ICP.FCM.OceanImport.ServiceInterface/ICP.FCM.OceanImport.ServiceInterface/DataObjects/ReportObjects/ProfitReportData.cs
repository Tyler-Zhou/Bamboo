using System.Collections.Generic;

namespace ICP.FCM.OceanImport.ServiceInterface
{
    /// <summary>
    /// ProfitReportData详细对象
    /// </summary>
    public class ProfitReportData
    {
        public ProfitBaseReportData BaseReportData { get; set; }

        public List<ProfitReportFeeData> Fees { get; set; }
    }

    public class ProfitBaseReportData
    {
        public string PrintDate { get; set; }

        public string DefaultCurrency { get; set; }

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

        ///// <summary>
        ///// VoyageName
        ///// </summary>
        //public string VoyageName { get; set; }

        /// <summary>
        /// VesselName
        /// </summary>
        public string VesselVoyageNo { get; set; }

        /// <summary>
        /// ReferenceNo
        /// </summary>
        public string ReferenceNo { get; set; }

        /// <summary>
        /// AgentName
        /// </summary>
        public string AgentName { get; set; }

        public string TotalRevenue { get; set; }

        public string TotalCost{ get; set; }

        public string TotalAgent { get; set; }

        public string Profit { get; set; }
    }

    /// <summary>
    /// HBLFEEReportData详细对象
    /// </summary>
    public class ProfitReportFeeData
    {
        ///// <summary>
        ///// HBLID
        ///// </summary>
        //public Guid ConsignId { get; set; }

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

        //public Guid ChargeItemID { get; set; }

        /// <summary>
        /// pmt
        /// </summary>
        public string pmt { get; set; }


        /// <summary>
        /// Revenue
        /// </summary>
        public string Revenue { get; set; }


        /// <summary>
        /// Cost
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// agent
        /// </summary>
        public string agent { get; set; }


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
