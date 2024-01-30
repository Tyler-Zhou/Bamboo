using System;

namespace ICP.TaskCenter.ServiceInterface
{
    /// <summary>
    /// 用户协助类型
    /// </summary>
    public class UserAssistsType
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 安排人员的Userid
        /// </summary>
        public Guid AssisterId { get; set; }
        /// <summary>
        /// 被协助人员ID
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime FromDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime ToDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateBy { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public Guid UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 工作人员(显示用)
        /// </summary>
        public string Staff { get; set; }

        /// <summary>
        /// 执行操作
        /// </summary>
        public string Operation { get; set; }
    }
}
