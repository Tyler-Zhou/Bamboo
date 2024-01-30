using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using ICP.Common.ServiceInterface;
using ICP.FAM.ServiceInterface;
using ICP.Message.ServiceInterface;
using Microsoft.Practices.CompositeUI;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FCM.Common.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Common;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Helper;
using ICP.Sys.ServiceInterface;
using ICP.Sys.ServiceInterface.DataObjects;
using ICP.Common.ServiceInterface.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using System.Linq;
using System.IO;
using ICP.FileSystem.ServiceInterface;

namespace ICP.FAM.UI.CustomerBill.Print
{
    public abstract class BillController : IBillController
    {
        #region 服务

        [ServiceDependency]
        public WorkItem Workitem { get; set; }

        public IFinanceService FinanceService
        {
            get
            {
                return ServiceClient.GetService<IFinanceService>();
            }
        }

        public IFinanceReportService FinanceReportService
        {
            get
            {
                return ServiceClient.GetService<IFinanceReportService>();
            }
        }


        public IConfigureService ConfigureService
        {
            get
            {
                return ServiceClient.GetService<IConfigureService>();
            }
        }

        public ICustomerService CustomerService
        {
            get
            {
                return ServiceClient.GetService<ICustomerService>();
            }
        }

        public IUserService UserService
        {
            get
            {
                return ServiceClient.GetService<IUserService>();
            }
        }

        public FinancePrintHelper FinancePrintHelper
        {
            get
            {
                return ClientHelper.Get<FinancePrintHelper, FinancePrintHelper>();
            }
        }

        public IReportViewService ReportViewService
        {
            get
            {
                return ServiceClient.GetClientService<IReportViewService>();
            }
        }


        public RateHelper RateHelper
        {
            get
            {
                return ClientHelper.Get<RateHelper, RateHelper>();
            }
        }

        #endregion

        #region 本地变量

        Guid _ThailandCompanyId = new Guid("13C26E30-F2AD-4D94-B13D-5E337EA97936");//THAILAND 公司
        Guid _HARVESTCompanyId = new Guid("e563e2c9-6ca7-412b-99a4-f246379720c0");//温哥华 公司
        Guid _JDYCompanyId = new Guid("3835F05C-2779-E511-99BA-0026551CA878");//JDY 公司
        Guid _VietnamCompanyId = new Guid("5a827adf-38c7-4a2f-99a7-ad717ce91718");//越南 公司
        /// <summary>
        /// 马来西亚公司ID
        /// </summary>
        Guid _MLCompanyId = new Guid("1DBF7671-0D2D-4F08-A8A9-3663A0DB0037");//马来西亚公司

        Guid _MLGST = new Guid("E90CD1CD-72D8-40B4-9F2D-D86079E78FEB");//马来西亚GST费用代码

        CompanyReportConfigureList _reportConfigure = null;

        #endregion

        #region IBillController 成员

        /// <summary>
        /// 在控制器中更新了帐单
        /// </summary>
        public event BillUpdatedHandler UpdatedBill;

        public virtual void PrintFeeList(Guid operationID)
        {
            FinancePrintHelper.PritnFeeList(operationID);
        }

