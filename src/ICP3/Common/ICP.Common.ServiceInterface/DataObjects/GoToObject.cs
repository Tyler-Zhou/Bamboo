using System;

namespace ICP.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// GoTo窗体使用的实体
    /// </summary>
    public class GoToObject
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 主题单号
        /// </summary>
        public string Mblno { get; set; }
        /// <summary>
        /// 分提单号
        /// </summary>
        public string Hblno { get; set; }
        /// <summary>
        /// 订舱号
        /// </summary>
        public string Sono { get; set; }
        /// <summary>
        /// 订舱单的ID
        /// </summary>
        public Guid OperationId { get; set; }
        /// <summary>
        /// 主提单ID
        /// </summary>
        public Guid Mblid { get; set; }
        /// <summary>
        /// 分提单ID
        /// </summary>
        public Guid Hblid { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo { get; set; }
    }
}
