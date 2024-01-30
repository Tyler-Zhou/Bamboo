using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface.Client;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects.Report;
using ICP.Framework.CommonLibrary.Client;
using Microsoft.Practices.CompositeUI.SmartParts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace ICP.FAM.UI.WriteOff
{
    partial class WriteOffEditPart
    {
        #region 打印支票

        /// <summary>
        /// 打印支票
        /// </summary>
        private void PrintCash(ReportParameterList para)
        {
            if (para == null) return;
            WriteOffItemInfo currentItemInfo = bsWriteOff.DataSource as WriteOffItemInfo;
            List<WriteOffBill> currentBillList = bsBills.DataSource as List<WriteOffBill>;
            List<WriteOffCharge> currentChargeList = UCcharges.DataSource as List<WriteOffCharge>;
            string fileName = string.Empty;
            string titleString = string.Empty;
            CashReportData reportData = new CashReportData();
            Dictionary<string, object> reportSource = new Dictionary<string, object>();

            #region 构建数据源
            try
            {
                reportData.BaseReportData = new CashBaseReportData();
                reportData.BaseReportData.CustomerName = currentItemInfo.CustomerName;
                reportData.BaseReportData.Remark = currentItemInfo.Remark;
                reportData.BaseReportData.CheckNO = currentItemInfo.CheckNo;
                decimal amount = 0m;
                if (currentBillList != null && currentBillList.Count > 0)
                {
                    #region Amount

                    IEnumerable<IGrouping<Guid, WriteOffBill>> arr = currentBillList.GroupBy(i => i.CurrencyID);
                    foreach (IGrouping<Guid, WriteOffBill> item in arr)  //应该只有一个币种
                    {
                        List<WriteOffBill> listByGroup = item.ToList<WriteOffBill>();
                        amount = listByGroup.Sum(p => p.Way == FeeWay.AR ? p.WriteOffAmount : -p.WriteOffAmount);
                    }

                    #endregion
                }

                if (currentChargeList != null && currentChargeList.Count > 0)
                {
                    amount += currentChargeList.Sum(i => i.Way == FeeWay.AR ? i.Amount : -i.Amount);
                }

                reportData.BaseReportData.Amount = Math.Abs(amount).ToString("n");

                #region Total
                reportData.BaseReportData.Total = ReportHelper.GetText(Math.Abs(Convert.ToDecimal(reportData.BaseReportData.Amount)));
                #endregion

                fileName = Application.StartupPath + "\\Reports\\FAM\\";
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
            }
            #endregion

            if (para.ParameterValue.ToUpper() == ("check_la").ToUpper() ||
                (currentItemInfo.Way == FeeWay.AR &&
                (para.ParameterValue.ToUpper() == ("check_ca").ToUpper() || para.ParameterValue.ToUpper() == ("check_jdy").ToUpper() ||
                para.ParameterValue.ToUpper() == ("check_nj").ToUpper())))  //洛杉矶公司
            {
                reportData.BaseReportData.CheckDate = currentItemInfo.CheckDate.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);

                #region 拼装客户地址
                CustomerInfo customerInfo = CustomerService.GetCustomerInfo(currentItemInfo.CustomerID);
                reportData.BaseReportData.CustomerEAddress = customerInfo.EAddress;
                if (!string.IsNullOrEmpty(customerInfo.EMail))
                {
                    reportData.BaseReportData.CustomerEAddress += Environment.NewLine + "EMAIL: " + customerInfo.EMail;
                }

                if (!string.IsNullOrEmpty(customerInfo.Tel1))
                {
                    reportData.BaseReportData.CustomerEAddress += Environment.NewLine + "TEL: " + customerInfo.Tel1;
                }

                if (!string.IsNullOrEmpty(customerInfo.Fax))
                {
                    reportData.BaseReportData.CustomerEAddress += Environment.NewLine + "FAX: " + customerInfo.Tel1;
                }

                List<CustomerContactList> contacts = CustomerService.GetCustomerContactList(currentItemInfo.CustomerID);
                if (contacts != null && contacts.Count > 0)
                {
                    reportData.BaseReportData.CustomerEAddress += Environment.NewLine + "Attn: " + contacts[0].EName;
                }

                #endregion

                #region 账单明细
                reportData.BillList = new List<CashBillReportData>();
                if (currentBillList != null && currentBillList.Count > 0)
                {
                    List<Guid> billIds = new List<Guid>();

                    foreach (var item in currentBillList)
                    {
                        billIds.Add(item.BillID);

                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = item.RefNo;
                        billItem.BillNo = item.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == item.Way ? item.WriteOffAmount.ToString("n") : (-item.WriteOffAmount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }

                    List<CurrencyBillList> billList = FinanceService.GetBillListByIds(billIds.ToArray(), LocalData.IsEnglish);
                    foreach (var bi in reportData.BillList)
                    {
                        CurrencyBillList billFind = (from d in billList where d.BillNO == bi.BillNo select d).Take(1).SingleOrDefault();
                        if (billFind != null)
                        {
                            bi.BLNo = billFind.BLNo;
                            bi.RefNo = billFind.OperationNO;
                        }
                    }
                }

                if (currentChargeList != null && currentChargeList.Count > 0)
                {
                    foreach (var charge in currentChargeList)
                    {
                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = charge.GLDescription;
                        billItem.BillNo = charge.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == charge.Way ? charge.Amount.ToString("n") : (-charge.Amount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }
                }

                #endregion

                fileName += "RPT_Check_LA.frx";
                titleString = "LA";
                reportSource.Add("BaseReportData", reportData.BaseReportData);
                if (reportData.BillList != null)
                {
                    reportSource.Add("BillListReportData", reportData.BillList);
                }
            }
            else if ((currentItemInfo.Way == FeeWay.AP &&
                (para.ParameterValue.ToUpper() == ("check_nj").ToUpper() || para.ParameterValue.ToUpper() == ("check_jdy").ToUpper())) ||
                para.ParameterValue.ToUpper() == ("check_Pacgran").ToUpper())  //芝加哥公司,CTC International Inc.,纽约公司 或者 PACGRAN INC.(拖车公司支票)
            {
                reportData.BaseReportData.CheckDate = currentItemInfo.CheckDate.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);

                #region 拼装客户地址
                CustomerInfo customerInfo = CustomerService.GetCustomerInfo(currentItemInfo.CustomerID);
                reportData.BaseReportData.CustomerEAddress = customerInfo.EAddress;
                #endregion

                #region 账单明细

                reportData.BillList = new List<CashBillReportData>();
                if (currentBillList != null && currentBillList.Count > 0)
                {
                    List<Guid> billIds = new List<Guid>();

                    foreach (var item in currentBillList)
                    {
                        billIds.Add(item.BillID);

                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = item.RefNo;
                        billItem.BillNo = item.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == item.Way ? item.WriteOffAmount.ToString("n") : (-item.WriteOffAmount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }

                    List<CurrencyBillList> billList = FinanceService.GetBillListByIds(billIds.ToArray(), LocalData.IsEnglish);
                    foreach (var bi in reportData.BillList)
                    {
                        CurrencyBillList billFind = (from d in billList where d.BillNO == bi.BillNo select d).Take(1).SingleOrDefault();
                        if (billFind != null)
                        {
                            bi.BLNo = billFind.BLNo;
                            bi.RefNo = billFind.OperationNO;
                            if (para.ParameterValue.ToUpper() == "CHECK_NJ" || para.ParameterValue.ToUpper() == "CHECK_JDY")
                            {
                                bi.BillNo = billFind.BillOrCustomerRefNo;
                            }
                        }
                    }
                }

                if (currentChargeList != null && currentChargeList.Count > 0)
                {
                    foreach (var charge in currentChargeList)
                    {
                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = charge.GLDescription;
                        billItem.BillNo = charge.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == charge.Way ? charge.Amount.ToString("n") : (-charge.Amount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }
                }

                #endregion

                if (para.ParameterValue.ToUpper() == ("check_nj").ToUpper())
                {
                    fileName += "RPT_Check_NJ.frx";
                    titleString = "NJ";
                }
                else if (para.ParameterValue.ToUpper() == ("check_jdy").ToUpper())
                {
                    fileName += "RPT_Check_NJ.frx";
                    titleString = "JDY";
                }
                else
                {
                    if (currentItemInfo.Way == FeeWay.AR)
                    {
                        fileName += "RPT_Check_Pacgran.frx";
                    }
                    else
                    {
                        fileName += "RPT_Check_PacgranAP.frx";
                    }

                    titleString = "Pring Pacgran Check";
                }

                reportSource.Add("BaseReportData", reportData.BaseReportData);
                if (reportData.BillList != null)
                {
                    reportSource.Add("BillListReportData", reportData.BillList);
                }
            }
            else if (currentItemInfo.Way == FeeWay.AP && para.ParameterValue.ToUpper() == ("check_ca").ToUpper())   //温哥华公司
            {
                reportData.BaseReportData.CheckDate = currentItemInfo.CheckDate.ToString("MMMM dd,yyyy");
                List<OperationCurrencyAmountList> currentCurrencyAmountList = UCAccountListInfo.DataSource as List<OperationCurrencyAmountList>;
                reportData.BaseReportData.CurrencyName = currencyList[currentCurrencyAmountList[0].CurrencyID];

                #region 账单
                reportData.BillList = new List<CashBillReportData>();
                List<Guid> ids = new List<Guid>();
                foreach (var item in currentBillList)
                {
                    ids.Add(item.BillID);
                    CashBillReportData billItem = new CashBillReportData();
                    billItem.RefNo = item.RefNo;
                    billItem.BillNo = item.BillNo; 
                    billItem.WriteOffAmount = currentItemInfo.Way == item.Way ? item.WriteOffAmount.ToString("n") : (-item.WriteOffAmount).ToString("n");
                    reportData.BillList.Add(billItem);
                }

                List<CurrencyBillList> billList = FinanceService.GetBillListByIds(ids.ToArray(), LocalData.IsEnglish);
                foreach (var bi in reportData.BillList)
                {
                    CurrencyBillList billFind = (from d in billList where d.BillNO == bi.BillNo select d).Take(1).SingleOrDefault();
                    if (billFind != null)
                    {
                        bi.BillNo = billFind.BillOrCustomerRefNo;
                        bi.BLNo = billFind.BLNo;
                        bi.RefNo = billFind.OperationNO;
                    }
                }

                if (currentChargeList != null && currentChargeList.Count > 0)
                {
                    foreach (var charge in currentChargeList)
                    {
                        CashBillReportData billItem = new CashBillReportData();
                        billItem.RefNo = charge.GLDescription;
                        billItem.BillNo = charge.BillNo;
                        billItem.WriteOffAmount = currentItemInfo.Way == charge.Way ? charge.Amount.ToString("n") : (-charge.Amount).ToString("n");
                        reportData.BillList.Add(billItem);
                    }
                }

                int count = reportData.BillList.Count;
                if (count > 15 && count < 31)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        reportData.BaseReportData.CustomerRefNO1 += reportData.BillList[i].BillNo + "\r\n";
                        reportData.BaseReportData.Description1 += reportData.BillList[i].BLNo + "\r\n";
                        reportData.BaseReportData.Amount1 += Convert.ToDecimal(reportData.BillList[i].WriteOffAmount.ToString()).ToString("N") + "\r\n";
                    }
                    for (int i = 15; i < count; i++)
                    {
                        reportData.BaseReportData.CustomerRefNO2 += reportData.BillList[i].BillNo + "\r\n";
                        reportData.BaseReportData.Description2 += reportData.BillList[i].BLNo + "\r\n";
                        reportData.BaseReportData.Amount2 += Convert.ToDecimal(reportData.BillList[i].WriteOffAmount.ToString()).ToString("N") + "\r\n";
                    }
                }
                else if (count >= 0 && count < 16)
                {
                    for (int i = 0; i < count; i++)
                    {
                        reportData.BaseReportData.CustomerRefNO1 += reportData.BillList[i].BillNo + "\r\n";
                        reportData.BaseReportData.Description1 += reportData.BillList[i].BLNo + "\r\n";
                        reportData.BaseReportData.Amount1 += Convert.ToDecimal(reportData.BillList[i].WriteOffAmount.ToString()).ToString("N") + "\r\n";
                    }
                }
                else
                {
                    //ShowReport = "true";
                }
                #endregion

                fileName += "RPT_Check_CA.frx";
                titleString = "CA";
                reportSource.Add("BaseReportData", reportData.BaseReportData);
            }

            IReportViewer viewer = ReportViewService.ShowReportViewer(titleString, (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
            viewer.BindData(fileName, reportSource, null);
        }

        #endregion


        /// <summary>
        /// 打印销账单
        /// </summary>
        private void PrintCheck()
        {
            bsWriteOff.EndEdit();
            bsBills.EndEdit();

            if (IsChanged && barWriteOff.Enabled)
            {
                XtraMessageBox.Show(LocalData.IsEnglish ? "Current data has changed, want to print please save." : "当前数据有更改,欲打印请先保存.", LocalData.IsEnglish ? "Tip" : "提示");
                return;
            }

            WriteOffItemInfo currentItemInfo = bsWriteOff.DataSource as WriteOffItemInfo;
            //List<WriteOffBill> currentBillList = null;
            //if (this.cmbType.SelectedIndex == 0)
            //{
            //    currentBillList = bsBills.DataSource as List<WriteOffBill>;
            //}
            //else
            //{
            //    currentBillList = BaseDataList;
            //}
            List<WriteOffBill> currentBillList = bsBills.DataSource as List<WriteOffBill>;
            if (currentBillList == null)
            {
                currentBillList = new List<WriteOffBill>();
            }

            List<WriteOffCharge> currentChargeList = UCcharges.DataSource as List<WriteOffCharge>;
            List<OperationCurrencyAmountList> currentCurrencyAmountList = UCAccountListInfo.DataSource as List<OperationCurrencyAmountList>;
            if (currentItemInfo == null || currentCurrencyAmountList == null || currentCurrencyAmountList.Count == 0)
            {
                return;
            }

            CompanyReportConfigureList reportConfigure
                   = ConfigureService.GetReportConfigureList(currentItemInfo.CompanyID, UI.ModuleConstantsForFAM.WriteOffReportType);
            if (reportConfigure == null || reportConfigure.Parameters == null || reportConfigure.Parameters.Count == 0)
            {
                #region 收付款清单和对冲单

                string titleEName = string.Empty;
                string titleCName = string.Empty;
                try
                {
                    WriteOffBillReportData reportData = new WriteOffBillReportData();
                    reportData.BaseReportData = new WriteOffBillBaseReportData();
                    reportData.BaseReportData.CompanyName = currentItemInfo.CompanyName;
                    reportData.BaseReportData.No = currentItemInfo.No;
                    reportData.BaseReportData.Customer = currentItemInfo.CustomerName;
                    reportData.BaseReportData.Remark = currentItemInfo.Remark;
                    reportData.BaseReportData.CreateBy = currentItemInfo.CreateByName;
                    reportData.BaseReportData.PrintDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified).ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);

                    int flag = 0;
                    foreach (var bill in currentBillList)
                    {
                        if (bill.Way == FeeWay.AP)
                        {
                            flag++;
                        }
                        else if (bill.Way == FeeWay.AR)
                        {
                            flag--;
                        }
                    }

                    if (Math.Abs(flag) != currentBillList.Count)
                    {
                        titleEName = "Print Hedge Bill";
                        titleCName = "打印对冲单";
                        if (currentItemInfo.Way == FeeWay.AR)
                        {
                            reportData.BaseReportData.ReceivedOrPayedDateLabel = LocalData.IsEnglish ? "Received Date:" : "收款日期:";
                            reportData.BaseReportData.ActualReceivedOrPayedAmountLabel = LocalData.IsEnglish ? "Receive Actually:" : "实收金额:";
                            reportData.BaseReportData.UnReceivedOrPayAmountLabel = LocalData.IsEnglish ? "UnReceived Amount" : "未收金额";
                        }
                        else
                        {
                            reportData.BaseReportData.ReceivedOrPayedDateLabel = LocalData.IsEnglish ? "Payment Date:" : "付款日期:";
                            reportData.BaseReportData.ActualReceivedOrPayedAmountLabel = LocalData.IsEnglish ? "Pay Actually:" : "实付金额:";
                            reportData.BaseReportData.UnReceivedOrPayAmountLabel = LocalData.IsEnglish ? "UnPay Amount" : "未付金额";
                        }
                    }
                    else if (currentItemInfo.Way == FeeWay.AR)
                    {
                        titleEName = "Print Collection List";
                        titleCName = "打印收款清单";
                        reportData.BaseReportData.ReportTitle = LocalData.IsEnglish ? "Collection List" : "收款清单";
                        reportData.BaseReportData.ReceivedOrPayedDateLabel = LocalData.IsEnglish ? "Received Date:" : "收款日期:";
                        reportData.BaseReportData.ActualReceivedOrPayedAmountLabel = LocalData.IsEnglish ? "Receive Actually:" : "实收金额:";
                        reportData.BaseReportData.UnReceivedOrPayAmountLabel = LocalData.IsEnglish ? "UnReceived Amount" : "未收金额";
                    }
                    else
                    {
                        titleEName = "Print Payment List";
                        titleCName = "打印付款清单";
                        reportData.BaseReportData.ReportTitle = LocalData.IsEnglish ? "Payment List" : "付款清单";
                        reportData.BaseReportData.ReceivedOrPayedDateLabel = LocalData.IsEnglish ? "Payment Date:" : "付款日期:";
                        reportData.BaseReportData.ActualReceivedOrPayedAmountLabel = LocalData.IsEnglish ? "Pay Actually:" : "实付金额:";
                        reportData.BaseReportData.UnReceivedOrPayAmountLabel = LocalData.IsEnglish ? "UnPay Amount" : "未付金额";
                    }

                    string receivedOrPayedDateString = string.Empty;
                    string bank = string.Empty;
                    string actuallyAmount = string.Empty;

                    foreach (var currencyItem in currentCurrencyAmountList)
                    {
                        if (receivedOrPayedDateString != string.Empty && currencyItem.BankDate != null)
                        {
                            receivedOrPayedDateString += ", ";
                        }

                        if (actuallyAmount != string.Empty)
                        {
                            actuallyAmount += ", ";
                        }

                        if (bank != string.Empty && currencyItem.BankAccountID != null && currencyItem.BankAccountID != Guid.Empty)
                        {
                            bank += ", ";
                        }

                        string currencyName = string.Empty;
                        if (currencyList.Keys.Contains(currencyItem.CurrencyID))
                        {
                            currencyName = currencyList[currencyItem.CurrencyID];
                        }
                        else
                        {
                            currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                        }

                        if (currencyItem.BankDate != null)
                        {
                            receivedOrPayedDateString += "(" + currencyName + ")" + currencyItem.BankDate.Value.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                        }

                        actuallyAmount += "(" + currencyName + ")" + currencyItem.TotalAmount.ToString("n");
                        string bankName = string.Empty;
                        foreach (var bankItem in UCAccountListInfo.BankList)
                        {
                            if (bankItem.ID == currencyItem.BankAccountID)
                            {
                                bankName = bankItem.CurrencyName;
                                break;
                            }
                        }

                        bank += bankName;
                    }

                    reportData.BaseReportData.ReceivedOrPayedDate = receivedOrPayedDateString;
                    reportData.BaseReportData.Bank = bank;
                    reportData.BaseReportData.ActualReceivedOrPayedAmount = actuallyAmount;
                    ////reportData.BaseReportData.AmountWrittenOff = actuallyAmount;

                    reportData.DetailList = new List<WriteOffBillDetailReportData>();
                    foreach (var bill in currentBillList)
                    {
                        WriteOffBillDetailReportData billItem = new WriteOffBillDetailReportData();
                        billItem.ChargeName = bill.ChargeName;  //如果是“账单模式”，则为空
                        if (bill.IsCommission)
                        {
                            billItem.BillRefNo = bill.BillNo + "(Y)";
                        }
                        else
                        {
                            billItem.BillRefNo = bill.BillNo;
                        }

                        billItem.Currency = bill.CurrencyName;
                        billItem.WriteOffAmount = bill.Way == FeeWay.AR ? bill.WriteOffAmount.ToString("n") : (-bill.WriteOffAmount).ToString("n");
                        billItem.Amount = bill.Way == FeeWay.AR ? bill.Amount.ToString("n") : (-bill.Amount).ToString("n");
                        if (bill.AvailbeWriteOffAmount == 0)
                        {
                            //billItem.Amount = bill.Way == FeeWay.AR ? bill.WriteOffAmount.ToString("n") : (-bill.WriteOffAmount).ToString("n");
                            billItem.UnReceivedOrPayAmount = "0.00";
                        }
                        else
                        {
                            billItem.UnReceivedOrPayAmount = bill.WriteOffAmount < 0 ? "0.00" : (bill.Way == FeeWay.AR ? (bill.Amount - bill.WriteOffAmount).ToString("n") : (bill.WriteOffAmount - bill.Amount).ToString("n"));
                        }

                        billItem.FinalAmount = bill.Way == FeeWay.AR ? bill.FinalAmount.ToString("n") : (-bill.FinalAmount).ToString("n");
                        billItem.ExchangeRate = bill.ExchangeRate.ToString();
                        reportData.DetailList.Add(billItem);
                    }

                    reportData.TotalWriteOfFeeList = new List<TotalWriteOffFeeReportData>();
                    IEnumerable<IGrouping<Guid, WriteOffBill>> arr = currentBillList.GroupBy(i => i.CurrencyID);
                    foreach (IGrouping<Guid, WriteOffBill> item in arr)
                    {
                        List<WriteOffBill> listByGroup = item.ToList<WriteOffBill>();
                        TotalWriteOffFeeReportData totalFeeItem = new TotalWriteOffFeeReportData();
                        totalFeeItem.TotalBillAmount = listByGroup.Sum(p => p.Way == FeeWay.AR ? p.Amount : -p.Amount).ToString("n");
                        totalFeeItem.TotalWriteOffAmount = listByGroup.Sum(p => p.Way == FeeWay.AR ? p.WriteOffAmount : -p.WriteOffAmount).ToString("n");
                        totalFeeItem.FinalAmount = listByGroup.Sum(p => p.Way == FeeWay.AR ? p.FinalAmount : -p.FinalAmount).ToString("n");

                        if (currencyList.Keys.Contains(item.Key))
                        {
                            totalFeeItem.Currency = currencyList[item.Key];
                        }

                        reportData.TotalWriteOfFeeList.Add(totalFeeItem);
                    }

                    #region 核销金额
                    string totalWriteOfString = string.Empty;
                    foreach (var item in reportData.TotalWriteOfFeeList)
                    {
                        if (!string.IsNullOrEmpty(totalWriteOfString))
                        {
                            totalWriteOfString += ", ";
                        }

                        totalWriteOfString += "(" + item.Currency + ")" + item.TotalWriteOffAmount;
                    }

                    reportData.BaseReportData.AmountWrittenOff = totalWriteOfString;
                    #endregion

                    if (Math.Abs(flag) != currentBillList.Count)
                    {
                        reportData.DebitList = new List<TotalWriteOffFeeReportData>();
                        reportData.CreditList = new List<TotalWriteOffFeeReportData>();
                        IEnumerable<IGrouping<FeeWay, WriteOffBill>> arrWay = currentBillList.GroupBy(i => i.Way);
                        foreach (IGrouping<FeeWay, WriteOffBill> item in arrWay)
                        {
                            if (item.Key == FeeWay.AR)
                            {
                                List<WriteOffBill> listByDR = item.ToList<WriteOffBill>();

                                IEnumerable<IGrouping<Guid, WriteOffBill>> arrCurrency = listByDR.GroupBy(i => i.CurrencyID);
                                foreach (var itemByDRAndCurrency in arrCurrency)
                                {
                                    List<WriteOffBill> listByDRAndCurrency = itemByDRAndCurrency.ToList<WriteOffBill>();
                                    TotalWriteOffFeeReportData WayItem = new TotalWriteOffFeeReportData();

                                    if (currencyList.Keys.Contains(itemByDRAndCurrency.Key))
                                    {
                                        WayItem.Currency = currencyList[itemByDRAndCurrency.Key];
                                    }
                                    else
                                    {
                                        WayItem.Currency = LocalData.IsEnglish ? "Unknown" : "未知";
                                    }

                                    WayItem.TotalBillAmount = itemByDRAndCurrency.Sum(i => i.Amount).ToString("n");
                                    WayItem.TotalWriteOffAmount = itemByDRAndCurrency.Sum(i => i.WriteOffAmount).ToString("n");
                                    WayItem.FinalAmount = itemByDRAndCurrency.Sum(i => i.FinalAmount).ToString("n");
                                    reportData.DebitList.Add(WayItem);
                                }

                                if (reportData.DebitList.Count > 0)
                                {
                                    reportData.DebitList[0].LabelText = LocalData.IsEnglish ? "Debit:" : "应收:";
                                }
                            }
                            else
                            {
                                List<WriteOffBill> listByCR = item.ToList<WriteOffBill>();

                                IEnumerable<IGrouping<Guid, WriteOffBill>> arrCurrency = listByCR.GroupBy(i => i.CurrencyID);
                                foreach (var itemByCRAndCurrency in arrCurrency)
                                {
                                    List<WriteOffBill> listByDRAndCurrency = itemByCRAndCurrency.ToList<WriteOffBill>();
                                    TotalWriteOffFeeReportData cRWayItem = new TotalWriteOffFeeReportData();

                                    if (currencyList.Keys.Contains(itemByCRAndCurrency.Key))
                                    {
                                        cRWayItem.Currency = currencyList[itemByCRAndCurrency.Key];
                                    }
                                    else
                                    {
                                        cRWayItem.Currency = LocalData.IsEnglish ? "Unknown" : "未知";
                                    }

                                    cRWayItem.TotalBillAmount = (-itemByCRAndCurrency.Sum(i => i.Amount)).ToString("n");
                                    cRWayItem.TotalWriteOffAmount = (-itemByCRAndCurrency.Sum(i => i.WriteOffAmount)).ToString("n");
                                    cRWayItem.FinalAmount = (-itemByCRAndCurrency.Sum(i => i.FinalAmount)).ToString("n");
                                    reportData.CreditList.Add(cRWayItem);
                                }

                                if (reportData.CreditList.Count > 0)
                                {
                                    reportData.CreditList[0].LabelText = LocalData.IsEnglish ? "Credit:" : "应付:";
                                }
                            }
                        }
                    }

                    reportData.ChargeReportDataList = new List<WriteOffChargeReportData>();
                    foreach (var charge in currentChargeList)
                    {
                        WriteOffChargeReportData chargeItem = new WriteOffChargeReportData();
                        chargeItem.CustomerName = charge.CustomerName;
                        chargeItem.BillNo = charge.BillNo;
                        foreach (var gLItem in UCcharges.GLList)
                        {
                            if (gLItem.ID == charge.GLID)
                            {
                                chargeItem.GLDescription = LocalData.IsEnglish ? gLItem.EName : gLItem.CName;
                                break;
                            }
                        }

                        if (currencyList.Keys.Contains(charge.CurrencyID))
                        {
                            chargeItem.Currency = currencyList[charge.CurrencyID];
                        }
                        else
                        {
                            chargeItem.Currency = LocalData.IsEnglish ? "Unknown" : "未知";

                        }

                        chargeItem.Amout = charge.Way == FeeWay.AR ? charge.Amount.ToString("n") : (-charge.Amount).ToString("n");
                        chargeItem.ExchangeRate = charge.ExchangeRate.ToString();
                        chargeItem.Remark = charge.Remark;
                        reportData.ChargeReportDataList.Add(chargeItem);
                    }

                    reportData.TotalChargeFeeList = new List<TotalWriteOffFeeReportData>();
                    Dictionary<Guid, Decimal> chargeTotalList = UCcharges.GetTotalInfo();
                    foreach (var chargeTotal in chargeTotalList)
                    {
                        TotalWriteOffFeeReportData chargeTotalItem = new TotalWriteOffFeeReportData();
                        if (currencyList.Keys.Contains(chargeTotal.Key))
                        {
                            chargeTotalItem.Currency = currencyList[chargeTotal.Key];
                        }
                        else
                        {
                            chargeTotalItem.Currency = LocalData.IsEnglish ? "Unknown" : "未知";
                        }

                        chargeTotalItem.TotalBillAmount = chargeTotal.Value.ToString("n");
                        reportData.TotalChargeFeeList.Add(chargeTotalItem);
                    }

                    IReportViewer viewer = ReportViewService.ShowReportViewer(LocalData.IsEnglish ? titleEName : titleCName, (IWorkspace)Workitem.Workspaces[ClientConstants.MainWorkspace]);
                    string fileName = Application.StartupPath + "\\Reports\\FAM\\";
                    if (Math.Abs(flag) != currentBillList.Count)
                    {
                        if (cmbType.SelectedIndex == 0)
                        {
                            fileName += "HedgeBill_CN.frx";
                        }
                        else
                        {
                            fileName += "HedgeFee_CN.frx";
                        }
                    }
                    else if (cmbType.SelectedIndex == 0)
                    {
                        fileName += "WriteOffBill_CN.frx";
                    }
                    else
                    {
                        fileName += "WriteOffFee_CN.frx";
                    }

                    Dictionary<string, object> reportSource = new Dictionary<string, object>();
                    reportSource.Add("WriteOffBillBaseReportData", reportData.BaseReportData);
                    reportSource.Add("WriteOffBillDetailReportData", reportData.DetailList.OrderBy(i => i.BillRefNo).ToList()); 
                    reportSource.Add("TotalWriteOffFeeReportData", reportData.TotalWriteOfFeeList);
                    reportSource.Add("WriteOffChargeReportData", reportData.ChargeReportDataList);
                    reportSource.Add("TotalChargeFeeReportData", reportData.TotalChargeFeeList);
                    if (Math.Abs(flag) != currentBillList.Count)
                    {
                        reportSource.Add("DebitBillDetailReportData", reportData.DebitList);
                        reportSource.Add("CreditBillDetailReportData", reportData.CreditList);
                    }

                    viewer.BindData(fileName, reportSource, null);
                }
                catch (Exception ex)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex);
                }

                #endregion
            }
            else
            {
                // 打印支票
                PrintCash(reportConfigure.Parameters[0]);
            }
        }


    }


}
