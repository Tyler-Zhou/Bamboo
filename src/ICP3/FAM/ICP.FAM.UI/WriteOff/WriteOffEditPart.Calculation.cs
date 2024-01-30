using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ICP.Common.ServiceInterface.DataObjects;
using ICP.FAM.ServiceInterface.DataObjects;
using ICP.Framework.CommonLibrary.Client;

namespace ICP.FAM.UI.WriteOff
{
    partial class WriteOffEditPart
    {
        /// <summary>
        /// 统计币种与金额
        /// <param name="isChangeCurrenAmount"></param>
        /// <param name="updateTotalAmount"></param>
        /// </summary>
        private void TotalCurrencyAndAmount(bool isChangeCurrenAmount,bool updateTotalAmount)
        {
            if (isChangeCurrenAmount)
            {
                List<CurrentAndAmountTotalInfo> list = new List<CurrentAndAmountTotalInfo>();

                if (UCAccountListInfo.CurrencyType == WriteOffType.Muitl)
                {
                    #region 账单

                    foreach (WriteOffBill item in BaseDataList)
                    {
                        CurrentAndAmountTotalInfo info = (from l in list where l.CurrentID == item.CurrencyID select l).SingleOrDefault();
                        if (info == null)
                        {
                            info = new CurrentAndAmountTotalInfo();
                            info.TotalOtherAmount = 0;
                            info.TotalOtherAmountToStandardCurrency = 0;
                            info.TotalBillAmount = 0;
                            info.TotalBillAmountToStandardCurrency = 0;
                            info.CurrentID = item.CurrencyID;

                            list.Add(info);
                        }
                        info.TotalBillAmount += GetCheckAmount(item.Way,item.WriteOffAmount);
                        info.TotalBillAmountToStandardCurrency += GetCheckAmount(item.Way,item.StandardCurrencyAmount);
                      
                    }
                    #endregion

                    #region 其它项目
                    foreach (WriteOffCharge item in UCcharges.DataList)
                    {
                        CurrentAndAmountTotalInfo info = (from l in list where l.CurrentID == item.CurrencyID select l).SingleOrDefault();
                        if (info == null)
                        {
                            info = new CurrentAndAmountTotalInfo();
                            info.TotalOtherAmount = 0;
                            info.TotalOtherAmountToStandardCurrency = 0;
                            info.TotalBillAmount = 0;
                            info.TotalBillAmountToStandardCurrency = 0;
                            info.CurrentID = item.CurrencyID;

                            list.Add(info);
                        }
                        info.TotalBillAmount += GetCheckAmount(item.Way,item.Amount);
                        info.TotalBillAmountToStandardCurrency += GetCheckAmount(item.Way,item.StandardCurrencyAmount) ;

                    }
                    #endregion
                }
                else
                {
                    CurrentAndAmountTotalInfo info = new CurrentAndAmountTotalInfo();
                    info.TotalBillAmount = 0;
                    info.TotalBillAmountToStandardCurrency = 0;
                    info.TotalOtherAmount = 0;
                    info.TotalOtherAmountToStandardCurrency = 0;

                    decimal rate = 1;
                    #region 账单
                    foreach (WriteOffBill item in BaseDataList)
                    {
                        if (UCAccountListInfo.CurrencyID == Guid.Empty)
                        {
                            //没有选银行，默认为本位币
                            info.CurrentID = StandardCurrencyID;
                            info.TotalBillAmount += GetCheckAmount(item.Way,item.StandardCurrencyAmount);
                            info.TotalBillAmountToStandardCurrency +=GetCheckAmount(item.Way,item.StandardCurrencyAmount);
                        }
                        else
                        {
                            info.CurrentID = UCAccountListInfo.CurrencyID;
                            rate = RateHelper.GetRate(item.CurrencyID, UCAccountListInfo.CurrencyID, DateTime.SpecifyKind(writeOffItemInfo.CheckDate, DateTimeKind.Unspecified), _rateList);
                            info.TotalBillAmount += GetCheckAmount(item.Way,item.WriteOffAmount * rate);
                            info.TotalBillAmountToStandardCurrency +=GetCheckAmount(item.Way,item.StandardCurrencyAmount);
                        }
                    }
                    #endregion

                    #region 其它项目
                    foreach (WriteOffCharge item in UCcharges.DataList)
                    {
                        if (UCAccountListInfo.CurrencyID == Guid.Empty)
                        {
                            //没有选银行，默认为本位币
                            info.CurrentID = StandardCurrencyID;
                            info.TotalOtherAmount += GetCheckAmount(item.Way,item.StandardCurrencyAmount);
                            info.TotalOtherAmountToStandardCurrency += GetCheckAmount(item.Way,item.StandardCurrencyAmount);
                        }
                        else
                        {
                            info.CurrentID = UCAccountListInfo.CurrencyID;
                            rate = RateHelper.GetRate(item.CurrencyID, UCAccountListInfo.CurrencyID, DateTime.SpecifyKind(writeOffItemInfo.CheckDate, DateTimeKind.Unspecified), _rateList);
                            info.TotalOtherAmount += GetCheckAmount(item.Way, item.Amount * rate);
                            info.TotalOtherAmountToStandardCurrency +=GetCheckAmount(item.Way,item.StandardCurrencyAmount);
                        }
                    }
                    #endregion

                    list.Add(info);
                }
                UCAccountListInfo.SetCurrenyList(list, updateTotalAmount);

            }

            #region 账单合计信息
            txtTotalAmount.Text = string.Empty;
             Dictionary<Guid, decimal> dicBillList = (from d in DataList group d by d.CurrencyID into g select new { g.Key, TotalAcmount = g.Sum(p => p.Way == _feeWay ? p.WriteOffAmount : -p.WriteOffAmount) }).ToDictionary(c => c.Key, c => c.TotalAcmount);
             foreach (KeyValuePair<Guid, Decimal> item in dicBillList)
             {
                 string currencyName = string.Empty;
                 if (currencyList.Keys.Contains(item.Key))
                 {
                     currencyName = currencyList[item.Key];
                 }
                 else
                 {
                     currencyName = LocalData.IsEnglish ? "Unknown" : "未知";
                 }

                 txtTotalAmount.Text += currencyName + ": " + item.Value.ToString("n") + "  ";
             }
             TotalBillAmountByCurrency();
            #endregion

        }


