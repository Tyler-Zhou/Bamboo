using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.DataObjects.SaveRequests
{
    /// <summary>
    /// 总总电放保存时用到的实体
    /// </summary>
    [Serializable]
    public class TelexRequestSaveRequest
    {
        /// <summary>
        /// 总电放ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// 客户描述
        /// </summary>
        public string CustomerDescription { get; set; }
        /// <summary>
        /// 电放类型:0所有;1电放;2.SWB
        /// </summary>
        public TelexType TelexType { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime ValidDate { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 收货人ID列表
        /// </summary>
        public List<Guid> ConsigneeIds { get; set; }

        /// <summary>
        /// 建立人
        /// </summary>
        public Guid CreateById { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public Guid? UpdateById { get; set; }

        /// <summary>
        /// 修改时间（版本）
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
