using System;
using System.Collections.Generic;
using System.Text;

namespace LongWin.BusinessInfo.ServiceInterface.DataObject
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public  class OperationSearchKey
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid CompanyId { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class OperationSearchResult
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid OperationId { get; set; }
    }
}