        /// <summary>
        /// 统计账单 折成某一种币种时的总金额
        /// 20140520 周任平 取消此功能，全部改为统计折合为本位币
        /// </summary>
        private void TotalBillAmountByCurrency()
        {
            decimal totalAmountToStandardCrrency = decimal.Round((from d in DataList select d.Way == _feeWay ? d.StandardCurrencyAmount : -d.StandardCurrencyAmount).Sum(), 2, MidpointRounding.AwayFromZero);
            txtTotalByCurrency.Text = totalAmountToStandardCrrency.ToString("n");

            #region 已无效
            //txtTotalByCurrency.Text = string.Empty;
            //if (this.cmbCurrency.EditValue == null || (Guid)(this.cmbCurrency.EditValue) == Guid.Empty)
            //{
            //    return;
            //}

            //Guid currencyID = (Guid)this.cmbCurrency.EditValue;
            //decimal amount = 0;

            //if (Utility.GuidIsNullOrEmpty(currencyID) || _rateList == null || _rateList.Count == 0)
            //{
            //    string message = LocalData.IsEnglish ? "Have no rate at current company." : "找不到当前公司下的汇率.";
            //    LocalCommonServices.ErrorTrace.SetSuccessfullyInfo(this.FindForm(), message);
            //}

            //if (currencyID == this.UCAccountListInfo.CurrencyID)
            //{
            //    amount = UCAccountListInfo.CurrentRow.TotalBillAmount;
            //}
            //else
            //{
            //    //循环账单的信息
            //    foreach (KeyValuePair<Guid, Decimal> item in dicBillList)
            //    {
            //        if (Utility.GuidIsNullOrEmpty(item.Key))
            //        {
            //            continue;
            //        }
            //        if (currencyID == item.Key)
            //        {
            //            amount += decimal.Round(item.Value, 2);
            //        }
            //        else
            //        {
            //            decimal rate = decimal.Round(RateHelper.GetRate(item.Key, currencyID, DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified), _rateList), 7);
            //            if (rate > 0)
            //            {
            //                amount += decimal.Round(item.Value * rate, 2);
            //            }
            //        }
            //    }
            //}

            //this.txtTotalByCurrency.Text = amount.ToString("n")

            #endregion;
        }

