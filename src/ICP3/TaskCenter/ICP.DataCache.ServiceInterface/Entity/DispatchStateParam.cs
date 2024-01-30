using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICP.DataCache.ServiceInterface1;

namespace ICP.DataCache.ServiceInterface1
{
    /// <summary>
    /// 分发状态参数类
    /// </summary>    
    [Serializable]
    public class DispatchStateParam
    {
        public DispatchStateParam(){}

        public DispatchStateParam(List<Guid> documentIds, List<DocumentState?> states, List<DateTime?> updateDates)
        {
            this.DocumentIds = documentIds;
            this.States = states;
            this.UpdateDates = updateDates;
        }

        public List<Guid> DocumentIds { get; set; }
        public List<DocumentState?> States { get; set; }
        public List<DateTime?> UpdateDates { get; set; }
    }
}
