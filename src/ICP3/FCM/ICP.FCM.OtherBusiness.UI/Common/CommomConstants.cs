
namespace ICP.FCM.OtherBusiness.UI
{
    /// <summary>
    /// 其他业务-公用常量
    /// </summary>
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
        /// <summary>
        /// 其他业务-电商物流
        /// </summary>
        public const string FCM_OTHERBUSINESS_ECOMMERCE = "fcm_OtherBusiness_ECommerce";
    }
    /// <summary>
    /// 模块常量
    /// </summary>
    public class ModuleCodeConstants
    {
        /// <summary>
        /// 其他业务
        /// </summary>
        public const string OtherBusiness = "OtherBusiness";
    }

    /// <summary>
    /// 
    /// </summary>
    public class OBWorkSpaceConstants
    {
        /// <summary>
        /// 
        /// </summary>
        public const string ToolbarWorkspace = "ToolbarWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string SearchWorkspace = "SearchWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string ListWorkspace = "ListWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string FastSearchWorkspace = "FastSearchWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string BaseEditWorkSpace = "BaseEditWorkSpace";
        /// <summary>
        /// 
        /// </summary>
        public const string ChildWorkspace = "ChildWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string EventListWorkspace = "EventListWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string FaxMailEDIListWorkspace = "FaxMailEDIListWorkspace";
        /// <summary>
        /// 
        /// </summary>
        public const string DocumentListWorkspace = "DocumentListWorkspace";
    }

    /// <summary>
    /// 查询字段常量集合
    /// </summary>
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
        /// <summary>
        /// 业务返回值
        /// </summary>
        public static readonly string[] BusinessResultValue = new string[] { "ID", "No" };

        /// <summary>
        /// 船名/航次
        /// </summary>
        public const string VesselVoyage = @"Vessel/Voyage";
        /// <summary>
        /// 船名
        /// </summary>
        public const string Vessel = @"Vessel";
        /// <summary>
        /// 航次
        /// </summary>
        public const string Voyage = @"Voyage";
        /// <summary>
        /// BookingNO
        /// </summary>
        public const string BookingNO = "BookingNO";
        /// <summary>
        /// 代码名称
        /// </summary>
        public const string CodeName = @"Code/Name";
        /// <summary>
        /// 名称
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// 代码
        /// </summary>
        public const string Code = "Code";
        /// <summary>
        /// 业务号
        /// </summary>
        public const string BusinessNo = "BusinessNo";
    }
}
