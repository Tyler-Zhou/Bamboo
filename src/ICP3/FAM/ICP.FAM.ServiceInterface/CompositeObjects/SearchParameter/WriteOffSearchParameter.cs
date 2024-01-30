using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using System;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 销账查询实体
    /// </summary>
    [Serializable]
    public class WriteOffSearchParameter
    {
        /// <summary>
        /// 付款类型(0:全部、1:仅直付)
        /// </summary>
        public BillSearchPaymentWay PaymentWay { get; set; }
        /// <summary>
        /// 收付款类型(0:全部、1:应收、2:应付)
        /// </summary>
        public BillSearchFeeWay FeeWay { get; set; }
        /// <summary>
        /// 公司ID集合
        /// </summary>
        public Guid[] CompanyID { get; set; }
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public Guid CurrentUserID { get; set; }
        /// <summary>
        /// 银行账号ID
        /// </summary>
        public Guid? BankAccountID { get; set; }
        /// <summary>
        /// 支票号/单据号
        /// </summary>
        public string CheckNo { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 实际收/付款人名称
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string OperationNo { get; set; }
        /// <summary>
        /// 客户参考号
        /// </summary>
        public string CustomerRefNo { get; set; }
        /// <summary>
        /// 凭证号
        /// </summary>
        public string CertificateNo { get; set; }
        /// <summary>
        /// 审核状态(0:全部、1:审核、2:未审核)
        /// </summary>
        public BillSearchAuditorStatue AuditorState { get; set; }
        /// <summary>
        /// 其它项目ID集合
        /// </summary>
        public string OtherIDs { get; set; }
        /// <summary>
        /// 有效性
        /// </summary>
        public bool? IsValid { get; set; }
        /// <summary>
        /// 是否到帐
        /// </summary>
        public bool? IsReached { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序名称
        /// </summary>
        public string OrderByName { get; set; }
        /// <summary>
        /// 最小金额
        /// </summary>
        public decimal? AmountMin { get; set; }
        /// <summary>
        /// 最大金额
        /// </summary>
        public decimal? AmountMax { get; set; }
        /// <summary>
        /// 日期类型(0:全部、1:核销日期、2:到帐日期、3:支票日期)
        /// </summary>
        public WriteOffSearchDateType DateType { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 页对象：包含了 当前页码数 每页显示行数 排序名
        /// </summary>
        public DataPageInfo DataPageInfo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 到帐日期小于核销日期
        /// </summary>
        public bool IsDateLess { get; set; }
    }

    /// <summary>
    /// 客户银行搜索参数
    /// </summary>
    [Serializable]
    public class CustomerBankInfoSearchParameter
    {
        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
    }
}
