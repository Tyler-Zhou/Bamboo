using System;
using System.Collections.Generic;
using System.Linq;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.CompositeObjects;
using ICP.Framework.ClientComponents.Service;
using ICP.Framework.CommonLibrary.Client;
using ICP.Framework.CommonLibrary.Common;
using Microsoft.Practices.CompositeUI.Utility;
using ICP.Framework.CommonLibrary;

namespace ICP.FAM.UI.WriteOff
{
    partial class WriteOffEditPart
    {

        #region ValidateData() 验证数据
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
           if(writeOffItemInfo!=null)
           {
               writeOffItemInfo.PayCustomerName = txtPayCustomerName.CustomerName;
           }
           txtRefNo.Focus();
            gvTreeGridMain.EndUpdate();
            bsWriteOff.EndEdit();
            bsBills.EndEdit();
            //验证销账信息
            if (!writeOffItemInfo.Validate())
            {
                return false;
            }
            if (BaseDataList != null)
            {
                foreach (WriteOffBill billItem in BaseDataList)
                {
                    if (!billItem.Validate())
                    {
                        return false;
                    }
                }
            }

            #region  验证币种信息
            UCAccountListInfo.ValidateData();
            if (UCAccountListInfo.DataList == null || UCAccountListInfo.DataList.Count == 0)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "1109290002"));
                return false;
            }
            //验证单、多币种的情况下，纪录的条数
            if (UCAccountListInfo.CurrencyType == WriteOffType.Muitl)
            {
                if (UCAccountListInfo.DataList.Count <= 1)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "1109290004"));
                    return false;
                }
            }
            else
            {
                if (UCAccountListInfo.DataList.Count > 1)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "1109290003"));
                    return false;
                }
            }
            List<Guid> currencyIDList = new List<Guid>();
            List<Guid> bankIDList = new List<Guid>();

            //验证详细信息
            foreach (OperationCurrencyAmountList amountItem in UCAccountListInfo.DataList)
            {
                //为空的验证
                if (!amountItem.Validate())
                {
                    return false;
                }
                //到账时间的验证
                if (amountItem.IsReached && amountItem.BankDate == null)
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "1109210001"));
                    return false;
                }
                //验证币种不能相同
                if (!currencyIDList.Contains(amountItem.CurrencyID))
                {
                    currencyIDList.Add(amountItem.CurrencyID);
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "1110210003"));
                    return false;
                }
                //验证银行不能相同
                if (!bankIDList.Contains(amountItem.BankAccountID))
                {
                    bankIDList.Add(amountItem.BankAccountID);
                }
                else
                {
                    LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), NativeLanguageService.GetText(this, "1110210002"));
                    return false;
                }
            }
            #endregion

            #region 验证其他项目
            UCcharges.ValidateData();
            foreach (WriteOffCharge chargeItem in UCcharges.DataList)
            {
                if (!chargeItem.Validate())
                {
                    return false;
                }
            }
            #endregion

            return true;
        }
        #endregion
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        private bool Save()
        {
            if (!ValidateData())
            {
                return false;
            }

            ValidateBalance();

            try
            {
                SaveRequestCheck saveRequest = GetSaveRequestData();

                Dictionary<Guid, SaveResponse> saveResponse = FinanceService.SaveWriteOffInfo(saveRequest, LocalData.UserInfo.LoginID, LocalData.IsEnglish);

                #region 更新界面数据
                //刷新销账信息
                SingleResult woSingleResult = saveResponse[saveRequest.RequestId].SingleResult;
                writeOffItemInfo.ID = woSingleResult.GetValue<Guid>("ID");
                writeOffItemInfo.No = woSingleResult.GetValue<String>("No");
                writeOffItemInfo.InvoiceNo = woSingleResult.GetValue<String>("InvoiceNo");
                writeOffItemInfo.UpdateDate = woSingleResult.GetValue<DateTime?>("UpdateDate");

                //刷新账单费用信息
                ManyResult billFeeMany = saveResponse[saveRequest.BillFeeListRequestID].ManyResult;
                for (int n = 0; n < saveRequest.CheckItems.Count; n++)
                {
                    CheckItem billFeeRequest = saveRequest.CheckItems[n];
                    List<WriteOffBill> billFees = billFeeRequest.UnBoxInvolvedObject<WriteOffBill>();
                    for (int i = 0; i < billFees.Count; i++)
                    {
                        billFees[i].ID = billFeeMany.Items[n].GetValue<Guid>("ID");
                        billFees[i].UpdateDate = billFeeMany.Items[n].GetValue<DateTime?>("UpdateDate");
                        billFees[i].ChargeUpdateDate = billFeeMany.Items[n].GetValue<DateTime?>("ChargeUpdateDate");
                        billFees[i].BillUpdateDate = billFeeMany.Items[n].GetValue<DateTime?>("BillUpdateDate");
                        billFees[i].IsDirty = false;
                    }
                }

                //刷新币种信息
                ManyResult currencyMany = saveResponse[saveRequest.CurrencyListRequestID].ManyResult;
                UCAccountListInfo.RefreshUI(saveRequest.CheckAmounts, currencyMany);

                //刷新其他项目信息
                ManyResult expenseMany = saveResponse[saveRequest.ExpenseListRequestID].ManyResult;
                UCcharges.RefreshUI(saveRequest.Expenses, expenseMany);

                LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(FindForm(), LocalData.IsEnglish ? "Check Successfully" : "销账成功");

                AfterSave();
                RefreshControl();
                UCcharges.AfterSave();
                UCAccountListInfo.AfterSave();
                #endregion

                #region 销帐成功后刷新帐单列表面板
                if (RefreshBillListPartEvent != null)
                {
                    RefreshBillListPartEvent(this, new DataEventArgs<object>(new object()));
                }
                #endregion


                return true;
            }
            catch (Exception ex)
            {
                LocalCommonServices.ErrorTrace.SetErrorInfo(FindForm(), ex.Message);
                return false;
            }
        }

        private void ValidateBalance()
        {
            if (writeOffType == WriteOffType.Single)
            {
                decimal totalAmount3 = Get2Round( (from d in UCAccountListInfo.DataList select DataTypeHelper.GetDecimal(d.StandardCurrencyAmount, 0)).Sum());
                decimal totalAmount2 = (from d in UCAccountListInfo.DataList select Get2Round(d.StandardCurrencyBillAmount) + Get2Round(d.StandardCurrencyOtherAmount)).Sum();
                if (Math.Abs(totalAmount3 - totalAmount2) > 0.00m)
                {
                    string message = LocalData.IsEnglish ? "Enter the amount [{0}] equal to the total standard currency amount [{1}] is automatically carried forward to the exchange gains and losses?" : "输入的金额 [{0}] 不等于折算后的本位金额 [{1}] ,是否自动结转为汇兑损益?";
                    message = string.Format(message, totalAmount3.ToString("F2"), totalAmount2.ToString("F2"));

                    decimal amount = 0m;
                    int i = (from d in UCcharges.DataList where d.GLID == glId select d).Count();
                    if (i == 0)
                    {
                        amount = totalAmount3 - totalAmount2;
                    }
                    else
                    {
                        amount = totalAmount3 - (Get2Round(UCAccountListInfo.DataList[0].StandardCurrencyBillAmount) + UCcharges.TotalNoGLAmount);
                    }
                    SetLosses(message, amount);
                }
            }
        }
     
        /// <summary>
        /// 刷新保存
        /// </summary>
        private void AfterSave()
        {
            isCharge = false;
            writeOffItemInfo.IsDirty = false;
        }

        /// <summary>
        /// 刷新工具栏
        /// </summary>
        private void RefreshControl()
        {
            if (writeOffItemInfo.ID == Guid.Empty)
            {
                bbiViewCheck.Enabled = false;
                bbiListCredentials.Enabled = false;
                barPrint.Enabled = false;
            }
            else
            {
                bbiListCredentials.Enabled = true;
                barPrint.Enabled = true;
                if (string.IsNullOrEmpty(writeOffItemInfo.InvoiceNo))
                {
                    bbiViewCheck.Enabled = false;
                }
                else
                {
                    bbiViewCheck.Enabled = true;
                }
            }
        }

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <returns></returns>
        private SaveRequestCheck GetSaveRequestData()
        {
            SaveRequestCheck saveRequest = new SaveRequestCheck();

            //获取基本信息
            saveRequest.ID = writeOffItemInfo.ID;
            saveRequest.No = writeOffItemInfo.No;
            saveRequest.Type = writeOffItemInfo.Way.GetHashCode();
            saveRequest.CompanyID = writeOffItemInfo.CompanyID;
            saveRequest.CustomerID = writeOffItemInfo.CustomerID;
            saveRequest.CheckNo = writeOffItemInfo.CheckNo;
            saveRequest.PayCustomerName = writeOffItemInfo.PayCustomerName;
            saveRequest.PayBankAccountNo = writeOffItemInfo.PayBankAccountNo;
            saveRequest.PayBankName = writeOffItemInfo.PayBankName;
            saveRequest.PayBankBranchName = writeOffItemInfo.PayBankBranchName;
            saveRequest.PayBankNumber = writeOffItemInfo.PayBankNumber;
            saveRequest.IsValid = writeOffItemInfo.IsValid.GetHashCode();
            saveRequest.IsPublic = writeOffItemInfo.IsPublic;
            saveRequest.CheckBy = LocalData.UserInfo.LoginID;
            saveRequest.CheckMode = writeOffItemInfo.CheckMode.GetHashCode();
            saveRequest.BankReceiptID = writeOffItemInfo.BankReceiptID;
            saveRequest.BankReceiptNO = writeOffItemInfo.BankReceiptNO;


            if (writeOffType == WriteOffType.Muitl)
            {
                saveRequest.IsMultCurrency = 1;
            }
            else
            {
                saveRequest.IsMultCurrency = 0;
            }
            saveRequest.Remark = writeOffItemInfo.Remark;
            saveRequest.UpdateDate = writeOffItemInfo.UpdateDate == null ? string.Empty : writeOffItemInfo.UpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
            saveRequest.CheckDate = DateTime.SpecifyKind(writeOffItemInfo.CheckDate, DateTimeKind.Unspecified);
            saveRequest.AddInvolvedObject(writeOffItemInfo);

            saveRequest.CheckItems = new List<CheckItem>();
            foreach (WriteOffBill item in BaseDataList)
            {
                CheckItem feeSave = new CheckItem();

                feeSave.ID = item.ID;
                feeSave.ChargeID = item.ChargeID;
                feeSave.CheckID = saveRequest.ID;

                //Amount为本次销帐的金额、WriteOffAmount为折合以后的金额
                feeSave.Amount = item.WriteOffAmount;
                if (writeOffType == WriteOffType.Muitl)
                {
                    feeSave.WriteOffRate = 1;
                    feeSave.WriteOffAmount = item.WriteOffAmount;
                    if (item.WriteOffAmount == 0)
                    {
                        continue;
                    }
                }
                else
                {
                    //根据银行计算出销账汇率、折合金额
                    decimal rate = RateHelper.GetRate(item.CurrencyID, UCAccountListInfo.CurrencyID, DateTime.SpecifyKind(writeOffItemInfo.CheckDate, DateTimeKind.Unspecified), _rateList);
                    feeSave.WriteOffRate = item.ExchangeRate = rate;
                    feeSave.WriteOffAmount = item.FinalAmount = Get2Round(item.WriteOffAmount * item.ExchangeRate);

                     if (item.FinalAmount == 0)
                    {
                        continue;
                    }
                }
                feeSave.StandardCurrencyAmount = item.StandardCurrencyAmount;
                feeSave.ChargeUpdateDate = item.ChargeUpdateDate == null ? string.Empty : item.ChargeUpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                feeSave.BillUpdateDate = item.BillUpdateDate == null ? string.Empty : item.BillUpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                feeSave.UpdateDate = item.UpdateDate == null ? string.Empty : item.UpdateDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff");
                feeSave.AddInvolvedObject(item);

                saveRequest.CheckItems.Add(feeSave);
            }
            saveRequest.BillFeeListRequestID = Guid.NewGuid();

            saveRequest.ExpenseListRequestID = Guid.NewGuid();
            saveRequest.Expenses = UCcharges.GetDataList(writeOffType == WriteOffType.Muitl, UCAccountListInfo.CurrencyID);
            saveRequest.Expenses.ForEach(o => o.CheckID = saveRequest.ID);

            TotalCurrencyAndAmount(true,false);

            saveRequest.CurrencyListRequestID = Guid.NewGuid();
            saveRequest.CheckAmounts = UCAccountListInfo.GetCurrencySaveRequest();
            saveRequest.CheckAmounts.ForEach(o => o.CheckID = saveRequest.ID);

            return saveRequest;
        }
    }
}
