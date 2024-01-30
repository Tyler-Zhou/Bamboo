namespace ICP.FCM.Common.UI
{
    /// <summary>
    /// 模块常量
    /// </summary>
    public class CommandConstants
    {
        /// <summary>
        /// 申请代理列表命令名
        /// </summary>
        public const string OceanExport_AgentRequestList = "OCEANEXPORT_AGENTREQUESTLIST";
    }

    public class SearchFieldConstants
    {
        public const string CodeName = @"Code/Name";

        /// <summary>
        /// "ID", "Code", "EName", "CName","Type","TradeTermID","TradeTermName","State","CheckedState"
        /// </summary>
        public static readonly string[] CustomerResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "TradeTermID", "TradeTermName", "State", "CheckedState" };

        /// <summary>
        ///  "ID", "Code", "EName", "CName", "Type", "State","CheckedState"
        ///  适用于搜索“客户”
        /// </summary>
        public static readonly string[] ResultValue = new string[] { "ID", "Code", "EName", "CName", "Type", "State", "CheckedState" };

    }
}
