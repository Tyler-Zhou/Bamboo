using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI;
using ICP.Framework.CommonLibrary.Client;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using System.Text.RegularExpressions;
using ICP.FAM.ServiceInterface;

namespace ICP.FAM.UI
{
    public class InvoiceReportHelper
    {
   

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }

        //[ServiceDependency]
        //public IFinanceService financeService { get; set; }

        IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        Dictionary<Guid, string> _dicCurrency = new Dictionary<Guid, string>();
        #region Check report Data
        public void CheckPrintData(InvoiceInfo info)
        {
            if (string.IsNullOrEmpty(info.Bank1Name)) { info.Bank1Name = string.Empty; }
            if (string.IsNullOrEmpty(info.Bank2Name)) { info.Bank2Name = string.Empty; }
            if (string.IsNullOrEmpty(info.BLNo)) { info.BLNo = string.Empty; }
            if (string.IsNullOrEmpty(info.CustomerName)) { info.CustomerName = string.Empty; }
            if (string.IsNullOrEmpty(info.PlaceOfDeliveryName)) { info.PlaceOfDeliveryName = string.Empty; }
            //if (string.IsNullOrEmpty(info.ETD.ToString())) { info.ETD = Convert.ToDateTime(System.DBNull.Value); }
            if (string.IsNullOrEmpty(info.InvoiceDate.ToString())) { info.InvoiceDate = Convert.ToDateTime(DBNull.Value); }
            if (string.IsNullOrEmpty(info.PODName)) { info.PODName = string.Empty; }
            if (string.IsNullOrEmpty(info.POLName)) { info.POLName = string.Empty; }
            if (string.IsNullOrEmpty(info.Vessel)) { info.Vessel = string.Empty; }
            if (string.IsNullOrEmpty(info.Voyage)) { info.Voyage = string.Empty; }
            if (string.IsNullOrEmpty(info.BankAccountNo)) { info.BankAccountNo = string.Empty; }
            if (string.IsNullOrEmpty(info.Remark)) { info.Remark = string.Empty; }
        }
        #endregion

