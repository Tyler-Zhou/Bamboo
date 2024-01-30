using System;


namespace ICP.DataCache.ServiceInterface
{    
    /// <summary>
    /// 业务联系人
    /// </summary>
    [Serializable]
   public class OperationContactInfo
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 联系人的Email
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// 关联的客户ID
        /// </summary>
        public Guid? CustomerID { get; set; }
        /// <summary>
        /// 是否是客户
        /// </summary>
        public bool Customer { get; set; }
        /// <summary>
        /// 是否是承运人
        /// </summary>
        public bool Carrier { get; set; }
        /// <summary>
        /// 是否是海出业务联系人
        /// </summary>
        public bool? OE { get; set; }
        /// <summary>
        /// 是否是海进业务联系人
        /// </summary>
        public bool? OI { get; set; }
        /// <summary>
        /// 是否是空出业务联系人
        /// </summary>
        public bool? AE { get; set; }
        /// <summary>
        /// 是否是空进业务联系人
        /// </summary>
        public bool? AI { get; set; }
        /// <summary>
        /// 是否是拖车业务联系人
        /// </summary>
        public bool? Trk { get; set; }
        /// <summary>
        /// 是否属于其他业务联系人
        /// </summary>
        public bool? Other { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
         
    }
}