        /// <summary>
        /// 结转成汇兑损益
        /// </summary>
        /// <returns></returns>
        private void ChangeToCharge()
        {
            string message = LocalData.IsEnglish ? "Enter the amount [{0}] equal to the total standard currency amount [{1}] is automatically carried forward to the exchange gains and losses?" : "输入的本位币金额 [{0}] 不等于合计的金额 [{1}] ,是否自动结转为汇兑损益?";
            message = string.Format(message, UCAccountListInfo.DataList[0].StandardCurrencyAmount.ToString("F2"), (UCAccountListInfo.DataList[0].StandardCurrencyBillAmount + UCAccountListInfo.DataList[0].StandardCurrencyOtherAmount).ToString("F2"));

            decimal amount =0m;
            int i = (from d in UCcharges.DataList where d.GLID == glId select d).Count();
             if(i==0)
            {
                 amount = UCAccountListInfo.DataList[0].StandardCurrencyAmount - (UCAccountListInfo.DataList[0].StandardCurrencyBillAmount + UCAccountListInfo.DataList[0].StandardCurrencyOtherAmount);
             }
            else
            {
                amount = UCAccountListInfo.DataList[0].StandardCurrencyAmount - (UCAccountListInfo.DataList[0].StandardCurrencyBillAmount + UCcharges.TotalNoGLAmount);
            }       
            SetLosses(message, amount);
        }