        public string GetReportCompany(InvoiceInfo info)
        {
            if (string.IsNullOrEmpty(info.CompanyName))
                return null;
            string reportName = "";
            string Company = info.CompanyName.ToString();
            switch (Company)
            {
                case "深圳公司":
                    reportName = "InvoiceReportSZ.frx";
                    break;
                case "湖南公司":
                    reportName = "InvoiceReportHN.frx";
                    break;
                case "大连公司":
                    reportName = "InvoiceReportDL.frx";
                    break;
                case "越南公司":
                    reportName = "InvoiceReportYN.frx";
                    break;
                case "远东区办公室":
                    reportName = "InvoiceReportSZ.frx";
                    break;
                default:
                    reportName = "InvoiceReportSZ.frx";
                    break;
            }

            return reportName;
        }
        public void PrintInvoice()
        {
            string fileName = Application.StartupPath + "\\Reports\\FAM\\Invoice\\InvoiceReport.frx";
            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Invoice" : "Invoice", ServiceClient.GetClientService<WorkItem>().Workspaces[ClientConstants.MainWorkspace]);
            viewer.BindData(fileName, null, null);
        }
        /// ㈠新格式report显示/不显示本位币均一样,旧格式显示本位币为RMB结算
        /// ㈡新格式report费用栏币种均是RMB,且只显示单行,其他统计币种也都采用RMB统计
        /// ㈢代理文本与否即费用项目前是否添加"代理"二字
        /// ㈣显示公司与否即是否显示企业签章,深圳公司和其他公司显示有区别
        /// ㈤新格式备注栏统计币种为折合币种,备注栏汇率为折合币种之汇率
        /// ㈥旧格式不需显示转换币种,显示为原费用币种,显示本位币则备注栏添加显示RMB统计,汇率
        /// ㈦旧格式：不显示本位币时,备注栏只显示备注,无需显示汇率,金额统计等
        public Dictionary<string, object> Print
            (InvoiceInfo invoice,
            List<InvoiceFeeDate> Details,
            string[] reportPara)
        {
            InvoiceReportData invoiceReportData = new InvoiceReportData();
            invoiceReportData.invoiceReportSZ = new InvoiceReportDataSZ();
            invoiceReportData.invoiceReportOthersInfo = new InvoiceReportOthersInfo();
            invoiceReportData.invoiceReportOthers = new InvoiceReportOthers();
            CheckPrintData(invoice);
            invoiceReportData.invoiceReportSZ.InvoiceDate = invoice.InvoiceDate.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);//开票日期
            invoiceReportData.invoiceReportSZ.InvoiceNo = invoice.InvoiceNo.ToString();
            string[] sArray = Regex.Split(invoice.Bank1Name, "-", RegexOptions.IgnoreCase);
            string[] sArray1 = Regex.Split(invoice.Bank2Name, "-", RegexOptions.IgnoreCase);
            if (sArray.Length == 0) { invoiceReportData.invoiceReportSZ.Bank1 = string.Empty; } else { invoiceReportData.invoiceReportSZ.Bank1 = sArray[0].ToString(); }//name/id过滤掉"-"字符之后字符
            if (sArray1.Length == 0) { invoiceReportData.invoiceReportSZ.Bank2 = string.Empty; } else{ invoiceReportData.invoiceReportSZ.Bank2 = sArray1[0].ToString(); }
            invoiceReportData.invoiceReportSZ.BLNo = invoice.BLNo.ToString();
            //invoiceReportData.invoiceReportSZ.CompanyCName = invoice.CustomerName.ToString();//付款单位
            invoiceReportData.invoiceReportSZ.CompanyCName = LocalData.IsEnglish ? invoice.TitleEName : invoice.TitleCName;
            invoiceReportData.invoiceReportSZ.Destination = invoice.PlaceOfDeliveryName.ToString();
            invoiceReportData.invoiceReportSZ.ETD = invoice.ETD== null? string.Empty: invoice.ETD.Value.ToShortDateString();
            invoiceReportData.invoiceReportSZ.POD = invoice.PODName.ToString();
            invoiceReportData.invoiceReportSZ.POL = invoice.POLName.ToString();
            invoiceReportData.invoiceReportSZ.Vessel = invoice.Vessel.ToString();
            invoiceReportData.invoiceReportSZ.Voyage = invoice.Voyage.ToString();
            string CompanyChild = "";
            if (invoice.CompanyName == "深圳公司") { CompanyChild = string.Empty; }//除深圳公司外其他分公司显示格式=深圳市鹏城海物流有限公司+XX分公司(企业签章)
            else { CompanyChild = invoice.CompanyName.Substring(0, 2) + "分" + invoice.CompanyName.Substring(2, 2); }
            if (reportPara[2].ToLower() == "false") { invoiceReportData.invoiceReportSZ.Company = string.Empty; }
            else { invoiceReportData.invoiceReportSZ.Company = "深圳市鹏城海物流有限公司" + CompanyChild; }//企业签章
            string[] info = { "244031107431", "服务业(国际货物代理)", "4403012106086", "", "深圳市鹏城海物流有限公司", "440300746620601", LocalData.IsEnglish ? LocalData.UserInfo.LoginName : LocalData.UserInfo.UserName, "0755-36562314" };//此数据待定
            invoiceReportData.invoiceReportOthersInfo.InvoiceCode = info[0];
            invoiceReportData.invoiceReportOthersInfo.IndustryClassification = info[1];                  
            invoiceReportData.invoiceReportOthersInfo.CheckBy = info[3];
            invoiceReportData.invoiceReportOthersInfo.BussinessSeal = info[4];
            invoiceReportData.invoiceReportOthersInfo.LssuedBy = info[6];
            invoiceReportData.invoiceReportOthersInfo.Tel = info[7];//湖南发票使用,数据待定
            invoiceReportData.invoiceReportOthersInfo.BankAccount = invoice.BankAccountNo.ToString();
            if (string.IsNullOrEmpty(invoice.BusinessNo))
            {
                InvoiceInfo invoiceInfoTemp = FinanceService.GetInvoiceInfo(invoice.ID, LocalData.IsEnglish);
                invoiceReportData.invoiceReportOthersInfo.BussinessRegNo = invoiceInfoTemp.BusinessNo;
                invoiceReportData.invoiceReportOthersInfo.TaxpayerIdenNo = invoiceInfoTemp.TaxNo;
            }
            else
            {
                invoiceReportData.invoiceReportOthersInfo.BussinessRegNo = invoice.BusinessNo;
                invoiceReportData.invoiceReportOthersInfo.TaxpayerIdenNo = invoice.TaxNo;
            }

            if (string.IsNullOrEmpty(invoiceReportData.invoiceReportOthersInfo.BankAccount))
            {
                BankAccountInfo account = FinanceService.GetBankAccountInfo(invoice.Bank1ID, LocalData.IsEnglish);
                invoiceReportData.invoiceReportOthersInfo.BankAccount = account.AccountNo;
            }

            if (!FAMUtility.GuidIsNullOrEmpty(invoice.Bank2ID))
            {
                BankAccountInfo accountInfo = FinanceService.GetBankAccountInfo(invoice.Bank2ID.Value, LocalData.IsEnglish);
                invoiceReportData.invoiceReportOthersInfo.Bank2Account = accountInfo.AccountNo;
            }

