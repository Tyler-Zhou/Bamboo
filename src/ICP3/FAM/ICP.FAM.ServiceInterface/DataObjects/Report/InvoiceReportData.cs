using System;
using System.Collections.Generic;

namespace ICP.FAM.ServiceInterface.DataObjects.Report
{
    #region 发票打印服务
    /// <summary>
    /// 发票打印服务
    /// </summary>
    public class InvoiceReportData
    {
        /// <summary>
        /// 越南发票基本数据
        /// </summary>
        public VietnamInvoiceReportData VietnamInvoiceReportData { get; set; }

        /// <summary>
        /// 越南发票费用明细
        /// </summary>
        public List<VietnamInvoiceReportFeeData> VietnamInvoiceReportFeeData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InvoiceReportDataSZ invoiceReportSZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InvoiceReportFeeDataSZ> invoiceReportFeeDataSZ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InvoiceReportOthers invoiceReportOthers { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<InvoiceReportOthers> TotalFeeList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public InvoiceReportOthersInfo invoiceReportOthersInfo { get; set; }

    } 
    #endregion

    #region 越南发票报表数据对象
    /// <summary>
    /// 越南发票报表数据对象
    /// </summary>
    public class VietnamInvoiceReportData
    {
        /// <summary>
        /// Address(地址)       
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Tel(电话）
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// Fax(传真)
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// CreateBy
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// InvoiceDate(开票日期）
        /// </summary>
        public string InvoiceDate { get; set; }
        /// <summary>
        /// CustomerName(客户名称）
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// CustomerAddress(客户地址)
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// TaxCode(税款代码）
        /// </summary>
        public string TaxCode { get; set; }
        /// <summary>
        /// Currency(币制）
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// TotalInvoice
        /// </summary>
        public string TotalInvoice { get; set; }
        /// <summary>
        /// GrandTotal(全部汇总)
        /// </summary>
        public string GrandTotal { get; set; }
        /// <summary>
        /// No
        /// </summary>
        public string No { get; set; }
        /// <summary>
        /// BankAccountNo(银行账号)
        /// </summary>
        public string BankAccountNo { get; set; }
        /// <summary>
        /// BLText(提运)
        /// </summary>
        public string BLText { get; set; }
        /// <summary>
        /// GrandWords
        /// </summary>
        public string GrandWords { get; set; }
        /// <summary>
        /// TaxNO（税款号码）
        /// </summary>
        public string TaxNO { get; set; }
        /// <summary>
        /// Rate（汇率）
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// Amount（金额）
        /// </summary>
        public string Amount { get; set; }

    } 
    #endregion

    #region 越南发票费用
    /// <summary>
    /// 越南发票费用
    /// </summary>
    public class VietnamInvoiceReportFeeData
    {
        /// <summary>
        /// Amount
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// FeeItemName
        /// </summary>
        public string FeeItemName { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Rate
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// TotalAmount
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// unitPrice
        /// </summary>
        public decimal unitPrice { get; set; }
    } 
    #endregion

