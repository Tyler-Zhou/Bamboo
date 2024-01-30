//-----------------------------------------------------------------------
// <copyright file="CommandConstants.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ICP.FCM.AirImport.UI.Common
{
    /// <summary>
    /// Workspace常量
    /// </summary>
    public class WorkSpaceConstants
    {
        /// <summary>
        /// 主Workspace常量
        /// </summary>
        public const string MainWorkSpace = "MainWorkspace";
    }

    /// <summary>
    /// 空运进口模块常量
    /// </summary>
    public class CommandConstants
    {
        ///// <summary>
        ///// 提单列表命令名
        ///// </summary>
        //public const string AirImport_BLList = "AirIMPORT_BLLIST";

        /// <summary>
        /// 订单列表命令
        /// </summary>
        public const string AirImport_OrderList = "FCM_AI_ORDERLIST";

        /// <summary>
        /// 业务管理命令
        /// </summary>
        public const string AirImport_BusinessManage = "FCM_AIR_IMPORT_BUSINESSMANAGE";
    }

    /// <summary>
    /// 模块常量
    /// </summary>
    public class ModuleCodeConstants
    {

    }

    /// <summary>
    /// 模块下命令常量
    /// </summary>
    public class ModuleCommandCodeConstants
    {
        /// <summary>
        /// 出口订单
        /// </summary>
        public const string AirExport_Order = "Order";

        /// <summary>
        /// 出口提单
        /// </summary>
        public const string AirExport_BL = "BL";
    }

    /// <summary>
    /// 搜索常量
    /// </summary>
    public class SearchConstants
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

        public const string OEMBLFinder = "MBLFinder";

        public static readonly string[] CommonReturnFields = new string[] { "ID", "Code", "EName", "CName" };

        public static readonly string[] VesselVoyageReturnFields = new string[] { "ID", "NO", "VesselName" };    

        /// <summary>
        /// 船名/航次
        /// </summary>
        public const string VesselVoyage = @"Vessel/Voyage";
        public const string Vessel = @"Vessel";
        public const string Voyage = @"Voyage";

        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";
    }
}