            decimal totalAll = 0;
            decimal totalAll_ = 0;
            decimal totalTip = 0;
            decimal total = 0;
            invoiceReportData.invoiceReportFeeDataSZ = new List<InvoiceReportFeeDataSZ>();
            if (Details.Count <= 0) return null;
            foreach (var detailItem in Details)
            {
                InvoiceReportFeeDataSZ feeData = new InvoiceReportFeeDataSZ();
                if (_dicCurrency.Keys.Contains(detailItem.CurrencyID))
                {
                    feeData.CurrencyName = _dicCurrency[detailItem.CurrencyID]; 
                }

                if (string.IsNullOrEmpty(detailItem.Amount.ToString())) { detailItem.Amount = Convert.ToDecimal(string.Empty); }
                if (string.IsNullOrEmpty(detailItem.ChargingCode.ToString())) { detailItem.ChargingCode = string.Empty; }
                if (string.IsNullOrEmpty(detailItem.Rate.ToString())) { detailItem.Rate = Convert.ToDecimal(string.Empty); }
                if (string.IsNullOrEmpty(detailItem.Quantity.ToString())) { detailItem.Quantity = Convert.ToDecimal(string.Empty); }
                if (string.IsNullOrEmpty(detailItem.Remark)) { detailItem.Remark = string.Empty; }
                if (string.IsNullOrEmpty(detailItem.CurrencyName)) { detailItem.CurrencyName = string.Empty; }
            
                totalTip += detailItem.Amount * detailItem.Rate;//大于10W RMB提示
                feeData.CurrencyName = detailItem.CurrencyName;
                if (reportPara[5].ToLower() == "old")//旧格式,显示本位币
                {
                    feeData.Amout = detailItem.Amount;
                }
                else
                {
                    feeData.Amout = detailItem.Amount * detailItem.Rate;//RMB(单项统计）
                }
             
                feeData.Amout_ = detailItem.Amount * detailItem.Rate;//原币种(单项统计)
                total += detailItem.Amount;
                if (reportPara[4] == "0" || string.IsNullOrEmpty(reportPara[4])) { reportPara[4] = "1"; }
                totalAll_ += feeData.Amout_ / Convert.ToDecimal(reportPara[4]);//金额汇总
                totalAll += feeData.Amout;//(RMB)
                feeData.ChargingCode = reportPara[0] + detailItem.ChargingCode;
                feeData.Rate = detailItem.Rate;
                feeData.Quantity = detailItem.Quantity;
                feeData.Remark = detailItem.Remark;
                feeData.Price = feeData.Amout;//单价(RMB计算)
                if (reportPara[5].ToLower() == "new" && reportPara[6] != "Ship") { feeData.CurrencyName = string.Empty; } else { feeData.CurrencyName = detailItem.CurrencyName; }
                invoiceReportData.invoiceReportFeeDataSZ.Add(feeData);
            }

            #region 费用项目相同需要合并
            Dictionary<string, InvoiceReportFeeDataSZ> reportShowFeeList = new Dictionary<string, InvoiceReportFeeDataSZ>();
            foreach (var fee in invoiceReportData.invoiceReportFeeDataSZ)
            {
                if (!reportShowFeeList.Keys.Contains(fee.ChargingCode))
                {
                    reportShowFeeList.Add(fee.ChargingCode, fee);
                }
                else
                {
                    reportShowFeeList[fee.ChargingCode].Amout += fee.Amout;
                }
            }

            invoiceReportData.invoiceReportFeeDataSZ = reportShowFeeList.Values.ToList();
            #endregion

