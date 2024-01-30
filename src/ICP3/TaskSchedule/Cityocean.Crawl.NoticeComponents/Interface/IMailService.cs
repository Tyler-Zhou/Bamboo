#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/5/2 15:09:09
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace Cityocean.Crawl.NoticeComponents
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="paramReportInfo">报告对象</param>
        void SendEMail(EReportInfo paramReportInfo);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="paramOwerJob">所属任务</param>
        /// <param name="paramMessageContent">消息内容</param>
        /// <param name="paramException">异常信息</param>
        void SendEMail(string paramOwerJob, string paramMessageContent, Exception paramException);
    }
}
