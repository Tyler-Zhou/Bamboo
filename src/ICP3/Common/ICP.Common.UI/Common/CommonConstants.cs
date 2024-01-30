

//-----------------------------------------------------------------------
// <copyright file="CommonCommandConstants.cs" company="LongWin">
//     Copyright (c) LongWin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
namespace ICP.Common.UI
{
    public class Constants
    {
        public static Guid NewRowID = new Guid("11111111-1111-1111-1111-111111111111");
    }

    /// <summary>
    /// 公共模块常量
    /// </summary>
    public class CommonCommandConstants
    {
        /// <summary>
        /// 品名列表命令名
        /// </summary>
        public const string Common_CommodityList = "COMMON_COMMODITYLIST";

        /// <summary>
        /// 箱型列表命令名
        /// </summary>
        public const string Common_ContainerList = "COMMON_CONTAINERLIST";

        /// <summary>
        /// 字典列表命令名
        /// </summary>
        public const string Common_DataDictionaryList = "COMMON_DATADICTIONARYLIST";

        /// <summary>
        /// 航班列表命令名
        /// </summary>
        public const string Common_FlightList = "COMMON_FLIGHTLIST";

        /// <summary>
        /// 航线列表命令名
        /// </summary>
        public const string Common_ShippingLineList = "COMMON_SHIPPINGLINELIST";

        /// <summary>
        /// 运输条款列表命令名
        /// </summary>
        public const string Common_TransportClauseList = "COMMON_TRANSPORTCLAUSELIST";

        /// <summary>
        /// 国家省份列表命令名
        /// </summary>
        public const string Common_CountryProvinceList = "COMMON_COUNTRYPROVINCELIST";

        /// <summary>
        /// 地点列表命令名
        /// </summary>
        public const string Common_LocationList = "COMMON_LOCATIONLIST";

        /// <summary>
        /// 客户列表命令名
        /// </summary>
        public const string Common_CustomerList = "COMMON_CUSTOMERLIST";

        /// <summary>
        /// 船名命令名

        /// </summary>
        public const string Common_VesselList = "COMMON_VESSELLIST";

        /// <summary>
        /// 航次命令名

        /// </summary>
        public const string Common_VoyageList = "COMMON_VOYAGELIST";

        /// <summary>
        /// 费用代码列表命令名
        /// </summary>
        public const string Common_ChargingCodeList = "COMMON_CHARGINGCODELIST";

        /// <summary>
        /// 币种列表命令名
        /// </summary>
        public const string Common_CurrencyList = "COMMON_CURRENCYLIST";

        /// <summary>
        /// 码头账户列表命令名
        /// </summary>
        public const string Common_TerminalLogin = "COMMON_TERMINALLOGINSLIST";

        /// <summary>
        /// 会计科目列表命令名
        /// </summary>
        public const string Common_GLCodeList = "COMMON_GLCODELIST";

        /// <summary>
        /// 解决方案列表命令名
        /// </summary>
        public const string Common_SolutionList = "COMMON_SOLUTIONLIST";

        /// <summary>
        /// 配置列表命令名
        /// </summary>
        public const string Common_ConfigureList = "COMMON_CONFIGURELIST";
       
        /// <summary>
        /// EDI配置列表命令名
        /// </summary>
        public const string Common_EDIConfigureList = "COMMON_EDICONFIGURELIST";

        /// <summary>
        /// Report配置列表命令名
        /// </summary>
        public const string Common_ReportConfigList = "COMMON_REPORTCONFIGLIST";


        /// <summary>
        /// Report配置列表命令名
        /// </summary>
        public const string Common_CooperationCustomer = "Common_CooperationCustomer";

        /// <summary>
        /// 影视项目
        /// </summary>
        public const string Common_MovieProjects = "COMMON_MOVIEPROJECTS";

        /// <summary>
        /// MAC地址管理
        /// </summary>
        public const string Common_AuthCode = "Common_AuthCode";
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
        /// "ID", "Code", "EName", "CName","Type","TradeTermID","TradeTermName"
        /// </summary>
        public static readonly string[] CustomerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName" };

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
        /// 用于费用代码的搜索
        /// </summary>
        public static readonly string[] ChargingCodeResultValue = new string[] { "ChargingCodeID", "Code", "EName", "CName" };
        /// <summary>
        /// 客户银行结果集
        /// </summary>
        public static readonly string[] CustomerBankResultValue = new string[] { "ID" , "AccountName", "AccountNO", "BranchName" };

        public const string VesselVoyage = @"Vessel/Voyage";
        public const string Vessel = @"Vessel";
        public const string Voyage = @"Voyage";

        public const string BookingNO = "BookingNO";
        public const string CodeName = @"Code/Name";
        public const string Name = "Name";
        public const string Code = "Code";
    }
}
