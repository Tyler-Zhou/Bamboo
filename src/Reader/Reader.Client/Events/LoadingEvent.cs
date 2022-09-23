using Prism.Events;
using Reader.Client.Models;

namespace Reader.Client.Events
{
    /// <summary>
    /// 发布订阅[正在加载]事件
    /// </summary>
    public class LoadingEvent : PubSubEvent<LoadingModel>
    {

    }
}
