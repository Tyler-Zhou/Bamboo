
namespace ICP.TMS.UI
{
    /// <summary>
    /// 模块常量
    /// </summary>
    public class FunctionConstants
    {
        /// <summary>
        /// 拖车业务界面
        /// </summary>
        public const string TMS_TRUCKBUSINESS = "TMS_TRUCKBUSINESS";

        /// <summary>
        /// 司机列表界面
        /// </summary>
        public const string TMS_DRIVERLIST = "TMS_DRIVERLIST";

        /// <summary>
        /// 拖车管理界面
        /// </summary>
        public const string TMS_TRUCKLIST = "TMS_TRUCKLIST";


    }

    public class SearchFieldConstants
    {
        /// <summary>
        ///  "ID", "Code", "EName", "CName", "Type"
        ///  适用于搜索“客户”
        /// </summary>
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName", "Type" };

        /// <summary>
        /// "ID", "Code", "EName", "CName"
        /// 适用于搜索“港口”
        /// </summary>
        public static readonly string[] PortResultValue = new string[] { "ID", "Code", "EName", "CName" };

        /// <summary>
        /// "ID", "Code", "EName", "CName","Type","TradeTermID","TradeTermName","State","CheckedState"
        /// </summary>
        public static readonly string[] CustomerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName", "State", "CheckedState" };

        /// <summary>
        ///  "ID","OceanShippingOrderID", "No","OceanMBLs" 
        /// </summary>
        public static readonly string[] OceanBookingResultValue = new string[] { "ID", "OceanShippingOrderID", "No", "OceanMBLs" };

        /// <summary>
        /// "ID", "BLNo", "RefNo", "OceanShippingOrderID"
        /// </summary>
        public static readonly string[] MBLResultValue = new string[] { "ID", "BLNo", "RefNo", "OceanShippingOrderID" };

        /// <summary>
        /// ID，NO，VesselName
        /// </summary>
        public static readonly string[] VesselResultValue = new string[] { "ID", "No", "VesselName" };


        /// <summary>
        /// ID，NO，VesselName
        /// </summary>
        public static readonly string[] VoyageResultValue = new string[] { "ID", "No", "VoyageName" };

        public const string VesselVoyage = @"Vessel/Voyage";
        public const string Vessel = @"Vessel";
        public const string Voyage = @"Voyage";

        public const string BookingNO = "BookingNO";
        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";
    }
}
