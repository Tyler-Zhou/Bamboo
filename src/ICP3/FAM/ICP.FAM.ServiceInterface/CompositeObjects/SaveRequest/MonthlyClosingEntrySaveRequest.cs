using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;
using System.Xml;

namespace ICP.FAM.ServiceInterface.DataObjects.SaveRequests
{
    /// <summary>
    /// 月结协议保存时用到的实体
    /// </summary>
    [Serializable]
    public class MonthlyClosingEntrySaveRequest
    {
        /// <summary>
        /// 月结ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// 所属公司ID
        /// </summary>
        // public Guid CompanyId { get; set; }

        /// <summary>
        /// 月结间隔
        /// </summary>
        public string IntervalMonths { get; set; }

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

        /// <summary>
        /// 所属公司ID
        /// </summary>
        public List<Guid> monthlyCompanyIDs { get; set; }

        /// <summary>
        /// 生成月结标识的客户类型
        /// </summary>
        public string CustomerTypes { get; set; }

        /// <summary>
        /// 是否投保
        /// </summary>
        public bool IsInsured { get; set; }
        /// <summary>
        /// 风险评估等级
        /// </summary>
        public RiskRatingLevel RiskRating { get; set; }
        /// <summary>
        /// 承保金额
        /// </summary>
        public decimal InsuredAmount { get; set; }
        /// <summary>
        /// 投保时间
        /// </summary>
        public DateTime? InsuredDate { get; set; }

        /// <summary>
        /// 人员ID集合
        /// </summary>
        public List<Guid?> UserIDs { get; set; }

        /// <summary>
        /// 记账日类型集合
        /// </summary>
        public List<CalculateTermType> CalculateTermTypes { get; set; }

        /// <summary>
        /// 付款日集合
        /// </summary>
        public List<int> PaymentDates { get; set; }

        /// <summary>
        /// 信用期限集合
        /// </summary>
        public List<int> CreditDate { get; set; }

        /// <summary>
        /// 信用额度
        /// </summary>
        public List<Decimal?> CreditAmount { get; set; }

        /// <summary>
        /// 货值/T(USD)
        /// </summary>
        public List<Decimal?> Estimatedvalue { get; set; }

        /// <summary>
        /// 建立人
        /// </summary>
        public List<Guid?> ApplyByIDs { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public List<DateTime?> ValidDates { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public List<bool?> IsValids { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public List<DateTime?> ApplyTimes { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public List<string> Remarks { get; set; }

      
    }

    /// <summary>
    /// 客户偏好保存时用到的实体
    /// </summary>
    [Serializable]
    public class CustomerPreferencesSaveRequest
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
        /// 账单抬头
        /// </summary>
        public string InvoiceTitle { get; set; }

        /// <summary>
        /// 账单日期
        /// </summary>
        public int NotifyPaymentDay { get; set; }

        /// <summary>
        /// Tue
        /// </summary>
        public decimal Tue { get; set; }

        /// <summary>
        /// 账单邮箱
        /// </summary>
        public string NotifyMail { get; set; }

        /// <summary>
        /// 账单电话
        /// </summary>
        public string NotifyContact { get; set; }

        /// <summary>
        /// ShipTo
        /// </summary>
        public string ShipTo { get; set; }

        /// <summary>
        /// PDF类型
        /// </summary>
        public byte PdfAssembled { get; set; }

        /// <summary>
        /// 其他设置
        /// </summary>
        public byte OtherAttachments { get; set; }

        /// <summary>
        /// 是否需要PO
        /// </summary>
        public bool IsNeedPO { get; set; }

        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }

    /// <summary>
    /// 费用配置
    /// </summary>
    [Serializable]
    public class LocalFeeConfigureSaveRequest
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 费用ID
        /// </summary>
        public Guid ChargeID { get; set; }

        /// <summary>
        /// 是否启用公司
        /// </summary>
        public bool IsCommpany { get; set; }

        /// <summary>
        /// 是否启用船东
        /// </summary>
        public bool IsCarrier { get; set; }

        /// <summary>
        /// 是否启用航线
        /// </summary>
        public bool IsShippingLine { get; set; }

        /// <summary>
        /// 是否启用地点
        /// </summary>
        public bool IsLocation { get; set; }

        /// <summary>
        /// 费用单位
        /// </summary>
        public byte ChargeUnit { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? ENDDate { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 币种ID
        /// </summary>
        public Guid CurrencyID { get; set; }

        /// <summary>
        /// 船东列表
        /// </summary>
        public Guid[] CarrierIDs { get; set; }

        /// <summary>
        /// 地点列表
        /// </summary>
        public Guid[] LocationIDs { get; set; }

        /// <summary>
        /// 航线列表
        /// </summary>
        public Guid[] ShippingLineIDs { get; set; }

        /// <summary>
        /// 公司列表
        /// </summary>
        public Guid[] CompanyIDs { get; set; }

        /// <summary>
        /// 单位列表
        /// </summary>
        public string[] ChargeUnits { get; set; }

        /// <summary>
        /// 价格列表
        /// </summary>
        public string[] Prices { get; set; }

        /// <summary>
        /// 保存人
        /// </summary>
        public Guid SaveByID { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    }
}
