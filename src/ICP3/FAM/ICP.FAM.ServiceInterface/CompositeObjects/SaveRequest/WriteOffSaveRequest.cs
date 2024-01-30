using ICP.Framework.CommonLibrary.Common;
using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.CompositeObjects
{
    /// <summary>
    /// 销账保存实体
    /// </summary>
    [Serializable]
    public class SaveRequestCheck : SaveRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int CheckMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CompanyID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CustomerID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CheckNo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CheckBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PayCustomerName { get; set; }
        /// <summary>
        /// 实际交易银行账号
        /// </summary>
        public string PayBankAccountNo { get; set; }
        /// <summary>
        /// 交易银行名称
        /// </summary>
        public string PayBankName { get; set; }
        /// <summary>
        /// 实际交易银行支行
        /// </summary>
        public string PayBankBranchName { get; set; }
        /// <summary>
        /// 交易银行行联号
        /// </summary>
        public string PayBankNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsMultCurrency { get; set; }
        
        /// <summary>
        /// 银行水单ID
        /// </summary>
        public Guid BankReceiptID { get; set; }
        /// <summary>
        /// 银行水单号码
        /// </summary>
        public string BankReceiptNO { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UpdateDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CheckDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsShare { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsValid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid CurrencyListRequestID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CheckAmount> CheckAmounts { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public Guid BillFeeListRequestID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CheckItem> CheckItems { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Guid ExpenseListRequestID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<Expense> Expenses { get; set; }
    }

    /// <summary>
    /// 销账币种保存实体
    /// </summary>
    [Serializable]
    public class CheckAmount : SaveRequest
    {
        public Guid? ID { get; set; }
        public Guid? CheckID { get; set; }
        public Guid CurrencyID { get; set; }
        public Decimal Amount { get; set; }
        public Decimal StandardCurrencyAmount { get; set; }
        public Guid BankAccountID { get; set; }
        public Decimal BillAmount { get; set; }
        public Decimal StandardCurrencyBillAmount { get; set; }
        public Decimal ExpensesAmount { get; set; }
        public Decimal StandardCurrencyOtherAmount { get; set; }
        public Guid? BankBy { get; set; }
        public DateTime? BankDate { get; set; }
        public String VoucherSeqNo { get; set; }
        /// <summary>
        /// 银行流水ID
        /// </summary>
        public Guid? BankTransactionID { get; set; }
        /// <summary>
        /// 银行流水号码
        /// </summary>
        public string BankTransactionNO { get; set; }
        public BTAWType AssociationType { get; set; }
        public String UpdateDate { get; set; }
    }
    
    /// <summary>
    /// 销账账单/费用 保存实体
    /// </summary>
    [Serializable]
    public class CheckItem : SaveRequest
    {
        public Guid? ID { get; set; }
        public Guid? CheckID { get; set; }
        public Guid ChargeID { get; set; }
        public Decimal Amount { get; set; }
        public Decimal WriteOffRate { get; set; }
        public Decimal WriteOffAmount { get; set; }
        public String UpdateDate { get; set; }
        public String BillUpdateDate { get; set; }
        public String ChargeUpdateDate { get; set; }
        public Decimal StandardCurrencyAmount { get; set; }
    }

    /// <summary>
    /// 销账其他项目保存实体
    /// </summary>
    [Serializable]
    public class Expense : SaveRequest
    {
        public Guid? ID { get; set; }
        public Guid? CheckID { get; set; }
        public Guid? CustomerID { get; set; }
        public String BillNo { get; set; }
        public Guid GLID { get; set; }
        public String GLDescription { get; set; }
        public Guid CurrencyID { get; set; }
        public Decimal Rate { get; set; }
        public Decimal Amount { get; set; }
        public String Remark { get; set; }
        public String UpdateDate { get; set; }
        public Int32 Way { get; set; }
        public Guid? RefID { get; set; }
        public Decimal StandardCurrencyAmount { get; set; }
    }


    /// <summary>
    /// 其他项目列表保存实体
    /// </summary>
    [Serializable]
    public class SaveExpenseList : SaveRequest
    {
        public Guid[] CheckIDs { get; set; }
        public Guid?[] CustomerIDs { get; set; }
        public String[] BillNos { get; set; }
        public Guid[] GLIDs { get; set; }
        public Guid[] CurrencyIDs { get; set; }
        public Decimal[] Rates { get; set; }
        public Decimal[] Amounts { get; set; }
        public String[] Remarks { get; set; }
        public Int32[] Ways { get; set; }
    }


}
