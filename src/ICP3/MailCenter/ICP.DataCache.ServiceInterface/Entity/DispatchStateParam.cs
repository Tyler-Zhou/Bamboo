using ICP.FileSystem.ServiceInterface;
using System;
using System.Collections.Generic;

namespace ICP.DataCache.ServiceInterface
{
    /// <summary>
    /// 分发状态参数类
    /// </summary>    
    [Serializable]
    public class DispatchStateParam
    {
        /// <summary>
        /// 
        /// </summary>
        public DispatchStateParam(){}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="documentIds"></param>
        /// <param name="states"></param>
        /// <param name="updateDates"></param>
        public DispatchStateParam(List<Guid> documentIds, List<DocumentState?> states, List<DateTime?> updateDates)
        {
            DocumentIds = documentIds;
            States = states;
            UpdateDates = updateDates;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Guid> DocumentIds { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DocumentState?> States { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<DateTime?> UpdateDates { get; set; }
    }
}
