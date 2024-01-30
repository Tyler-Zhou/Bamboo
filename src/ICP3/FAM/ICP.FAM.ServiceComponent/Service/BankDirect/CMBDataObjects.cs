using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICP.FAM.ServiceComponent
{

    #region 招行接口所需数据
    /// <summary>
    /// 招行接口所需数据
    /// </summary>
    [Serializable]
    public class CMBData<T>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string AccountNo { get; set; }

        /// <summary>
        /// 币种名称
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public T Content { get; set; }
    } 
    #endregion

    #region 流水查询
    #region 流水查询结构
    /// <summary>
    /// 流水查询结构
    /// </summary>
    [Serializable]
    public class TransactionSearchData : CMBData<TransactionSearchDataDetail>
    {
        /// <summary>
        /// 
        /// </summary>
        public TransactionSearchData()
        {
            Content = new TransactionSearchDataDetail();
        }
    }
    #endregion

    #region 流水查询结构明细
    /// <summary>
    /// 流水查询结构明细
    /// </summary>
    [Serializable]
    public class TransactionSearchDataDetail
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string beginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string endDate { get; set; }
    }
    #endregion
    #endregion

    #region 支付验证
    #region 支付验证结构
    /// <summary>
    /// 单笔支付结构
    /// </summary>
    [Serializable]
    public class PaymentValidData : CMBData<PaymentValidDataDetail>
    {

    }
    #endregion

    #region 支付验证结构明细
    /// <summary>
    /// 单笔支付结构明细
    /// </summary>
    [Serializable]
    public class PaymentValidDataDetail
    {
        /// <summary>
        /// 销账ID
        /// </summary>
        public Guid WriteOffID { get; set; }
        /// <summary>
        /// 销账单号
        /// </summary>
        public string WriteOffNO { get; set; }
        /// <summary>
        /// 销账金额
        /// </summary>
        public string WriteOffAmount { get; set; }
        /// <summary>
        /// 支付人ID
        /// </summary>
        public Guid PayerID { get; set; }
        /// <summary>
        /// 支付人名称
        /// </summary>
        public string Payer { get; set; }
    }
    #endregion
    #endregion

    #region 单笔支付
    #region 单笔支付结构
    /// <summary>
    /// 单笔支付结构
    /// </summary>
    [Serializable]
    public class SinglePaymentData : CMBData<SinglePaymentDataDetail>
    {
        
    } 
    #endregion

    #region 单笔支付结构明细
    /// <summary>
    /// 单笔支付结构明细
    /// </summary>
    [Serializable]
    public class SinglePaymentDataDetail
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string ValidCode { get; set; }
        /// <summary>
        /// 权限模式
        /// </summary>
        public string PermissionMode { get; set; }
        /// <summary>
        /// 业务参考号ID
        /// </summary>
        public Guid BusinessReferenceID { get; set; }
        /// <summary>
        /// 业务参考号
        /// </summary>
        public string BusinessReferenceNO { get; set; }
        /// <summary>
        /// 收方账户
        /// </summary>
        public string RelativeAccountNO { get; set; }
        /// <summary>
        /// 收方户名
        /// </summary>
        public string RelativeAccountName { get; set; }
        /// <summary>
        /// 收方支行
        /// </summary>
        public string RelativeBranchName { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string RelativeBankName { get; set; }
        /// <summary>
        /// 对方银行行号
        /// </summary>
        public string RelativeBankNumber { get; set; }
        /// <summary>
        /// 结算方式
        /// </summary>
        public string SettlementMethod { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal TransactionAmount { get; set; }
        /// <summary>
        /// 交易备注
        /// </summary>
        public string TransactionRemark { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string UseDescription { get; set; }
        /// <summary>
        /// 支付人
        /// </summary>
        public Guid PayerID { get; set; }
    }  
    #endregion
    #endregion
}
