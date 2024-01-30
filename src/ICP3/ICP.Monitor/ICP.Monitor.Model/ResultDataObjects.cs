#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/6 11:55:53
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.Monitor.Model
{
    /// <summary>
    /// 返回结果
    /// </summary>
    [Serializable]
    public class EResultInfo
    {
        /// <summary>
        /// 方法返回值
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public CustomException Exception { get; set; } 
    }
}
