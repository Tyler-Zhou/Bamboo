using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceComponent.JSONObjects
{
    #region CSP账单
    /// <summary>
    /// CSP账单
    /// </summary>
    [Serializable]
    public class BillForCSPAPI
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int customerId { get; set; }
        /// <summary>
        /// 账单号
        /// </summary>
        public string billNo { get; set; }
        /// <summary>
        /// 业务ID
        /// </summary>
        public int shipmentId { get; set; }
        /// <summary>
        /// 提单ID
        /// </summary>
        public int shipmentItemId { get; set; }
        /// <summary>
        /// 账单日期
        /// </summary>
        public string issuedTime { get; set; }
        /// <summary>
        /// 逾期日期
        /// </summary>
        public string dueTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string description { get; set; }
    } 
    #endregion

    #region CSP账单-单个
    /// <summary>
    /// CSP账单-单个
    /// </summary>
    [Serializable]
    public class BillForCSPAPIItem : PlatformResponseContent<BillForCSPAPI>
    {
    }
    #endregion

    #region CSP账单-列表
    /// <summary>
    /// CSP账单-列表
    /// </summary>
    [Serializable]
    public class BillForCSPAPIList : PlatformResponseBaseList<List<BillForCSPAPI>>
    {
    }
    #endregion
}
