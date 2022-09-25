using Prism.Events;

namespace Reader.Client.Events
{
    /// <summary>
    /// [任务]模型
    /// </summary>
    public class TaskModel
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 发布订阅[任务]事件
    /// </summary>
    public class TaskEvent : PubSubEvent<TaskModel>
    {

    }
}
