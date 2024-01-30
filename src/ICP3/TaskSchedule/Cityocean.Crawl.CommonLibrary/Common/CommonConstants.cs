#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/7 16:13:28
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

namespace Cityocean.Crawl.CommonLibrary
{
    /// <summary>
    /// 通用常量
    /// </summary>
    public sealed class CommonConstants
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        public const string MODULENAME_SERVICECONFIG = "ServiceConfig";
        /// <summary>
        /// 爬虫工具
        /// </summary>
        public const string MODULENAME_CRAWLTOOL = "CrawlTool";
        /// <summary>
        /// 调度中心
        /// </summary>
        public const string MODULENAME_SCHEDULERCENTER = "SchedulerCenter";
        /// <summary>
        /// 空白页
        /// </summary>
        public const string BLANK_PAGE ="about:blank";
        /// <summary>
        /// 消息队列路径
        /// </summary>
        public const string MESSAGE_QUEUE_PATH = ".\\private$\\WebCrawlerMessage";
        /// <summary>
        /// 数据分割符号
        /// </summary>
        public const string DIVIDED_SYMBOL = "&#;";
        /// <summary>
        /// 保存到数据库时间格式字符串
        /// </summary>
        public const string DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string INI_CONFIG_NAME = "ICPWebCrawler.ini";
        /// <summary>
        /// 邮箱帐号
        /// </summary>
        public const string CONFIG_MAILUSERNAME = "MailAccount";
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public const string CONFIG_MAILPASSWORD = "MailPassword";
        /// <summary>
        /// Smtp服务器
        /// </summary>
        public const string CONFIG_SMTPSERVER = "SMTP";
        /// <summary>
        /// 发件人
        /// </summary>
        public const string CONFIG_MAILFROM = "MailFrom";
        /// <summary>
        /// 收件人
        /// </summary>
        public const string CONFIG_MAILTO = "MailTo";
        /// <summary>
        /// 抄送
        /// </summary>
        public const string CONFIG_MAILCC = "MailCC";
        /// <summary>
        /// 抓取程序名称
        /// </summary>
        public const string CONFIG_CRAWLPROCESSS = "CrawlProcesss";
        /// <summary>
        /// 爬虫异常
        /// </summary>
        public const string CONFIG_CRAWLEXCEPTION = "CrawlException";
        /// <summary>
        /// 爬虫可忽略异常(不停止抓取数据)
        /// </summary>
        public const string CONFIG_CRAWLIGNOREXCEPTION = "CrawlIgnoreException";
    }
}
