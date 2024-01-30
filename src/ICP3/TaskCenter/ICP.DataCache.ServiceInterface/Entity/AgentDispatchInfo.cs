using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.DataCache.ServiceInterface1
{
    /// <summary>
    /// 海进下载分发文档列表信息实体类
    /// </summary>
    [Serializable]
    public class AgentDispatchInfo
    {
        public string DispatchByName { get; set; }
        public DateTime? DispatchOn { get; set; }
        public string AcceptByName { get; set; }
        public DateTime? AcceptOn { get; set; }
        public string AssignToName { get; set; }
    }
}
