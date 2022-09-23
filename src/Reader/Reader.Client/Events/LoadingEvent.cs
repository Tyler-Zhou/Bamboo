using Prism.Events;

namespace Reader.Client.Events
{
    /// <summary>
    /// [正在加载]模型
    /// </summary>
    public class LoadingModel
    {
        /// <summary>
        /// 是否打开
        /// </summary>
        public bool IsOpen { get; set; }
    }

    /// <summary>
    /// 发布订阅[正在加载]事件
    /// </summary>
    public class LoadingEvent : PubSubEvent<LoadingModel>
    {

    }
}
