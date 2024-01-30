using ICP.FAM.ServiceInterface;
using ICP.Framework.CommonLibrary.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceComponent.JSONObjects
{
    #region CSP账单状态
    /// <summary>
    /// CSP账单状态
    /// </summary>
    [Serializable]
    public class BillStatusForCSPAPI
    {
        /// <summary>
        /// 唯一键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 新状态
        /// </summary>
        public CSP_BILL_STATUS newStatus { get; set; }
        
    }
    #endregion

    #region CSP账单状态-单个
    /// <summary>
    /// CSP账单状态-单个
    /// </summary>
    [Serializable]
    public class BillStatusForCSPAPIItem : PlatformResponseContent<BillStatusForCSPAPI>
    {
    }
    #endregion

    #region CSP账单状态状态-列表
    /// <summary>
    /// CSP账单状态-列表
    /// </summary>
    [Serializable]
    public class BillStatusForCSPAPIList : PlatformResponseBaseList<List<BillStatusForCSPAPI>>
    {
    }
    #endregion
}
