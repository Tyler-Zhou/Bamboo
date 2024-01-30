using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;

namespace ICP.DataCache.ServiceInterface1
{
    /// <summary>
    /// 分发文档参数类
    /// </summary>
    /// 
    [Serializable]
    public class AgentDispatchParam
    {
        public DocumentState DocumentState { get; set; }
        public Guid AssignTo { get; set; }
        public string Name { get; set; }
        public Guid OceanAgentDispatchId { get; set; }
        public Guid LoginId { get; set; }
    }
}
