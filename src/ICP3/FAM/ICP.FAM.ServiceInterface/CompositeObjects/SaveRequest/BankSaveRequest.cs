using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.DataObjects.SaveRequests
{
    #region BankSaveRequest

    /// <summary>
    /// 保存银行时用到的实体
    /// </summary>
    [Serializable]
    public class BankSaveRequest
    {
        /// <summary>
        /// 银行ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string CShortName { get; set; }
        /// <summary>
        /// EShortName
        /// </summary>
        public string EShortName { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public Guid CompanyId { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EName { get; set; }

        /// <summary>
        /// 中文地址
        /// </summary>
        public string CAddress { get; set; }

        /// <summary>
        /// 英文地址
        /// </summary>
        public string EAddress { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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

    #endregion

    #region BankAccountSaveRequest
    /// <summary>
    /// BankAccountSaveRequest
    /// </summary>
    [Serializable]
    public class BankAccountSaveRequest
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// BankId
        /// </summary>
        public Guid BankId { get; set; }
        /// <summary>
        /// AcccountNo
        /// </summary>
        public string AcccountNo { get; set; }
        /// <summary>
        /// CurrencyId
        /// </summary>
        public Guid CurrencyId { get; set; }
        /// <summary>
        /// GlId
        /// </summary>
        public Guid? GlId { get; set; }
        /// <summary>
        /// IsValid
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 是否显示在发票的银行列表
        /// </summary>
        public bool IsShowInInvoiceBankList { get; set; }
        /// <summary>
        /// 是否对公账号
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 是否支持银企直连
        /// </summary>
        public bool IsSupportDirectBank { get; set; }

        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// SaveById
        /// </summary>
        public Guid SaveById { get; set; }
        /// <summary>
        /// UpdateDate
        /// </summary>
        public DateTime? UpdateDate { get; set; }


    }

    #endregion

    #region 汇率调整保存
    /// <summary>
    /// 汇率调整保存
    /// </summary>
    [Serializable]
    public class AdjustRateSaveRequest
    {
        /// <summary>
        /// ID集合
        /// </summary>
        public List<Guid?> Ids { get; set; }
        /// <summary>
        /// 源币种
        /// </summary>
        public List<Guid> SourceCurrencyIDs { get; set; }
        /// <summary>
        /// 目标币种
        /// </summary>
        public List<Guid> TargetCurrencyIDs { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public List<DateTime?> Versions { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public List<DateTime> FromDates { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public List<DateTime> ToDates { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public List<decimal> Rates { get; set; }
        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID { get; set; }
        /// <summary>
        /// 解决方案ID
        /// </summary>
        public Guid SolutionID { get; set; }
    }
    #endregion

}
