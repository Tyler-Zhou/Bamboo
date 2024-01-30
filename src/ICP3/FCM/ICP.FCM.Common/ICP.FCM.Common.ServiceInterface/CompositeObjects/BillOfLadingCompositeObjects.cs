using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FCM.Common.ServiceInterface.CompositeObjects
{
    #region 提单保存对象
    /// <summary>
    /// 提单保存对象
    /// </summary>
    [Serializable]
    public class SaveRequestBillOfLading : SaveRequest
    {
        #region ID
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }
        #endregion

        #region 业务ID
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
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

    /// <summary>
    /// 提单状态保存对象
    /// </summary>
    [Serializable]
    public class SaveRequestBLState : SaveRequest
    {
        /// <summary>
        /// 分提单ID
        /// </summary>
        public Guid[] HBLIDs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BLAMSState AMSState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid SaveBy { get; set; }
    }
}
