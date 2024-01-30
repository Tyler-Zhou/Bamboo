#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/22 17:54:24
 *
 * Description:
 *         ->
 *
 * History:
 *         ->
 */
#endregion

using System;
using System.Collections.Generic;
using ICP.Monitor.Model.Framework;

namespace ICP.Monitor.Model.ComputerManage
{
    /// <summary>
    /// 服务器实体
    /// </summary>
    [Serializable]
    public class EServerInfo : BaseDataObject
    {
        public EServerInfo()
        {
            Drives = new List<EDriveInfo>();
        }
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// CPU信息
        /// </summary>
        public string CPU { get; set; }
        /// <summary>
        /// 内存
        /// </summary>
        public string Memory { get; set; }
        /// <summary>
        /// 驱动集合
        /// </summary>
        public List<EDriveInfo> Drives { get; set; }
    }
}
