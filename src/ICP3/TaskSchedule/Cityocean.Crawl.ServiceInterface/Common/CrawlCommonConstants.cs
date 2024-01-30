#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/12/21 星期四 09:13:51
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion


namespace Cityocean.Crawl.ServiceInterface
{
    /// <summary>
    /// 抓取通用常量
    /// </summary>
    public sealed class CrawlCommonConstants
    {
        /// <summary>
        /// 码头船期
        /// </summary>
        public const string MODULENAME_TERMINALVESSELSCHEDULE = "TerminalVesselSchedule";
        /// <summary>
        /// 货物跟踪
        /// </summary>
        public const string MODULENAME_CARGOTRACKING = "CargoTracking";
        /// <summary>
        /// 码头箱状态
        /// </summary>
        public const string MODULENAME_TERMINALCONTAINERSTATUS = "TerminalContainerStatus";
        /// <summary>
        /// 最后执行批次
        /// </summary>
        public const string LAST_BATCHNO = "LastBatchNO";
        /// <summary>
        /// 批量保存发生异常的批次号
        /// </summary>
        public const string EXCEPTION_BATCHNO = "ExceptionBatchNO";
    }
}
