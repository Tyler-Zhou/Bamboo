#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/7/20 星期五 15:08:20
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FCM.Common.ServiceInterface
{
    /// <summary>
    /// 用于保存关联电商业务的对象
    /// </summary>
    [Serializable]
    public class ECommerceSaveRequest : SaveRequest
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 是否关联
        /// </summary>
        public bool[] IsAssociateds { get; set; }

        /// <summary>
        /// 关联ID
        /// </summary>
        public Guid[] AssociationIDs { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate;
    }
}
