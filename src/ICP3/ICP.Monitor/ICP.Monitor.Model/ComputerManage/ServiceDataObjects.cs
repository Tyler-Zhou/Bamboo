#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/12 15:46:22
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
    /// Windows 服务实体
    /// </summary>
    [Serializable]
    public class EWindowsServiceInfo : BaseDataObject
    {
        /// <summary>
        /// 选择
        /// </summary>
        public bool Choosed { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// 服务状态
        /// </summary>
        public string ServiceState { get; set; }
    }
}
