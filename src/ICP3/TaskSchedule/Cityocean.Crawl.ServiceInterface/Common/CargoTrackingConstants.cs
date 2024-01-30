#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/31 10:25:31
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
    /// 货物跟踪服务常量
    /// </summary>
    public sealed class CargoTrackingConstants
    {
        /// <summary>
        /// 代码前缀
        /// </summary>
        public const string CODEPREFIX = "CT";
        /// <summary>
        /// 预计时间之前天数(EstimatedTime Before Days)
        /// </summary>
        public const string CONFIG_ESTBEFOREDAYS = "EstBeforeDays";
        /// <summary>
        /// 预计交货时间范围(Expected Time of Delivery Range)
        /// </summary>
        public const string CONFIG_ETDRANGE = "ETDRange";
        /// <summary>
        /// 事件时间
        /// </summary>
        public const string CELL_EVENTDATETIME = "EventDateTime";
        /// <summary>
        /// 事件日期
        /// </summary>
        public const string CELL_EVENTDATE = "EventDate";
        /// <summary>
        /// 事件时间
        /// </summary>
        public const string CELL_EVENTTIME = "EventTime";
        /// <summary>
        /// 事件描述
        /// </summary>
        public const string CELL_STATEDESCRIPTION = "StateDescription";
        /// <summary>
        /// 事件地点
        /// </summary>
        public const string CELL_STATION = "Station";
        /// <summary>
        /// 事件船名
        /// </summary>
        public const string CELL_VESSELNAME = "VesselName";
        /// <summary>
        /// 事件航次
        /// </summary>
        public const string CELL_VOYAGENUMBER = "VoyageNumber";
        /// <summary>
        /// 是否预计动态
        /// </summary>
        public const string CELL_EST = "Est";
        /// <summary>
        /// 运输模式
        /// </summary>
        public const string CELL_MODEL = "Model";

    }
}
