#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/6/16 17:07:47
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

namespace Cityocean.Crawl.CommonTool
{
    /// <summary>
    /// 爬虫停止事件参数
    /// </summary>
    public sealed class ParamStop
    {
        /// <summary>
        /// 任务对象
        /// </summary>
        public dynamic TaskObject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paramTaskObject">任务对象</param>
        public ParamStop(dynamic paramTaskObject)
        {
            TaskObject = paramTaskObject;
        }
    }
}