        //public Guid glId = new Guid("4D652752-A5BE-4159-BDDB-DFE263AAFF32");
        public Guid glId = Guid.Empty;
        public void SetLosses(string message,decimal amount)
        {
            if (amount == 0)
            {
                //2014-11-05 周任平 :如果差异金额是0，则不管汇总损益
                return;
            }
            DialogResult result = XtraMessageBox.Show(message
                  , LocalData.IsEnglish ? "Tip" : "提示"
                  , MessageBoxButtons.YesNo
                  , MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            //默认取帐单所属公司的配置信息(不能直接取销帐单的公司，需要取帐单的所属公司)
            Guid billCompanyID = Guid.Empty;
            if (FAMUtility.GuidIsNullOrEmpty(CompanyID))
            {
                if (DataList != null && DataList.Count > 0)
                {
                    List<Guid> billIds = new List<Guid>();
                    billIds.Add(DataList[0].BillID);
                    List<CurrencyBillList> billList = FinanceService.GetBillListByIds(billIds.ToArray(), LocalData.IsEnglish);
                    if (billList != null && billList.Count > 0)
                    {
                        billCompanyID = billList[0].CompanyID;
                    }
                }
                else
                {
                    billCompanyID = LocalData.UserInfo.DefaultCompanyID;
                }
            }
            else
            {
                billCompanyID = CompanyID;
            }

            ConfigureInfo companyConfig = ConfigureService.GetCompanyConfigureInfo(billCompanyID);

            string glCodeName = string.Empty;

            //glCodeName = "(660302) 财务费用->汇兑损益";
            List<SolutionGLConfigList> sList = ConfigureService.GetSolutionGLConfigList(companyConfig.SolutionID,true);

            if (sList.Count(r => r.GLConfigTypeID == 5) > 0)
            {
                glId = sList.Find(r => r.GLConfigTypeID == 5).CRGLCodeID;
                glCodeName = sList.Find(r => r.GLConfigTypeID == 5).CRGLCodeName;
            }

            WriteOffCharge chargeTaget = (from d in UCcharges.DataList where d.GLID == glId select d).Take(1).SingleOrDefault();
            if (chargeTaget == null)
            {
                //不存在，则新增
                chargeTaget = new WriteOffCharge();
                chargeTaget.Way = amount > 0 ? _feeWay : (_feeWay == FeeWay.AR ? FeeWay.AP : FeeWay.AR);
                chargeTaget.CurrencyID = companyConfig.StandardCurrencyID;
                chargeTaget.GLID = glId;
                chargeTaget.GLFullName = glCodeName;
                chargeTaget.CustomerID = companyConfig.CustomerID;
                chargeTaget.CustomerName = companyConfig.CustomerName;

                chargeTaget.Amount = Math.Abs(amount);
                chargeTaget.StandardCurrencyRate = 1;
                chargeTaget.StandardCurrencyAmount = Math.Abs(amount);

                UCcharges.DataList.Add(chargeTaget);
                UCcharges.RefreshUI();
                UCcharges.TotalInfo(false);

                UCAccountListInfo.DataList[0].TotalOtherAmount = UCcharges.TotalAmount(_feeWay);
                UCAccountListInfo.RefreshUI();
            }
            else
            {
                //差异金额=总金额-(帐单金额+其他项目中会计科目不等于汇兑损益的费用)
                chargeTaget.Way = amount > 0 ? _feeWay : (_feeWay == FeeWay.AR ? FeeWay.AP : FeeWay.AR);
                chargeTaget.CurrencyID = companyConfig.StandardCurrencyID;

                chargeTaget.Amount = Math.Abs(amount);
                chargeTaget.StandardCurrencyRate = 1;
                chargeTaget.StandardCurrencyAmount = Math.Abs(amount);

                UCcharges.RefreshUI();
                UCcharges.TotalInfo(false);

                UCAccountListInfo.DataList[0].TotalOtherAmount = UCcharges.TotalAmount(_feeWay);
                UCAccountListInfo.RefreshUI();
            }
        }
        /// <summary>
        /// 得到两位小数的数字
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        decimal Get2Round(decimal number)
        {
            return decimal.Round(number, 2, MidpointRounding.AwayFromZero);
        }
        decimal GetCheckAmount(FeeWay way,decimal amount)
        {
            if (way == _feeWay)
            {
                return Get2Round(amount);
            }
            else
            {
                return -Get2Round(amount);
            }
        }
        /// <summary>
        /// 金额发生改变时
        /// <param name="amount">输入的核销金额</param>
        /// </summary>
        private void AmountChange(decimal amount)
        {
            if (writeOffBill == null)
            {
                return;
            }
            bool isBill=false;
            List<WriteOffBill> itemList = new List<WriteOffBill>();
            if (cmbType.SelectedIndex <= 0)
            {
                #region 账单模式
                isBill = true;
                //先找出当前数据的所有费用信息
                itemList = (from d in BaseDataList
                                               where
                                                   d.BillID == writeOffBill.BillID &&
                                                   d.CurrencyID == writeOffBill.CurrencyID &&
                                                   d.Way == writeOffBill.Way &&
                                                   d.IsCommission == writeOffBill.IsCommission &&
                                                   d.AvailbeWriteOffAmount != 0m
                                               select d).ToList();
             

                #endregion
            }
            else
            {
                #region 费用模式
                isBill = false;
                //先找出当前数据的所有费用信息
                itemList = (from d in BaseDataList
                                               where
                                                   d.BillID == writeOffBill.BillID &&
                                                   d.CurrencyID == writeOffBill.CurrencyID &&
                                                   d.Way == writeOffBill.Way &&
                                                   d.ChargingCodeID == writeOffBill.ChargingCodeID &&
                                                   d.IsCommission == writeOffBill.IsCommission &&
                                                   d.AvailbeWriteOffAmount != 0m
                                               select d).ToList();
              

                #endregion
            }
            if (itemList == null || itemList.Count == 0)
            {
                return;
            }
            int count = itemList.Count;
            ///总可核销金额
            decimal totalAmount = Get2Round((from d in itemList select d.AvailbeWriteOffAmount).Sum());
            ///差异的金额
            decimal gapAmount = Get2Round(amount-totalAmount);
            ///已加入核销金额的
            decimal checkAmount = 0;

            //先将所有的销帐金额都设置为0
            CleareAmount(itemList);

            if (gapAmount >= 0)
            {
                ///多销或者销完
                CheckGapAmount(itemList, amount, totalAmount);
                return;
            }
            else
            {
                #region 少核销的

                #region 3.1 只有一条的数据的时候
                if (itemList.Count == 1)
                {
                    itemList[0].WriteOffAmount = Get2Round(amount);
                    itemList[0].StandardCurrencyAmount =  Get2Round(itemList[0].WriteOffAmount * itemList[0].StandardCurrencyRate);

                    return;
                }
                #endregion

                if (isBill)
                {
                    //只有账单模式才会有下面的情况
                    #region 3.2 找到费用的可销帐金额等于本次销帐金额
                    WriteOffBill searchItem1 = itemList.Find(delegate(WriteOffBill jItem) { return jItem.AvailbeWriteOffAmount == amount; });
                    if (searchItem1 != null)
                    {
                        searchItem1.WriteOffAmount = Get2Round(amount);
                        searchItem1.StandardCurrencyAmount = Get2Round(searchItem1.WriteOffAmount * searchItem1.StandardCurrencyRate);

                        return;
                    }
                    #endregion

                    #region 3.2、如果没有找到金额一样的，可销帐金额是本次销帐金额的倍数的
                    WriteOffBill searchItem2 = null;
                    if (amount != 0)
                    {
                        searchItem2 = itemList.Find(delegate(WriteOffBill jItem)
                        {
                            return jItem.AvailbeWriteOffAmount % amount == 0 &&
                            jItem.AvailbeWriteOffAmount > amount;
                        });
                    }

                    if (searchItem2 != null)
                    {
                        searchItem2.WriteOffAmount = Get2Round(amount);
                        searchItem2.StandardCurrencyAmount = Get2Round(searchItem2.WriteOffAmount * searchItem2.StandardCurrencyRate); ;

                        return;
                    }

                    #endregion

                    #region 3.3、循环逐减(按帐单下的费用的可核销金额升序排练后再循环逐减)
                    itemList = itemList.OrderBy(i => i.AvailbeWriteOffAmount).ToList(); 
                    int n = 0;
                    foreach (WriteOffBill item in itemList)
                    {
                        n++;
                        item.WriteOffAmount = item.AvailbeWriteOffAmount;
                        item.StandardCurrencyAmount = Get2Round(item.WriteOffAmount * item.StandardCurrencyRate);

                        //如果只有一这条纪录，或者是最后一条纪录，把所有的都加上
                        if (itemList.Count == 1 ||
                            itemList.Count == n ||
                            (checkAmount + item.WriteOffAmount > amount))
                        {
                            item.WriteOffAmount = Get2Round(amount - checkAmount);
                            item.StandardCurrencyAmount = Get2Round(item.WriteOffAmount*item.StandardCurrencyRate); ;
                        }

                        checkAmount = checkAmount + item.WriteOffAmount;
                        if (checkAmount == amount)
                        {
                            break;
                        }
                    }

                    #endregion
                }
                #endregion
            }   
        }
        /// <summary>
        /// 清空金额
        /// </summary>
        /// <param name="list"></param>
        private void CleareAmount(List<WriteOffBill> list)
        {
            foreach (WriteOffBill item in list)
            {
                item.WriteOffAmount = 0;
                item.FinalAmount = 0;
                item.StandardCurrencyAmount = 0;
            }
        }
        /// <summary>
        /// 将所有的金额都核销
        /// </summary>
        /// <param name="list"></param>
        private void CheckAllAmount(List<WriteOffBill> list)
        {
            foreach (WriteOffBill item in list)
            {
                item.WriteOffAmount = item.AvailbeWriteOffAmount;
                item.StandardCurrencyAmount = Get2Round(item.WriteOffAmount*item.StandardCurrencyRate);
            }
        }
        /// <summary>
        /// 多核销或完全核销时的情况
        /// </summary>
        /// <param name="list"></param>
        private void CheckGapAmount(List<WriteOffBill> list,decimal amount,decimal totalAmoun)
        {
            //1、将所有的金额都核销                      
            CheckAllAmount(list);
            if (amount > totalAmoun )
            {
                string strFeeWay=string.Empty;
                if (_feeWay == FeeWay.AP)
                {
                    strFeeWay = LocalData.IsEnglish ? "Prepaid" : "预付";
                }
                else
                {
                    strFeeWay = LocalData.IsEnglish ? "Receipts" : "预收";
                }
                string message = LocalData.IsEnglish ? "Enter the amount [{0}] greater than to the total amount [{1}] is automatically carried forward to the {2}?" : "输入的金额 [{0}] 大于销账的金额 [{1}] ,是否自动转为{2}信息?";
                message = string.Format(message, amount.ToString("F2"), totalAmoun.ToString("F2"), strFeeWay);

                DialogResult result = XtraMessageBox.Show(message
                        , LocalData.IsEnglish ? "Tip" : "提示"
                        , MessageBoxButtons.YesNo
                        , MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    //2、将多出的金额，加到其它项目的"预收预付中"
                    WriteOffBill lastItem = list[list.Count - 1];
                    UCcharges.AddPrepayment(lastItem, amount-totalAmoun);
                }
            }
        }
    }
}
