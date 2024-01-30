#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/4/19 15:26:23
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;

namespace Cityocean.Crawl.NoticeComponents
{
    /// <summary>
    /// 队列消息服务
    /// </summary>
    public interface IMessageQueueService
    {
        /// <summary>
        /// 设置队列启用
        /// </summary>
        bool SetQueueEnabled();

        /// <summary>
        /// 检查WebCrawlerMessage队列路径,不存在则添加
        /// </summary>
        void CreateQueuePath();

        /// <summary>
        /// 移除WebCrawlerMessage队列路径
        /// </summary>
        void RemoveQueuePath();

        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <returns></returns>
        List<EMessageInfo> GetNewMessageInfo();

        /// <summary>
        /// 在[MessageQueue]中写入日志信息
        /// </summary>
        /// <param name="paramOwerJob">消息所属</param>
        /// <param name="msg">消息内容</param>
        void SendInfoToQueue(string paramOwerJob, string msg);

        /// <summary>
        /// 在[MessageQueue]中写入警告信息
        /// </summary>
        /// <param name="paramOwerJob">消息所属</param>
        /// <param name="msg">消息内容</param>
        void SendWarnToQueue(string paramOwerJob, string msg);

        /// <summary>
        /// 在[MessageQueue]中写入错误信息
        /// </summary>
        /// <param name="paramOwerJob">消息所属</param>
        /// <param name="msg">消息内容</param>
        /// <param name="ex">异常信息</param>
        void SendErrorToQueue(string paramOwerJob, string msg, Exception ex);
    }
}
