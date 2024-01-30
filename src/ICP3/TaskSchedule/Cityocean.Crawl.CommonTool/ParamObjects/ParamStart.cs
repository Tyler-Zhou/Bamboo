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

using OpenQA.Selenium;

namespace Cityocean.Crawl.CommonTool
{
    /// <summary>
    /// 爬虫抓取数据参数
    /// </summary>
    public sealed class ParamStart
    {
        /// <summary>
        /// 网页驱动器
        /// </summary>
        public IWebDriver WebDriver { get; set; }
        /// <summary>
        /// 任务对象
        /// </summary>
        public dynamic TaskObject { get; set; }
        /// <summary>
        /// 任务配置
        /// </summary>
        public dynamic TaskConfig { get; set; }

        /// <summary>
        /// 爬虫抓取数据参数
        /// </summary>
        /// <param name="paramWebDriver">网页驱动器</param>
        /// <param name="paramTaskObject">任务对象</param>
        /// <param name="paramTaskConfig">任务配置</param>
        public ParamStart(IWebDriver paramWebDriver,dynamic paramTaskObject, dynamic paramTaskConfig)
        {
            WebDriver = paramWebDriver;
            TaskObject = paramTaskObject;
            TaskConfig = paramTaskConfig;
        }
    }
}
