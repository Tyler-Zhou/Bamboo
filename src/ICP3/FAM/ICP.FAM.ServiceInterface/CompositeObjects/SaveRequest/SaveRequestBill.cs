using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    #region 账单保存对象
    /// <summary>
    /// 账单保存对象
    /// </summary>
    [Serializable]
    public class SaveRequestBill : SaveRequest
    {
        /// <summary>
        /// 业务ID
        /// </summary>
        public Guid OperationID { get; set; }
        /// <summary>
        /// 表单ID
        /// </summary>
        public Guid FormID { get; set; }
        /// <summary>
        /// 表单类型
        /// </summary>
        public FormType FormType { get; set; }
        /// <summary>
        /// 帐单ID
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 公司ID
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// Ship To ID
        /// </summary>
        public Guid? ShipToID { get; set; }
        /// <summary>
        /// 财务客户描述
        /// </summary>
        public FAMCustomerDescription CustomerDescription { get; set; }
        /// <summary>
        /// 客户参考号
        /// </summary>
        public string CustomerRefNo { get; set; }
        /// <summary>
        /// 账单类型（0:应收,1:应付,2:代理）
        /// </summary>
        public BillType Type { get; set; }
        /// <summary>
        /// 账单日
        /// </summary>
        public DateTime AccountDate { get; set; }
        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// 按一种币种支付.为空时说明不是按一种币种支付的
        /// </summary>
        public Guid? PayCurrencyID { get; set; }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public Guid? AuditorID { get; set; }
        /// <summary>
        /// 审核人邮件地址
        /// </summary>
        public string AuditorEmail { get; set; }
        /// <summary>
        /// 账单状态
        /// </summary>
        public BillState State { get; set; }
        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 业务时间
        /// </summary>
        public DateTime OperationDate { get; set; }
        /// <summary>
        /// 账单来源类型海出1海进2
        /// </summary>
        public int? BillFromType { get; set; }
        /// <summary>
        /// 汇率的币种(如果这个列表里没有该币种就按公司配置下默认的)
        /// </summary>
        public Guid[] RateCurrencyID { get; set; }
        /// <summary>
        /// 汇率值(如果这个列表里没有该币种就按公司配置下默认的)
        /// </summary>
        public decimal[] RateValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 是否开增值税发票
        /// </summary>
        public bool IsVATInvoiced { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        public Decimal? TaxRate { get; set; }
        /// <summary>
        /// 是否是关账后的修改
        /// </summary>
        public bool IsClosingEdit { get; set; }
        /// <summary>
        /// 是否产生APPCFM事件(是否和承运人已确认费用)
        /// </summary>
        public bool IsAPPCFM { get; set; }
        /// <summary>
        /// 保存源
        /// </summary>
        public byte SaveSource { get; set; }
        #region 费用相关
        /// <summary>
        /// 费用-ID列表
        /// </summary>
        public Guid?[] FeeIDs { get; set; }
        /// <summary>
        /// 费用-方向列表
        /// </summary>
        public FeeWay[] FeeWays { get; set; }
        /// <summary>
        /// 费用-类型列表
        /// </summary>
        public FeeType[] FeeTypes { get; set; }
        /// <summary>
        /// 费用-是否代理费列表
        /// </summary>
        public bool[] FeeIsAgents { get; set; }
        /// <summary>
        /// 费用-是否二次销售列表
        /// </summary>
        public bool[] FeeIsSecondSales { get; set; }
        /// <summary>
        /// 费用-是否开增值税发票列表
        /// </summary>
        public bool[] FeeIsVATInvoiceds { get; set; }
        /// <summary>
        /// 费用-GSTs列表
        /// </summary>
        public bool[] FeeIsGSTs { get; set; }
        /// <summary>
        /// 费用-费用代码ID列表
        /// </summary>
        public Guid[] FeeChargingCodeIDs { get; set; }
        /// <summary>
        /// 费用-费用描述列表
        /// </summary>
        public string[] FeeDescriptions { get; set; }
        /// <summary>
        /// 费用-币种ID列表
        /// </summary>
        public Guid[] FeeCurrencyIDs { get; set; }
        /// <summary>
        /// 费用-汇率列表
        /// </summary>
        public decimal[] FeeRates { get; set; }
        /// <summary>
        /// 费用-关联柜号ID列表
        /// </summary>
        public Guid?[] FeeContainerIDs { get; set; }
        /// <summary>
        /// 费用-费用单位列表
        /// </summary>
        public Guid[] FeeUnitIDs { get; set; }
        /// <summary>
        /// 费用-费用单价列表
        /// </summary>
        public decimal[] FeeUnitPrices { get; set; }
        /// <summary>
        /// 费用-费用数量列表
        /// </summary>
        public decimal[] FeeQuantities { get; set; }
        /// <summary>
        /// 费用-费用金额列表
        /// </summary>
        public decimal[] FeeAmounts { get; set; }
        /// <summary>
        /// 费用-费用备注列表
        /// </summary>
        public string[] FeeRemarks { get; set; }
        /// <summary>
        /// 费用-数据版本
        /// </summary>
        public DateTime?[] FeeUpdateDates { get; set; }
        /// <summary>
        /// 费用-费用来源类型海出1海进2
        /// </summary>
        public int?[] FeeFromTypes { get; set; }
        /// <summary>
        /// 费用-是否修改
        /// </summary>
        public bool[] FeeIsRevises { get; set; } 
        #endregion
        /// <summary>
        /// 是否移除
        /// </summary>
        public bool IsRemove { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        public Guid SaveByID { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDate { get; set; }
    } 
    #endregion
}
