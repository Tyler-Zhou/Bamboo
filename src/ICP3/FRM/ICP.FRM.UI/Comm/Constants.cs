#region Comment

/*
 * 
 * FileName:    Constants.cs
 * CreatedOn:   
 * CreatedBy:   
 * 
 * 
 * Description：
 *      ->公用常量
 * History：
 *      ->
 * 
 * 
 * 
 */

#endregion

using System.Drawing;

namespace ICP.FRM.UI
{
    /// <summary>
    /// 模块常量
    /// </summary>
    public class FunctionConstants
    {
        /// <summary>
        /// 海运运价列表
        /// </summary>
        public const string FRM_OCEANPRICELIST = "FRM_OCEANPRICELIST";

        /// <summary>
        /// 商务周报表列表
        /// </summary>
        public const string FRM_BUSINESSWEEKLYREPORT = "FRM_BUSINESSWEEKLYREPORT";

        /// <summary>
        /// 运价
        /// </summary>
        public const string FRM_SEARCHRATE = "FRM_SEARCHRATE";

        /// <summary>
        /// 询价
        /// </summary>
        public const string FRM_InquireRates = "FRM_INQUIRERATES";

        /// <summary>
        /// 订舱统计报表
        /// </summary>
        public const string FRM_BookingReport = "FRM_BOOKINGREPORT";
        /// <summary>
        /// 利润配比
        /// </summary>
        public const string FRM_ProfitRatios = "FRM_ProfitRatios";
    }

    /// <summary>
    /// 海运出口动作项常量
    /// </summary>
    public class ActionsConstants
    {
        /// <summary>
        /// 维护所有人的运价.在维护运价的查询面板中用到
        /// </summary>
        public const string FRM_EDITALLCONTRACT = "FRM_EDITALLCONTRACT";
    }
    /// <summary>
    /// UI界面常量
    /// </summary>
    public class UIConstants
    { 
        public static Color IsDirtyColor = Color.Red;

        public static char DividedSymbol =';';

        /// <summary>
        /// 装运模板代码
        /// </summary>
        public const string ShipmentTemplateCode = "Shipment4InquirePrices";
    }
    /// <summary>
    /// 查询字段常量
    /// </summary>
    public class SearchFieldConstants
    {
        /// <summary>
        /// 返回字段
        /// </summary>
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName" };
        /// <summary>
        /// 船名(返回字段)
        /// </summary>
        public static readonly string[] VesselResultValue = new string[] { "ID", "No", "VesselName" };
        /// <summary>
        /// 船名/航线
        /// </summary>
        public const string VesselVoyage = @"Vessel/Voyage";
        /// <summary>
        /// 船名
        /// </summary>
        public const string Vessel = @"Vessel";
        /// <summary>
        /// 航线
        /// </summary>
        public const string Voyage = @"Voyage";
        /// <summary>
        /// 代码/名称
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
    }
}
