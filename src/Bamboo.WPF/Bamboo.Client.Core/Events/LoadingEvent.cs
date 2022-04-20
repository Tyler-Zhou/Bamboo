using Bamboo.Client.Core.Models;
using Prism.Events;

namespace Bamboo.Client.Core.Events
{

    /// <summary>
    /// 发布订阅[正在加载]事件
    /// </summary>
    public class LoadingEvent : PubSubEvent<LoadingModel>
    {

    }
}