        public virtual void PrintBill(BillList billList
                                    , BillType billType
                                    , OperationCommonInfo operationCommonInfo
                                    , ConfigureInfo configureInfo
                                    , List<SolutionExchangeRateList> rates)
        {
            _reportConfigure = ConfigureService.GetReportConfigureList(configureInfo.CompanyID, BillReportConfigConstants.CustomerBillConfig);
            if (_reportConfigure == null || _reportConfigure.Parameters == null || _reportConfigure.Parameters.Count == 0)
            {
                throw new ApplicationException(LocalData.IsEnglish ? "" : "当前公司未配置打印帐单的参数.");
            }

            BillReportNameEnum billReportNameEnum = BillReportNameEnum.BillReport;
            #region  获取报表类型
            if (billType == BillType.AP)
            {
                ReportParameterList parameter = _reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.CR_ReportName);
                if (parameter != null)
                {
                    short temp = 0;
                    if (short.TryParse(parameter.ParameterValue, out temp))
                    {
                        billReportNameEnum = (BillReportNameEnum)temp;
                    }
                }
            }
            else if (billType == BillType.AR)
            {
                ReportParameterList parameter = _reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.DR_ReportName);
                if (parameter != null)
                {
                    short temp = 0;
                    if (short.TryParse(parameter.ParameterValue, out temp))
                    {
                        billReportNameEnum = (BillReportNameEnum)temp;
                    }

                }
            }
            else if (billType == BillType.DC)
            {
                ReportParameterList parameter = _reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.DRCR_ReportName);
                if (parameter != null)
                {
                    short temp = 0;
                    if (short.TryParse(parameter.ParameterValue, out temp))
                    {
                        billReportNameEnum = (BillReportNameEnum)temp;
                    }

                }
            }

            #endregion

            if (billReportNameEnum == BillReportNameEnum.BillReport)
            {
                PrintCombinBill(new List<BillList> { billList }, operationCommonInfo, configureInfo, rates);
            }
            else
            {
                PrintLocalBill(billList, billReportNameEnum, operationCommonInfo, configureInfo, rates);
            }

        }

        private void PrintLocalBill(BillList billList
                                    , BillReportNameEnum billReportNameEnum
                                    , OperationCommonInfo operationCommonInfo
                                    , ConfigureInfo configureInfo
                                    , List<SolutionExchangeRateList> rates)
        {
            #region 打开Titel 配置面板
            PrintBillTitelConfigData printBillConfigData = new PrintBillTitelConfigData();
            Dictionary<string, object> keyValue = new Dictionary<string, object>();
            keyValue.Add("CompanyID", operationCommonInfo.CompanyID);
            keyValue.Add("BillWay", billReportNameEnum);
            PrintBillTitelConfigPart pb = Workitem.Items.AddNew<PrintBillTitelConfigPart>();
            pb.DataSource = printBillConfigData;
            pb.Init(keyValue);
            if (FAMUtility.ShowDialog(pb, LocalData.IsEnglish ? "Print Bill Config" : "打印帐单配置") != DialogResult.OK) return;
            printBillConfigData = pb.DataSource as PrintBillTitelConfigData;

            #endregion

            BillInfo billInfo = FinanceService.GetBillInfo(billList.ID);

            #region 设置一些基本信息

            LocalBillReportData localBillReportData = FinanceReportService.GetLocalBillReportData(billList.ID);
            LocalBillReportSource reportData = new LocalBillReportSource();
            FAMUtility.CopyToValue(localBillReportData, reportData, typeof(LocalBillReportData));

            //reportData.CompanyDsc = printBillConfigData.CompanyName + "\r\n" + printBillConfigData.CompanyDsc;
            ConfigureInfo confiInfo = ConfigureService.GetCompanyConfigureInfo(printBillConfigData.CompanyID);
            if (confiInfo == null)
            {
                FAMUtility.ShowMessage(printBillConfigData.CompanyName + "没有配置信息不能打印!");
                return;
            }
            CustomerInfo customerInfo = CustomerService.GetCustomerInfo(confiInfo.CustomerID);
            reportData.CompanyDsc = customerInfo.EName + "\r\n" + printBillConfigData.CompanyDsc;

            reportData.CustomerRefNo = localBillReportData.CustomerRefNo;
            reportData.BillDate = billInfo.AccountDate.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
            reportData.DueDate = billInfo.DueDate.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
            reportData.Trem = Convert.ToInt32((billInfo.DueDate - billInfo.AccountDate).TotalDays);
            reportData.BillToCustomerName = billInfo.CustomerName;
            reportData.BillToCustomerAdd = billInfo.CustomerDescription == null ? string.Empty : billInfo.CustomerDescription.Address;
            reportData.BillToCustomerDsc = billInfo.CustomerDescription == null ? string.Empty : billInfo.CustomerDescription.ToString(LocalData.IsEnglish);
            reportData.CreateDate = billInfo.CreateDate.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
            reportData.ShipperTo = localBillReportData.truckerDescription == null ? null : localBillReportData.truckerDescription.ToString();
            reportData.BillNo = billInfo.No;
            reportData.OperationNo = operationCommonInfo.OperationNo;
            reportData.Remark = billInfo.Remark;
            reportData.CurrentUser = LocalData.UserInfo.LoginName;
            reportData.MainCurrency = configureInfo.DefaultCurrency;

            #endregion

            #region 转换费用

            Dictionary<string, FeeTotalInfo> feeTotals = new Dictionary<string, FeeTotalInfo>();

            //应收总金额 主币种
            decimal totalAmount = 0m;

            List<ChargeItemReportData> fees = new List<ChargeItemReportData>();
            foreach (var item in billInfo.Fees)
            {
                ChargeItemReportData temp = new ChargeItemReportData();
                //temp.ChargeName = item.ChargingCode;
                temp.ChargeName = item.ChargingDescription;
                temp.Credit = item.Way == FeeWay.AR ? 0m : item.Amount;
                temp.Debit = item.Way == FeeWay.AP ? 0m : item.Amount;
                temp.Currency = item.CurrencyName;
                temp.Qty = item.Quantity;
                temp.Rate = item.Rate;
                temp.UnitPrice = item.UnitPrice;
                temp.Way = item.Way;
                temp.Amount = temp.Debit - temp.Credit;

                temp.TotalAmoint = item.Rate * item.Amount;

                fees.Add(temp);

                totalAmount += (item.Way == FeeWay.AR ? (item.Amount * item.Rate) : -(item.Amount * item.Rate));

                if (feeTotals.ContainsKey(item.CurrencyName) == false) feeTotals.Add(item.CurrencyName, new FeeTotalInfo());


                if (item.Way == FeeWay.AR)
                {
                    feeTotals[item.CurrencyName].Debit += item.Amount;
                    feeTotals[item.CurrencyName].PadAmount += item.PayAmount;
                }
                else
                {
                    feeTotals[item.CurrencyName].Credit += item.Amount;
                    feeTotals[item.CurrencyName].PadAmount -= item.PayAmount;
                }

            }

            //reportData.TotalAmount = reportData.MainCurrency + ":" + totalAmount.ToString("F2");//应收总金额 主币种

            #endregion

            #region 应收总金额 分币种

            foreach (var item in feeTotals)
            {
                reportData.TotalAmount += item.Key + ":" + (item.Value.Debit - item.Value.Credit).ToString("N") + " ";
                reportData.DRTotalAmount += item.Key + ":" + item.Value.Debit.ToString("N") + " ";
                reportData.CRTotalAmount += item.Key + ":" + item.Value.Credit.ToString("N") + " ";
                reportData.CrAmount += item.Key + ":" + (item.Value.Debit - item.Value.Credit).ToString("N") + " ";
                reportData.APAmount += item.Key + ":" + (item.Value.Credit - item.Value.Debit).ToString("N") + " ";

                reportData.PaidAmount += item.Key + ":" + item.Value.PadAmount.ToString("N") + " ";
                reportData.FinAmount += item.Key + ":" + ((item.Value.Debit - item.Value.Credit) - item.Value.PadAmount).ToString("N") + " ";

            }
            #endregion

            #region 显示

            string reportPath = FinancePrintHelper.GetOEReportPath();
            #region reportName
            if (billReportNameEnum == BillReportNameEnum.LocalInvoiceDR)
            {
                if (billList.CompanyID == _ThailandCompanyId)
                {
                    if (printBillConfigData.CompanyBank == _ThailandCompanyId)
                    {
                        //string taxAmount = string.Empty; //-%1
                        //string vatTaxAmount = string.Empty; //%7
                        //string subTotalAmount = string.Empty; //%107
                        //string netTotalAmount = string.Empty; //%106
                        foreach (var item in feeTotals)
                        {
                            //百分之1的退税
                            reportData.TaxAmountForTHAILAND += item.Key + ": - " + ((item.Value.Debit - item.Value.Credit) * 0.01m).ToString("F2") + " ";
                            //百分之7的税
                            reportData.VatTaxAmountForTHAILAND += item.Key + ":" + ((item.Value.Debit - item.Value.Credit) * 0.07m).ToString("F2") + " ";

                            //原金额 + 百分之7的税
                            reportData.SubTotalAmountForTHAILAND += item.Key + ":" + ((item.Value.Debit - item.Value.Credit) * 1.07m).ToString("F2") + " ";

                            //原金额 -百分之1的退税 + 百分之7的税 
                            reportData.NetTotalAmountForTHAILAND += item.Key + ":" + ((item.Value.Debit - item.Value.Credit) * 1.06m).ToString("F2") + " ";
                        }

                        reportPath += "LocalInvoiceDRForTHAILAND.frx";
                    }
                    else
                    {
                        reportData.CompanyBankAccount = string.Empty;
                        CompanyReportConfigureList reportConfigure = ConfigureService.GetReportConfigureList(printBillConfigData.CompanyBank, BillReportConfigConstants.CustomerBillConfig);
                        if (_reportConfigure != null && _reportConfigure.Parameters != null && _reportConfigure.Parameters.Count > 0)
                        {
                            ReportParameterList para = reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.BillBankInfo_EN);
                            if (para != null && !string.IsNullOrEmpty(para.ParameterValue))
                            {
                                reportData.CompanyBankAccount = para.ParameterValue.ToString();
                            }
                        }

                        reportPath += "LocalInvoiceDRForTHAILAND_0.frx";
                    }
                }
                else if (billList.CompanyID == _HARVESTCompanyId)
                {
                    reportData.CompanySignature = "HARVEST LOGISTIC CORP";
                    reportPath += "LocalInvoiceDR.frx";
                }
                else if (billList.CompanyID == _JDYCompanyId)
                {
                    reportData.CompanySignature = "JDY INTERNATIONAL INC.";
                    if (!string.IsNullOrEmpty(reportData.Remark))
                        reportData.Remark += Environment.NewLine;

                    reportData.Remark += ("Upon Request, We shall provide a detailed breakout of the components of all charges" +
                    Environment.NewLine + "assessed and a true copy of each pertinent docuement relating to the charges.");
                    reportPath += "LocalInvoiceDR.frx";
                }
                else
                {
                    reportData.CompanySignature = "CITY OCEAN INT'L INC.";
                    reportPath += "LocalInvoiceDR.frx";
                }
            }
            else if (billReportNameEnum == BillReportNameEnum.LocalInvoiceDRCR)
            {
                if (billInfo.CompanyID == new Guid("0501D29D-0EFE-E111-B376-0026551CA87B"))  //巴西
                {
                    reportData.BillToCustomerAdd = reportData.BillToCustomerAdd.Replace("\r\n", " ");
                    CustomerInfo customer = CustomerService.GetCustomerInfo(billInfo.CustomerID);
                    reportData.BillToCustomerName = customer.EName;
                    reportData.BillToCustomerCity = string.IsNullOrEmpty(customer.CityName) ? string.Empty : customer.CityName;
                    reportData.BillToCustomerPostCode = string.IsNullOrEmpty(customer.PostCode) ? string.Empty : customer.PostCode;

                    DirectoryInfo dir = new DirectoryInfo(Application.StartupPath + "\\Reports\\BrazilLOGO\\");
                    FileInfo[] files = dir.GetFiles();
                    reportData.LogoPathForBrazil = files[0].FullName;
                    reportPath += "BillReportBrazil.frx";
                }
                else if (billInfo.CompanyID == new Guid("82D15564-0786-E211-BCDE-0026551CA87B"))  //澳大利亚
                {
                    reportPath += "BillReport_AUS.frx";
                }
                else
                {
                    reportPath += "LocalInvoiceDRCR.frx";
                }
            }
            else if (billReportNameEnum == BillReportNameEnum.LocalInvoiceCR)
                reportPath += "LocalInvoiceCR.frx";
            else
            {
                reportPath += "LocalInvoiceDR.frx";
            }


            #endregion

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Bill" : "打印帐单",
            (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            string BillName = string.Empty;
            DocumentType DocType = DocumentType.AP;

            BillName = billList.FormNo;
            DocType = (DocumentType)Enum.Parse(typeof(DocumentType), billInfo.Type.ToString());

            reportSource.Add(CommonConstants.DocumentNameKey, BillName);
            reportSource.Add(CommonConstants.DocumentTypeKey, DocType);

            reportData.Memo = string.Empty;
            reportSource.Add("ReportSource", reportData);
            reportSource.Add("Fees", fees);
            viewer.BindData(reportPath, reportSource, null, GetOperationInfo(billInfo));

            #endregion

            #region 后续动作

            //viewer.AfterPrint += delegate
            //{
            //    SetBillIsSend(new List<BillList> { billList });
            //};

            //viewer.BeforeSendEmail += delegate
            //{
            //    SetBillIsSend(new List<BillList> { billList });
            //};

            //viewer.AfterExport += delegate
            //{
            //    SetBillIsSend(new List<BillList> { billList });
            //};

            #endregion
        }


        public virtual void PrintCombinBill(List<BillList> billLists
                                            , OperationCommonInfo operationCommonInfo
                                            , ConfigureInfo configureInfo
                                            , List<SolutionExchangeRateList> rates)
        {



            BillInfo billInfo = FinanceService.GetBillInfo(billLists[0].ID);

            BillType billtype = BillType.None;

            billtype = billLists[0].Type;


            List<Guid> billIDs = new List<Guid>();
            foreach (var item in billLists) { billIDs.Add(item.ID); }

            #region 构建PrintBillConfig

            PrintBillConfigData printBillConfigData = new PrintBillConfigData();
            printBillConfigData.BillID = billInfo.ID;
            printBillConfigData.To = billInfo.CustomerDescription == null ? string.Empty : billInfo.CustomerDescription.ToString(LocalData.IsEnglish);
            printBillConfigData.PrintData = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
            if (operationCommonInfo.OperationType == OperationType.OceanImport) printBillConfigData.IsShowFETA = true;

            PrintBillConfigPart pb = Workitem.Items.AddNew<PrintBillConfigPart>();
            pb.DataSource = printBillConfigData;
            Dictionary<string, object> keyValue = new Dictionary<string, object>();
            keyValue.Add("BLCommonInfo", operationCommonInfo);
            keyValue.Add("ConfigureInfo", configureInfo);
            keyValue.Add("SolutionExchangeRateList", rates);
            keyValue.Add("BillInfo", billInfo);

            pb.Init(keyValue);
            if (FAMUtility.ShowDialog(pb, LocalData.IsEnglish ? "Print Bill Config" : "打印帐单配置") != DialogResult.OK) return;
            printBillConfigData = pb.DataSource as PrintBillConfigData;

            #endregion

            #region 根据中英文显示费用
            if (billIDs.Count > 0)
            {
                List<ChargeList> feeForPrint = FinanceService.GetChargeListForPrintBill(billIDs.ToArray(), printBillConfigData.IsEN);
                billInfo.Fees = feeForPrint;
            }
            #endregion

            #region 把ConfigForm里的数据转成ReportData
            CommonBillReportData commonBillReportData = FinanceReportService.GetCommonBillReportData(operationCommonInfo.OperationID);
            CommonBillReportSource reportData = new CommonBillReportSource();
            FAMUtility.CopyToValue(commonBillReportData, reportData, typeof(CommonBillReportData));
            reportData.BillDate = printBillConfigData.PrintData;
            reportData.BillNo = billInfo.No;

            //后缀号的处理
            if (!string.IsNullOrEmpty(printBillConfigData.SuffixNo))
            {
                int length = printBillConfigData.SuffixNo.Length;

                for (int i = 0; i < 5 - length; i++)
                    printBillConfigData.SuffixNo = "0" + printBillConfigData.SuffixNo;

                reportData.BillNo += ("-" + printBillConfigData.SuffixNo);
            }

            reportData.OperationType = operationCommonInfo.OperationType;
            //reportData.BillNote = "Tax Invoice";
            reportData.BillNote = printBillConfigData.BillNote;
            reportData.BLNo = printBillConfigData.BLNO;
            reportData.CommonRemark = billInfo.Remark;
            reportData.CompanyDsc = printBillConfigData.TitelCompanyDes;
            reportData.CompanyName = printBillConfigData.TitelCompanyName;
            reportData.OperationType = operationCommonInfo.OperationType;
            reportData.To = printBillConfigData.To;
            reportData.BillDate = printBillConfigData.PrintData;
            reportData.ETACaption = operationCommonInfo.OperationType == OperationType.OceanImport ? "FETA" : "ETA";

            if (operationCommonInfo.CompanyID == _MLCompanyId)
            {
                reportData.GST = "GST Reg No:001775427584";
            }


            if (operationCommonInfo.OperationType == OperationType.OceanImport)
            {
                if (printBillConfigData.IsShowFETA == false)
                    reportData.FETA = null;
            }
            else
            {
                reportData.FETA = reportData.ETA;
            }

            reportData.LogoPath = printBillConfigData.LogoFileName;
            if (operationCommonInfo.OperationType == OperationType.OceanExport)
            {
                if (printBillConfigData.PrintBillShipType == PrintBillShipType.PreShip)
                    reportData.VesselVoyage = reportData.PreVesselVoyage;
            }

            StringBuilder otherRemark = new StringBuilder();
            if (printBillConfigData.BankInfo.IsNullOrEmpty() == false)
            {
                otherRemark.Append(printBillConfigData.BankInfo + "\r\n");
            }

            bool rateInfoIsNeedHide = false;   //处理类似马来西亚帐单不需要显示汇率信息的需求
            if (_reportConfigure != null && _reportConfigure.Parameters != null)
            {
                ReportParameterList reportParameter = _reportConfigure.Parameters.Find(p => p.Code == BillReportConfigConstants.RateInfoIsHide);
                if (reportParameter != null && reportParameter.ParameterValue.IsNullOrEmpty() == false)
                {
                    bool rateInfoIsHide = false;
                    if (bool.TryParse(reportParameter.ParameterValue, out rateInfoIsHide))
                    {
                        rateInfoIsNeedHide = rateInfoIsHide;
                    }
                }
            }

            if (!rateInfoIsNeedHide && printBillConfigData.RateInfo.IsNullOrEmpty() == false && printBillConfigData.TitelCompanyName != "CITY OCEAN INTERNATIONAL INC., (LOS ANGELES OFFICE)")
            {
                otherRemark.Append(printBillConfigData.RateInfo);
            }

            if (printBillConfigData.RemarkInfo.IsNullOrEmpty() == false)
            {
                otherRemark.Append(printBillConfigData.RemarkInfo + "\r\n");
            }

            reportData.OtherRemark = otherRemark.ToString();


            UserInfo printBy = UserService.GetUserInfo(LocalData.UserInfo.LoginID);
            reportData.PrintBy = printBy.EName;
            reportData.PrintByFax = printBy.Fax;
            reportData.PrintByTel = printBy.Tel;

            reportData.Signature = printBillConfigData.Signature;
            reportData.SuffixNo = printBillConfigData.SuffixNo;
            #endregion

            #region 费用

            reportData.Fees = new List<ChargeItemReportData>();
            List<ChargeList> chargeList = billInfo.Fees;

            #region  普通帐单要去掉佣金费用

            List<ChargingCodeList> commissionCodes = ConfigureService.GetChargingCodeListBySearch(string.Empty, string.Empty, null, true, true, 0);
            List<Guid> commissionIds = new List<Guid>();
            foreach (var item in commissionCodes)
            {
                commissionIds.Add(item.ID);
            }
            //commissionIds.Add(_MLGST);//打印账单时不用统计马来西亚的GST

            if (printBillConfigData.PrintBillType == PrintBillType.Normal)
            {
                foreach (var item in chargeList)
                {
                    if (commissionIds.Contains(item.ChargingCodeID)) continue;
                    if (operationCommonInfo.CompanyID == _MLCompanyId && _MLGST == item.ChargingCodeID) continue;

                    ChargeItemReportData reportcharge = new ChargeItemReportData();
                    reportcharge.UnitPrice = item.UnitPrice;

                    if (item.Way == FeeWay.AR)
                    {
                        if (operationCommonInfo.CompanyID == _MLCompanyId && item.IsGST && billtype == BillType.AR)
                        {
                            reportcharge.Debit = item.Amount;
                            reportcharge.DebitGST = item.Amount + item.Amount * Convert.ToDecimal(0.06);
                            reportcharge.GstTaxRate = item.Amount * Convert.ToDecimal(0.06);
                        }
                        else
                        {
                            reportcharge.Debit = item.Amount;
                            reportcharge.DebitGST = item.Amount;
                        }

                        reportcharge.ForPrint = reportcharge.DebitForPrint;
                    }
                    else
                    {
                        if (operationCommonInfo.CompanyID == _MLCompanyId && item.IsGST && billtype == BillType.AP)
                        {
                            reportcharge.Credit = item.Amount;
                            reportcharge.DebitGST = item.Amount + item.Amount * Convert.ToDecimal(0.06);
                            reportcharge.GstTaxRate = item.Amount * Convert.ToDecimal(0.06);
                        }
                        else
                        {
                            reportcharge.Credit = item.Amount;
                            reportcharge.DebitGST = item.Amount;
                        }

                        reportcharge.ForPrint = reportcharge.CreditForPrint;
                    }

                    reportcharge.ChargeName = LocalData.IsEnglish == printBillConfigData.IsEN ? item.ChargingDescription : item.ChargingCode;
                    reportcharge.Currency = item.CurrencyName;
                    reportcharge.Qty = item.Quantity;
                    reportcharge.Rate = item.Rate;
                    reportcharge.Way = item.Way;
                    reportData.Fees.Add(reportcharge);
                }
            }
            else
            {
                foreach (var item in chargeList)
                {
                    if (commissionIds.Contains(item.ChargingCodeID) == false) continue;

                    ChargeItemReportData reportcharge = new ChargeItemReportData();
                    reportcharge.UnitPrice = item.UnitPrice;

                    if (item.Way == FeeWay.AR)
                    {
                        if (operationCommonInfo.CompanyID == _MLCompanyId && item.IsGST && billtype == BillType.AR)
                        {
                            reportcharge.Debit = item.Amount;
                            reportcharge.DebitGST = item.Amount + item.Amount * Convert.ToDecimal(0.06);
                            reportcharge.GstTaxRate = item.Amount * Convert.ToDecimal(0.06);
                        }
                        else
                        {
                            reportcharge.Debit = item.Amount;
                            reportcharge.DebitGST = item.Amount;
                        }
                        reportcharge.ForPrint = reportcharge.DebitForPrint;
                    }
                    else
                    {

                        if (operationCommonInfo.CompanyID == _MLCompanyId && item.IsGST && billtype == BillType.AP)
                        {
                            reportcharge.Credit = item.Amount;
                            reportcharge.DebitGST = item.Amount + item.Amount * Convert.ToDecimal(0.06);
                            reportcharge.GstTaxRate = item.Amount * Convert.ToDecimal(0.06);
                        }
                        else
                        {
                            reportcharge.Credit = item.Amount;
                            reportcharge.DebitGST = item.Amount;
                        }

                        reportcharge.ForPrint = reportcharge.CreditForPrint;
                    }

                    reportcharge.ChargeName = LocalData.IsEnglish == printBillConfigData.IsEN ? item.ChargingDescription : item.ChargingCode;
                    reportcharge.Currency = item.CurrencyName;
                    reportcharge.Qty = item.Quantity;
                    reportcharge.Rate = item.Rate;
                    reportcharge.Way = item.Way;
                    reportData.Fees.Add(reportcharge);
                }
            }

            #endregion

            #region Total

            List<FeeTotalInfo> feeTotal = new List<FeeTotalInfo>();

            //构建一个币种列表
            Dictionary<string, decimal> dic = new Dictionary<string, decimal>();
            foreach (var item in reportData.Fees)
            {
                if (dic.ContainsKey(item.Currency) == false) dic.Add(item.Currency, 0);

                dic[item.Currency] += item.Debit;
                dic[item.Currency] -= item.Credit;
            }

            //foreach (var item in dic)
            //{
            //    FeeTotalInfo total = new FeeTotalInfo();
            //    total.Currency = item.Key;
            //    if (item.Value >= 0)
            //        total.Debit = item.Value;
            //    else
            //        total.Credit = -item.Value;

            //    feeTotal.Add(total);
            //}

            //if (feeTotal != null && feeTotal.Count > 0)
            //{
            //    feeTotal[0].Description = "Total:";
            //}


            IEnumerable<IGrouping<string, ChargeItemReportData>> arr = reportData.Fees.GroupBy(i => i.Currency);
            foreach (IGrouping<string, ChargeItemReportData> item in arr)
            {
                List<ChargeItemReportData> listByCurrencyGroup = item.ToList<ChargeItemReportData>();
                FeeTotalInfo total = new FeeTotalInfo();
                total.Currency = item.Key;
                total.Debit = listByCurrencyGroup.Sum(i => i.Debit);
                total.Credit = listByCurrencyGroup.Sum(i => i.Credit);
                total.GstTaxRate = listByCurrencyGroup.Sum(i => i.GstTaxRate);

                if (billtype == BillType.AP)
                {
                    total.TotalGst = total.Credit + total.GstTaxRate;
                    total.SumTotal = total.Credit;
                }
                else
                {
                    total.TotalGst = total.Debit + total.GstTaxRate;
                    total.SumTotal = total.Debit;
                }
                feeTotal.Add(total);
            }

            if (feeTotal != null && feeTotal.Count > 0)
            {
                feeTotal[0].Description = "Total:";
            }

            #endregion

            #region Balance

            List<FeeTotalInfo> feeBalance = new List<FeeTotalInfo>();
            if (printBillConfigData.IsShowMainCurrency == false)
            {
                //FeeTotalInfo usdTotal = new FeeTotalInfo();
                //usdTotal.Currency = "USD";
                //foreach (var item in dic)
                //{
                //    usdTotal.Debit += item.Value * RateHelper.GetRate(item.Key, "USD", DateTime.SpecifyKind(DateTime.Now,DateTimeKind.Unspecified), rates);
                //}
                //feeBalance.Add(usdTotal);
                foreach (var item in feeTotal)
                {
                    FeeTotalInfo balanceItem = new FeeTotalInfo();
                    balanceItem.Currency = item.Currency;
                    balanceItem.Debit = item.Debit - item.Credit;
                    feeBalance.Add(balanceItem);
                }
            }
            else
            {
                FeeTotalInfo mainCurrencyTotal = new FeeTotalInfo();
                mainCurrencyTotal.Currency = printBillConfigData.PrintBillCacheInfo.ConfigureInfo.StandardCurrency;
                foreach (var item in dic)
                {
                    mainCurrencyTotal.Debit += item.Value * RateHelper.GetRate(item.Key, printBillConfigData.PrintBillCacheInfo.ConfigureInfo.StandardCurrency, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), rates);
                }

                feeBalance.Add(mainCurrencyTotal);
            }

            if (feeBalance.Count > 0)
            {
                feeBalance[0].Description = "Balance:";
            }

            //是否预算增值税
            if (printBillConfigData.IsTrial)
            {
                reportData.CommonRemark += LocalData.IsEnglish ? "If you need to open a VAT （tax rate " + printBillConfigData.TaxRate.ToString() + "%）,After-tax total amount will be "
                                      : "如果需要开增值税发票(税率" + printBillConfigData.TaxRate.ToString() + "%), 税后总金额为：";
                if (printBillConfigData.IsSepc)
                {
                    foreach (var item in feeTotal)
                    {
                        reportData.CommonRemark += item.Currency + ":" + ((item.Debit - item.Credit) * printBillConfigData.TaxRate / 100).ToString("n") + " ";
                    }
                }
                else
                {
                    decimal amount = 0m;
                    foreach (var item in feeTotal)
                    {
                        amount += (item.Debit - item.Credit) * RateHelper.GetRate(item.Currency, printBillConfigData.PrintBillCacheInfo.ConfigureInfo.DefaultCurrency, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), rates);
                    }
                    reportData.CommonRemark += printBillConfigData.PrintBillCacheInfo.ConfigureInfo.DefaultCurrency + ":" + (amount * printBillConfigData.TaxRate / 100).ToString("n") + " ";
                }
            }


            #endregion

            #endregion

            #region 显示报表

            IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? "Print Bill" : "打印帐单",
             (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);

            string fileName = "";

            if ((billtype == BillType.AR || billtype == BillType.AP) && operationCommonInfo.CompanyID == _MLCompanyId)
            {
                fileName = FinancePrintHelper.GetOEReportPath() + "BillReport_ML.frx";
            }
            else
            {
                fileName = FinancePrintHelper.GetOEReportPath() + (printBillConfigData.IsEN ? "BillReport_EN.frx" : "BillReport_CN.frx");
            }

            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            string BillName = string.Empty;
            DocumentType DocType = DocumentType.AP;

            BillName = billLists[0].FormNo;
            DocType = (DocumentType)Enum.Parse(typeof(DocumentType), billInfo.Type.ToString());

            reportSource.Add(CommonConstants.DocumentNameKey, BillName);
            reportSource.Add(CommonConstants.DocumentTypeKey, DocType);
            reportSource.Add("ReportSource", reportData);
            reportSource.Add("Fees", reportData.Fees);
            reportSource.Add("Total", feeTotal);
            reportSource.Add("Balance", feeBalance);

            viewer.BindData(fileName, reportSource, null, GetOperationInfo(billInfo));
            #endregion

            #region 后续动作

            viewer.AfterPrint += delegate
            {
                //如果 有后缀号 把后缀号保存
                if (!string.IsNullOrEmpty(reportData.SuffixNo))
                    FinanceReportService.SaveSuffixNo(billInfo.ID, reportData.SuffixNo, LocalData.UserInfo.LoginID);

                //SetBillIsSend(billLists);
            };


            //viewer.AfterSendEmail += delegate
            //{
            //    SetBillIsSend(billLists);
            // };

            //viewer.AfterExport += delegate
            //{
            //    SetBillIsSend(billLists);
            //};

            #endregion
        }

        private Message.ServiceInterface.Message GetOperationInfo(BillInfo billInfo)
        {
            if (billInfo == null)
                return null;
            Message.ServiceInterface.Message message = new Message.ServiceInterface.Message();
            message.UserProperties = new MessageUserPropertiesObject();
            message.UserProperties.OperationType = OperationType.OceanExport;
            message.UserProperties.OperationId = billInfo.OperationID;
            message.UserProperties.FormType = FormType.Bill;
            message.UserProperties.FormId = billInfo.OperationID;
            return message;
        }

        #endregion

        private void SetBillIsSend(List<BillList> billLists)
        {
            //if (billLists == null || billLists.Count <= 0)
            //{
            //    return;
            //}
            //List<Guid> ids = new List<Guid>();
            //List<DateTime?> updateDates = new List<DateTime?>();

            //foreach (var item in billLists)
            //{
            //    ids.Add(item.ID);
            //    updateDates.Add(item.UpdateDate);
            //}
            //try
            //{
            //    ManyResult result = FinanceService.SetBillIsSend(ids.ToArray(), LocalData.UserInfo.LoginID, updateDates.ToArray());
            //    if (UpdatedBill != null) UpdatedBill(this, result);
            //}
            //catch (Exception ex) { DevExpress.XtraEditors.XtraMessageBox.Show((LocalData.IsEnglish ? "Set Bill Sended Faily." : "设置寄单状态失败.") + "\r\n" + ex.ToString()); }
        }

    }


    #region OE
    public class OEBillController : BillController
    {

    }
    #endregion

    #region OI
    public class OIBillController : BillController
    {

    }

    #endregion

    #region 未实现
    public class AEBillController : BillController
    {

    }
    public class AIBillController : BillController
    {

    }
    public class OtherBillController : BillController
    {

    }
    public class CustomsBillController : BillController
    {

    }
    public class TruckBillController : BillController
    {

    }

    public class InternalBillController : BillController
    {

    }

    #endregion
}
