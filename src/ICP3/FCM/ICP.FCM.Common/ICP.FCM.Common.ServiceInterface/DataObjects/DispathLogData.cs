using System;

namespace ICP.FCM.Common.ServiceInterface.DataObjects
{
    /// <summary>
    /// 分文件日志
    /// </summary>
    public class DispathLogData
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 业务id
        /// </summary>
        public Guid OperationID { get; set; }

        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public byte OperationType { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 签收人
        /// </summary>
        public Guid? AcceptBy { get; set; }

        /// <summary>
        /// 签收时间
        /// </summary>
        public DateTime? AcceptDate { get; set; }

        /// <summary>
        /// 是否已经COPY 到对方服务器
        /// </summary>
        public bool IsTransTo { get; set; }

        /// <summary>
        /// 是否签收
        /// </summary>
        public byte State { get; set; }
    }
}
