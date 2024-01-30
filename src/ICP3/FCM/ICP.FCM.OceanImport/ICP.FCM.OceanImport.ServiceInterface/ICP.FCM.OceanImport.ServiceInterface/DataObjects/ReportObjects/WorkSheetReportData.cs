using System.Collections.Generic;

namespace ICP.FCM.OceanImport.ServiceInterface
{   
    /// <summary>
    /// OIWorkSheet详细对象
    /// </summary>   
    public class WorkSheetReportData
    {
        public WorkSheetBaseReportData BaseReportData { get; set; }

        public List<BillReportData> BillList { get; set; }
    }

    public class WorkSheetBaseReportData
    {
        public string PrintDate { get; set; }

        public string PrintByName { get; set; }

        public string DefaultCurrency { get; set; }

        /// <summary>
        /// ReferenceNO
        /// </summary>
        public string ReferenceNO { get; set; }

        /// <summary>
        /// MasterBLNo
        /// </summary>
        public string MasterBLNo { get; set; }

        /// <summary>
        /// AgentName
        /// </summary>
        public string AgentName { get; set; }

        /// <summary>
        /// VesselVoyageNo
        /// </summary>
        public string VesselVoyageNo { get; set; }

        /// <summary>
        /// LoadPortName
        /// </summary>
        public string LoadPortName { get; set; }

        /// <summary>
        /// DiscPortName
        /// </summary>
        public string DiscPortName { get; set; }

        /// <summary>
        /// ETA
        /// </summary>
        public string ETA { get; set; }

        /// <summary>
        /// ETD
        /// </summary>
        public string ETD { get; set; }

        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// OperatorName
        /// </summary>
        public string OperatorName { get; set; }

        /// <summary>
        /// DeliveryName
        /// </summary>
        public string DeliveryName { get; set; }

        public string TotalAmount { get; set; }
    }

    public class BillReportData
    {
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// HouseBLNo
        /// </summary>
        public string HouseBLNo { get; set; }  

        /// <summary>
        /// BillNo
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// PostDate
        /// </summary>
        public string PostDate { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Agent
        /// </summary>
        public string Agent { get; set; }

        /// <summary>
        /// Revenue
        /// </summary>
        public string Revenue { get; set; }

        /// <summary>
        /// Cost
        /// </summary>
        public string Cost { get; set; }
    }
}
