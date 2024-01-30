#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/6 16:48:11
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.Monitor.Model.Framework
{
    /// <summary>
    /// 基础查询参数
    /// </summary>
    [Serializable]
    public class BaseSearchParam
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool? IsValid { get; set; }
    }
}
