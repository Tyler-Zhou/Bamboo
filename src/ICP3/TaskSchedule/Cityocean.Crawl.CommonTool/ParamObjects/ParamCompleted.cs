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

using System;
using OpenQA.Selenium;

namespace Cityocean.Crawl.CommonTool
{
    /// <summary>
    /// 爬虫完成事件参数
    /// </summary>
    public sealed class ParamCompleted
    {
        /// <summary>
        /// URL路径
        /// </summary>
        public string URLAddress
        {
            get
            {
                string urlAddress = string.Empty;

                try
                {
                    urlAddress = WebDriver.Url;
                }
                catch (Exception)
                {
                    urlAddress = string.Empty;
                }
                return urlAddress;
            }
        }
        /// <summary>
        /// 页面源代码
        /// </summary>
        public string PageSource
        {
            get { return WebDriver.GetPageSource(); }
        }

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
        /// 爬虫执行时间
        /// </summary>
        public long Milliseconds { get; private set; }
        /// <summary>
        /// 爬虫完成事件参数
        /// </summary>
        /// <param name="paramWebDriver">网页驱动器</param>
        /// <param name="paramTaskObject">任务对象</param>
        /// <param name="paramTaskConfig">任务配置</param>
        /// <param name="paramMilliseconds">爬虫执行时间</param>
        public ParamCompleted(IWebDriver paramWebDriver, dynamic paramTaskObject, dynamic paramTaskConfig, long paramMilliseconds)
        {
            WebDriver = paramWebDriver;
            TaskObject = paramTaskObject;
            TaskConfig = paramTaskConfig;
            Milliseconds = paramMilliseconds;
        }
    }
}
