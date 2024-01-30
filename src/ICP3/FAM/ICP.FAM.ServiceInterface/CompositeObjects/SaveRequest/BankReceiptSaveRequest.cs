using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// BankReceiptSaveRequest
    /// </summary>
    [Serializable]
    public class BankReceiptSaveRequest : SaveRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id;
        /// <summary>
        /// 单号
        /// </summary>
        public string No;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyId;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid? CustomerId;
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount;
        /// <summary>
        /// 状态
        /// </summary>
        public BankReceiptStatus Status;
        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid;
        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid SaveByID;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate;
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid UpdateBy;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate;

        /// <summary>
        /// ID
        /// </summary>
        public Guid[] Ids;
        /// <summary>
        /// 单号
        /// </summary>
        public string Nos;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid?[] CompanyIds;
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid?[] CustomerIds;
        /// <summary>
        /// 保存人ID
        /// </summary>
        public Guid SaveByIds;
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid?[] UpdateBys;
        /// <summary>
        /// 备注
        /// </summary>
        public string[] Remarks;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime?[] UpdateDates;
        /// <summary>
        /// 是否英文版本
        /// </summary>
        public bool IsEnglish;
    }
}
