

//-----------------------------------------------------------------------
// <copyright file="CommonCommandConstants.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirExport.UI
{
    /// <summary>
    /// 空运出口模块常量
    /// </summary>
    public class CommandConstants
    {
        /// <summary>
        /// 提单列表命令名
        /// </summary>
        public const string FCM_AE_BLLIST = "FCM_AE_BLLIST";

        /// <summary>
        /// 订舱列表命令名
        /// </summary>
        public const string FCM_AE_BOOKINGLIST = "FCM_AE_BOOKINGLIST";

        /// <summary>
        /// 订单列表命令名
        /// </summary>
        public const string FCM_AE_ORDERLIST = "FCM_AE_ORDERLIST";
    }

    /// <summary>
    /// 空运出口动作项常量
    /// </summary>
    public class ActionsConstants
    {
        /// <summary>
        /// 取消确认装船命令名
        /// </summary>
        public const string AirExport_CancelLoadShip = "AIREXPORT_CANCELlOADSHIP";
    }

    /// <summary>
    /// 模块常量
    /// </summary>
    public class ModuleCodeConstants
    {
        public const string AirExport = "AirExport";
    }

    public class SearchFieldConstants
    {
        /// <summary>
        ///  "ID", "Code", "EName", "CName", "Type", "State","CheckedState"
        ///  适用于搜索“客户”
        /// </summary>
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "State", "CheckedState" };

        /// <summary>
        /// "ID", "Code", "EName", "CName"
        /// 适用于搜索“港口”
        /// </summary>
        public static readonly string[] PortResultValue = new string[] { "ID", "Code", "EName", "CName" };

        /// <summary>
        /// "ID", "Code", "EName", "CName","Type","TradeTermID","TradeTermName","State","CheckedState"
        /// </summary>
        public static readonly string[] CustomerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName", "State", "CheckedState", "UpdateDate", "Fax", "EMail" };

        /// <summary>
        ///  "ID","AirShippingOrderID", "No","AirMBLs" 
        /// </summary>
        public static readonly string[] AirBookingResultValue = new string[] { "ID", "AirShippingOrderID", "No", "AirMBLs" };

        /// <summary>
        /// "ID", "BLNo", "RefNo", "AirShippingOrderID"
        /// </summary>
        public static readonly string[] MBLResultValue = new string[] { "ID", "BLNo", "RefNo", "AirShippingOrderID" };

        /// <summary>
        /// ID，NO，VesselName
        /// </summary>
        public static readonly string[] VesselResultValue = new string[] { "ID", "No", "VesselName" };

        /// <summary>
        /// 船名/航次
        /// </summary>
        public const string VesselVoyage = @"Vessel/Voyage";
        public const string Vessel = @"Vessel";
        public const string Voyage = @"Voyage";

        public const string BookingNO = "BookingNO";
        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";
    }
}
