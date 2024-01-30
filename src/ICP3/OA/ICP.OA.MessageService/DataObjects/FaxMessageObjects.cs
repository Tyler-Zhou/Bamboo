using System;

namespace ICP.Message.ServiceInterface
{
    /// <summary>
    /// 传真实体类
    /// </summary>
    [Serializable]
    public class FaxMessageObjects : Message
    {
        public FaxMessageObjects()
        {
            FaxState = ReceiveFaxState.Return;
        }
        /// <summary>
        /// 传真ID
        /// </summary>
        public Guid ReceiveFaxID { get; set; }
        /// <summary>
        /// 传真状态
        /// </summary>
        public ReceiveFaxState FaxState { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid FaxUpdateBy { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? FaxUpdateDate { get; set; }
        /// <summary>
        /// 组织ID
        /// </summary>
        public Guid CompanyID { get; set; }
    }
}
