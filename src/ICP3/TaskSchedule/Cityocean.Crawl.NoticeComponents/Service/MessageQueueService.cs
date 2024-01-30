#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/3/24 10:33:51
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
using System.Messaging;
using System.Threading;
using Cityocean.Crawl.CommonLibrary;
using Cityocean.Crawl.LogComponents;

namespace Cityocean.Crawl.NoticeComponents
{
    /// <summary>
    /// 队列消息服务
    /// </summary>
    public sealed class MessageQueueService : IMessageQueueService
    {
        /// <summary>
        /// 设置队列启用
        /// </summary>
        public bool SetQueueEnabled()
        {
            return true;
        }

        /// <summary>
        /// 检查WebCrawlerMessage队列路径,不存在则添加
        /// </summary>
        public void CreateQueuePath()
        {
            CreateQueue();
        }

        /// <summary>
        /// 检查WebCrawlerMessage队列路径,不存在则添加
        /// </summary>
        private MessageQueue CreateQueue()
        {
            MessageQueue MQueue;
            if (!MessageQueue.Exists(CommonConstants.MESSAGE_QUEUE_PATH))
            {
                MQueue = MessageQueue.Create(CommonConstants.MESSAGE_QUEUE_PATH);
                MQueue.SetPermissions("Administrators", MessageQueueAccessRights.FullControl);
                MQueue.Label = "数据抓取数据队列：通知数据抓取客户端";
            }
            else
                MQueue = new MessageQueue(CommonConstants.MESSAGE_QUEUE_PATH);
            return MQueue;
        }

        /// <summary>
        /// 移除WebCrawlerMessage队列路径
        /// </summary>
        public void RemoveQueuePath()
        {
            if (MessageQueue.Exists(CommonConstants.MESSAGE_QUEUE_PATH))
            {
                MessageQueue.Delete(CommonConstants.MESSAGE_QUEUE_PATH);
            }
        }

        /// <summary>
        /// 获取所有的信息
        /// </summary>
        /// <returns></returns>
        public List<EMessageInfo> GetNewMessageInfo()
        {
            List<EMessageInfo> results = new List<EMessageInfo>();
            MessageQueue MQueue = CreateQueue();
            //一次读一条,取一条自动去掉读取的这一条
            //System.Messaging.Message Msg = MQueue.Receive(new TimeSpan(0, 0, 2));
            //一次读取全部消息,但是不去除读过的消息
            Message[] Msg = MQueue.GetAllMessages();
            //删除所有消息
            MQueue.Purge();

            foreach (Message m in Msg)
            {
                //XML格式化传输量较大
                //Msg.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(string) });
                m.Formatter = new BinaryMessageFormatter();
                EMessageInfo mInfo = (EMessageInfo)m.Body;
                if (mInfo != null && !mInfo.MOwerJob.IsNullOrEmpty() && !mInfo.MContent.IsNullOrEmpty())
                    results.Add(mInfo);
            }
            return results;
        }

        /// <summary>
        /// 在[MessageQueue]中写入日志信息
        /// </summary>
        /// <param name="paramOwerJob">消息所属</param>
        /// <param name="paramMessageContent">消息内容</param>
        public void SendInfoToQueue(string paramOwerJob, string paramMessageContent)
        {
            SendMessage(paramOwerJob, 1, paramMessageContent, null);
        }

        /// <summary>
        /// 在[MessageQueue]中写入警告信息
        /// </summary>
        /// <param name="paramOwerJob">消息所属</param>
        /// <param name="paramMessageContent">消息内容</param>
        public void SendWarnToQueue(string paramOwerJob, string paramMessageContent)
        {
            SendMessage(paramOwerJob, 2, paramMessageContent, null);
        }

        /// <summary>
        /// 在[MessageQueue]中写入错误信息
        /// </summary>
        /// <param name="paramOwerJob">消息所属</param>
        /// <param name="paramMessageContent">消息内容</param>
        /// <param name="ex">异常信息</param>
        public void SendErrorToQueue(string paramOwerJob, string paramMessageContent, Exception ex)
        {
            SendMessage(paramOwerJob, 3, paramMessageContent, ex);
        }

        /// <summary>
        /// 另开进程写[Message]到[MessageQueue]
        /// </summary>
        /// <param name="paramOwerJob">消息所属任务</param>
        /// <param name="paramMessageType">消息类型</param>
        /// <param name="paramMessageContent">消息内容</param>
        /// <param name="paramException">异常信息</param>
        void SendMessage(string paramOwerJob, int paramMessageType, string paramMessageContent, Exception paramException)
        {
            try
            {
                EMessageInfo messageInfo = new EMessageInfo { MType = paramMessageType, MOwerJob = paramOwerJob, MContent = paramMessageContent };
                if (MessageQueue.Exists(CommonConstants.MESSAGE_QUEUE_PATH))
                {
                    ParameterizedThreadStart paramThreadStart = SendMessage;
                    Thread m_thread1 = new Thread(paramThreadStart) { IsBackground = true };
                    m_thread1.Start(messageInfo);
                }
            }
            catch (Exception ex)
            {
                LogService.Error("","",ex);
            }
        }

        /// <summary>
        /// 发送消息到[MessageQueue]
        /// </summary>
        /// <param name="messageBody"></param>
        void SendMessage(object messageBody)
        {
            try
            {
                MessageQueue MQueue = new MessageQueue(CommonConstants.MESSAGE_QUEUE_PATH);
                Message Msg = new Message { Body = messageBody, Formatter = new BinaryMessageFormatter() };
                //XML格式化传输量较大
                MQueue.Send(Msg);
            }
            catch (Exception ex)
            {
                LogService.Error("","",ex);
            }
        }
    }
}
