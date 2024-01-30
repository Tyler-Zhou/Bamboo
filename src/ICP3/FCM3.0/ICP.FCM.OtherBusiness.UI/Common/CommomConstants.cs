using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.OtherBusiness.UI
{
    public class CommandConstants
    {
         /// <summary>
        /// 其他业务列表命令名
        /// </summary>
        public const string FCM_OTHERBUSINESS = "FCM_OTHERBUSINESS";

        /// <summary>
        /// 订舱列表命令名
        /// </summary>
        public const string FCM_OTHERORDER = "FCM_OTHERORDER";

        /// <summary>
        /// 订单列表命令名
        /// </summary>
        public const string FCM_AE_ORDERLIST = "FCM_AE_ORDERLIST"; 
    }
    /// <summary>
    /// 模块常量
    /// </summary>
    public class ModuleCodeConstants
    {
        public const string OtherBusiness = "OtherBusiness";
    }
    public class SearchFieldConstants
    {
        /// <summary>
        ///  "ID", "Code", "EName", "CName", "Type", "State","CheckedState"
        ///  适用于搜索“客户”
        /// </summary>
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "State", "CheckedState", "UpdateDate", "Fax", "EMail" };

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

        public static readonly string[] BusinessResultValue = new string[] { "ID", "No" };

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
        public const string BusinessNo = "BusinessNo";
    }
}