            if (totalTip > 100000)
            {
                if (!FAMUtility.ShowResultMessage(LocalData.IsEnglish ? "Invoice amount exceeds the 100000RMB, whether to continue printing?" : "发票金额超过了100000RMB,是否继续打印?"))
                {
                    return null;
                }
            }//此处具体做法待定
            if (reportPara[5].ToLower() == "new" && reportPara[6] != "Ship")//新格式
            {
                invoiceReportData.invoiceReportOthers.CurrencyName = "RMB";//费用栏显示币种
                invoiceReportData.invoiceReportOthers.Currency = reportPara[3].ToString();//备注栏币种 
                invoiceReportData.invoiceReportOthers.Rate = reportPara[4].ToString();//汇率
                invoiceReportData.invoiceReportOthers.remark = invoice.Remark;
                invoiceReportData.invoiceReportOthers.TotalRMB = FAMUtility.MoneyToString(totalAll);
                invoiceReportData.invoiceReportOthers.TotalAmout = totalAll.ToString("0.00");//金额汇总
                invoiceReportData.invoiceReportOthers.TotalAmout_ = (totalTip / Convert.ToDecimal(reportPara[4])).ToString("0.00");//转换后金额汇总,备注栏用
            }
            else if (reportPara[1] == string.Empty && reportPara[5].ToLower() == "old")//旧格式,不显示本位币
            {
                invoiceReportData.invoiceReportOthersInfo.InvoiceCode = string.Empty;//此处为备注栏"合计"二字不显示
                invoiceReportData.invoiceReportOthers.Rate = string.Empty;
                invoiceReportData.invoiceReportOthers.TotalAmout = totalAll.ToString("0.00");//金额汇总
                invoiceReportData.invoiceReportOthers.TotalAmout_ = string.Empty;//金额汇总
                invoiceReportData.invoiceReportOthers.CurrencyName = invoiceReportData.invoiceReportFeeDataSZ[0].CurrencyName;
                if (invoiceReportData.invoiceReportOthers.CurrencyName == "RMB")
                {
                    invoiceReportData.invoiceReportOthers.Currency = "人民币";
                }
                else if (invoiceReportData.invoiceReportOthers.CurrencyName == "USD")
                {
                    invoiceReportData.invoiceReportOthers.Currency = "美元";
                }
               
                invoiceReportData.invoiceReportOthersInfo.IndustryClassification = string.Empty;//此时用作汇率
                invoiceReportData.invoiceReportOthers.remark = invoice.Remark;
                invoiceReportData.invoiceReportOthers.TotalRMB = FAMUtility.MoneyToString(totalAll);
            }//此时备注栏只显示备注,不显示汇率等
            else if (reportPara[1] == "RMB" && reportPara[5].ToLower() == "old")//旧格式,显示本位币
            {
                invoiceReportData.invoiceReportOthersInfo.InvoiceCode = "合计:";
                invoiceReportData.invoiceReportOthersInfo.IndustryClassification = "汇率:";
                invoiceReportData.invoiceReportOthers.remark = invoice.Remark;
                invoiceReportData.invoiceReportOthers.CurrencyName = invoiceReportData.invoiceReportFeeDataSZ[0].CurrencyName;
                invoiceReportData.invoiceReportOthers.Currency = reportPara[1];

                invoiceReportData.invoiceReportOthers.Rate = reportPara[4];
                invoiceReportData.invoiceReportOthers.TotalRMB = FAMUtility.MoneyToString(total);
                invoiceReportData.invoiceReportOthers.TotalAmout = total.ToString("0.00");//金额汇总
                invoiceReportData.invoiceReportOthers.TotalAmout_ = (total * Convert.ToDecimal(reportPara[4])).ToString("0.00");//转换后金额汇总,备注栏用
            }
            else
            {
                invoiceReportData.invoiceReportOthers.CurrencyName = reportPara[3].ToString();
                invoiceReportData.invoiceReportOthers.Currency = "RMB";//备注栏币种
                invoiceReportData.invoiceReportOthersInfo.InvoiceCode = "合计:";
                invoiceReportData.invoiceReportOthersInfo.IndustryClassification = "汇率:";
                invoiceReportData.invoiceReportOthers.Rate = reportPara[4];//汇率
                invoiceReportData.invoiceReportOthers.remark = invoice.Remark;
                invoiceReportData.invoiceReportOthers.TotalRMB = FAMUtility.MoneyToString(totalAll);
                invoiceReportData.invoiceReportOthers.TotalAmout = total.ToString("0.00");//金额汇总
                invoiceReportData.invoiceReportOthers.TotalAmout_ = (total / Convert.ToDecimal(reportPara[4])).ToString("0.00");//转换后金额汇总,备注栏用
            }
            //#region 打印数据填充 BEGIN
            ///*================数据源=================*/
            Dictionary<string, object> reportSource = new Dictionary<string, object>();
            reportSource.Add("invoiceReportSZ", invoiceReportData.invoiceReportSZ);
            reportSource.Add("invoiceReportFeeDataSZ", invoiceReportData.invoiceReportFeeDataSZ);
            reportSource.Add("invoiceReportOthers", invoiceReportData.invoiceReportOthers);
            reportSource.Add("invoiceReportOthersInfo", invoiceReportData.invoiceReportOthersInfo);
            ///*================数据源=================*/
            //#endregion 打印数据填充 END
            return reportSource;
        }
    }

    public enum InvoiceReportType
    { 
        /// <summary>
        /// 发票统计
        /// </summary>
        InvoiceCount=1,
        /// <summary>
        /// 免税明细表
        /// </summary>
        DutyFreeDetail=2,
        /// <summary>
        /// 开票统计
        /// </summary>
        OperationInvoice=3
    }
}
