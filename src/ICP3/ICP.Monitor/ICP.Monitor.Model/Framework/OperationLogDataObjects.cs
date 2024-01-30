#region Comment
/*
 * Create By:Taylor Zhou
 * Create On:2017/1/6 11:25:09
 *
 * Description:
 *         ->操作日志实体
 *
 * History:
 *         ->
 */
#endregion

using System;

namespace ICP.Monitor.Model.Framework
{
    /// <summary>
    /// 操作日志信息
    /// </summary>
    [Serializable]
    public class EOperationLogInfo : BaseDataObject
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string OperationName { get; set; }
        /// <summary>
        /// 操作描述
        /// </summary>
        public string OperationContent { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
        /// <summary>
        /// 操作时间间隔(ms)
        /// </summary> 
        public int OperationDuration { get; set; }
        /// <summary>
        /// 操作步骤1
        /// </summary>
        public string OperationSteps1 { get; set; }
        /// <summary>
        /// 操作步骤2
        /// </summary>
        public string OperationSteps2 { get; set; }
        /// <summary>
        /// 操作步骤3
        /// </summary>
        public string OperationSteps3 { get; set; }
        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 加载的程序集名称
        /// </summary>
        public string AssemblyNames { get; set; }
    }

    /// <summary>
    /// 操作日志查询参数
    /// </summary>
    [Serializable]
    public class OperationLogSearchParam : BaseSearchParam
    {
        public string UserCode { get; set; }

        public string UserDepartment { get; set; }
    }
}
