using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 账单转换保存对象
    /// </summary>
    [Serializable]
    public class SaveRequestBillConvert:SaveRequest
    {
        /// <summary>
        /// 帐单ID
        /// </summary>
        public Guid BillID { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveByID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
