using System;
using ICP.Message.ServiceInterface;

namespace ICP.OA.ServiceInterface
{
    /// <summary>
    /// 传真查询条件实体类
    /// </summary>
    [Serializable]
    public class MailQuery
    {
        public MailQuery() 
        {
            this.IsFaxHall = false;
        }

        /// <summary>
        /// 文件夹Id
        /// </summary>
        public Guid? FolderId { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否包含附件
        /// </summary>
        public bool? IncludeAttachment { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public MessagePriority? Priority { get; set; }
        /// <summary>
        /// 标志
        /// </summary>
        public MessageFlag? Flag { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? FromTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ToTime { get; set; }

        /// <summary>
        /// 邮件列表是否选择传真大厅的数据源
        /// </summary>
        public bool IsFaxHall { get; set; }

    }
}
