using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FCM.Common.ServiceInterface.CompositeObjects
{
    #region CSP运单提单箱保存对象
    /// <summary>
    /// CSP运单提单箱保存对象
    /// </summary>
    [Serializable]
    public class SaveRequestShipmentContainerForCSP : SaveRequest
    {
        #region 业务ID
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        #endregion

        #region 业务类型
        /// <summary>
        /// 业务类型
        /// </summary>
        public OperationType OperationType { get; set; }
        #endregion

        #region 业务箱ID
        /// <summary>
        /// 业务箱ID
        /// </summary>
        public Guid[] OperationContainerIDs { get; set; }
        #endregion

        #region 更新人
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveBy { get; set; }
        #endregion

        #region 更新时间
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
        #endregion
    }
    #endregion
}
