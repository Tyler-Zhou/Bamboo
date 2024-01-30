using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.DataObjects.Report
{  
    /// <summary>
    /// 销账单报表数据对象
    /// </summary>
    [Serializable]
    public class WriteOffBillReportData
    {
        /// <summary>
        /// WriteOffBillBaseReportData
        /// </summary>
        public WriteOffBillBaseReportData BaseReportData { get; set; }

        /// <summary>
        /// 账单/费用明细列表
        /// </summary>
        public List<WriteOffBillDetailReportData> DetailList { get; set; }

        /// <summary>
        /// 账单/费用合计明细列表
        /// </summary>
        public List<TotalWriteOffFeeReportData> TotalWriteOfFeeList { get; set; }

        /// <summary>
        /// 其它项目明细列表
        /// </summary>
        public List<WriteOffChargeReportData> ChargeReportDataList { get; set; }

        /// <summary>
        /// 其它项目合计明细列表
        /// </summary>
        public List<TotalWriteOffFeeReportData> TotalChargeFeeList { get; set; }

        /// <summary>
        /// 应收明细列表
        /// </summary>
        public List<TotalWriteOffFeeReportData> DebitList { get; set; }

        /// <summary>
        /// 应付明细列表
        /// </summary>
        public List<TotalWriteOffFeeReportData> CreditList { get; set; }
    }

    /// <summary>
    /// WriteOffBillBaseReportData
    /// </summary>
    [Serializable]
    public class WriteOffBillBaseReportData
    {
        /// <summary>
        /// 报表打印日期
        /// </summary>
        public string PrintDate { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string ReportTitle { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 收款/付款日期 for label 
        /// </summary>
        public string ReceivedOrPayedDateLabel { get; set; }

        /// <summary>
        /// 收款/付款日期
        /// </summary>
        public string ReceivedOrPayedDate { get; set; }

        /// <summary>
        /// 实收/实付金额 for label 
        /// </summary>
        public string ActualReceivedOrPayedAmountLabel { get; set; }

        /// <summary>
        /// 实收/实付金额 
        /// </summary>
        public string ActualReceivedOrPayedAmount { get; set; }

        /// <summary>
        /// 预收、付金额
        /// </summary>
        public string PreReceivedOrPayedAmount { get; set; }  

        /// <summary>
        /// 银行
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// 核销金额
        /// </summary>
        public string AmountWrittenOff { get; set; }      

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 未收/未付金额 for label
        /// </summary>
        public string UnReceivedOrPayAmountLabel { get; set; }

        public string PreReceivedOrPayAmountLabel { get; set; }  
    }

    /// <summary>
    /// 账单/费用明细
    /// </summary>
    [Serializable]
    public class WriteOffBillDetailReportData
    {      
        /// <summary>
        /// 费用名称
        /// 如果是“账单模式”，则为空
        /// </summary>
        public string ChargeName { get; set; }

        /// <summary>
        /// 账单号
        /// </summary>
        public string BillRefNo { get; set; }
        
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
        
        /// <summary>
        /// 账单金额
        /// </summary>
        public string Amount{ get; set; }

        /// <summary>
        /// 未收/未付金额
        /// </summary>
        public string UnReceivedOrPayAmount { get; set; }  
      
        /// <summary>
        /// 核销金额
        /// </summary>
        public string WriteOffAmount { get; set; }
             
        /// <summary>
        /// 汇率
        /// </summary>
        public string ExchangeRate { get; set; }
     
        /// <summary>
        /// 核销金额（折合）
        /// </summary>
        public string FinalAmount{ get; set; }       
    }

    #region 销账编辑界面--其它项目
   
    /// <summary>
    /// 销账单编辑界面的财务费用明细
    /// </summary>
    [Serializable]
    public class WriteOffChargeReportData 
    {                 
        /// <summary>
        /// 客户
        /// </summary>
        public string CustomerName{ get; set; }

        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo { get; set; } 
       
        /// <summary>
        /// 会计科目名称
        /// </summary>
        public string GLDescription { get; set; }
             
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
       
        /// <summary>
        /// 金额
        /// </summary>
        public string Amout{ get; set; }
        
        /// <summary>
        /// 汇率
        /// </summary>
        public string ExchangeRate{ get; set; }  
      
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

    #endregion

    /// <summary>
    /// 账单/费用合计明细
    /// </summary>
    [Serializable]
    public class TotalWriteOffFeeReportData
    {
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 总账单金额
        /// </summary>
        public string TotalBillAmount { get; set; }

        /// <summary>
        /// 总核销金额
        /// </summary>
        public string TotalWriteOffAmount { get; set; }

        /// <summary>
        /// 折合金额
        /// </summary>
        public string FinalAmount { get; set; }

        /// <summary>
        /// Label Text
        /// </summary>
        public string LabelText { get; set; }
    }

    #region 支票打印

    #region 支票打印数据对象

    /// <summary>
    /// 支票打印数据对象
    /// </summary>    
    public class CashReportData
    {
        /// <summary>
        /// CashBaseReportData
        /// </summary>
        public CashBaseReportData BaseReportData { get; set; }

        /// <summary>
        /// 支票账单明细列表
        /// </summary>
        public List<CashBillReportData> BillList { get; set; }
    }

    #endregion

    #region 支票打印Base

    /// <summary>
    /// 支票打印Base
    /// </summary>
    public class CashBaseReportData
    {
        /// <summary>
        /// 核销日期
        /// </summary>
        public string CheckDate { get; set; }

        public string Amount { get; set; }

        public string Total { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        public string CustomerEAddress { get; set; }

        public string Remark { get; set; }

        /// <summary>
        /// 支票号
        /// </summary>
        public string CheckNO { get; set; }

        /// <summary>
        /// 加拿大支票打印用
        /// </summary>
        public string CustomerRefNO1 { get; set; }

        /// <summary>
        /// 加拿大支票打印用
        /// </summary>
        public string Description1 { get; set; }

        /// <summary>
        /// 加拿大支票打印用
        /// </summary>
        public string Amount1 { get; set; }

        /// <summary>
        /// 加拿大支票打印用
        /// </summary>
        public string CustomerRefNO2 { get; set; }

        /// <summary>
        /// 加拿大支票打印用
        /// </summary>
        public string Description2 { get; set; }

        /// <summary>
        /// 加拿大支票打印用
        /// </summary>
        public string Amount2 { get; set; }

        /// <summary>
        /// 加拿大支票打印用
        /// </summary>
        public string CurrencyName { get; set; }
    }

    #endregion

    #region 支票账单

    public class CashBillReportData
    {
        /// <summary>
        /// 业务号
        /// </summary>
        public string RefNo { get; set; }

        /// <summary>
        /// 账单号
        /// </summary>
        public string BillNo { get; set; }

        /// <summary>
        /// 提单号
        /// </summary>
        public string BLNo { get; set; }

        /// <summary>
        /// 核销金额
        /// </summary>
        public string WriteOffAmount { get; set; }
    }

    #endregion

    #endregion
}
