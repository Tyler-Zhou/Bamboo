
//-----------------------------------------------------------------------
// <copyright file="CommonCommandConstants.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.DomesticTrade.UI
{
    /// <summary>
    /// 海运出口模块常量
    /// </summary>
    public class CommandConstants
    {
        /// <summary>
        /// 提单列表命令名        /// </summary>
        public const string OceanExport_BLList = "DOMESTICTRADE_BLLIST";

        /// <summary>
        /// 订舱列表命令名        /// </summary>
        public const string DOMESTICTRADE_BOOKINGLIST = "DOMESTICTRADE_BOOKINGLIST";

        /// <summary>
        /// 联单列表命令名        /// </summary>
        public const string DOMESTICTRADE_ORDERLIST = "DOMESTICTRADE_ORDERLIST";

        /// <summary>
        /// 申请代理列表命令名
        /// </summary>
        public const string DomesticTrade_AgentRequestList = "DOMESTICTRADE_AGENTREQUESTLIST";

    }

    /// <summary>
    /// 海运出口动作项常量
    /// </summary>
    public class ActionsConstants
    {
        /// <summary>
        /// 取消确认装船命令名
        /// </summary>
        public const string DomesticTrade_CancelLoadShip = "DOMESTICTRADE_CANCELlOADSHIP";
    }

    /// <summary>
    /// 模块常量
    /// </summary>
    public class ModuleCodeConstants
    {
        public const string DomesticTrade = "DomesticTrade";
    }

    public class SearchFieldConstants
    {
        /// <summary>
        ///  "ID", "Code", "EName", "CName", "Type", "State","CheckedState"
        ///  适用于搜索“客户”
        /// </summary>
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "State","CheckedState" };

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
        ///  "ID","OceanShippingOrderID", "No","OceanMBLs" 
        /// </summary>
        public static readonly string[] OceanBookingResultValue = new string[] { "ID","OceanShippingOrderID", "No","OceanMBLs" };

        /// <summary>
        /// "ID", "BLNo", "RefNo", "OceanShippingOrderID"
        /// </summary>
        public static readonly string[] MBLResultValue = new string[] { "ID", "BLNo", "RefNo", "OceanShippingOrderID" };

        /// <summary>
        /// ID，NO，VesselName
        /// </summary>
        public static readonly string[] VesselResultValue = new string[] { "ID","VesselName", "No"  };

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
