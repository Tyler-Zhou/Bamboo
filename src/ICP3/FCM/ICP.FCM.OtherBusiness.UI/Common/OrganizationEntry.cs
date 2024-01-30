#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2018/3/13 星期二 14:08:33
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.FCM.OtherBusiness.UI.Common
{
    /// <summary>
    /// 组织架构实体
    /// </summary>
    public class OrganizationEntry
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string EName { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        public string CName { get; set; }
    }
}
