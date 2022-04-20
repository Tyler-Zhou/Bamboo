using Bamboo.Client.Core.Model;
using Prism.Events;

namespace Bamboo.Client.Core.Events
{
    /// <summary>
    /// 发布订阅消息事件
    /// </summary>
    public class MessageEvent : PubSubEvent<MessageModel>
    {
    }
}
