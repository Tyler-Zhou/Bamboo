#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/23 10:35:07
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.Model.ComputerManage
{
    /// <summary>
    /// 驱动信息
    /// </summary>
    [Serializable]
    public class EDriveInfo : BaseDataObject
    {
        /// <summary>
        /// 驱动名称
        /// </summary>
        public string DriveName { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string DriveType { get; set; }
        /// <summary>
        /// 容量
        /// </summary>
        public float Capacity { get; set; }
        /// <summary>
        /// 可用空间
        /// </summary>
        public long FeeSpace { get; set; }
    }
}