    #region InvoiceReportDataSZ
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class InvoiceReportDataSZ
    {
        /// <summary>
        /// 开票日期
        /// </summary>
        public string InvoiceDate { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 单位中文名称（付款单位）
        /// </summary>
        public string CompanyCName { get; set; }
        /// <summary>
        /// 单位英文名称
        /// </summary>
        public string CompanyEName { get; set; }
        /// <summary>
        /// 离港日（到港日）
        /// </summary>
        public string ETD { get; set; }
        /// <summary>
        /// 开户银行一
        /// </summary>
        public string Bank1 { get; set; }
        /// <summary>
        /// 开户银行二
        /// </summary>
        public string Bank2 { get; set; }
        /// <summary>
        /// 船名
        /// </summary>
        public string Voyage { get; set; }
        /// <summary>
        /// 航次
        /// </summary>
        public string Vessel { get; set; }
        /// <summary>
        /// 船名航次（船名+航次）
        /// </summary>
        public string VoyageVessel { get; set; }
        /// <summary>
        /// 提运单号
        /// </summary>
        public string BLNo { get; set; }
        /// <summary>
        /// 装货港
        /// </summary>
        public string POL { get; set; }
        /// <summary>
        /// 卸货港
        /// </summary>
        public string POD { get; set; }
        /// <summary>
        /// 目的地
        /// </summary>
        public string Destination { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    } 
    #endregion

    #region 费用明细
    /// <summary>
    /// 费用明细
    /// </summary>
    [Serializable]
    public class InvoiceReportFeeDataSZ
    {
        /// <summary>
        /// 费用项目
        /// </summary>
        public string ChargingCode { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// 金额(数量*单价，转换成人民币后）
        /// </summary>
        public decimal Amout { get; set; }
        /// <summary>
        /// 金额(数量*单价，原币种）
        /// </summary>
        public decimal Amout_ { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// AmountOfUSD(折合成USD)
        /// </summary>
        public decimal AmountOfUSD { get; set; }
        /// <summary>
        /// 备注（总备注：包含金额统计、汇率、单项备注）
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Quantity { get; set; }
    } 
    #endregion

    #region 其他项
    /// <summary>
    /// 其他项
    /// </summary>
    [Serializable]
    public class InvoiceReportOthers
    {
        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// RMB
        /// </summary>
        public string CurrencyName { get; set; }
        /// <summary>
        /// 金额统计（转换成RMB）
        /// </summary>
        public string TotalAmout { get; set; }
        /// <summary>
        /// 原币种统计
        /// </summary>
        public string TotalAmout_ { get; set; }
        /// <summary>
        /// 大写金额（本位币）
        /// </summary>
        public string TotalRMB { get; set; }
        ///// <summary>
        ///// 大写金额（其他币种）
        ///// </summary>
        //public string TotalOther { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    } 
    #endregion

    #region  其他项（数据来源待定）
    /// <summary>
    /// 其他项（数据来源待定）
    /// </summary>
    [Serializable]
    public class InvoiceReportOthersInfo
    {
        ///// <summary>
        ///// 发票代码
        ///// </summary>
        //public const string InvoiceCode = "244031107431";
        ///// <summary>
        ///// 行业分类
        ///// </summary>
        //public const string IndustryClassification = "服务业(国际货物代理)";
        ///// <summary>
        ///// 营业执照号
        ///// </summary>
        //public const string BussinessRegNo = "4403012106086";
        ///// <summary>
        ///// 复核
        ///// </summary>
        //public const string CheckBy="Jim Green";
        ///// <summary>
        ///// 企业签章
        ///// </summary>
        //public const string BussinessSeal = "企业签章";
        ///// <summary>
        ///// 税务登记号
        ///// </summary>
        //public const string TaxpayerIdenNo = "440300746620601";
        ///// <summary>
        ///// 开票人
        ///// </summary>
        //public string LssuedBy { get; set; }

        /// <summary>
        /// 发票代码
        /// </summary>
        public string InvoiceCode { get; set; }
        /// <summary>
        /// 行业分类
        /// </summary>
        public string IndustryClassification { get; set; }
        /// <summary>
        /// 营业执照号
        /// </summary>
        public string BussinessRegNo { get; set; }
        /// <summary>
        /// 复核
        /// </summary>
        public string CheckBy { get; set; }
        /// <summary>
        /// 企业签章
        /// </summary>
        public string BussinessSeal { get; set; }
        /// <summary>
        /// 税务登记号
        /// </summary>
        public string TaxpayerIdenNo { get; set; }
        /// <summary>
        /// 开票人
        /// </summary>
        public string LssuedBy { get; set; }
        /// <summary>
        /// 电话（湖南发票使用）
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// 银行二账号
        /// </summary>
        public string Bank2Account { get; set; }

        /// <summary>
        /// 起运地
        /// </summary>
        public string StartAddress { get; set; }
    } 
    #endregion

    #region 发票统计
    /// <summary>
    /// 发票统计
    /// </summary>
    [Serializable]
    public class InvoiceCountReport
    {
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceDate { get; set; }
        /// <summary>
        /// 发票号
        /// </summary>
        public string InvoiceNo { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// 币种 
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    } 
    #endregion

    #region InvoiceCountReportTotal
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class InvoiceCountReportTotal
    {
        public string Currency
        {
            get;
            set;
        }
        public decimal TotalAmount
        {
            get;
            set;
        }
    } 
    #endregion

    #region 发票统计数据源
    /// <summary>
    /// 发票统计数据源
    /// </summary>
    [Serializable]
    public class InvoieCountBaseReport
    {
        public string CompanyName
        {
            get;
            set;
        }


        public List<InvoiceCountReport> DataList
        {
            get;
            set;
        }

        public List<InvoiceCountReportTotal> TotalList
        {
            get;
            set;
        }

    } 
    #endregion

}
