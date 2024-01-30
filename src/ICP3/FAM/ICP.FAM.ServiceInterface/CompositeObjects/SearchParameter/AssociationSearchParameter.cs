using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    [Serializable]
    public class AssociationSearchParameter
    {
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid BankTransactionID { get; set; }
        /// <summary>
        /// 操作口岸
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 销账单号
        /// </summary>
        public string WriteOffNO { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveByID { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}
