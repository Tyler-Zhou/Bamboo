using Prism.Events;
using Reader.Client.Models;

namespace Reader.Client.Events
{
    /// <summary>
    /// 发布提示(信息)事件
    /// </summary>
    public class TipsEvent : PubSubEvent<TipsModel>
    {
    }
}
