using System;

namespace ICP.WF.ServiceInterface.DataObject
{
    /// <summary>
    /// 队列项数据对象
    /// </summary>
    [Serializable]
    public class EnqueueItem
    {
        public EnqueueItem()
        {
        }

        public EnqueueItem(Guid taskId, bool opinion, Guid[] participants)
        {
            TaskId = taskId;
            Opinion = opinion;
            Participants = participants;
        }

        /// <summary>
        /// 任务id
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// 审批意见 
        /// </summary>
        public bool Opinion { get; set; }

        /// <summary>
        /// 下步可以审批的人列表
        /// </summary>
        public Guid[] Participants{get;set;}

        /// <summary>
        /// 队列项类型
        /// </summary>
        public EnqueueItemType Type { get; set; }
    }

    /// <summary>
    /// 队列项类型
    /// </summary>
    [Serializable]
    public enum EnqueueItemType
    {
        /// <summary>
        ///审批 
        /// </summary>
        Approve,

        /// <summary>
        /// 执行负责人
        /// </summary>
        Participants,

        /// <summary>
        /// 异常情况
        /// </summary>
        Exception
    }
}
