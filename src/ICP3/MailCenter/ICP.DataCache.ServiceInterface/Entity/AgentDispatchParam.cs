using ICP.FileSystem.ServiceInterface;
using System;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 分发文档参数类
    /// </summary>
    [Serializable]
    public class AgentDispatchParam
    {
        /// <summary>
        /// 
        /// </summary>
        public DocumentState DocumentState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid AssignTo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid OceanAgentDispatchId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid LoginId { get; set; }
    }
}
