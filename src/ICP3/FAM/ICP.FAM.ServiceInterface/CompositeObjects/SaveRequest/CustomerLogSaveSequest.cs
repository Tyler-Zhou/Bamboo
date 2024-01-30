using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceInterface.DataObjects.SaveRequests
{
    /// <summary>
    /// 客户日志保存类
    /// </summary>
    [Serializable]
    public class CustomerLogSaveSequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public byte CustomerMark { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public byte Priority { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public byte Type { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public Guid[] LogAttsIds { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public Guid SaveBy { get; set; }
    }
}
