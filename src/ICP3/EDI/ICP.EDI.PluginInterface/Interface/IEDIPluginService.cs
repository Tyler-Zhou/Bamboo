using System.Collections.Generic;

namespace ICP.EDI.PluginInterface
{
    /// <summary>
    /// EDI插件服务
    /// </summary>
    public interface IEDIPluginService
    {
        /// <summary>
        /// 构建EDI数据
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="target">输出目标</param>
        void BuildData(EDIPluginInput source, EDIPluginOut target);
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="values"></param>
        void SendData(IDictionary<string, object> values);
    }
}
