using System;
using ICP.Framework.CommonLibrary.Common;

namespace ICP.FAM.ServiceInterface.DataObjects
{
    /// <summary>
    /// 退佣所需要的数据
    /// </summary>
    [Serializable]
    public class ComissionData : BaseDataObject
    {

        /// <summary>
        /// 佣金总额
        /// </summary>
        public decimal CommissionAmount
        {
            get;
            set;
        }

        /// <summary>
        ///  币种+金额
        /// </summary>
        public string StrcommissionAmount
        {
            get;
            set;
        }

        /// <summary>
        /// 所选帐单号组成的字符串
        /// </summary>
        public string DcNoteNos
        {
            get;
            set;
        }

        /// <summary>
        /// 所选帐单关联的提单号组成的字符串
        /// </summary>
        public string BlNos
        {
            get;
            set;
        }

        /// <summary>
        /// 所选帐单的业务号组成的字符串
        /// </summary>
        public string OperationNos
        {
            get;
            set;
        }


        /// <summary>
        /// 利润
        /// </summary>
        public string Profit
        {
            get;
            set;
        }

        /// <summary>
        /// 借
        /// </summary>
        public string Credit
        {
            get;
            set;
        }

        /// <summary>
        /// 货
        /// </summary>
        public string Debit
        {
            get;
            set;
        }

        /// <summary>
        /// 运费到付还是预付
        /// </summary>
        public short PaymentType
        {
            get;
            set;
        }

        /// <summary>
        /// 是否到帐
        /// </summary>
        public bool IsPaid
        {
            get;
            set;
        }

        /// <summary>
        /// 备注,提单号A+利润+应退佣金
        ///      提单号B+利润+应退佣金
        ///      提单号C+利润+应退佣金
        ///      按6%扣手术费=应退佣金总和*6%=USD17.16
        /// </summary>
        public string Remark
        {
            get;
            set;
        }
        /// <summary>
        /// 公司
        /// </summary>
        public Guid CompanyID
        {
            get;
            set;
        }
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName
        {
            get;
            set;
        }
        /// <summary>
        /// 货量
        /// </summary>
        public string Goods
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 退佣统计数据
    /// </summary>
    [Serializable]
    public class CommissionTotalData : BaseDataObject
    {

        /// <summary>
        /// 客户的Id
        /// </summary>
        public Guid CustomerId
        {
            get;
            set;
        }
        /// <summary>
        /// 客户的名称 
        /// </summary>
        public string CustomerName
        {
            get;
            set;
        }
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency
        {
            get;
            set;
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount
        {
            get;
            set;
        }
    }
}
