using System;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 邮件中心回调参数
    /// </summary>
    [Serializable]
    public class MessageParameter
    {
        /// <summary>
        /// 邮件中心传递的消息参数
        /// </summary>
        public Message Message { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        public ActionType ActionType { get; set; }
    }
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// 发送
        /// </summary>
        Send,
        /// <summary>
        /// 自动发送
        /// </summary>
        AutoSend,
        /// <summary>
        /// 回复
        /// </summary>
        Reply,
        /// <summary>
        /// 重发
        /// </summary>
        Resend,
        /// <summary>
        /// 打开
        /// </summary>
        Open,
        /// <summary>
        /// 转发
        /// </summary>
        Forward

    }
}
